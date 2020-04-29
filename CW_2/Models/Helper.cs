using System.Windows.Forms;
using static CW_2.Models.Enums;

namespace CW_2.Models
{
    public static class Helper
    {
        public static void DisplayMessage(string body, MessageType type)
        {
            if (type == MessageType.Complete)
            {
                MessageBox.Show(body, "Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (type == MessageType.Warning)
            {
                MessageBox.Show(body, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (type == MessageType.Error)
            {
                MessageBox.Show(body, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static bool ConfirmMessage(string body) {
            var result = MessageBox.Show(body, "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.OK)
                return true;
            return false;
        }

    }
}
