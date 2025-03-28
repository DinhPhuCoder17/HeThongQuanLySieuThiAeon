﻿using System;
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
using Microsoft.Office.Interop.Excel;
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
        }

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
            if (employeeShift == null)
            {
                employeeShift = new employeeShift();
                employeeShift.FormClosed += employeeAdd_FormClose;
                employeeShift.MdiParent = this;
                employeeShift.Dock = DockStyle.Fill;
                employeeShift.Show();
            }
            else
            {
                employeeShift.Activate();
            }
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
            if (statistic == null)
            {
                statistic = new Statistic();
                statistic.FormClosed += employeeAdd_FormClose;
                statistic.MdiParent = this;
                statistic.Dock = DockStyle.Fill;
                statistic.Show();
            }
            else
            {
                statistic.Activate();
            }
        }

      

        private void btn_AddData_banHang_Click(object sender, EventArgs e)
        {
            
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

        }
    }
}
