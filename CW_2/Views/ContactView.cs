using CW_2.Models;
using System;
using System.Windows.Forms;
using static CW_2.Models.ContactModal;
using static CW_2.Models.Enums;
using static CW_2.Models.UserModal;

namespace CW_2.Views
{
    public partial class ContactView : Form
    {
        private readonly RegisterUserDTO _loggedUser;
        private readonly ContactModal _contact;

        public ContactView(RegisterUserDTO user)
        {
            _loggedUser = user;
            _contact = new ContactModal();
            InitializeComponent();
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            var type = cmbType.SelectedItem.ToString();
            if (type == ContactType.PAYEE)
            {
                var payee = new PayeeDTO()
                {
                    Id = Guid.NewGuid(),
                    Contact = txtContact.Text,
                    Email = txtEmail.Text,
                    Type = ContactType.PAYER,
                    Name = txtName.Text,
                    AccountNo = txtDyn.Text,
                    BankName = txtBankName.Text
                };

                await _contact.SaveUser(payee, _loggedUser);
            }
            else if (type == ContactType.PAYER)
            {
                try
                {
                    if (!string.IsNullOrEmpty(txtDyn.Text))
                        Double.Parse(txtDyn.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Invalid expected amount");
                }

                var payer = new PayerDTO()
                {
                    Id = Guid.NewGuid(),
                    Contact = txtContact.Text,
                    Email = txtEmail.Text,
                    Type = ContactType.PAYEE,
                    Name = txtName.Text,
                    ExpectAmt = Double.Parse(txtDyn.Text)
                };

                await _contact.SaveUser(payer, _loggedUser);
            }


        }

        private void btnClear_Click(object sender, EventArgs e)
        {

        }

        private void cmbType_SelectedValueChanged(object sender, EventArgs e)
        {
            var select = ((ComboBox)sender).SelectedItem.ToString();
            if (select == ContactType.PAYEE)
            {
                lblDyn.Name = "lblAcctNo";
                lblDyn.Text = "Account Number";
                txtDyn.Name = "txtAccNo";
                lblBnkName.Visible = true;
                txtBankName.Visible = true;
            }
            else if (select == ContactType.PAYER)
            {
                lblDyn.Name = "lblExpAmt";
                lblDyn.Text = "Expect Amount";
                txtDyn.Name = "txtExpAmt";
                lblBnkName.Visible = false;
                txtBankName.Visible = false;
            }

            lblDyn.Visible = true;
            txtDyn.Visible = true;
        }

        private void tableContacts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void ContactView_Load(object sender, EventArgs e)
        {
            var contacts = _contact.loadAllContacts();
            tableContacts.DataSource = contacts;
        }
    }
}
