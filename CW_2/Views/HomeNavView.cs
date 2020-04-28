using System;
using System.Windows.Forms;
using static CW_2.Models.UserModal;

namespace CW_2.Views
{
    public partial class HomeNavView : Form
    {
        private readonly RegisterUserDTO _loggedUser;
        public HomeNavView(RegisterUserDTO user)
        {
            _loggedUser = user;
            InitializeComponent();
        }

        private void btnContact_Click(object sender, EventArgs e)
        {
            var next = new ContactView(_loggedUser);
            next.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var next = new TransactionView(_loggedUser);
            next.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var next = new EventView();
            next.Show();
            this.Hide();
        }
    }
}
