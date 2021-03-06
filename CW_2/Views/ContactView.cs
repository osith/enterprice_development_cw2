﻿using CW_2.Models;
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
        private Guid updateUserId;
        public ContactView(RegisterUserDTO user)
        {
            _loggedUser = user;
            _contact = new ContactModal();
            InitializeComponent();
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {

            if (cmbType.SelectedItem == null || string.IsNullOrEmpty(txtName.Text)
                || string.IsNullOrEmpty(txtContact.Text) || string.IsNullOrEmpty(txtEmail.Text)
                || string.IsNullOrEmpty(txtDyn.Text))
            {
                Helper.DisplayMessage("Please fill all fileds", MessageType.Warning);
                return;
            }

            var type = cmbType.SelectedItem.ToString();
            if (type == ContactType.PAYEE)
            {
                var payee = new PayeeDTO()
                {
                    Id = Guid.NewGuid(),
                    Contact = txtContact.Text,
                    Email = txtEmail.Text,
                    Type = ContactType.PAYEE,
                    Name = txtName.Text,
                    AccountNo = txtDyn.Text,
                    BankName = txtBankName.Text
                };

                await _contact.SaveUser(payee, _loggedUser);
                Helper.DisplayMessage("Contact added", MessageType.Complete);
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
                    Helper.DisplayMessage("Invalid expected amount", MessageType.Error);
                    return;
                }

                var payer = new PayerDTO()
                {
                    Id = Guid.NewGuid(),
                    Contact = txtContact.Text,
                    Email = txtEmail.Text,
                    Type = ContactType.PAYER,
                    Name = txtName.Text,
                    ExpectAmt = Double.Parse(txtDyn.Text)
                };

                await _contact.SaveUser(payer, _loggedUser);
                Helper.DisplayMessage("Contact added", MessageType.Complete);
            }

            loadContacts();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtName.Text = string.Empty;
            txtContact.Text = string.Empty;
            txtEmail.Text = string.Empty;
            lblDyn.Visible = false;
            txtDyn.Visible = false;
            txtDyn.Text = string.Empty;
            txtBankName.Visible = false;
            txtBankName.Text = string.Empty;
            lblBnkName.Visible = false;
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
            var selected = (ContactViewDTO)tableContacts.SelectedRows[0].DataBoundItem;
            if (selected != null)
            {
                var contactDetail = _contact.LoadContactInfo(selected.Id);
                if (contactDetail != null)
                {
                    updateUserId = contactDetail.Id;
                    txtName.Text = contactDetail.Name;
                    cmbType.SelectedIndex = contactDetail.Type == ContactType.PAYER ? 0 : 1;
                    txtContact.Text = contactDetail.Contact;
                    txtEmail.Text = contactDetail.Email;
                    if (contactDetail.Type == ContactType.PAYER)
                    {
                        lblDyn.Name = "lblExpAmt";
                        txtDyn.Name = "txtExpAmt";
                        lblBnkName.Visible = false;
                        txtBankName.Visible = false;
                        lblDyn.Text = "Expect Amount";
                        txtDyn.Text = contactDetail.ExpectAmt.ToString();
                    }
                    else
                    {
                        lblDyn.Name = "lblAcctNo";
                        txtDyn.Name = "txtAccNo";
                        lblBnkName.Visible = true;
                        txtBankName.Visible = true;
                        lblDyn.Text = "Account Number";
                        txtDyn.Text = contactDetail.AccountNo;
                        txtBankName.Text = contactDetail.BankName;
                    }
                }
            }
        }

        private void ContactView_Load(object sender, EventArgs e)
        {
            loadContacts();
        }

        private void loadContacts()
        {
            var contacts = _contact.loadAllContacts();
            if (contacts.Count > 0)
            {
                tableContacts.DataSource = contacts;
                tableContacts.Columns["Id"].Visible = false;
                tableContacts.Columns["AccountNo"].Visible = false;
                tableContacts.Columns["ExpectAmt"].Visible = false;
                tableContacts.Columns["BankName"].Visible = false;
                tableContacts.Columns["UserDataId"].Visible = false;
            }
        }

        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            if (updateUserId != Guid.Empty)
            {
                var type = cmbType.SelectedItem.ToString();
                if (type == ContactType.PAYEE)
                {
                    var payee = new PayeeDTO()
                    {
                        Id = updateUserId,
                        Contact = txtContact.Text,
                        Email = txtEmail.Text,
                        Type = ContactType.PAYEE,
                        Name = txtName.Text,
                        AccountNo = txtDyn.Text,
                        BankName = txtBankName.Text
                    };

                    await _contact.UpdateContact(payee, _loggedUser);
                    Helper.DisplayMessage("Contact updated", MessageType.Complete);
                    updateUserId = Guid.Empty;
                    loadContacts();
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
                        Helper.DisplayMessage("Invalid expected amount", MessageType.Warning);
                    }

                    var payer = new PayerDTO()
                    {
                        Id = updateUserId,
                        Contact = txtContact.Text,
                        Email = txtEmail.Text,
                        Type = ContactType.PAYER,
                        Name = txtName.Text,
                        ExpectAmt = Double.Parse(txtDyn.Text)
                    };

                    await _contact.UpdateContact(payer, _loggedUser);
                    Helper.DisplayMessage("Contact updated", MessageType.Complete);
                    updateUserId = Guid.Empty;
                    loadContacts();
                }
            }
            else
            {
                Helper.DisplayMessage("Please select contact to update", MessageType.Warning);
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var selected = (ContactViewDTO)tableContacts.SelectedRows[0].DataBoundItem;
            if (Helper.ConfirmMessage("Do you need to delete selected contact in table?"))
            {
                if (selected.Id != Guid.Empty)
                {
                    if (await _contact.DeleteContact(selected.Id))
                        Helper.DisplayMessage("Contact deleted", MessageType.Complete);
                    else
                        Helper.DisplayMessage("Contact not deleted", MessageType.Error);
                }
                else
                    Helper.DisplayMessage("Please select contact to delete", MessageType.Warning);
                loadContacts();
            }
        }

        private void ContactView_FormClosed(object sender, FormClosedEventArgs e)
        {
            var next = new HomeNavView(_loggedUser);
            _contact.Dispose();
            next.Show();
            this.Dispose();
        }
    }
}
