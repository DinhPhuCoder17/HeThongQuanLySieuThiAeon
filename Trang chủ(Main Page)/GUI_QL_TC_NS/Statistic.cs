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
using BLL;
using Bunifu.Charts.WinForms;
using Guna.UI2.WinForms;

namespace Trang_chủ_Main_Page_
{
    public partial class Statistic : Form
    {
        public Statistic()
        {
            InitializeComponent();
            selectedMonth = DateTime.Now.AddMonths(-1); // Bắt đầu từ tháng trước
            UpdateMonthLabel();
        }
        private readonly BLL_QuanlyTCNS bLL_QuanlyTCNS = new BLL_QuanlyTCNS();
        DateTime selectedMonth = DateTime.Now.AddMonths(-1); // Bắt đầu từ tháng trước
        private void Statistic_Load(object sender, EventArgs e)
        {
            cProfit.FillColors = new Guna.Charts.WinForms.ColorCollection()
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


            DateTime selectedMonth = DateTime.Now.AddMonths(-1); // Mặc định là tháng trước

            int selectedYear = selectedMonth.Year;
            int selectedMonthValue = selectedMonth.Month;

            List<string> labels = new List<string>();
            double[] expenseData = new double[4];
            DataTable dt = bLL_QuanlyTCNS.LoadDuLieuChartChi(selectedYear, selectedMonthValue);
            foreach (DataRow row in dt.Rows)
            {
                int week = Convert.ToInt32(row["WeekNumber"]);
                double expense = Convert.ToDouble(row["TotalExpense"]);

                if (week >= 1 && week <= 4)
                {
                    expenseData[week - 1] = expense;
                }
            }
            cExpense.DataPoints.Clear();

            // Cập nhật biểu đồ với dữ liệu mới
            for (int i = 0; i < 4; i++)
            {
                cExpense.DataPoints.Add(labels[i], expenseData[i]);
            }

            // Cập nhật giao diện
            c_Satistic.Update();
        }
        //List<string> labels = new List<string>();
        //double[] profitData = { 1000, 1200, 1100, 1500 };   // Lợi nhuận mỗi tuần
        //double[] revenueData = { 5000, 5200, 5100, 5500 };  // Doanh thu mỗi tuần
        //double[] expenseData = { 4000, 4000, 4000, 4000 };  // Chi phí mỗi tuần

        //double totalProfit = 0, totalRevenue = 0, totalExpense = 0;

        //// Lấy tháng từ biến selectedMonth thay vì DateTime.Now
        //int currentMonth = selectedMonth.Month;
        //int currentYear = selectedMonth.Year;

        //// Xác định số ngày trong tháng được chọn
        //int daysInMonth = DateTime.DaysInMonth(currentYear, currentMonth);

        //// Tạo nhãn cho 4 tuần
        //labels.Clear();
        //for (int i = 1; i <= 4; i++)
        //{
        //    labels.Add($"Week {i}");
        //}

        //// Xóa dữ liệu cũ trước khi thêm mới
        //cProfit.DataPoints.Clear();
        //cRevenue.DataPoints.Clear();
        //cExpense.DataPoints.Clear();

        //// Thêm dữ liệu theo từng tuần
        //for (int i = 0; i < 4; i++)
        //{
        //    cProfit.DataPoints.Add(labels[i], profitData[i]);
        //    totalProfit += profitData[i];
        //}

        //for (int i = 0; i < 4; i++)
        //{
        //    cRevenue.DataPoints.Add(labels[i], revenueData[i]);
        //    totalRevenue += revenueData[i];
        //}

        //for (int i = 0; i < 4; i++)
        //{
        //    cExpense.DataPoints.Add(labels[i], expenseData[i]);
        //    totalExpense += expenseData[i];
        //}

        //// Cập nhật biểu đồ
        //c_Satistic.Update();

        //// Cập nhật tổng giá trị
        //lblTotalProfit.Text = totalProfit.ToString();
        //lblTotalRevenue.Text = totalRevenue.ToString();
        //lblTotalExpense.Text = totalExpense.ToString();

        //// Cập nhật tiêu đề tháng
        //lbl_Month_Display.Text = $"Tháng {selectedMonth.Month}";



    



        private void guna2Button1_Click(object sender, EventArgs e)
        {

        }

        private void gunaChart1_Load(object sender, EventArgs e)
        {

        }

        private void guna2Panel10_Paint(object sender, PaintEventArgs e)
        {

        }

        private void c_Satistic_Load(object sender, EventArgs e)
        {

        }

        private void btn_NextCalendar1_Click(object sender, EventArgs e)
        {
            DateTime lastMonth = DateTime.Now.AddMonths(-1); // Tháng giới hạn cuối
            if (selectedMonth.Month <= lastMonth.Month - 1) // Chặn tiến nếu đã đến tháng trước
            {
                selectedMonth = selectedMonth.AddMonths(1);
                UpdateMonthLabel();
            }
        }

        private void btnPrevCalendar1_Click(object sender, EventArgs e)
        {
            if (selectedMonth.Month > 1) // Chặn lùi nếu đã là tháng 1
            {
                selectedMonth = selectedMonth.AddMonths(-1);
                UpdateMonthLabel();
            }
        }
        private void UpdateMonthLabel()
        {
            lbl_Month_Display.Text = $"Tháng {selectedMonth.Month}"; // Định dạng "Tháng X"
           
        }
    }
}
