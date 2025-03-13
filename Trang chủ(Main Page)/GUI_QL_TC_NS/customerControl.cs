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
using BLL;

namespace Trang_chủ_Main_Page_
{
    public partial class customerControl : Form
    {
        bool menu_CusTomer_Add_Expand=false;
        BLL_Customer bllCustomer = new BLL_Customer();
        public customerControl()
        {
            InitializeComponent();
        }

        private void customerControl_Load(object sender, EventArgs e)
        {
            dtg_CustomerList.DataSource = bllCustomer.loadCustomer();
            dtg_CustomerList.Columns["Sodienthoai"].HeaderText = "Số điện thoại";
            dtg_CustomerList.Columns["Hoten"].HeaderText = "Họ tên";
            dtg_CustomerList.Columns["Diachi"].HeaderText = "Địa chỉ";
            dtg_CustomerList.Columns["Diemthuong"].HeaderText = "Điểm thưởng";
            dtg_CustomerList.Columns["Gioitinh"].HeaderText = "Giới tính";
            dtg_CustomerList.Columns["Hang"].HeaderText = "Hạng";
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
