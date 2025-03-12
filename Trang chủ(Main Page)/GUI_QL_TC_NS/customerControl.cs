using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace Trang_chủ_Main_Page_
{
    public partial class customerControl : Form
    {
        bool menu_CusTomer_Add_Expand=false;
        public customerControl()
        {
            InitializeComponent();
        }

        private void customerControl_Load(object sender, EventArgs e)
        {
            dtg_CustomerList.Rows.Add();
            dtg_CustomerList.Rows[0].Cells[0].Value = "Jan 21, 2020";
            dtg_CustomerList.Rows[0].Cells[1].Value = "Dilan Cooper";
            dtg_CustomerList.Rows[0].Cells[2].Value = "Dilan Cooper";
            dtg_CustomerList.Rows[0].Cells[3].Value = "(239)555-2020";

            dtg_CustomerList.Rows.Add();
            dtg_CustomerList.Rows[1].Cells[0].Value = "Jan 21, 2020";
            dtg_CustomerList.Rows[1].Cells[1].Value = "Dilan Cooper";
            dtg_CustomerList.Rows[1].Cells[2].Value = "Dilan Cooper";
            dtg_CustomerList.Rows[1].Cells[3].Value = "(239)555-2020";
            // Đảm bảo DataGridView có ít nhất 12 dòng
            while (dtg_CustomerList.Rows.Count < 12)
            {
                dtg_CustomerList.Rows.Add();
            }

            // Dữ liệu mẫu cho 12 dòng
            string[,] data = new string[,]
            {
    { "Jan 21, 2020", "Dilan Cooper", "Dilan Cooper", "(239)555-2020" },
    { "Jan 22, 2020", "Sarah Johnson", "Sarah Johnson", "(312)555-3030" },
    { "Jan 23, 2020", "Michael Brown", "Michael Brown", "(415)555-4040" },
    { "Jan 24, 2020", "Emily Davis", "Emily Davis", "(617)555-5050" },
    { "Jan 25, 2020", "James Wilson", "James Wilson", "(718)555-6060" },
    { "Jan 26, 2020", "Sophia Martinez", "Sophia Martinez", "(213)555-7070" },
    { "Jan 27, 2020", "David Clark", "David Clark", "(305)555-8080" },
    { "Jan 28, 2020", "Olivia White", "Olivia White", "(702)555-9090" },
    { "Jan 29, 2020", "William Harris", "William Harris", "(312)555-1010" },
    { "Jan 30, 2020", "Ava Thompson", "Ava Thompson", "(415)555-2020" },
    { "Jan 31, 2020", "Ethan Walker", "Ethan Walker", "(404)555-3030" },
    { "Feb 1, 2020", "Mia Robinson", "Mia Robinson", "(503)555-4040" }
            };

            // Gán dữ liệu vào từng dòng của DataGridView
            for (int i = 0; i < 12; i++)
            {
                for (int j = 0; j < 4; j++) // 4 cột
                {
                    dtg_CustomerList.Rows[i].Cells[j].Value = data[i, j];
                }
            }

        }

        private void dtgCustomerList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void btnCustomerAdd_Click(object sender, EventArgs e)
        {
            Timer_Customer_Add.Start();
        }

        private void logTransition_Tick(object sender, EventArgs e)
        {

        }

        private void Timer_Customer_Add_Tick(object sender, EventArgs e)
        {
            if (menu_CusTomer_Add_Expand == false)
            {
                menu_CusTomer_Add.Height += 10;
                if (menu_CusTomer_Add.Height >= 270)
                {
                    Timer_Customer_Add.Stop();
                    menu_CusTomer_Add_Expand = true;
                }
            }
            else
            {
                menu_CusTomer_Add.Height -= 10;
                if (menu_CusTomer_Add.Height <= 0)
                {
                    Timer_Customer_Add.Stop();
                    menu_CusTomer_Add_Expand = false;
                }
            }
        }

        private void menu_CusTomer_Add_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnCustomerFilterOut_Click(object sender, EventArgs e)
        {

        }
    }
}
