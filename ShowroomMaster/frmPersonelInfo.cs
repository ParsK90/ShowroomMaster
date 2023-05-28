using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShowroomMaster
{
    public partial class SaleManCtrl : Form
    {
        string mainEmpID;
        bool isUpdateData, isNewData = false;
        bool nameChange, contactChange, pinChange, addressChange, emailChange = false;
        bool nameFlag, pinFlag, addressFlag, contactFlag, emailFlag;

        SqlInfo.empInfo updateEmp;

        public SaleManCtrl()
        {
            InitializeComponent();
        }
        public SaleManCtrl(string id)
        {
            InitializeComponent();
            pictureVanish();
            startChecker();
            isNewData = true;
            mainEmpID = id;
            hireBtn.Location = new Point(383, 384);
            updateBtn.Enabled = updateBtn.Visible = false;
        }
        public SaleManCtrl(SqlInfo.empInfo emp, string empID)
        {
            InitializeComponent();
            pictureVanish();
            updateBtn.Location = new Point(383, 384);
            hireBtn.Enabled = hireBtn.Visible = false;
            isUpdateData = true;
            mainEmpID = empID;
            updateEmp = emp;
            nameBox.Text = emp.name;
            pinBox.Text = emp.pin;
            addressBox.Text = emp.address;
            contactBox.Text = emp.contact;
            emailBox.Text = emp.email;
        }

        private void startChecker()
        {
            if (nameBox.Text == "") nameFlag = true;
            if (pinBox.Text == "") pinFlag = true;
            if (addressBox.Text == "") addressFlag = true;
            if (contactBox.Text == "") contactFlag = true;
            if (emailBox.Text == "") emailFlag = true;
        }

        private void pictureVanish()
        {
            nameBoxErrorIcon.Visible = false;
            pinBoxErrorIcon.Visible = false;
            addressBoxErrorIcon.Visible = false;
            contactBoxErrorIcon.Visible = false;
            emailBoxErrorIcon.Visible = false;
        }

        private void hireBtn_MouseClick_1(object sender, MouseEventArgs e)
        {
            if (isNewData)
            {
                string name = nameBox.Text;
                string pin = pinBox.Text;
                string contact = contactBox.Text;
                string address = addressBox.Text;
                string email = emailBox.Text;

                if ((nameFlag || contactFlag || pinFlag || addressFlag || emailFlag) == true)
                {
                    if (nameFlag) nameBoxErrorIcon.Visible = true;
                    if (pinFlag) pinBoxErrorIcon.Visible = true;
                    if (addressFlag) addressBoxErrorIcon.Visible = true;
                    if (contactFlag) contactBoxErrorIcon.Visible = true;
                    if (emailFlag) emailBoxErrorIcon.Visible = true;
                    frmCustomMsgBox.Show("Hatalı Giriş!.\nLütfen Doğru Bilgileri Girin ve Gerekli Bilgileri İçeren Alanları Doldurun.", "Tamam");
                }
                else
                {
                    SqlInfo.con.Open();
                    string mEmailCheckQuery = "select * from PERSONEL where PERSONEL_EMAIL = @email";
                    SqlCommand mEmailCheckCMD = new SqlCommand(mEmailCheckQuery, SqlInfo.con);
                    mEmailCheckCMD.Parameters.AddWithValue("@email", email);
                    SqlDataAdapter mEmailCheckAdapter = new SqlDataAdapter(mEmailCheckCMD);
                    DataSet mEmailCheckSet = new DataSet();
                    mEmailCheckAdapter.Fill(mEmailCheckSet);
                    SqlInfo.con.Close();
                    if (mEmailCheckSet.Tables[0].Rows.Count == 0)
                    {
                        SqlInfo.con.Open();
                        string getEmpIdQuery = "Select max(substring(PERSONEL.PERSONEL_ID,3,len(PERSONEL.PERSONEL_ID))) from PERSONEL where PERSONEL_DESIGNATION = 'SATICI' ";
                        SqlCommand getEmpIdCmd = new SqlCommand(getEmpIdQuery, SqlInfo.con);
                        SqlDataAdapter empIdAdapter = new SqlDataAdapter(getEmpIdCmd);
                        DataSet empIdDataSet = new DataSet();
                        empIdAdapter.Fill(empIdDataSet);
                        string id;
                        if ((empIdDataSet.Tables[0].Rows.Count) > 0)
                        {
                            id = Convert.ToString(empIdDataSet.Tables[0].Rows[0].ItemArray[0]);
                        }
                        else
                        {
                            id = string.Empty;
                        }
                        string newEmpID = idGenerator(id);
                        string newEmpQuery = "INSERT INTO PERSONEL(PERSONEL_ID,PERSONEL_NAME,PERSONEL_PASSWORD,PERSONEL_CONTACT,PERSONEL_ADDRESS,PERSONEL_EMAIL,PERSONEL_DESIGNATION,PERSONEL_HIREDATE,PERSONEL_STATUS,PERSONEL_SALES) VALUES(@id,@name,@password,@contact,@address,@email,'SATICI',(CONVERT(DATE, GETDATE())),'Calisiyor',0)";
                        SqlCommand newEmpCmd = new SqlCommand(newEmpQuery, SqlInfo.con);
                        newEmpCmd.Parameters.AddWithValue("@id", newEmpID);
                        newEmpCmd.Parameters.AddWithValue("@name", name);
                        newEmpCmd.Parameters.AddWithValue("@contact", contact);
                        newEmpCmd.Parameters.AddWithValue("@address", address);
                        newEmpCmd.Parameters.AddWithValue("@email", email);
                        newEmpCmd.Parameters.AddWithValue("@password", pin);
                        newEmpCmd.ExecuteNonQuery();
                        SqlInfo.con.Close();
                        frmCustomSuccessBox.Show("Yeni Personel Başarıyla Eklendi!");
                        nameBox.Text = contactBox.Text = pinBox.Text = addressBox.Text = contactBox.Text = emailBox.Text = "";
                    }
                    else
                    {
                        frmCustomMsgBox.Show("Verilen Email Geçersiz.\nLütfen Tekrar Kontrol Edin", "Tamam");
                        emailBoxErrorIcon.Visible = true;
                    }
                }
            }
        }


        private void updateBtn_MouseClick(object sender, MouseEventArgs e)
        {
            if (isUpdateData)
            {
                if (nameChange || pinChange || addressChange || contactChange || emailChange)
                {
                    string name = nameBox.Text;
                    string pin = pinBox.Text;
                    string contact = contactBox.Text;
                    string address = addressBox.Text;
                    string email = emailBox.Text;
                    string PERSONELId = updateEmp.id;
                    SqlInfo.con.Open();
                    string mEmailCheckQuery = "select * from PERSONEL where PERSONEL_EMAIL = @email and PERSONEL_ID <> @id";
                    SqlCommand mEmailCheckCMD = new SqlCommand(mEmailCheckQuery, SqlInfo.con);
                    mEmailCheckCMD.Parameters.AddWithValue("@email", email);
                    mEmailCheckCMD.Parameters.AddWithValue("@id", PERSONELId);
                    SqlDataAdapter mEmailCheckAdapter = new SqlDataAdapter(mEmailCheckCMD);
                    DataSet mEmailCheckSet = new DataSet();
                    mEmailCheckAdapter.Fill(mEmailCheckSet);
                    SqlInfo.con.Close();
                    if (mEmailCheckSet.Tables[0].Rows.Count == 0)
                    {
                        SqlInfo.con.Open();
                        string updateEmpQuery = "Update PERSONEL set PERSONEL_NAME = @newName, PERSONEL_PASSWORD= @newPin, PERSONEL_CONTACT = @newContact, PERSONEL_ADDRESS = @newAddress, PERSONEL_EMAIL = @newEmail  where PERSONEL_ID = @updateId";
                        SqlCommand updateEmpCMD = new SqlCommand(updateEmpQuery, SqlInfo.con);
                        updateEmpCMD.Parameters.AddWithValue("@newName", name);
                        updateEmpCMD.Parameters.AddWithValue("@newPin", pin);
                        updateEmpCMD.Parameters.AddWithValue("@newContact", contact);
                        updateEmpCMD.Parameters.AddWithValue("@newAddress", address);
                        updateEmpCMD.Parameters.AddWithValue("@newEmail", email);
                        updateEmpCMD.Parameters.AddWithValue("@updateId", PERSONELId);
                        updateEmpCMD.ExecuteNonQuery();
                        SqlInfo.con.Close();
                        frmCustomSuccessBox.Show("Personel Bilgiler Başarıyla Güncellendi.");
                        nameBox.Text = contactBox.Text = pinBox.Text = addressBox.Text = contactBox.Text = emailBox.Text = "";
                    }
                    else
                    {
                        frmCustomMsgBox.Show("Verilen Email Geçersiz.\nLütfen Tekrar Kontrol Edin", "Tamam");
                        emailBoxErrorIcon.Visible = true;
                    }
                }
                else
                {
                    frmCustomMsgBox.Show("Hiçbir Veriyi Değiştirmediniz.", "Tamam");
                }
            }
        }


        private void backBtn_MouseClick(object sender, MouseEventArgs e)
        {
            new empControl(mainEmpID).Show();
            this.Hide();
        }

        private void nameBox_TextChanged(object sender, EventArgs e)
        {
            if (updateEmp.name != nameBox.Text)
                nameChange = true;
        }
        private void contactBox_TextChanged(object sender, EventArgs e)
        {
            if (updateEmp.contact != contactBox.Text)
                contactChange = true;
        }
        private void pinBox_TextChanged(object sender, EventArgs e)
        {
            if (updateEmp.pin != pinBox.Text)
                pinChange = true;
        }
        private void addressBox_TextChanged(object sender, EventArgs e)
        {
            if (updateEmp.address != addressBox.Text)
                addressChange = true;
        }
        private void emailBox_TextChanged(object sender, EventArgs e)
        {
            if (updateEmp.email != emailBox.Text)
                emailChange = true;
        }

        private void nameBox_Enter(object sender, EventArgs e)
        {
            nameBoxErrorIcon.Visible = false;
            nameBox.BorderStyle = BorderStyle.None;
            nameBox.BackColor = Color.FromArgb(34, 36, 49);
            nameBox.ForeColor = Color.White;
        }
        private void nameBox_Leave(object sender, EventArgs e)
        {
            if (nameBox.Text == "")
            {
                nameBoxErrorIcon.Visible = true;
                nameFlag = true;
            }
            else
            {
                nameBoxErrorIcon.Visible = false;
                nameFlag = false;
            }
            nameBoxErrorIcon.BackColor = Color.Transparent;
            nameBox.BorderStyle = BorderStyle.Fixed3D;
            nameBox.BackColor = Color.White;
            nameBox.ForeColor = Color.FromArgb(77, 74, 82);
        }

        private void pinBox_Enter(object sender, EventArgs e)
        {
            pinBoxErrorIcon.Visible = false;
            pinBox.BorderStyle = BorderStyle.None;
            pinBox.BackColor = Color.FromArgb(34, 36, 49);
            pinBox.ForeColor = Color.White;
        }
        private void pinBox_Leave(object sender, EventArgs e)
        {
            if (pinBox.Text == "")
            {
                pinBoxErrorIcon.Visible = true;
                pinFlag = true;
            }
            else
            {
                pinBoxErrorIcon.Visible = false;
                pinFlag = false;
            }
            pinBoxErrorIcon.BackColor = Color.Transparent;
            pinBox.BorderStyle = BorderStyle.Fixed3D;
            pinBox.BackColor = Color.White;
            pinBox.ForeColor = Color.FromArgb(77, 74, 82);
        }

        private void addressBox_Enter(object sender, EventArgs e)
        {
            addressBoxErrorIcon.Visible = false;
            addressBox.BorderStyle = BorderStyle.None;
            addressBox.BackColor = Color.FromArgb(34, 36, 49);
            addressBox.ForeColor = Color.White;
        }
        private void addressBox_Leave(object sender, EventArgs e)
        {
            if (addressBox.Text == "")
            {
                addressBoxErrorIcon.Visible = true;
                addressFlag = true;
            }
            else
            {
                addressBoxErrorIcon.Visible = false;
                addressFlag = false;
            }
            addressBoxErrorIcon.BackColor = Color.Transparent;
            addressBox.BorderStyle = BorderStyle.Fixed3D;
            addressBox.BackColor = Color.White;
            addressBox.ForeColor = Color.FromArgb(77, 74, 82);
        }

        private void contactBox_Enter(object sender, EventArgs e)
        {
            contactBoxErrorIcon.Visible = false;
            contactBox.BorderStyle = BorderStyle.None;
            contactBox.BackColor = Color.FromArgb(34, 36, 49);
            contactBox.ForeColor = Color.White;
        }
        private void contactBox_Leave(object sender, EventArgs e)
        {
            if ((contactBox.Text == "") || ((contactBox.Text.Length) != 11))
            {
                contactBoxErrorIcon.Visible = true;
                contactFlag = true;
            }
            else if (contactBox.Text.Length == 11)
            {
                contactBoxErrorIcon.Visible = false;
                contactFlag = false;
            }
            contactBoxErrorIcon.BackColor = Color.Transparent;
            contactBox.BorderStyle = BorderStyle.Fixed3D;
            contactBox.BackColor = Color.White;
            contactBox.ForeColor = Color.FromArgb(77, 74, 82);
        }

        private void emailBox_Enter(object sender, EventArgs e)
        {
            emailBoxErrorIcon.Visible = false;
            emailBox.BorderStyle = BorderStyle.None;
            emailBox.BackColor = Color.FromArgb(34, 36, 49);
            emailBox.ForeColor = Color.White;
        }
        private void emailBox_Leave(object sender, EventArgs e)
        {
            if (emailBox.Text == "")
            {
                emailBoxErrorIcon.Visible = true;
                emailFlag = true;
            }
            else
            {
                emailBoxErrorIcon.Visible = false;
                emailFlag = false;
            }
            emailBoxErrorIcon.BackColor = Color.Transparent;
            emailBox.BorderStyle = BorderStyle.Fixed3D;
            emailBox.BackColor = Color.White;
            emailBox.ForeColor = Color.FromArgb(77, 74, 82);
        }
        private void nameBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar) || char.IsLetter(e.KeyChar) || char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                frmCustomMsgBox.Show("Hatalı Giriş.\nLütfen Her Metin Alanının Altında Gösterilen Şekilde Giriniz.", "Tamam");
                e.Handled = true;
            }
        }
        private void contactBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar) || char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                frmCustomMsgBox.Show("Hatalı Giriş.\nLütfen Her Metin Alanının Altında Gösterilen Şekilde Giriniz.", "Tamam");
                e.Handled = true;
            }
        }
        private void addressBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar) || char.IsLetterOrDigit(e.KeyChar) || (e.KeyChar == '/')
                || (e.KeyChar == '#') || (e.KeyChar == ',') || char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                frmCustomMsgBox.Show("Hatalı Giriş.\nLütfen Her Metin Alanının Altında Gösterilen Şekilde Giriniz.", "Tamam");
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
                frmCustomMsgBox.Show("Hatalı Giriş.\nLütfen Her Metin Alanının Altında Gösterilen Şekilde Giriniz.", "Tamam");
                e.Handled = true;
            }
        }
        private void emailBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar) || char.IsLetterOrDigit(e.KeyChar) || (e.KeyChar == '@')
                || (e.KeyChar == '.'))
            {
                e.Handled = false;
            }
            else
            {
                frmCustomMsgBox.Show("Hatalı Giriş.\nLütfen Her Metin Alanının Altında Gösterilen Şekilde Giriniz.", "Tamam");
                e.Handled = true;
            }
        }
        
        private void hireBtn_MouseEnter(object sender, EventArgs e)
        {
            hireBtn.BackColor = Color.FromArgb(34, 36, 49);
        }
        private void hireBtn_MouseLeave(object sender, EventArgs e)
        {
            hireBtn.BackColor = Color.FromArgb(77, 74, 82);
        }

        private void updateBtn_MouseEnter(object sender, EventArgs e)
        {
            updateBtn.BackColor = Color.FromArgb(34, 36, 49);
        }
        private void updateBtn_MouseLeave(object sender, EventArgs e)
        {
            updateBtn.BackColor = Color.FromArgb(77, 74, 82);
        }

        private void exitBtn_MouseEnter(object sender, EventArgs e)
        {
            exitBtn.BackColor = Color.Red;
            exitBtn.ForeColor = Color.White;
        }
        private void exitBtn_MouseLeave(object sender, EventArgs e)
        {
            exitBtn.BackColor = Color.White;
            exitBtn.ForeColor = Color.Red;
        }

        private void backBtn_MouseEnter(object sender, EventArgs e)
        {
            backBtn.BackColor = Color.FromArgb(34, 36, 49);
        }
        private void backBtn_MouseLeave(object sender, EventArgs e)
        {
            backBtn.BackColor = Color.Transparent;
        }
        private string idGenerator(string id)
        {
            string digits, letters;
            letters = "ST";
            if (id == string.Empty)
            {
                digits = "000";
            }
            else
            {
                digits = new string(id.Where(char.IsDigit).ToArray());
            }
            int number;
            int.TryParse(digits, out number);
            string new_id = letters + (++number).ToString("D3");

            return new_id;
        }
    }
}
