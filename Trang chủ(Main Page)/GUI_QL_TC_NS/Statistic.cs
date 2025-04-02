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

            LoadData();

        }




        public void LoadData()
        {
            UpdateMonthLabel();
            int totalExpense = 0;
            int totalRevenue = 0;
            int totalProfit = 0; // Tổng lợi nhuận

            int selectedYear = selectedMonth.Year;
            int selectedMonthValue = selectedMonth.Month;

            List<string> labels = new List<string> { "Tuần 1", "Tuần 2", "Tuần 3", "Tuần 4" };

            double[] expenseData = new double[4];
            double[] revenueData = new double[4];
            double[] profitData = new double[4]; // Mảng lưu lợi nhuận

            DataTable dt = bLL_QuanlyTCNS.LoadDuLieuChartChi(selectedYear, selectedMonthValue);
            DataTable dt1 = bLL_QuanlyTCNS.LoadDuLieuChartThu(selectedYear, selectedMonthValue);

            // Khởi tạo mảng dữ liệu với giá trị 0
            for (int i = 0; i < 4; i++)
            {
                expenseData[i] = 0;
                revenueData[i] = 0;
                profitData[i] = 0;
            }

            // Xóa dữ liệu cũ trước khi cập nhật mới
            cRevenue.DataPoints.Clear();
            cExpense.DataPoints.Clear();
            cProfit.DataPoints.Clear();
            // Lấy dữ liệu doanh thu
            foreach (DataRow row in dt1.Rows)
            {
                int week = Convert.ToInt32(row["WeekNumber"]);
                double revenue = Convert.ToDouble(row["TotalRevenue"]);
                if (week >= 1 && week <= 4)
                {
                    revenueData[week - 1] = revenue;
                }
            }

            // Lấy dữ liệu chi phí
            foreach (DataRow row in dt.Rows)
            {
                int week = Convert.ToInt32(row["WeekNumber"]);
                double expense = Convert.ToDouble(row["TotalExpense"]);
                if (week >= 1 && week <= 4)
                {
                    expenseData[week - 1] = expense;
                }
            }

            // Tính lợi nhuận = Doanh thu - Chi phí
            for (int i = 0; i < 4; i++)
            {
                profitData[i] = revenueData[i] - expenseData[i]; // Lợi nhuận của từng tuần
                totalRevenue += Convert.ToInt32(revenueData[i]);
                totalExpense += Convert.ToInt32(expenseData[i]);
                totalProfit += Convert.ToInt32(profitData[i]); // Tính tổng lợi nhuận
            }

            // Cập nhật dữ liệu lên biểu đồ
            for (int i = 0; i < 4; i++)
            {
                cRevenue.DataPoints.Add(labels[i], revenueData[i]);
                cExpense.DataPoints.Add(labels[i], expenseData[i]);
                cProfit.DataPoints.Add(labels[i], profitData[i]); // Thêm cột lợi nhuận
            }

            // Cập nhật tổng giá trị vào giao diện
            lblTotalRevenue.Text = totalRevenue.ToString();
            lblTotalExpense.Text = totalExpense.ToString();
            lblTotalProfit.Text = totalProfit.ToString(); // Hiển thị tổng lợi nhuận

            // Cập nhật giao diện biểu đồ
            c_Satistic.Update();
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
                LoadData();
            }
        }

        private void btnPrevCalendar1_Click(object sender, EventArgs e)
        {
            if (selectedMonth.Month > 1) // Chặn lùi nếu đã là tháng 1
            {
                selectedMonth = selectedMonth.AddMonths(-1);
                UpdateMonthLabel();
                LoadData(); 
            }
        }
        private void UpdateMonthLabel()
        {
            lbl_Month_Display.Text = $"Tháng {selectedMonth.Month}"; // Định dạng "Tháng X"
           
        }
    }
}
