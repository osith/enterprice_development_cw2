using CW_2.Models;
using CW_2.Views;
using System;
using System.Windows.Forms;
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
            var user = new LoginUserDTO()
            {
                Password = "osith",
                Phone = "0717482166"
            };
            var userData = await _user.ValidateLogin(user);
            if (userData != null)
            {
                var next = new HomeNavView(userData);
                next.Show();
                this.Hide();
            }
            else {
                MessageBox.Show("Invalid user credentials");
            }

        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            var next = new RegisterView();
            next.Show();
            this.Hide();
        }
    }
}
