using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Trang_chủ_Main_Page_
{
    public partial class employeeFilterOut : Form
    {
        bool menuExpand = false;
        bool menuExpand_2=false;    

        public employeeFilterOut()
        {
            InitializeComponent();
        }

        private void employeeFilterOut_Load(object sender, EventArgs e)
        {
            dtg_Employee.Rows.Add();
            dtg_Employee.Rows[0].Cells[0].Value = "Jan 21, 2020";
            dtg_Employee.Rows[0].Cells[1].Value = "Dilan Cooper";
            dtg_Employee.Rows[0].Cells[2].Value = "Dilan Cooper";
            dtg_Employee.Rows[0].Cells[3].Value = "(239)555-2020";
            dtg_Employee.Rows[0].Cells[4].Value = "Chicago";
            dtg_Employee.Rows[0].Cells[5].Value = "Jan 21, 2020 - 13:30";
            dtg_Employee.Rows[0].Cells[6].Value = "Jan 21, 2020";
            dtg_Employee.Rows[0].Cells[7].Value = "Jan 21, 2020";
            dtg_Employee.Rows.Add();
            dtg_Employee.Rows[1].Cells[0].Value = "Jan 21, 2020";
            dtg_Employee.Rows[1].Cells[1].Value = "Dilan Cooper";
            dtg_Employee.Rows[1].Cells[2].Value = "Dilan Cooper";
            dtg_Employee.Rows[1].Cells[3].Value = "(239)555-2020";
            dtg_Employee.Rows[1].Cells[4].Value = "Chicago";
            dtg_Employee.Rows[1].Cells[5].Value = "Jan 21, 2020 - 13:30";
            dtg_Employee.Rows[1].Cells[6].Value = "Jan 21, 2020";
            dtg_Employee.Rows[1].Cells[7].Value = "Jan 21, 2020";
            // Đảm bảo DataGridView có ít nhất 12 dòng
            while (dtg_Employee.Rows.Count < 12)
            {
                dtg_Employee.Rows.Add();
            }

            // Dữ liệu mẫu
            string[,] data = new string[,]
            {
    { "Jan 21, 2020", "Dilan Cooper", "Dilan Cooper", "(239)555-2020", "Chicago", "Jan 21, 2020 - 13:30", "Jan 21, 2020", "Jan 21, 2020" },
    { "Jan 22, 2020", "Sarah Johnson", "Sarah Johnson", "(312)555-3030", "New York", "Jan 22, 2020 - 14:00", "Jan 22, 2020", "Jan 22, 2020" },
    { "Jan 23, 2020", "Michael Brown", "Michael Brown", "(415)555-4040", "Los Angeles", "Jan 23, 2020 - 15:15", "Jan 23, 2020", "Jan 23, 2020" },
    { "Jan 24, 2020", "Emily Davis", "Emily Davis", "(617)555-5050", "Boston", "Jan 24, 2020 - 10:45", "Jan 24, 2020", "Jan 24, 2020" },
    { "Jan 25, 2020", "James Wilson", "James Wilson", "(718)555-6060", "Brooklyn", "Jan 25, 2020 - 09:30", "Jan 25, 2020", "Jan 25, 2020" },
    { "Jan 26, 2020", "Sophia Martinez", "Sophia Martinez", "(213)555-7070", "Los Angeles", "Jan 26, 2020 - 11:20", "Jan 26, 2020", "Jan 26, 2020" },
    { "Jan 27, 2020", "David Clark", "David Clark", "(305)555-8080", "Miami", "Jan 27, 2020 - 12:55", "Jan 27, 2020", "Jan 27, 2020" },
    { "Jan 28, 2020", "Olivia White", "Olivia White", "(702)555-9090", "Las Vegas", "Jan 28, 2020 - 14:40", "Jan 28, 2020", "Jan 28, 2020" },
    { "Jan 29, 2020", "William Harris", "William Harris", "(312)555-1010", "Chicago", "Jan 29, 2020 - 16:05", "Jan 29, 2020", "Jan 29, 2020" },
    { "Jan 30, 2020", "Ava Thompson", "Ava Thompson", "(415)555-2020", "San Francisco", "Jan 30, 2020 - 17:25", "Jan 30, 2020", "Jan 30, 2020" },
    { "Jan 31, 2020", "Ethan Walker", "Ethan Walker", "(404)555-3030", "Atlanta", "Jan 31, 2020 - 18:10", "Jan 31, 2020", "Jan 31, 2020" },
    { "Feb 1, 2020", "Mia Robinson", "Mia Robinson", "(503)555-4040", "Portland", "Feb 1, 2020 - 19:45", "Feb 1, 2020", "Feb 1, 2020" }
            };

            // Gán dữ liệu vào từng dòng
            for (int i = 0; i < 12; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    dtg_Employee.Rows[i].Cells[j].Value = data[i, j];
                }
            }

        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void employeeFilterOut_BindingContextChanged(object sender, EventArgs e)
        {

        }

        private void employeeFilterOut_SizeChanged(object sender, EventArgs e)
        {
          
        }

        private void menuContainer_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
          logTransition.Start();
        }

        private void menuContainer_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void logTransition_Tick(object sender, EventArgs e)
        {
            if (menuExpand == false)
            {
                menuContainer.Height += 10;
                if (menuContainer.Height >= 290)
                {
                    logTransition.Stop();
                    menuExpand = true;
                }
            }
            else
            {
                menuContainer.Height -= 10;
                if (menuContainer.Height <= 0)
                {
                    logTransition.Stop();
                    menuExpand = false;
                }
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            logTransition.Start();
        }

        private void menuContainer_Paint_2(object sender, PaintEventArgs e)
        {

        }

        private void guna2TextBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            logTransition_2.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (menuExpand_2 == false)
            {
                guna2Panel2.Height += 25;
                if (guna2Panel2.Height >= 600)
                {
                    logTransition_2.Stop();
                    menuExpand_2 = true;
                }
            }
            else
            {
                guna2Panel2.Height -= 25;
                if (guna2Panel2.Height <= 0)
                {
                    logTransition_2.Stop();
                    menuExpand_2 = false;
                }
            }
        }
    }
}

