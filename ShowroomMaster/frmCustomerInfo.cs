using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShowroomMaster
{
    public partial class CarSell : Form
    {
        string perId,carID;
        bool nameFlag, cnicFlag, addressFlag, contactFlag;

        public CarSell()
        {
            InitializeComponent();
            this.CenterToScreen();
        }

        public CarSell(string id)
        {
            InitializeComponent();
            perId = id;
            this.CenterToScreen();
        }
        public CarSell(string id,string carid)
        {
            InitializeComponent();
            perId = id;
            carID = carid;
            pictureVanish();
            startChecker();
        }

        private void pictureVanish()
        {
            nameBoxErrorIcon.Visible = false;
            cnicBoxErrorIcon.Visible = false;
            addressBoxErrorIcon.Visible = false;
            contactBoxErrorIcon.Visible = false;
        }
        private void startChecker()
        {
            if (nameBox.Text == "")
            {
                nameFlag = true;
            }

            if (cnicBox.Text == "")
            {
                cnicFlag = true;
            }
            if (addressBox.Text == "")
            {
                addressFlag = true;
            }

            if (contactBox.Text == "")
            {
                contactFlag = true;
            }
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

       
        private void backBtn_MouseClick(object sender, MouseEventArgs e)
        {
            new SMMenu(perId).Show();
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

        private void sellBtn_MouseEnter(object sender, EventArgs e)
        {
            sellBtn.BackColor = Color.FromArgb(34, 36, 49);
        }
        private void sellBtn_MouseLeave(object sender, EventArgs e)
        {
            sellBtn.BackColor = Color.FromArgb(77, 74, 82);
        }
        private void sellBtn_MouseClick(object sender, MouseEventArgs e)
        {
            string cust_name, cust_cnic, cust_address, cust_contact;
            bool oldCustomer = false;
            bool newCustomer = true;
            cust_name = nameBox.Text;
            cust_cnic = cnicBox.Text;
            cust_address = addressBox.Text;
            cust_contact = contactBox.Text;
            if ((nameFlag || cnicFlag || addressFlag || contactFlag) == true)
            {
                if (nameFlag) nameBoxErrorIcon.Visible = true;
                if (cnicFlag) cnicBoxErrorIcon.Visible = true;
                if (addressFlag) addressBoxErrorIcon.Visible = true;
                if (contactFlag) contactBoxErrorIcon.Visible = true;

                frmCustomMsgBox.Show("Hatalı Giriş.\nLütfen Doğru Bilgileri Girin ve Gerekli Bilgileri İçeren Alanları Doldurun.", "Tamam");
            }
            else
            {
                SqlInfo.con.Open();
                string cnicCheckQuery = "select * from CUSTOMER where CUSTOMER_TC = @id";
                SqlCommand cnicCheckCMD = new SqlCommand(cnicCheckQuery, SqlInfo.con);
                cnicCheckCMD.Parameters.AddWithValue("@id", cust_cnic);
                SqlDataAdapter cnicCheckAdapter = new SqlDataAdapter(cnicCheckCMD);
                DataSet cnicCheckSet = new DataSet();
                cnicCheckAdapter.Fill(cnicCheckSet);
                SqlInfo.con.Close();
                
                if ((cnicCheckSet.Tables[0].Rows.Count) > 0)
                {
                    newCustomer = false;
                    SqlInfo.con.Open();
                    string nameCheckQuery = "select * from CUSTOMER where CUSTOMER_NAME = @name and CUSTOMER_TC = @id";
                    SqlCommand nameCheckCMD = new SqlCommand(nameCheckQuery, SqlInfo.con);
                    nameCheckCMD.Parameters.AddWithValue("@name", cust_name);
                    nameCheckCMD.Parameters.AddWithValue("@id", cust_cnic);
                    SqlDataAdapter nameCheckAdapter = new SqlDataAdapter(nameCheckCMD);
                    DataSet nameCheckSet = new DataSet();
                    nameCheckAdapter.Fill(nameCheckSet);
                    SqlInfo.con.Close();
                    if (nameCheckSet.Tables[0].Rows.Count > 0) oldCustomer = true;
                    else frmCustomMsgBox.Show("Verilen T.C./Ad Geçersiz. Lütfen Doğru T.C. ve Ad Girin.", "Tamam");
                }
                if (newCustomer) 
                {
                    SqlInfo.con.Open();
                    string insertQuery = "Insert into CUSTOMER(CUSTOMER_TC,CUSTOMER_NAME,CUSTOMER_CONTACT,CUSTOMER_ADDRESS) VALUES(@tc,@name,@contact,@address)";
                    SqlCommand cmd = new SqlCommand(insertQuery, SqlInfo.con);
                    cmd.Parameters.AddWithValue("@tc", cust_cnic);
                    cmd.Parameters.AddWithValue("@name", cust_name);
                    cmd.Parameters.AddWithValue("@contact", cust_contact);
                    cmd.Parameters.AddWithValue("@address", cust_address);
                    cmd.ExecuteNonQuery();
                    SqlInfo.con.Close();
                }
                if (oldCustomer || newCustomer)
                {
                    SqlInfo.con.Open();
                    string getOrderQuery = "Select max(substring(CUSTOMER_ORDER.ORDER_ID,4,len(CUSTOMER_ORDER.ORDER_ID))) from CUSTOMER_ORDER ";
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
                    string getPriceQuery = "Select CAR.CAR_PRICE from CAR where CAR.CAR_ID = @id";
                    SqlCommand getPriceCmd = new SqlCommand(getPriceQuery, SqlInfo.con);
                    getPriceCmd.Parameters.AddWithValue("@id", carID);
                    SqlDataAdapter priceAdapter = new SqlDataAdapter(getPriceCmd);
                    DataSet priceData = new DataSet();
                    priceAdapter.Fill(priceData);
                    int price = Convert.ToInt32(priceData.Tables[0].Rows[0].ItemArray[0]);
                    int newBill = price + ((price * 7) / 100);
                    string upOrderQuery = "Insert into CUSTOMER_ORDER(ORDER_ID,PERSONEL_ID,CAR_ID,CUSTOMER_TC,ORDER_DATE,BILL) values(@Oid,@perId,@CiD,@tc,getDate(),@bill)";
                    SqlCommand upCMD = new SqlCommand(upOrderQuery, SqlInfo.con);
                    upCMD.Parameters.AddWithValue("@Oid", OrderID);
                    upCMD.Parameters.AddWithValue("@perId", perId);
                    upCMD.Parameters.AddWithValue("@CiD", carID);
                    upCMD.Parameters.AddWithValue("@tc", cust_cnic);
                    upCMD.Parameters.AddWithValue("@bill", newBill);
                    upCMD.ExecuteNonQuery();
                    string paymentQuery = "insert into SELL_PAYMENT(ORDER_ID,PAYMENT_DATE) values(@order,getDate())";
                    SqlCommand paymentCMD = new SqlCommand(paymentQuery, SqlInfo.con);
                    paymentCMD.Parameters.AddWithValue("@order", OrderID);
                    paymentCMD.ExecuteNonQuery();
                    string updateCarQuery = "update CAR set CAR.CAR_STATUS='Satıldı' where CAR_ID = @carid";
                    SqlCommand updateCMD = new SqlCommand(updateCarQuery, SqlInfo.con);
                    updateCMD.Parameters.AddWithValue("@carid", carID);
                    updateCMD.ExecuteNonQuery();
                    string updateSalesQuery = "Update PERSONEL set PERSONEL_SALES = (PERSONEL_SALES+1) where PERSONEL_ID = @id";
                    SqlCommand updateSaleCMD = new SqlCommand(updateSalesQuery, SqlInfo.con);
                    updateSaleCMD.Parameters.AddWithValue("@id", perId);
                    updateSaleCMD.ExecuteNonQuery();
                    string updateAccountQuery = "Insert into ACCOUNT(CUST_ORDER,AMOUNT,IS_PAID,PAYMENT_DATE) Values(@order,@amount,'FALSE',GETDATE())";
                    SqlCommand updateAccountCMD = new SqlCommand(updateAccountQuery, SqlInfo.con);
                    updateAccountCMD.Parameters.AddWithValue("@order", OrderID);
                    updateAccountCMD.Parameters.AddWithValue("@amount", newBill);
                    updateAccountCMD.ExecuteNonQuery();
                    frmCustomSuccessBox.Show("İşlem Başarıyla Tamamlandı!");
                    SqlInfo.con.Close();
                    new SMMenu(perId).Show();
                    this.Close();
                    nameBox.Text = "";
                    cnicBox.Text = "";
                    addressBox.Text = "";
                    contactBox.Text = "";                 
                }
            }
        
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
            if(nameBox.Text == "")
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

        private void cnicBox_Enter(object sender, EventArgs e)
        {
            cnicBoxErrorIcon.Visible = false;
            cnicBox.BorderStyle = BorderStyle.None;
            cnicBox.BackColor = Color.FromArgb(34, 36, 49);
            cnicBox.ForeColor = Color.White;
        }
        private void cnicBox_Leave(object sender, EventArgs e)
        {
            if((cnicBox.Text == "") || (cnicBox.Text.Length != 11))
            {
                cnicBoxErrorIcon.Visible = true;
                cnicFlag = true;
            }
            else if(cnicBox.Text.Length == 11)
            {
                cnicBoxErrorIcon.Visible = false;
                cnicFlag = false;
            }
            cnicBoxErrorIcon.BackColor = Color.Transparent;
            cnicBox.BorderStyle = BorderStyle.Fixed3D;
            cnicBox.BackColor = Color.White;
            cnicBox.ForeColor = Color.FromArgb(77, 74, 82);
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
            if(addressBox.Text == "")
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
            if((contactBox.Text == "") || ((contactBox.Text.Length) != 11 ))
            {
                contactBoxErrorIcon.Visible = true;
                contactFlag = true;
            }
            else if(contactBox.Text.Length == 11)
            {
                contactBoxErrorIcon.Visible = false;
                contactFlag = false;
            }
            contactBoxErrorIcon.BackColor = Color.Transparent;
            contactBox.BorderStyle = BorderStyle.Fixed3D;
            contactBox.BackColor = Color.White;
            contactBox.ForeColor = Color.FromArgb(77, 74, 82);
        }

        private void nameBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar) || char.IsLetter(e.KeyChar) || char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                frmCustomMsgBox.Show("Hatalı Giriş!\nLütfen Doğru Bilgileri Girin ve Gerekli Bilgileri İçeren Alanları Doldurun.", "Tamam");
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
                frmCustomMsgBox.Show("Hatalı Giriş!\nLütfen Doğru Bilgileri Girin ve Gerekli Bilgileri İçeren Alanları Doldurun.", "Tamam");
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
                frmCustomMsgBox.Show("Hatalı Giriş!\nLütfen Doğru Bilgileri Girin ve Gerekli Bilgileri İçeren Alanları Doldurun.", "Tamam");
                e.Handled = true;
            }

        }

        private void cnicBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar) || char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                frmCustomMsgBox.Show("Hatalı Giriş!\nLütfen Doğru Bilgileri Girin ve Gerekli Bilgileri İçeren Alanları Doldurun.", "Tamam");
                e.Handled = true;
            }
        }


        private string idGenerator(string id)
        {
            string digits,letters;
            letters = "MTS";
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
