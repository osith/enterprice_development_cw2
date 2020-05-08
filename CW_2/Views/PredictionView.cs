using CW_2.Models;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using static CW_2.Models.PredictionModel;
using static CW_2.Models.UserModal;

namespace CW_2.Views
{
    public partial class PredictionView : Form
    {
        private readonly RegisterUserDTO _loggedUser;
        private readonly PredictionModel _predictionModel;
        public PredictionView(RegisterUserDTO user)
        {
            InitializeComponent();
            _loggedUser = user;
            _predictionModel = new PredictionModel();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            var responce = Task.Run(() => _predictionModel.MakePrediction(new PredictionDTO()
            {
                UserId = _loggedUser.Id,
                PredictDate = dtpPredictDay.Value.Day
            })).Result;

            lblIncometot.Text = "Rs. " + string.Format("{0:#.00}", (responce.PredictIncome > 0 ? responce.PredictIncome : 0));
            lblExptot.Text = "Rs. " + string.Format("{0:#.00}", (responce.PredictExpence > 0 ? responce.PredictExpence : 0));

            panel1.Visible = true;
        }

        private void PredictionView_FormClosed(object sender, FormClosedEventArgs e)
        {
            var next = new HomeNavView(_loggedUser);
            _predictionModel.Dispose();
            next.Show();
            this.Dispose();
        }
    }
}
