using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Bunifu.Charts.WinForms;
using Guna.UI2.WinForms;

namespace Trang_chủ_Main_Page_
{
    public partial class Statistic : Form
    {
        public Statistic()
        {
            InitializeComponent();
        }


        private void Statistic_Load(object sender, EventArgs e)
        {
            cProfit.FillColors= new Guna.Charts.WinForms.ColorCollection()
{
    Color.FromArgb(80, 200, 120)  
};

            cRevenue.FillColors = new Guna.Charts.WinForms.ColorCollection()
{
    Color.FromArgb(137, 207, 240)  
};

            cExpense.FillColors = new Guna.Charts.WinForms.ColorCollection()
{
    Color.FromArgb(255, 0, 79)   
};


            List<string> labels = new List<string>();
            double[] profitData = { 1000, 1200, 1100, 1500, 1600, 1400, 1300, 1700, 1800, 1900, 2000, 2100 };
            double[] revenueData = { 5000, 5200, 5100, 5500, 5600, 5400, 5300, 5700, 5800, 5900, 6000, 6100 };
            double[] expenseData = { 4000, 4000, 4000, 4000, 4000, 4000, 4000, 4000, 4000, 4000, 4000, 4000 };

           
            double totalProfit = 0, totalRevenue = 0, totalExpense = 0;

            for (int i = 1; i <= 12; i++)
            {
                var date = new DateTime(2025, i, 1);
                string monthName = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(date.Month);
                labels.Add(monthName);
            }

          
            for (int i = 0; i < 12; i++)
            {
                cProfit.DataPoints.Add(labels[i], profitData[i]);
                totalProfit += profitData[i]; 
            }

            for (int i = 0; i < 12; i++)
            {
                cRevenue.DataPoints.Add(labels[i], revenueData[i]);
                totalRevenue += revenueData[i]; 
            }

            for (int i = 0; i < 12; i++)
            {
                cExpense.DataPoints.Add(labels[i], expenseData[i]);
                totalExpense += expenseData[i]; 
            }

            c_Satistic.Update();

           
           lblTotalProfit.Text = totalProfit.ToString();
            lblTotalRevenue.Text = totalRevenue.ToString();
            lblTotalExpense.Text = totalExpense.ToString();

   
     

        }



        private void guna2Button1_Click(object sender, EventArgs e)
        {

        }

        private void gunaChart1_Load(object sender, EventArgs e)
        {

        }

        private void guna2Panel10_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
