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
   
    public partial class empControl : Form
    {
        string mainEmpID;
       
        SqlInfo.empInfo empUpdateInfo = new SqlInfo.empInfo("");
       
        
        public empControl()
        {
            InitializeComponent();
            gridFill();

        }
        public empControl(string id)
        {
            InitializeComponent();
            gridFill();
            mainEmpID = id;
            
        }
        private void gridFill()
        {
            bool isFired = false;
            DateTime fireDate = default(DateTime);
            SqlInfo.con.Open();
            SqlCommand getEmpCmd = new SqlCommand("select * from PERSONEL where PERSONEL_DESIGNATION = 'satıcı' order by PERSONEL_DESIGNATION", SqlInfo.con);
            SqlDataAdapter empAdapter = new SqlDataAdapter(getEmpCmd);
            DataSet empDataset = new DataSet();
            empAdapter.Fill(empDataset);

            empGrid.Rows.Clear();
            for (int i = 0; i < (empDataset.Tables[0].Rows.Count); i++)
            {
                string id = Convert.ToString(empDataset.Tables[0].Rows[i].ItemArray[0]);
                string name = Convert.ToString(empDataset.Tables[0].Rows[i].ItemArray[1]);
                string pin = Convert.ToString(empDataset.Tables[0].Rows[i].ItemArray[2]);
                string contact = Convert.ToString(empDataset.Tables[0].Rows[i].ItemArray[3]);
                string address = Convert.ToString(empDataset.Tables[0].Rows[i].ItemArray[4]);
                string email = Convert.ToString(empDataset.Tables[0].Rows[i].ItemArray[5]);
                string designation = Convert.ToString(empDataset.Tables[0].Rows[i].ItemArray[6]);
                string hire = Convert.ToString(empDataset.Tables[0].Rows[i].ItemArray[7]);
                string fire = Convert.ToString(empDataset.Tables[0].Rows[i].ItemArray[8]);
                string status = Convert.ToString(empDataset.Tables[0].Rows[i].ItemArray[9]);
                string sales = Convert.ToString(empDataset.Tables[0].Rows[i].ItemArray[10]);

                DateTime hireDate = (Convert.ToDateTime(hire)).Date;
                if (fire == string.Empty)
                {
                    isFired = false;
                    fire = "---";
                }
                else
                {
                    isFired = true;
                    fireDate = (Convert.ToDateTime(fire)).Date;
                }

                DataGridViewRow pushData = new DataGridViewRow();
                pushData.CreateCells(empGrid);
                pushData.Cells[0].Value = id;
                pushData.Cells[1].Value = name;
                pushData.Cells[2].Value = pin;
                pushData.Cells[3].Value = contact;
                pushData.Cells[4].Value = address;
                pushData.Cells[5].Value = email;
                pushData.Cells[6].Value = hireDate;
                if (isFired) pushData.Cells[7].Value = fireDate;
                else pushData.Cells[7].Value = fire;
                pushData.Cells[8].Value = status;
                pushData.Cells[9].Value = sales;
                empGrid.Rows.Add(pushData);

            }
            SqlInfo.con.Close();
        }
        private void fireEmp()
        {
            if (empUpdateInfo.status == "Fired")
            {
                frmCustomMsgBox.Show("Seçilen Çalışan Zaten İşten Çıkarıldı.\nLütfen Geçerli Çalışanı Seçin.", "Tamam");
            }
            else
            {

                if (empUpdateInfo.id == "")
                {
                    frmCustomMsgBox.Show("Lütfen İşten Çıkarmak İstediğiniz Çalışanı Seçin.", "Tamam");
                }
                else
                {
                    SqlInfo.con.Open();
                    string fireQuery = "Update PERSONEL set PERSONEL_STATUS = 'Fired',PERSONEL_FIREDATE = CONVERT(DATE, GETDATE()) where PERSONEL_ID = @id";
                    SqlCommand fireCmd = new SqlCommand(fireQuery, SqlInfo.con);
                    fireCmd.Parameters.AddWithValue("@id", empUpdateInfo.id);
                    fireCmd.ExecuteNonQuery();
                    SqlInfo.con.Close();
                    gridFill();
                    frmCustomSuccessBox.Show("İşlem başarılıyla Tamamlandı!");
                }
            }
            
        }
        private void rehireEmp()
        {
            if (empUpdateInfo.status == "Calisiyor")
            {
                frmCustomMsgBox.Show("Seçilen Çalışan Zaten Çalışıyor.\nLütfen Geçerli Bir Çalışan Seçin.", "Tamam");
            }
            else
            {

                if (empUpdateInfo.id == "")
                {
                    frmCustomMsgBox.Show("Lütfen İşe Almak İstediğiniz Çalışanı Seçin.", "Tamam");
                }
                else
                {
                    SqlInfo.con.Open();
                    string fireQuery = "Update PERSONEL set PERSONEL_STATUS = 'Calisiyor',PERSONEL_HIREDATE = CONVERT(DATE, GETDATE()),PERSONEL_FIREDATE = NULL, PERSONEL_SALES = 0 where PERSONEL_ID = @id";
                    SqlCommand fireCmd = new SqlCommand(fireQuery, SqlInfo.con);
                    fireCmd.Parameters.AddWithValue("@id", empUpdateInfo.id);
                    fireCmd.ExecuteNonQuery();
                    SqlInfo.con.Close();
                    gridFill();
                    frmCustomSuccessBox.Show("İşlem başarılıyla Tamamlandı!");
                }
            }
        }

        private void hireEmpPanel_MouseEnter(object sender, EventArgs e)
        {
            hireEmpPanel.BackColor = Color.FromArgb(34, 36, 49);
        }
        private void hireEmpPanel_MouseLeave(object sender, EventArgs e)
        {
            hireEmpPanel.BackColor = Color.Transparent;
        }
        private void hireEmpPanel_MouseClick(object sender, MouseEventArgs e)
        {
            new SaleManCtrl(mainEmpID).Show();
            this.Hide();
        }



        private void updateEmpPanel_MouseEnter(object sender, EventArgs e)
        {
            updateEmpPanel.BackColor = Color.FromArgb(34, 36, 49);
        }
        private void updateEmpPanel_MouseLeave(object sender, EventArgs e)
        {
            updateEmpPanel.BackColor = Color.Transparent;
        }
        private void updateEmpPanel_MouseClick(object sender, MouseEventArgs e)
        {
            new SaleManCtrl(empUpdateInfo,mainEmpID).Show();
            this.Hide();
        }
       


        private void firEmpPanel_MouseEnter(object sender, EventArgs e)
        {
            firEmpPanel.BackColor = Color.FromArgb(34, 36, 49);
        }
        private void firEmpPanel_MouseLeave(object sender, EventArgs e)
        {
            firEmpPanel.BackColor = Color.Transparent;
        }
        private void firEmpPanel_MouseClick(object sender, MouseEventArgs e)
        {
            fireEmp();
        }

        private void rehireEmpPanel_MouseEnter(object sender, EventArgs e)
        {
            rehireEmpPanel.BackColor = Color.FromArgb(34, 36, 49);
        }
        private void rehireEmpPanel_MouseLeave(object sender, EventArgs e)
        {
            rehireEmpPanel.BackColor = Color.Transparent;
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
        private void exitBtn_MouseClick(object sender, MouseEventArgs e)
        {
            Application.Exit();
        }
        
        private void backBtn_MouseEnter(object sender, EventArgs e)
        {
            backBtn.BackColor = Color.FromArgb(34, 36, 49);
        }
        private void backBtn_MouseLeave(object sender, EventArgs e)
        {
            backBtn.BackColor = Color.Transparent;
        }
        private void backBtn_MouseClick(object sender, MouseEventArgs e)
        {
            new Manager_Menu(mainEmpID).Show();
            this.Hide();
        }

        private void empGrid_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;
            DataGridViewRow row = empGrid.Rows[rowIndex];

            empUpdateInfo.name = Convert.ToString(row.Cells[1].Value);
            empUpdateInfo.pin = Convert.ToString(row.Cells[2].Value);
            empUpdateInfo.contact = Convert.ToString(row.Cells[3].Value);
            empUpdateInfo.address = Convert.ToString(row.Cells[4].Value);
            empUpdateInfo.email = Convert.ToString(row.Cells[5].Value);
            empUpdateInfo.id = Convert.ToString(row.Cells[0].Value);
            empUpdateInfo.status = Convert.ToString(row.Cells[8].Value);
        }

        private void rehireEmpPanel_MouseClick(object sender, MouseEventArgs e)
        {
            rehireEmp();
        }

   

        private void empControl_Load(object sender, EventArgs e)
        {

        }
    }
}