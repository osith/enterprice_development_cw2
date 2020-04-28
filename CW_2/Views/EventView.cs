using System;
using System.Windows.Forms;
using static CW_2.Models.UserModal;

namespace CW_2.Views
{
    public partial class EventView : Form
    {
        private readonly RegisterUserDTO _loggedUser;
        public EventView(RegisterUserDTO user)
        {
            _loggedUser = user;
            InitializeComponent();
        }

        private void EventView_Load(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {

        }

        private void EventView_FormClosed(object sender, FormClosedEventArgs e)
        {
            var next = new HomeNavView(_loggedUser);
            next.Show();
            this.Dispose();
        }
    }
}
