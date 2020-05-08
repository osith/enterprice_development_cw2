using CW_2.Models;
using CW_2.Views;
using System;
using System.Windows.Forms;
using static CW_2.Models.Enums;
using static CW_2.Models.UserModal;

namespace CW_2
{
    public partial class LoginView : Form
    {
        private readonly UserModal _user;
        public LoginView()
        {
            _user = new UserModal();
            InitializeComponent();
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPassword.Text) || string.IsNullOrEmpty(txtEmail.Text))
            {
                Helper.DisplayMessage("Please fill all fileds", MessageType.Warning);
                return;
            }
            var user = new LoginUserDTO()
            {
                Password = txtPassword.Text,
                Email = txtEmail.Text
            };

            //Need to remove
            //var user = new LoginUserDTO()
            //{
            //    Password = "osith",
            //    Email = "osith94@gmail.com"
            //};

            var userData = await _user.ValidateLogin(user);
            if (userData != null)
            {
                var next = new HomeNavView(userData);
                next.Show();
                this.Hide();
            }
            else
            {
                Helper.DisplayMessage("Invalid user credentials", MessageType.Error);
            }

        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            var next = new RegisterView();
            _user.Dispose();
            next.Show();
            this.Hide();
        }

        private void LoginView_Load(object sender, EventArgs e)
        {

        }
    }
}
