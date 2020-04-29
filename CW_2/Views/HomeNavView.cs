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
            var next = new EventView(_loggedUser);
            next.Show();
            this.Dispose();
        }

        private void HomeNavView_Load(object sender, EventArgs e)
        {

        }

        private void HomeNavView_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var next = new LoginView();
            next.Show();
            this.Dispose();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var next = new ReportView(_loggedUser);
            next.Show();
            this.Dispose();
        }
    }
}
