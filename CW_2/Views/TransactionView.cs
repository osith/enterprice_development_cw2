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
        private readonly TransactionModel _transaction;
        private readonly RegisterUserDTO _loggedUser;

        public TransactionView(RegisterUserDTO user)
        {
            lblamounts = new List<Label>();
            txtAmounts = new List<TextBox>();
            lblContacts = new List<Label>();
            cmbContacts = new List<ComboBox>();
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
                var transaction = new TransactionDTO()
                {
                    Id = Guid.NewGuid(),
                    Amount = Double.Parse(txtAmounts[i].Text),
                    ContactDataId = _transaction.FindContact(cmbContacts[i].SelectedIndex),
                    CreatedAt = DateTime.UtcNow,
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

            //lblContact
            lblContact.AutoSize = true;
            lblContact.Location = new System.Drawing.Point(330, 87 + sCount * 25);
            lblContact.Name = "label3";
            lblContact.Size = new System.Drawing.Size(65, 20);
            lblContact.TabIndex = 2;
            lblContact.Text = "Contact";
            lblContacts.Add(lblContact);
            this.Controls.Add(lblContact);

            //txtAmount
            txtAmount.Location = new System.Drawing.Point(109, 84 + sCount * 25);
            txtAmount.Name = "textBox1";
            txtAmount.Size = new System.Drawing.Size(200, 26);
            txtAmount.TabIndex = 3;
            txtAmounts.Add(txtAmount);
            this.Controls.Add(txtAmount);

            //cmbContact
            cmbContact.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmbContact.FormattingEnabled = true;
            cmbContact.Location = new System.Drawing.Point(388, 84 + sCount * 25);
            cmbContact.Name = "comboBox1";
            cmbContact.Size = new System.Drawing.Size(200, 28);
            cmbContact.TabIndex = 4;
            cmbContact.Items.AddRange(_transaction.comboContacts.ToArray());
            cmbContacts.Add(cmbContact);
            this.Controls.Add(cmbContact);
        }
    }
}
