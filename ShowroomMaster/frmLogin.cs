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
    public partial class LogInForm : Form
    {
        public LogInForm()
        {
            InitializeComponent();
            LblVanish();
        }

        private void nameBox_Enter(object sender, EventArgs e)
        {
            //    userImage.Image = SqlInfo.ReplaceColor((Image)userImage.Image.Clone(), Color.FromArgb(148, 0, 241), Color.White);
            //    lockImage.Image = SqlInfo.ReplaceColor((Image)lockImage.Image.Clone(), Color.White, Color.FromArgb(148, 0, 241));

            if (nameBox.Text == "Kullanıcı adı")
            {
                nameBox.Text = "";
            }
            namePnl.BorderStyle = BorderStyle.FixedSingle;
            namePnl.BackColor = Color.FromArgb(148, 0, 241);
            nameBox.BackColor = Color.FromArgb(148, 0, 241);
            nameBox.ForeColor = Color.White;
            //userImage.BackColor = Color.FromArgb(148, 0, 241);
            //lockImage.BackColor = Color.White;
            LblVanish();

        }

        private void nameBox_Leave(object sender, EventArgs e)
        {
            //userImage.Image = SqlInfo.ReplaceColor((Image)userImage.Image.Clone(), Color.White, Color.FromArgb(148, 0, 241));

            if (nameBox.Text == "")
            {
                nameBox.Text = "Kullanıcı adı";
            }
            namePnl.BackColor = Color.White;
            nameBox.BackColor = Color.White;
            nameBox.ForeColor = Color.Black;
            userImage.BackColor = Color.White;

        }
        private void pinBox_Enter(object sender, EventArgs e)
        {
            //lockImage.Image = SqlInfo.ReplaceColor((Image)lockImage.Image.Clone(), Color.Silver, Color.White);
            //userImage.Image = SqlInfo.ReplaceColor((Image)userImage.Image.Clone(), Color.White, Color.FromArgb(148, 0, 241));

            if (pinBox.Text == "Parola")
            {
                pinBox.Text = "";
                pinBox.PasswordChar = '*';
            }
            pinPnl.BorderStyle = BorderStyle.FixedSingle;
            pinPnl.BackColor = Color.FromArgb(148, 0, 241);
            pinBox.BackColor = Color.FromArgb(148, 0, 241);
            pinBox.ForeColor = Color.White;
            //lockImage.BackColor = Color.FromArgb(148, 0, 241);
            //userImage.BackColor = Color.White;
            LblVanish();
        }
        private void pinBox_Leave(object sender, EventArgs e)
        {
            lockImage.Image = SqlInfo.ReplaceColor((Image)lockImage.Image.Clone(), Color.White, Color.FromArgb(148, 0, 241));
            if (pinBox.Text == "")
            {
                pinBox.Text = "Parola";
                pinBox.PasswordChar = '\0';
            }
            pinPnl.BackColor = Color.White;
            pinBox.BackColor = Color.White;
            pinBox.ForeColor = Color.Black;
            //lockImage.BackColor = Color.White;
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

        private void logBtn_Click(object sender, EventArgs e)
        {
            SqlInfo.con.Open();
            string empID, empDesgination, empPin;
            empID = nameBox.Text;
            empPin = pinBox.Text;

            string logQuery = "select * from PERSONEL where PERSONEL_ID = @id and PERSONEL_PASSWORD = @pin and PERSONEL_STATUS = @work";

            SqlCommand logCmd = new SqlCommand(logQuery, SqlInfo.con);
            logCmd.Parameters.AddWithValue("@id", empID);
            logCmd.Parameters.AddWithValue("@pin", empPin);
            logCmd.Parameters.AddWithValue("@work", "Calisiyor");

            SqlDataAdapter LogAdpater = new SqlDataAdapter(logCmd);
            DataSet LogSet = new DataSet();
            LogAdpater.Fill(LogSet);

            if ((LogSet.Tables[0].Rows.Count) > 0)
            {
                empDesgination = Convert.ToString(LogSet.Tables[0].Rows[0].ItemArray[6]);
                if (empDesgination.ToLower() == "satıcı")
                {
                    new SMMenu(empID).Show();
                    this.Hide();
                }
                else if (empDesgination.ToLower() == "yonetıcı")
                {
                    new Manager_Menu(empID).Show();
                    this.Hide();
                }
            }
            else
            {
                //userImage.Image = SqlInfo.ReplaceColor((Image)userImage.Image.Clone(), Color.FromArgb(148, 0, 241), Color.White);
                //lockImage.Image = SqlInfo.ReplaceColor((Image)lockImage.Image.Clone(), Color.FromArgb(148, 0, 241), Color.White);
                LblVisible();
                if (pinBox.Text != "Parola")
                    pinBox.Text = "";
            }

            SqlInfo.con.Close();
        }


        private void LblVisible()
        {
            nameErrorIcon.Visible = true;
            pinErrorIcon.Visible = true;
            //userImage.BackColor = Color.Red;
            //lockImage.BackColor = Color.Red;
            frmCustomMsgBox.Show("Hatalı Ad/Şifre. \nTekrar Kontrol Edin", "Tamam");
        }
        private void LblVanish()
        {
            nameErrorIcon.Visible = false;
            pinErrorIcon.Visible = false;
        }

        private void nameBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar) || char.IsLetterOrDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                frmCustomMsgBox.Show("Hatalı Giriş!.\nLütfen Yalnızca Numeric Veya Alfabetik Değer Giriniz.", "Tamam");
                e.Handled = true;
            }
        }

        private void pinBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar) || char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                frmCustomMsgBox.Show("Hatalı Giriş!\nLütfen Yalnızca Numeric Değer Giriniz.", "Tamam");
                e.Handled = true;
            }
        }

        private void remember_CheckedChanged(object sender, EventArgs e)
        {
            if (remember.Checked)
            {
                Properties.Settings.Default.Username = nameBox.Text;
                Properties.Settings.Default.Password = pinBox.Text;
                Properties.Settings.Default.Save();
            }
            else
            {
                Properties.Settings.Default.Username = "";
                Properties.Settings.Default.Password = "";
                Properties.Settings.Default.Save();
            }
        }

        private void LogInForm_Load(object sender, EventArgs e)
        {
            nameBox.Text = Properties.Settings.Default.Username;
            pinBox.Text = Properties.Settings.Default.Password;
            remember.Checked = !string.IsNullOrEmpty(nameBox.Text) && !string.IsNullOrEmpty(pinBox.Text);
        }

        private void LogInForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                logBtn.PerformClick();
    
        }

        private void exitBtn_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.F7)
            {
                ConfigForm frm = new ConfigForm();
                frm.ShowDialog();
            }
        }
    }
}

