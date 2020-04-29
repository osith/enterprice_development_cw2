using CW_2.Models;
using System;
using System.Windows.Forms;
using static CW_2.Models.Enums;
using static CW_2.Models.ReportModel;
using static CW_2.Models.UserModal;

namespace CW_2.Views
{
    public partial class ReportView : Form
    {
        private readonly RegisterUserDTO _loggedUser;
        private readonly ReportModel _reportModel;
        public ReportView(RegisterUserDTO user)
        {
            InitializeComponent();
            _loggedUser = user;
            _reportModel = new ReportModel(user);
        }

        private async void btnGenarate_Click(object sender, EventArgs e)
        {

            if (cmbCategory.SelectedItem == null)
            {
                Helper.DisplayMessage("Please select category", MessageType.Warning);
                return;
            }

            var type = cmbCategory.SelectedItem.ToString();
            var reportModel = new ReportModelDTO()
            {
                UserId = _loggedUser.Id,
                From = dtpFrom.Value,
                To = dtpTo.Value
            };

            if (type == ReportType.TRANSACTIONS)
            {
                reportModel.Category = 1;
                dataView.DataSource = await _reportModel.SearchTransactions(reportModel);
            }
            else
            {
                reportModel.Category = 2;
                dataView.DataSource = await _reportModel.SearchEvents(reportModel);
            }



        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ReportView_Load(object sender, EventArgs e)
        {

        }

        private void dataView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void ReportView_FormClosed(object sender, FormClosedEventArgs e)
        {
            var next = new HomeNavView(_loggedUser);
            next.Show();
            this.Dispose();
        }
    }
}
