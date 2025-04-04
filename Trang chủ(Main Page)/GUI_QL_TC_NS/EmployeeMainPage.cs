using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls.Adapters;
using System.Windows.Forms;
using BLL;
using OfficeOpenXml;
using System.Threading;
using System.Drawing; 

namespace Trang_chủ_Main_Page_
{
    public partial class EmployeeMainPage : Form
    {
        employeeFilterOut employeeFilterOut;
        employeeShift employeeShift;
        customerControl customerControl;
        financialManagement financialManagement;
        Statistic statistic;

        public EmployeeMainPage()
        {
           
            InitializeComponent();
            if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
            {
                btnEmployeeAdd.ImageOffset = new Point(-7, 0);
                btnEmployeeAdd.TextOffset = new Point(5, 0);

                btnEmployeeShift.ImageOffset = new Point(-13, 0);
                btnEmployeeShift.TextOffset = new Point(-1, 0);

                btnCustomer.ImageOffset = new Point(0, 0);
                btnCustomer.TextOffset = new Point(-1, 0);

                btn_FinancialManagement.ImageOffset = new Point(-2, 0);
                btn_FinancialManagement.TextOffset = new Point(-15, 0);

                btn_Statistic.ImageOffset = new Point(0, 0);
                btn_Statistic.TextOffset = new Point(0, 0);

                btn_AddData.ImageOffset = new Point(0, 0);
                btn_AddData.TextOffset = new Point(1, 0);

                btn_AddData_ChamCong.ImageOffset = new Point(20, 0);
                btn_AddData_ChamCong.TextOffset = new Point(1, 0);

                btn_AddData_banHang.ImageOffset = new Point(20, 0);
                btn_AddData_banHang.TextOffset = new Point(1, 0);

                btn_SignOut.ImageOffset = new Point(7, 0);
                btn_SignOut.TextOffset = new Point(1, 0);
            }
        }
        bool menuExpand = false;
        private void EmployeeMainPage_Load(object sender, EventArgs e)
        {
            if (employeeFilterOut == null)
            {
                employeeFilterOut = new employeeFilterOut();
                employeeFilterOut.FormClosed += employeeAdd_FormClose;
                employeeFilterOut.MdiParent = this;
                employeeFilterOut.Dock = DockStyle.Fill;
                employeeFilterOut.Show();
            }
            else
            {
                employeeFilterOut.Activate();
            }
        }

     

        private void btnEmployeeAdd_Click(object sender, EventArgs e)
        {
            if (employeeFilterOut == null)
            {
                employeeFilterOut = new employeeFilterOut();
                employeeFilterOut.FormClosed += employeeAdd_FormClose;
                employeeFilterOut.MdiParent = this;
                employeeFilterOut.Dock = DockStyle.Fill;
                employeeFilterOut.Show();
            }
            else
            {
                employeeFilterOut.Activate();
            }
        }
        private void employeeAdd_FormClose(object sender, FormClosedEventArgs e)
        {
            employeeShift = null;   
            customerControl = null;
            employeeFilterOut=null;
            financialManagement = null;
        }

      
        private void btnEmployeeShift_Click(object sender, EventArgs e)
        {
           
                employeeShift = new employeeShift();
                employeeShift.FormClosed += employeeAdd_FormClose;
                employeeShift.MdiParent = this;
                employeeShift.Dock = DockStyle.Fill;
                employeeShift.Show();
            
           
        }


        private void btnCustomer_Click(object sender, EventArgs e)
        {
            if (customerControl == null)
            {
                customerControl = new customerControl();
                customerControl.FormClosed += employeeAdd_FormClose;
                customerControl.MdiParent = this;
                customerControl.Dock = DockStyle.Fill;
                customerControl.Show();
            }
            else
            {
                customerControl.Activate();
            }
        }

        private void btn_FinancialManagement_Click(object sender, EventArgs e)
        {
            if (financialManagement == null)
            {
                financialManagement = new financialManagement();
                financialManagement.FormClosed += employeeAdd_FormClose;
                financialManagement.MdiParent = this;
                financialManagement.Dock = DockStyle.Fill;
                financialManagement.Show();
            }
            else
            {
                financialManagement.Activate();
            }
        }

      

        private void btn_Statistic_Click(object sender, EventArgs e)
        {
          
                statistic = new Statistic();
                statistic.FormClosed += employeeAdd_FormClose;
                statistic.MdiParent = this;
                statistic.Dock = DockStyle.Fill;
                statistic.Show();
            
          
        }



        private void btn_AddData_banHang_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Excel Files|*.xls;*.xlsx;",
                Title = "Chọn file Excel"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                BLL_QuanlyTCNS bLL_QuanLyTCNS = new BLL_QuanlyTCNS();

                bool result = bLL_QuanLyTCNS.ImportHoaDonFromExcel(filePath);

                if (result)
                {
                    MessageBox.Show("Import dữ liệu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    financialManagement = new financialManagement();
                    financialManagement.FormClosed += employeeAdd_FormClose;
                    financialManagement.MdiParent = this;
                    financialManagement.Dock = DockStyle.Fill;
                    financialManagement.Show();
                }
                else
                {
                    MessageBox.Show("Import dữ liệu thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
        }

        private void btn_AddData_ChamCong_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Excel Files|*.xls;*.xlsx;",
                Title = "Chọn file Excel"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                BLL_QuanlyTCNS bLL_QuanLyTCNS = new BLL_QuanlyTCNS();

                bool result = bLL_QuanLyTCNS.ImportChamCongFromExcel(filePath);

                if (result)
                    MessageBox.Show("Import dữ liệu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("Import dữ liệu thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void btn_AddData_Click(object sender, EventArgs e)
        {
            logTransition.Start();
        }

        private void btn_SignOut_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
            "Bạn có chắc chắn muốn đăng xuất?",
            "Xác nhận đăng xuất",
            MessageBoxButtons.OKCancel,
            MessageBoxIcon.Question
            );

            if (result == DialogResult.OK)
            {
                this.Hide();
                Mainpage mainpage = new Mainpage();
                mainpage.Show();

                this.Close();
            }
        }

        private void logTransition_Tick(object sender, EventArgs e)
        {
            if (menuExpand == false)
            {
                pn_Menu_Financial.Height += 15;
                if (pn_Menu_Financial.Height >= 150)
                {
                    logTransition.Stop();
                    menuExpand = true;
                }
            }
            else
            {
                pn_Menu_Financial.Height -= 15;
                if (pn_Menu_Financial.Height <= 0)
                {
                    logTransition.Stop();
                    menuExpand = false;
                }
            }
        }
    }
}
