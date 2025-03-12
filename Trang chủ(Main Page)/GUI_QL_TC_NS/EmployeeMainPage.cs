using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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
        bool menuEmployeeExpand=false;
        bool sidebarExpand = true;
        bool menuCustomerExpand=false;
        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {

        }

        private void guna2CustomGradientPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {


        }

        private void guna2CustomGradientPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void menuTrainsition_Tick(object sender, EventArgs e)
        {
          
        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
              
        }

        private void sidebarTransition_Tick(object sender, EventArgs e)
        {
          
        }

        private void guna2PictureBox2_Click(object sender, EventArgs e)
        {
            
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

        private void sidebarContainer_Paint(object sender, PaintEventArgs e)
        {

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

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2VSeparator1_Click(object sender, EventArgs e)
        {
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

        private void guna2Button5_Click(object sender, EventArgs e)
        {
     
        }

     

       

        private void menuCustomerTransiton_Tick(object sender, EventArgs e)
        {
           
        }

        private void menuEmployeeTransition_Tick(object sender, EventArgs e)
        {
           
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

        private void guna2Separator5_Click(object sender, EventArgs e)
        {

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
    }
}
