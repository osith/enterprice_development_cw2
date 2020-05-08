using CW_2.Models;
using System;
using System.Windows.Forms;
using static CW_2.Models.Enums;
using static CW_2.Models.UserModal;

namespace CW_2.Views
{
    public partial class RegisterView : Form
    {
        private readonly UserModal _user;
        public RegisterView()
        {
            _user = new UserModal();
            InitializeComponent();
        }

        private void RegisterView_Load(object sender, EventArgs e)
        {

        }

        private async void btnRegister_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtEmail.Text)
                || string.IsNullOrEmpty(txtPassword.Text) || string.IsNullOrEmpty(txtCPassword.Text)
                || string.IsNullOrEmpty(txtPhone.Text))
            {
                Helper.DisplayMessage("Please fill all fileds", MessageType.Warning);
                return;
            }

            if (txtPassword.Text != txtCPassword.Text)
            {
                Helper.DisplayMessage("Passwords are not matching", MessageType.Warning);
                return;
            }

            var user = new RegisterUserDTO()
            {
                Id = Guid.NewGuid(),
                Name = txtName.Text,
                Email = txtEmail.Text,
                Password = txtPassword.Text,
                Phone = txtPhone.Text
            };
            await _user.SaveUser(user);
            Helper.DisplayMessage("User register complete", MessageType.Complete);

            new LoginView().Show();
            this.Dispose();
        }

        private void RegisterView_FormClosed(object sender, FormClosedEventArgs e)
        {
            var next = new LoginView();
            _user.Dispose();
            next.Show();
            this.Dispose();
        }
    }
}
