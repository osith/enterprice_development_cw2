using CW_2.Models;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using static CW_2.Models.Enums;
using static CW_2.Models.TransactionModel;
using static CW_2.Models.UserModal;

namespace CW_2.Views
{
    public partial class TransactionView : Form
    {
        List<Label> lblAmounts;
        List<TextBox> txtAmounts;
        List<Label> lblContacts;
        List<ComboBox> cmbContacts;
        List<CheckBox> chkRecurrings;
        List<ComboBox> cmbRecurrings;

        private readonly TransactionModel _transaction;
        private readonly RegisterUserDTO _loggedUser;
        private TransactionViewDTO selectedTransaction;

        public TransactionView(RegisterUserDTO user)
        {
            lblAmounts = new List<Label>();
            txtAmounts = new List<TextBox>();
            lblContacts = new List<Label>();
            cmbContacts = new List<ComboBox>();
            cmbRecurrings = new List<ComboBox>();
            chkRecurrings = new List<CheckBox>();
            InitializeComponent();
            _loggedUser = user;
            _transaction = new TransactionModel(_loggedUser.Id);
            addTransactionField();
        }

        private void TransactionView_Load(object sender, EventArgs e)
        {
            loadTransactions();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            addTransactionField();
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (lblAmounts.Count <= 0)
            {
                MessageBox.Show("No transaction records to save, add using Add button");
                return;
            }

            var transactionList = new List<TransactionDTO>();
            for (int i = 0; i < lblAmounts.Count; i++)
            {
                if (cmbContacts[i].SelectedItem == null || string.IsNullOrEmpty(txtAmounts[i].Text))
                {
                    Helper.DisplayMessage("Please fill all fileds in slot " + (i + 1), MessageType.Warning);
                    return;
                }

                if (chkRecurrings[i].Checked)
                {
                    if (cmbRecurrings[i].SelectedItem == null)
                    {
                        Helper.DisplayMessage("Please select recurring type in slot " + (i + 1), MessageType.Warning);
                        return;
                    }
                }

                var transaction = new TransactionDTO()
                {
                    Id = Guid.NewGuid(),
                    Amount = Double.Parse(txtAmounts[i].Text),
                    ContactDataId = _transaction.FindContact(cmbContacts[i].SelectedIndex),
                    CreatedAt = DateTime.UtcNow,
                    Recurring = chkRecurrings[i].Checked ? true : false,
                    RecurringType = chkRecurrings[i].Checked == true ? _transaction.getRecurringTypeEnum(cmbRecurrings[i].SelectedItem.ToString()) : null,
                    UserDataId = _loggedUser.Id
                };
                transactionList.Add(transaction);
            }
            if (transactionList.Count > 0)
            {
                await _transaction.SaveTransactions(transactionList);
                loadTransactions();
                await _transaction.WriteTransactionToXML(transactionList);
                Helper.DisplayMessage("Transactions added", MessageType.Complete);
            }
            else
            {
                Helper.DisplayMessage("No transaction records to save, add using Add button ", MessageType.Warning);
                return;
            }
        }

        private void addTransactionField()
        {
            Label lblAmount = new Label();
            TextBox txtAmount = new TextBox();
            Label lblContact = new Label();
            ComboBox cmbContact = new ComboBox();
            Label lblRecurring = new Label();
            CheckBox chkRecurring = new CheckBox();
            ComboBox cmbRecurring = new ComboBox();

            var sCount = lblAmounts.Count;

            if (sCount > 9)
            {
                MessageBox.Show("You can add ten(10) transactions at a time");
                return;
            }

            //lblAmount
            lblAmount.AutoSize = true;
            lblAmount.Location = new System.Drawing.Point(10, 87 + sCount * 30);
            lblAmount.Name = "label" + sCount;
            lblAmount.Size = new System.Drawing.Size(65, 20);
            lblAmount.TabIndex = 1;
            lblAmount.Text = "Amount";
            lblAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lblAmounts.Add(lblAmount);
            this.Controls.Add(lblAmount);
            //txtAmount
            txtAmount.Location = new System.Drawing.Point(70, 84 + sCount * 30);
            txtAmount.Name = "textBox" + sCount;
            txtAmount.Size = new System.Drawing.Size(100, 26);
            txtAmount.TabIndex = 3;
            txtAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            txtAmounts.Add(txtAmount);
            this.Controls.Add(txtAmount);

            //lblContact
            lblContact.AutoSize = true;
            lblContact.Location = new System.Drawing.Point(175, 87 + sCount * 30);
            lblContact.Name = "label" + sCount;
            lblContact.Size = new System.Drawing.Size(65, 20);
            lblContact.TabIndex = 2;
            lblContact.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lblContact.Text = "Contact";
            lblContacts.Add(lblContact);
            this.Controls.Add(lblContact);
            //cmbContact
            cmbContact.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmbContact.FormattingEnabled = true;
            cmbContact.Location = new System.Drawing.Point(235, 84 + sCount * 30);
            cmbContact.Name = "comboBox" + sCount;
            cmbContact.Items.AddRange(_transaction.comboContacts.ToArray());
            cmbContact.Size = new System.Drawing.Size(100, 28);
            cmbContact.TabIndex = 4;
            cmbContact.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            cmbContacts.Add(cmbContact);
            this.Controls.Add(cmbContact);

            //chkRecurring
            chkRecurring.AutoSize = true;
            chkRecurring.CheckAlign = System.Drawing.ContentAlignment.TopRight;
            chkRecurring.Location = new System.Drawing.Point(335, 85 + sCount * 30);
            chkRecurring.Name = "checkBox" + sCount;
            chkRecurring.Size = new System.Drawing.Size(113, 24);
            chkRecurring.TabIndex = 9;
            chkRecurring.Text = "Is Recurring";
            chkRecurring.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chkRecurring.UseVisualStyleBackColor = true;
            chkRecurrings.Add(chkRecurring);
            chkRecurring.CheckedChanged += ChkRecurring_CheckedChanged;
            this.Controls.Add(chkRecurring);
            //cmbRecurring
            cmbRecurring.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmbRecurring.FormattingEnabled = true;
            cmbRecurring.Location = new System.Drawing.Point(445, 84 + sCount * 30);
            cmbRecurring.Name = "comboBox" + sCount;
            cmbRecurring.Size = new System.Drawing.Size(70, 28);
            cmbRecurring.TabIndex = 4;
            cmbRecurring.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            cmbRecurring.Items.AddRange(_transaction.getRecurringTypes().ToArray());
            cmbRecurring.Visible = false;
            cmbRecurrings.Add(cmbRecurring);
            this.Controls.Add(cmbRecurring);
        }

