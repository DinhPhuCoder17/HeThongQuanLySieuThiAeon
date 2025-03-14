using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using BLL;
using static Jenga.Theme;

namespace Trang_chủ_Main_Page_
{
    public partial class employeeFilterOut : Form
    {
        bool menuExpand = false;
        bool menuExpand_2=false;

        private readonly BLL_QuanlyTCNS bLL_QuanlyTCNS = new BLL_QuanlyTCNS();

        public employeeFilterOut()
        {
            InitializeComponent();
        }

        private void employeeFilterOut_Load(object sender, EventArgs e)
        {
            dtg_Employee.DataSource = bLL_QuanlyTCNS.xemDSNV();
            dtg_Employee.Columns[0].HeaderText = "Mã nhân viên";
            dtg_Employee.Columns[1].HeaderText = "Họ tên";
            dtg_Employee.Columns[2].HeaderText = "CCCD";
            dtg_Employee.Columns[3].HeaderText = "Ngày sinh";
            dtg_Employee.Columns[4].HeaderText = "Giới tính";
            dtg_Employee.Columns[5].HeaderText = "Địa chỉ";
            dtg_Employee.Columns[6].HeaderText = "Số điện thoại";

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

