using CW_2.Models;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using static CW_2.Models.TransactionModel;
using static CW_2.Models.UserModal;

namespace CW_2.Views
{
    public partial class TransactionView : Form
    {
        List<Label> lblamounts;
        List<TextBox> txtAmounts;
        List<Label> lblContacts;
        List<ComboBox> cmbContacts;
        List<CheckBox> chkRecurrings;
        List<ComboBox> cmbRecurrings;

        private readonly TransactionModel _transaction;
        private readonly RegisterUserDTO _loggedUser;

        public TransactionView(RegisterUserDTO user)
        {
            lblamounts = new List<Label>();
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

        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void TransactionView_Load(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            addTransactionField();
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (lblamounts.Count <= 0)
            {
                MessageBox.Show("No transaction records to save, add using Add button");
                return;
            }

            var transactionList = new List<TransactionDTO>();
            for (int i = 0; i < lblamounts.Count; i++)
            {
                var data = cmbRecurrings[i].SelectedItem.ToString();
                var transaction = new TransactionDTO()
                {
                    Id = Guid.NewGuid(),
                    Amount = Double.Parse(txtAmounts[i].Text),
                    ContactDataId = _transaction.FindContact(cmbContacts[i].SelectedIndex),
                    CreatedAt = DateTime.UtcNow,
                    Recurring = chkRecurrings[i].Checked == true ? true : false,
                    RecurringType = chkRecurrings[i].Checked == true ? _transaction.getRecurringTypeEnum(cmbRecurrings[i].SelectedItem.ToString()) : null,
                    UserDataId = _loggedUser.Id
                };
                transactionList.Add(transaction);
            }

            await _transaction.SaveTransactions(transactionList);
        }

        private async void addTransactionField()
        {
            Label lblAmount = new Label();
            TextBox txtAmount = new TextBox();
            Label lblContact = new Label();
            ComboBox cmbContact = new ComboBox();
            Label lblRecurring = new Label();
            CheckBox chkRecurring = new CheckBox();
            ComboBox cmbRecurring = new ComboBox();

            var sCount = lblamounts.Count;

            if (sCount > 10)
            {
                MessageBox.Show("You can add ten(10) transactions at a time");
                return;
            }

            //lblAmount
            lblAmount.AutoSize = true;
            lblAmount.Location = new System.Drawing.Point(38, 87 + sCount * 25);
            lblAmount.Name = "label2";
            lblAmount.Size = new System.Drawing.Size(65, 20);
            lblAmount.TabIndex = 1;
            lblAmount.Text = "Amount";
            lblamounts.Add(lblAmount);
            this.Controls.Add(lblAmount);
            //txtAmount
            txtAmount.Location = new System.Drawing.Point(89, 84 + sCount * 25);
            txtAmount.Name = "textBox1";
            txtAmount.Size = new System.Drawing.Size(100, 26);
            txtAmount.TabIndex = 3;
            txtAmounts.Add(txtAmount);
            this.Controls.Add(txtAmount);

            //lblContact
            lblContact.AutoSize = true;
            lblContact.Location = new System.Drawing.Point(195, 87 + sCount * 25);
            lblContact.Name = "label3";
            lblContact.Size = new System.Drawing.Size(65, 20);
            lblContact.TabIndex = 2;
            lblContact.Text = "Contact";
            lblContacts.Add(lblContact);
            this.Controls.Add(lblContact);
            //cmbContact
            cmbContact.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmbContact.FormattingEnabled = true;
            cmbContact.Location = new System.Drawing.Point(251, 84 + sCount * 25);
            cmbContact.Name = "comboBox1";
            cmbContact.Items.AddRange(_transaction.comboContacts.ToArray());
            cmbContact.Size = new System.Drawing.Size(100, 28);
            cmbContact.TabIndex = 4;
            cmbContacts.Add(cmbContact);
            this.Controls.Add(cmbContact);

            //chkRecurring
            chkRecurring.AutoSize = true;
            chkRecurring.CheckAlign = System.Drawing.ContentAlignment.TopRight;
            chkRecurring.Location = new System.Drawing.Point(365, 85 + sCount * 25);
            chkRecurring.Name = "checkBox1";
            chkRecurring.Size = new System.Drawing.Size(113, 24);
            chkRecurring.TabIndex = 9;
            chkRecurring.Text = "Is Recurring";
            chkRecurring.UseVisualStyleBackColor = true;
            chkRecurrings.Add(chkRecurring);
            this.Controls.Add(chkRecurring);
            //cmbRecurring
            cmbRecurring.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmbRecurring.FormattingEnabled = true;
            cmbRecurring.Location = new System.Drawing.Point(465, 84 + sCount * 25);
            cmbRecurring.Name = "comboBox1";
            cmbRecurring.Size = new System.Drawing.Size(100, 28);
            cmbRecurring.TabIndex = 4;
            cmbRecurring.Items.AddRange(_transaction.getRecurringTypes().ToArray());
            cmbRecurrings.Add(cmbRecurring);
            this.Controls.Add(cmbRecurring);
        }
    }
}