        private void ChkRecurring_CheckedChanged(object sender, EventArgs e)
        {
            var chkName = ((CheckBox)sender);
            var chkNo = int.Parse(chkName.Name.Replace("checkBox", ""));
            if (chkName.Checked)
            {
                chkRecurrings[chkNo].Checked = true;
                cmbRecurrings[chkNo].Visible = true;
            }
            else
            {
                chkRecurrings[chkNo].Checked = false;
                cmbRecurrings[chkNo].Visible = false;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            clearContent();
        }

        private void clearContent()
        {
            for (int i = 0; i < lblAmounts.Count; i++)
            {
                this.Controls.Remove(lblAmounts[i]);
                this.Controls.Remove(txtAmounts[i]);
                this.Controls.Remove(lblContacts[i]);
                this.Controls.Remove(cmbContacts[i]);
                this.Controls.Remove(chkRecurrings[i]);
                this.Controls.Remove(cmbRecurrings[i]);
            }
            lblAmounts.Clear();
            txtAmounts.Clear();
            lblContacts.Clear();
            cmbContacts.Clear();
            chkRecurrings.Clear();
            cmbRecurrings.Clear();

            //Add default
            addTransactionField();
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            var selected = (TransactionViewDTO)tableTransaction.SelectedRows[0].DataBoundItem;
            if (Helper.ConfirmMessage("Do you need to delete selected transaction in table?"))
            {
                if (selected.Id != Guid.Empty)
                {
                    if (await _transaction.DeleteTransaction(selected.Id))
                        Helper.DisplayMessage("Transaction deleted", MessageType.Complete);
                    else
                        Helper.DisplayMessage("Transaction not deleted", MessageType.Error);
                }
                else
                    Helper.DisplayMessage("Please select transaction to delete", MessageType.Warning);
                loadTransactions();
            }
        }

        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            if (selectedTransaction != null)
            {
                if (cmbContacts[0].SelectedItem == null || string.IsNullOrEmpty(txtAmounts[0].Text))
                {
                    Helper.DisplayMessage("Please fill all fileds", MessageType.Warning);
                    return;
                }

                if (chkRecurrings[0].Checked)
                {
                    if (cmbRecurrings[0].SelectedItem == null)
                    {
                        Helper.DisplayMessage("Please select recurring type", MessageType.Warning);
                        return;
                    }
                }

                var transaction = new TransactionDTO()
                {
                    Id = selectedTransaction.Id,
                    Amount = Double.Parse(txtAmounts[0].Text),
                    ContactDataId = _transaction.FindContact(cmbContacts[0].SelectedIndex),
                    CreatedAt = DateTime.UtcNow,
                    Recurring = chkRecurrings[0].Checked ? true : false,
                    RecurringType = chkRecurrings[0].Checked == true ? _transaction.getRecurringTypeEnum(cmbRecurrings[0].SelectedItem.ToString()) : null,
                    UserDataId = _loggedUser.Id
                };
                await _transaction.UpdateTransaction(transaction);
                loadTransactions();
                Helper.DisplayMessage("Transactions updated", MessageType.Complete);
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            addTransactionField();
        }

        private void loadTransactions()
        {
            var transactions = _transaction.LoadAllTransactions(_loggedUser.Id);
            if (transactions.Count > 0)
            {
                tableTransaction.DataSource = transactions;
                tableTransaction.Columns["Id"].Visible = false;
                tableTransaction.Columns["Recurring"].Visible = false;
            }
        }

        private void tableTransaction_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            selectedTransaction = (TransactionViewDTO)tableTransaction.SelectedRows[0].DataBoundItem;
            if (selectedTransaction != null)
            {
                var transactionDetails = _transaction.LoadTransactionInfo(selectedTransaction.Id);
                if (transactionDetails != null)
                {
                    clearContent();
                    txtAmounts[0].Text = transactionDetails.Amount.ToString();
                    cmbContacts[0].SelectedItem = transactionDetails.ContactName;
                    chkRecurrings[0].Checked = transactionDetails.Recurring == true ? true : false;
                    cmbRecurrings[0].SelectedItem = transactionDetails.RecurringType;

                    if (chkRecurrings[0].Checked)
                        cmbRecurrings[0].Visible = true;
                    else
                        cmbRecurrings[0].Visible = false;
                }
            }
        }

        private void TransactionView_FormClosed(object sender, FormClosedEventArgs e)
        {
            var next = new HomeNavView(_loggedUser);
            next.Show();
            this.Dispose();
        }
    }
}
