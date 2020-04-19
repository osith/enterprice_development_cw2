using CW_2.Models;
using System;
using System.Windows.Forms;
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
            var user = new RegisterUserDTO()
            {
                Id = Guid.NewGuid(),
                Name = "Osith",
                Email = "osith94@gmail.com",
                Password = "osith",
                Phone = "0717482166"
            };
            await _user.SaveUser(user);

            new LoginView().Show();
            this.Hide();
        }
    }
}
