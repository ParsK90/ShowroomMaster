using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShowroomMaster
{
    public partial class ConfigForm : Form
    {
        public ConfigForm()
        {
            InitializeComponent();
        }

        private void nameBox_Enter(object sender, EventArgs e)
        {
            //    userImage.Image = SqlInfo.ReplaceColor((Image)userImage.Image.Clone(), Color.FromArgb(148, 0, 241), Color.White);
            //    lockImage.Image = SqlInfo.ReplaceColor((Image)lockImage.Image.Clone(), Color.White, Color.FromArgb(148, 0, 241));

            if (txtConn.Text == "Sunucu adı")
            {
                txtConn.Text = "";
            }
            namePnl.BorderStyle = BorderStyle.FixedSingle;
            namePnl.BackColor = Color.FromArgb(148, 0, 241);
            txtConn.BackColor = Color.FromArgb(148, 0, 241);
            txtConn.ForeColor = Color.White;
            //userImage.BackColor = Color.FromArgb(148, 0, 241);
            //lockImage.BackColor = Color.White;

        }

        private void nameBox_Leave(object sender, EventArgs e)
        {
            //userImage.Image = SqlInfo.ReplaceColor((Image)userImage.Image.Clone(), Color.White, Color.FromArgb(148, 0, 241));

            if (txtConn.Text == "")
            {
                txtConn.Text = "Sunucu adı";
            }
            namePnl.BackColor = Color.White;
            txtConn.BackColor = Color.White;
            txtConn.ForeColor = Color.Black;

        }

        private void logBtn_MouseEnter(object sender, EventArgs e)
        {
            logBtn.FlatAppearance.BorderColor = Color.White;
            logBtn.FlatAppearance.BorderSize = 1;
            logBtn.BackColor = Color.FromArgb(148, 0, 241);
            logBtn.ForeColor = Color.White;
        }

        private void logBtn_MouseLeave(object sender, EventArgs e)
        {
            logBtn.BackColor = Color.White;
            logBtn.ForeColor = Color.Black;
        }



        private void exitBtn_MouseClick(object sender, MouseEventArgs e)
        {
            Application.Exit();
        }

        private void exitBtn_MouseEnter(object sender, EventArgs e)
        {
            exitBtn.BackColor = Color.Red;
            exitBtn.ForeColor = Color.White;
        }

        private void exitBtn_MouseLeave(object sender, EventArgs e)
        {
            exitBtn.BackColor = Color.Transparent;
            exitBtn.ForeColor = Color.White;
        }
      

        private void LogInForm_Load(object sender, EventArgs e)
        {
            txtConn.Text = Properties.Settings.Default.Database;
        }

        private void LogInForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                logBtn.PerformClick();
        }

        private void logBtn_Click_1(object sender, EventArgs e)
        {
            Properties.Settings.Default.Database = txtConn.Text;
            MessageBox.Show("Sunucu bağlantısı kayıt edildi", "Showroom Master", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

        private void txtConn_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                logBtn.PerformClick();
        }
    }
}

