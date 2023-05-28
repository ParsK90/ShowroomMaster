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
    public partial class carCtrl : Form
    {
        string per_ID;
        bool mLicenceFlag, mNameFlag, mAddressFlag, mContactFlag, mEmailFlag;
        bool cIDFlag, cNameFlag, cModelFlag, cCompanyFlag, cPriceFlag;
        public carCtrl()
        {
            InitializeComponent();
        }
        public carCtrl(string id)
        {
            InitializeComponent();
            pictureVanish();
            emptyChecker();
            per_ID = id;
        }

        private void emptyChecker()
        {
            if (licenseBox.Text == "") mLicenceFlag = true;
            if (nameBox.Text == "") mNameFlag = true;
            if (contactBox.Text == "") mContactFlag = true;
            if (addressBox.Text == "") mAddressFlag = true;
            if (emailBox.Text == "") mEmailFlag = true;

            if (cIDBox.Text == "") cIDFlag = true;
            if (cNameBox.Text == "") cNameFlag = true;
            if (cModelBox.Text == "") cModelFlag = true;
            if (cCmpyBox.Text == "") cCompanyFlag = true;
            if (cPriceBox.Text == "") cPriceFlag = true;

        }
        private void pictureVanish()
        {
            manufLicenseErrorIcon.Visible = false;
            manufNameErrorIcon.Visible = false;
            manufContactErrorIcon.Visible = false;
            manufAddressErrorIcon.Visible = false;
            manufEmailErrorIcon.Visible = false;

            carIDErrorIcon.Visible = false;
            carNameErrorIcon.Visible = false;
            carModelErrorIcon.Visible = false;
            carCompanyErrorIcon.Visible = false;
            carPriceErrorIcon.Visible = false;
        }
        private void clearRows()
        {
            licenseBox.Text = "";
            nameBox.Text = "";
            emailBox.Text = "";
            addressBox.Text = "";
            contactBox.Text = "";

            cIDBox.Text = "";
            cNameBox.Text = "";
            cModelBox.Text = "";
            cCmpyBox.Text = "";
            cPriceBox.Text = "";
        }

        private void buyBtn_MouseClick(object sender, MouseEventArgs e)
        {
            bool isOldSeller = false;
            bool isnewSeller = true;

            string manfID = licenseBox.Text;
            string manfName = nameBox.Text;
            string manfEmail = emailBox.Text;
            string manfAddress = addressBox.Text;
            string manfContact = contactBox.Text;

            string carID = cIDBox.Text;
            string carName = cNameBox.Text;
            string carModel = cModelBox.Text;
            string carCompany = cCmpyBox.Text;
            string carPrice = cPriceBox.Text;

            if ((mLicenceFlag || mNameFlag || mContactFlag || mAddressFlag || mEmailFlag
                || cIDFlag || cNameFlag || cModelFlag || cPriceFlag || cCompanyFlag))
            {
                if (mLicenceFlag) manufLicenseErrorIcon.Visible = true;
                if (mNameFlag) manufNameErrorIcon.Visible = true;
                if (mContactFlag) manufContactErrorIcon.Visible = true;
                if (mAddressFlag) manufAddressErrorIcon.Visible = true;
                if (mEmailFlag) manufEmailErrorIcon.Visible = true;

                if (cIDFlag) carIDErrorIcon.Visible = true;
                if (cNameFlag) carNameErrorIcon.Visible = true;
                if (cModelFlag) carModelErrorIcon.Visible = true;
                if (cCompanyFlag) carCompanyErrorIcon.Visible = true;
                if (cPriceFlag) carPriceErrorIcon.Visible = true;

                frmCustomMsgBox.Show("Hatalı Giriş.\nLütfen Her Metin Alanının Altında Gösterilen Şekilde Giriniz.", "Tamam");
            }
            else
            {
                SqlInfo.con.Open();
                string cIDCheckQuery = "select * from CAR where CAR_ID = @id";
                SqlCommand cIDCheckCMD = new SqlCommand(cIDCheckQuery, SqlInfo.con);
                cIDCheckCMD.Parameters.AddWithValue("@id", carID);
                SqlDataAdapter cIDCheckAdapter = new SqlDataAdapter(cIDCheckCMD);
                DataSet cIDCheckSet = new DataSet();
                cIDCheckAdapter.Fill(cIDCheckSet);
                SqlInfo.con.Close();
                if (cIDCheckSet.Tables[0].Rows.Count > 0)
                {
                    frmCustomMsgBox.Show("Girilen Araba Kimliği Yanlış.\nLütfen Tekrar Kontrol Edin", "Tamam");
                    carIDErrorIcon.Visible = true;
                }
                else
                {
                    SqlInfo.con.Open();
                    string mIDCheckQuery = "select * from MANUFACTURER where MANUFACTURER_ID = @id";
                    SqlCommand mIDCheckCMD = new SqlCommand(mIDCheckQuery, SqlInfo.con);
                    mIDCheckCMD.Parameters.AddWithValue("@id", manfID);
                    SqlDataAdapter mIDCheckAdapter = new SqlDataAdapter(mIDCheckCMD);
                    DataSet mIDCheckSet = new DataSet();
                    mIDCheckAdapter.Fill(mIDCheckSet);
                    SqlInfo.con.Close();

                    if (mIDCheckSet.Tables[0].Rows.Count > 0)
                    {
                        isnewSeller = false;
                        SqlInfo.con.Open();
                        string nameCheckQuery = "select * from MANUFACTURER where MANUFACTURER_NAME = @name and MANUFACTURER_ID = @id " +
                            "and MANUFACTURER_EMAIL = @email";
                        SqlCommand nameCheckCMD = new SqlCommand(nameCheckQuery, SqlInfo.con);
                        nameCheckCMD.Parameters.AddWithValue("@name", manfName);
                        nameCheckCMD.Parameters.AddWithValue("@id", manfID);
                        nameCheckCMD.Parameters.AddWithValue("@email", manfEmail);
                        SqlDataAdapter nameCheckAdapter = new SqlDataAdapter(nameCheckCMD);
                        DataSet nameCheckSet = new DataSet();
                        nameCheckAdapter.Fill(nameCheckSet);
                        SqlInfo.con.Close();
                        if (nameCheckSet.Tables[0].Rows.Count > 0) isOldSeller = true;
                        else
                        {
                            frmCustomMsgBox.Show("Verilen Üreticinin Lisansı/Adı Geçersiz. Lütfen Yeniden Kontrol Edin veya Geliştiriciyi Bilgilendirin", "Tamam");
                            manufLicenseErrorIcon.Visible = manufNameErrorIcon.Visible = true;
                        }

                    }
                    if (isnewSeller)
                    {
                        SqlInfo.con.Open();
                        string mEmailCheckQuery = "select * from MANUFACTURER where MANUFACTURER_EMAIL = @email";
                        SqlCommand mEmailCheckCMD = new SqlCommand(mEmailCheckQuery, SqlInfo.con);
                        mEmailCheckCMD.Parameters.AddWithValue("@email", manfEmail);
                        SqlDataAdapter mEmailCheckAdapter = new SqlDataAdapter(mEmailCheckCMD);
                        DataSet mEmailCheckSet = new DataSet();
                        mEmailCheckAdapter.Fill(mEmailCheckSet);
                        SqlInfo.con.Close();
                        if (mEmailCheckSet.Tables[0].Rows.Count == 0)
                        {
                            SqlInfo.con.Open();
                            string manufAddQuery = "INSERT INTO MANUFACTURER(MANUFACTURER_ID,MANUFACTURER_NAME,MANUFACTURER_CONTACT,MANUFACTURER_EMAIL,MANUFACTURER_ADDRESS) Values(@id,@name,@contact,@email,@address)";
                            SqlCommand manufAddCMD = new SqlCommand(manufAddQuery, SqlInfo.con);
                            manufAddCMD.Parameters.AddWithValue("@id", manfID);
                            manufAddCMD.Parameters.AddWithValue("@name", manfName);
                            manufAddCMD.Parameters.AddWithValue("@email", manfEmail);
                            manufAddCMD.Parameters.AddWithValue("@address", manfAddress);
                            manufAddCMD.Parameters.AddWithValue("@contact", manfContact);
                            manufAddCMD.ExecuteNonQuery();
                            SqlInfo.con.Close();
                        }
                        else
                        {
                            isnewSeller = false;
                            frmCustomMsgBox.Show("Hatalı Giriş.\nLütfen Her Metin Alanının Altında Gösterilen Şekilde Giriniz.", "Tamam");
                            manufEmailErrorIcon.Visible = true;

                        }
                    }

                    if (isOldSeller || isnewSeller)
                    {
                        SqlInfo.con.Open();
                        string carAddQuery = "INSERT INTO CAR(CAR_ID,CAR_NAME,CAR_MODEL,CAR_COMPANY,CAR_STATUS,CAR_PRICE) Values(@cID,@cName,@cModel,@cCompany,'Mevcut',@cPrice)";
                        SqlCommand carAddCMD = new SqlCommand(carAddQuery, SqlInfo.con);
                        carAddCMD.Parameters.AddWithValue("@cID", carID);
                        carAddCMD.Parameters.AddWithValue("@cName", carName);
                        carAddCMD.Parameters.AddWithValue("@cModel", carModel);
                        carAddCMD.Parameters.AddWithValue("@cCompany", carCompany);
                        carAddCMD.Parameters.AddWithValue("@cPrice", carPrice);
                        carAddCMD.ExecuteNonQuery();

                        string getOrderQuery = "Select max(substring(MANUF_ORDER.ORDER_ID,4,len(MANUF_ORDER.ORDER_ID))) from MANUF_ORDER ";
                        SqlCommand getCmd = new SqlCommand(getOrderQuery, SqlInfo.con);
                        SqlDataAdapter orderAdapter = new SqlDataAdapter(getCmd);
                        DataSet orderData = new DataSet();
                        orderAdapter.Fill(orderData);
                        string id;
                        if ((orderData.Tables[0].Rows.Count) > 0)
                        {
                            id = Convert.ToString(orderData.Tables[0].Rows[0].ItemArray[0]);
                        }
                        else
                        {
                            id = string.Empty;
                        }
                        string OrderID = idGenerator(id);

                        string addOrderQuery = "INSERT INTO MANUF_ORDER(ORDER_ID,PERSONEL_ID,CAR_ID,MANUFACTURER_ID,ORDER_DATE,BILL) Values(@orderID,@per_ID,@carID,@manfID,getdate(),@bill)";
                        SqlCommand addOrderCMD = new SqlCommand(addOrderQuery, SqlInfo.con);
                        addOrderCMD.Parameters.AddWithValue("@orderID", OrderID);
                        addOrderCMD.Parameters.AddWithValue("@per_ID", per_ID);
                        addOrderCMD.Parameters.AddWithValue("@carID", carID);
                        addOrderCMD.Parameters.AddWithValue("@manfID", manfID);
                        addOrderCMD.Parameters.AddWithValue("@bill", carPrice);
                        addOrderCMD.ExecuteNonQuery();

                        string addBill = "INSERT INTO STOCK_PAYMENT(ORDER_ID,PAYMENT_DATE) Values(@id,getdate())";
                        SqlCommand addBillCMD = new SqlCommand(addBill, SqlInfo.con);
                        addBillCMD.Parameters.AddWithValue("@id", OrderID);
                        addBillCMD.ExecuteNonQuery();

                        string addStock = "INSERT INTO Stock(ORDER_ID,Car_ID,REC_DATE) Values(@oID,@cID,getdate())";
                        SqlCommand addStockCMD = new SqlCommand(addStock, SqlInfo.con);
                        addStockCMD.Parameters.AddWithValue("@oID", OrderID);
                        addStockCMD.Parameters.AddWithValue("@cID", carID);
                        addStockCMD.ExecuteNonQuery();

                        string updateAccountQuery = "Insert into ACCOUNT(MANUF_ORDER,AMOUNT,IS_PAID,PAYMENT_DATE) Values(@order,@amount,'TRUE',GETDATE())";
                        SqlCommand updateAccountCMD = new SqlCommand(updateAccountQuery, SqlInfo.con);
                        updateAccountCMD.Parameters.AddWithValue("@order", OrderID);
                        updateAccountCMD.Parameters.AddWithValue("@amount", carPrice);
                        updateAccountCMD.ExecuteNonQuery();

                        SqlInfo.con.Close();
                        frmCustomSuccessBox.Show("Yeni Araba Başarıyla Eklendi.");
                        clearRows();
                    }
                }
            }
        }

        private void licenseBox_Enter(object sender, EventArgs e)
        {
            manufLicenseErrorIcon.Visible = false;
            licenseBox.BorderStyle = BorderStyle.None;
            licenseBox.BackColor = Color.FromArgb(34, 36, 49);
            licenseBox.ForeColor = Color.White;
        }
        private void licenseBox_Leave(object sender, EventArgs e)
        {
            if (licenseBox.Text == "")
            {
                manufLicenseErrorIcon.Visible = true;
                mLicenceFlag = true;
            }
            else
            {
                manufLicenseErrorIcon.Visible = false;
                mLicenceFlag = false;
            }
            manufLicenseErrorIcon.BackColor = Color.Transparent;
            licenseBox.BorderStyle = BorderStyle.Fixed3D;
            licenseBox.BackColor = Color.White;
            licenseBox.ForeColor = Color.FromArgb(77, 74, 82);
        }

        private void nameBox_Enter(object sender, EventArgs e)
        {
            manufNameErrorIcon.Visible = false;
            nameBox.BorderStyle = BorderStyle.None;
            nameBox.BackColor = Color.FromArgb(34, 36, 49);
            nameBox.ForeColor = Color.White;
        }
        private void nameBox_Leave(object sender, EventArgs e)
        {
            if (nameBox.Text == "")
            {
                manufNameErrorIcon.Visible = true;
                mNameFlag = true;
            }
            else
            {
                manufNameErrorIcon.Visible = false;
                mNameFlag = false;
            }
            manufNameErrorIcon.BackColor = Color.Transparent;
            nameBox.BorderStyle = BorderStyle.Fixed3D;
            nameBox.BackColor = Color.White;
            nameBox.ForeColor = Color.FromArgb(77, 74, 82);
        }

        private void contactBox_Enter(object sender, EventArgs e)
        {
            manufContactErrorIcon.Visible = false;
            contactBox.BorderStyle = BorderStyle.None;
            contactBox.BackColor = Color.FromArgb(34, 36, 49);
            contactBox.ForeColor = Color.White;
        }
        private void contactBox_Leave(object sender, EventArgs e)
        {
            if (contactBox.Text == "" || contactBox.Text.Length != 11)
            {
                manufContactErrorIcon.Visible = true;
                mContactFlag = true;
            }
            else
            {
                manufContactErrorIcon.Visible = false;
                mContactFlag = false;
            }
            manufContactErrorIcon.BackColor = Color.Transparent;
            contactBox.BorderStyle = BorderStyle.Fixed3D;
            contactBox.BackColor = Color.White;
            contactBox.ForeColor = Color.FromArgb(77, 74, 82);
        }

        private void addressBox_Enter(object sender, EventArgs e)
        {
            manufAddressErrorIcon.Visible = false;
            addressBox.BorderStyle = BorderStyle.None;
            addressBox.BackColor = Color.FromArgb(34, 36, 49);
            addressBox.ForeColor = Color.White;
        }
        private void addressBox_Leave(object sender, EventArgs e)
        {
            if (addressBox.Text == "")
            {
                manufAddressErrorIcon.Visible = true;
                mAddressFlag = true;
            }
            else
            {
                manufAddressErrorIcon.Visible = false;
                mAddressFlag = false;
            }
            manufAddressErrorIcon.BackColor = Color.Transparent;
            addressBox.BorderStyle = BorderStyle.Fixed3D;
            addressBox.BackColor = Color.White;
            addressBox.ForeColor = Color.FromArgb(77, 74, 82);
        }

        private void emailBox_Enter(object sender, EventArgs e)
        {
            manufEmailErrorIcon.Visible = false;
            emailBox.BorderStyle = BorderStyle.None;
            emailBox.BackColor = Color.FromArgb(34, 36, 49);
            emailBox.ForeColor = Color.White;
        }
        private void emailBox_Leave(object sender, EventArgs e)
        {
            if (emailBox.Text == "")
            {
                manufEmailErrorIcon.Visible = true;
                mEmailFlag = true;
            }
            else
            {
                manufEmailErrorIcon.Visible = false;
                mEmailFlag = false;
            }
            manufEmailErrorIcon.BackColor = Color.Transparent;
            emailBox.BorderStyle = BorderStyle.Fixed3D;
            emailBox.BackColor = Color.White;
            emailBox.ForeColor = Color.FromArgb(77, 74, 82);
        }

        private void cIDBox_Enter(object sender, EventArgs e)
        {
            carIDErrorIcon.Visible = false;
            cIDBox.BorderStyle = BorderStyle.None;
            cIDBox.BackColor = Color.FromArgb(34, 36, 49);
            cIDBox.ForeColor = Color.White;
        }
        private void cIDBox_Leave(object sender, EventArgs e)
        {
            if (cIDBox.Text == "")
            {
                carIDErrorIcon.Visible = true;
                cIDFlag = true;
            }
            else
            {
                carIDErrorIcon.Visible = false;
                cIDFlag = false;
            }
            carIDErrorIcon.BackColor = Color.Transparent;
            cIDBox.BorderStyle = BorderStyle.Fixed3D;
            cIDBox.BackColor = Color.White;
            cIDBox.ForeColor = Color.FromArgb(77, 74, 82);
        }

        private void cNameBox_Enter(object sender, EventArgs e)
        {
            carNameErrorIcon.Visible = false;
            cNameBox.BorderStyle = BorderStyle.None;
            cNameBox.BackColor = Color.FromArgb(34, 36, 49);
            cNameBox.ForeColor = Color.White;
        }
        private void cNameBox_Leave(object sender, EventArgs e)
        {
            if (cNameBox.Text == "")
            {
                carNameErrorIcon.Visible = true;
                cNameFlag = true;
            }
            else
            {
                carNameErrorIcon.Visible = false;
                cNameFlag = false;
            }
            carIDErrorIcon.BackColor = Color.Transparent;
            cNameBox.BorderStyle = BorderStyle.Fixed3D;
            cNameBox.BackColor = Color.White;
            cNameBox.ForeColor = Color.FromArgb(77, 74, 82);
        }

        private void cModelBox_Enter(object sender, EventArgs e)
        {
            carModelErrorIcon.Visible = false;
            cModelBox.BorderStyle = BorderStyle.None;
            cModelBox.BackColor = Color.FromArgb(34, 36, 49);
            cModelBox.ForeColor = Color.White;
        }
        private void cModelBox_Leave(object sender, EventArgs e)
        {
            if (cModelBox.Text == "" || cModelBox.Text.Length != 4)
            {
                carModelErrorIcon.Visible = true;
                cModelFlag = true;
            }
            else
            {
                carModelErrorIcon.Visible = false;
                cModelFlag = false;
            }
            carModelErrorIcon.BackColor = Color.Transparent;
            cModelBox.BorderStyle = BorderStyle.Fixed3D;
            cModelBox.BackColor = Color.White;
            cModelBox.ForeColor = Color.FromArgb(77, 74, 82);
        }

        private void cCmpyBox_Enter(object sender, EventArgs e)
        {
            carCompanyErrorIcon.Visible = false;
            cCmpyBox.BorderStyle = BorderStyle.None;
            cCmpyBox.BackColor = Color.FromArgb(34, 36, 49);
            cCmpyBox.ForeColor = Color.White;
        }
        private void cCmpyBox_Leave(object sender, EventArgs e)
        {
            if (cCmpyBox.Text == "")
            {
                carCompanyErrorIcon.Visible = true;
                cCompanyFlag = true;
            }
            else
            {
                carCompanyErrorIcon.Visible = false;
                cCompanyFlag = false;
            }
            carCompanyErrorIcon.BackColor = Color.Transparent;
            cCmpyBox.BorderStyle = BorderStyle.Fixed3D;
            cCmpyBox.BackColor = Color.White;
            cCmpyBox.ForeColor = Color.FromArgb(77, 74, 82);
        }

        private void cPriceBox_Enter(object sender, EventArgs e)
        {
            carPriceErrorIcon.Visible = false;
            cPriceBox.BorderStyle = BorderStyle.None;
            cPriceBox.BackColor = Color.FromArgb(34, 36, 49);
            cPriceBox.ForeColor = Color.White;
        }
        private void cPriceBox_Leave(object sender, EventArgs e)
        {
            if (cPriceBox.Text == "")
            {
                carPriceErrorIcon.Visible = true;
                cPriceFlag = true;
            }
            else
            {
                carPriceErrorIcon.Visible = false;
                cPriceFlag = false;
            }
            carPriceErrorIcon.BackColor = Color.Transparent;
            cPriceBox.BorderStyle = BorderStyle.Fixed3D;
            cPriceBox.BackColor = Color.White;
            cPriceBox.ForeColor = Color.FromArgb(77, 74, 82);
        }
        private void licenseBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar) || char.IsLetter(e.KeyChar) || char.IsDigit(e.KeyChar))
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


        private void cNameBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar) || char.IsLetterOrDigit(e.KeyChar) || char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                frmCustomMsgBox.Show("Hatalı Giriş.\nLütfen Her Metin Alanının Altında Gösterilen Şekilde Giriniz.", "Tamam");
                e.Handled = true;
            }
        }
        private void cCmpyBox_KeyPress(object sender, KeyPressEventArgs e)
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
        private void cPriceBox_KeyPress(object sender, KeyPressEventArgs e)
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
        private void cModelBox_KeyPress(object sender, KeyPressEventArgs e)
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

        private void buyBtn_MouseEnter(object sender, EventArgs e)
        {
            buyBtn.BackColor = Color.FromArgb(34, 36, 49);
        }

        private void buyBtn_MouseLeave(object sender, EventArgs e)
        {
            buyBtn.BackColor = Color.FromArgb(77, 74, 82);
        }

        private void cIDBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar) || char.IsLetter(e.KeyChar) || char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                frmCustomMsgBox.Show("Hatalı Giriş.\nLütfen Her Metin Alanının Altında Gösterilen Şekilde Giriniz.", "Tamam");
                e.Handled = true;
            }
        }
        private void backBtn_MouseClick(object sender, MouseEventArgs e)
        {
            new SMMenu(per_ID).Show();
            this.Hide();
        }
        private void backBtn_MouseEnter(object sender, EventArgs e)
        {
            backBtn.BackColor = Color.FromArgb(34, 36, 49);
        }
        private void backBtn_MouseLeave(object sender, EventArgs e)
        {
            backBtn.BackColor = Color.Transparent;
        }

        private void exitBtn_MouseEnter(object sender, EventArgs e)
        {
            exitBtn.BackColor = Color.Red;
            exitBtn.ForeColor = Color.White;
        }
        private void exitBtn_MouseLeave(object sender, EventArgs e)
        {
            exitBtn.BackColor = Color.Transparent;
            exitBtn.ForeColor = Color.Red;
        }

        private string idGenerator(string id)
        {
            string digits, letters;
            letters = "MOD";
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
            string new_id = letters + (++number).ToString("D4");

            return new_id;
        }
    }
}
