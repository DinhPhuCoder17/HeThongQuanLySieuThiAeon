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
            int selectedYear = selectedMonth.Year;
            int selectedMonthValue = selectedMonth.Month;

            List<string> labels = new List<string> { "Tuần 1", "Tuần 2", "Tuần 3", "Tuần 4" }; // Đặt nhãn cố định cho các tuần
            double[] expenseData = new double[4]; // Mảng lưu trữ dữ liệu chi phí cho từng tuần

            // Giả sử bLL_QuanlyTCNS.LoadDuLieuChartChi trả về một DataTable chứa các dữ liệu chi phí
            DataTable dt = bLL_QuanlyTCNS.LoadDuLieuChartChi(selectedYear, selectedMonthValue);

            // Khởi tạo mảng chi phí là 0 (tránh giá trị mặc định NaN hoặc không xác định)
            for (int i = 0; i < 4; i++)
            {
                expenseData[i] = 0;
            }

            foreach (DataRow row in dt.Rows)
            {
                int week = Convert.ToInt32(row["WeekNumber"]);
                double expense = Convert.ToDouble(row["TotalExpense"]);

                // Cập nhật dữ liệu cho tuần tương ứng
                if (week >= 1 && week <= 4)
                {
                    expenseData[week - 1] = expense; // Cập nhật chi phí cho tuần
                }
            }

            // Xóa các điểm dữ liệu cũ trong biểu đồ
            cExpense.DataPoints.Clear();

            // Cập nhật biểu đồ với dữ liệu mới
            for (int i = 0; i < 4; i++)
            {
                cExpense.DataPoints.Add(labels[i], expenseData[i]); // Gắn nhãn cố định vào biểu đồ
                totalExpense += Convert.ToInt32(expenseData[i]); // Tính tổng chi phí
            }

            //lblTotalProfit.Text = totalProfit.ToString();
            //lblTotalRevenue.Text = totalRevenue.ToString();
            lblTotalExpense.Text = totalExpense.ToString();
            // Cập nhật giao diện
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
