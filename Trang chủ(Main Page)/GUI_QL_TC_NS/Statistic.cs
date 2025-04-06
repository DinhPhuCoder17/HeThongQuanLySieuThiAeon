using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Media3D;
using BLL;
using Bunifu.Charts.WinForms;
using Guna.UI2.WinForms;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.PowerBI.Api.Models;
using Microsoft.PowerBI.Api;
using System.Text.RegularExpressions;

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
            LoadPieChartData();
            LoadData();

        }
 
        public void LoadPieChartData()
        {
            UpdateMonthLabel(); // Cập nhật nhãn tháng
            

            // Lấy năm và tháng hiện tại
            int selectedYear = selectedMonth.Year;
            int selectedMonthValue = selectedMonth.Month;

            // Lấy dữ liệu từ database
            DataTable dt = bLL_QuanlyTCNS.LoadDuLieuPieChart(selectedYear, selectedMonthValue);
            DataTable dt1 = bLL_QuanlyTCNS.LoadDuLieuPieChart1(selectedYear, selectedMonthValue);
            // Xóa dữ liệu cũ của PieChart
            c_No1.DataPoints.Clear();
            c_BestSeller.Datasets.Clear(); // Chỉ giữ lại 1 dataset duy nhất
            c_MostPayment.Datasets.Clear();
            c_No2.DataPoints.Clear();
            // Thêm dữ liệu từ DataTable vào PieChart
            foreach (DataRow row in dt1.Rows)
            {
                string tenSanPham = row["Tenhanghoa"].ToString();
                int tongSoLuong = Convert.ToInt32(row["TongSoluong"]); // Lấy tổng số lượng đã bán
                c_No2.DataPoints.Add(tenSanPham, tongSoLuong); // Thêm vào dataset duy nhất
            }
            foreach (DataRow row in dt.Rows)
            {
                string tenSanPham = row["Tenhanghoa"].ToString();
                int tongSoLuong = Convert.ToInt32(row["TongSoluong"]); // Lấy tổng số lượng đã bán

                c_No1.DataPoints.Add(tenSanPham, tongSoLuong); // Thêm vào dataset duy nhất
            }
            c_MostPayment.Datasets.Add(c_No2); // Thêm dataset duy nhất vào PieChart
            // Thêm dataset duy nhất vào PieChart
            c_BestSeller.Datasets.Add(c_No1);

            // Cập nhật biểu đồ để hiển thị dữ liệu mới
            c_BestSeller.Update();
            c_MostPayment.Update();
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
            // Thay thế các dòng hiển thị giá trị cũ bằng:
            lblTotalRevenue.Text = totalRevenue.ToString("N0", CultureInfo.InvariantCulture);
            lblTotalExpense.Text = totalExpense.ToString("N0", CultureInfo.InvariantCulture);
            lblTotalProfit.Text = totalProfit.ToString("N0", CultureInfo.InvariantCulture); 

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
                LoadPieChartData();
                LoadData();
            }
        }

        private void btnPrevCalendar1_Click(object sender, EventArgs e)
        {
            if (selectedMonth.Month > 1) // Chặn lùi nếu đã là tháng 1
            {
                selectedMonth = selectedMonth.AddMonths(-1);
                UpdateMonthLabel();
                LoadPieChartData();
                LoadData(); 
            }
        }
        private void UpdateMonthLabel()
        {
            lbl_Month_Display.Text = $"Tháng {selectedMonth.Month}"; // Định dạng "Tháng X"
           
        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void c_BestSeller_Load(object sender, EventArgs e)
        {

        }

        private void lblTotalRevenue_Click(object sender, EventArgs e)
        {

        }




        private void btn_Statistic_Print_Click(object sender, EventArgs e)
        {
            // Dữ liệu tài chính từ giao diện
            int totalRevenue = 0;
            int totalExpense = 0;
            int totalProfit = 0;

            int selectedYear = selectedMonth.Year;
            int selectedMonthValue = selectedMonth.Month;

            // Lấy dữ liệu từ các phương thức đã có
            DataTable dt = bLL_QuanlyTCNS.LoadDuLieuChartChi(selectedYear, selectedMonthValue);
            DataTable dt1 = bLL_QuanlyTCNS.LoadDuLieuChartThu(selectedYear, selectedMonthValue);
            DataTable dtPieChart = bLL_QuanlyTCNS.LoadDuLieuPieChart(selectedYear, selectedMonthValue);
            DataTable dtPieChart1 = bLL_QuanlyTCNS.LoadDuLieuPieChart1(selectedYear, selectedMonthValue);

            // Tính toán tổng doanh thu, chi phí và lợi nhuận
            double[] expenseData = new double[4];
            double[] revenueData = new double[4];
            double[] profitData = new double[4];

            for (int i = 0; i < 4; i++)
            {
                expenseData[i] = 0;
                revenueData[i] = 0;
                profitData[i] = 0;
            }

            foreach (DataRow row in dt1.Rows)
            {
                int week = Convert.ToInt32(row["WeekNumber"]);
                double revenue = Convert.ToDouble(row["TotalRevenue"]);
                if (week >= 1 && week <= 4)
                {
                    revenueData[week - 1] = revenue;
                }
            }

            foreach (DataRow row in dt.Rows)
            {
                int week = Convert.ToInt32(row["WeekNumber"]);
                double expense = Convert.ToDouble(row["TotalExpense"]);
                if (week >= 1 && week <= 4)
                {
                    expenseData[week - 1] = expense;
                }
            }

            // Tính lợi nhuận cho từng tuần và tổng lợi nhuận
            for (int i = 0; i < 4; i++)
            {
                profitData[i] = revenueData[i] - expenseData[i];
                totalRevenue += Convert.ToInt32(revenueData[i]);
                totalExpense += Convert.ToInt32(expenseData[i]);
                totalProfit += Convert.ToInt32(profitData[i]);
            }

            // Lấy dữ liệu các sản phẩm bán chạy nhất và bán ít nhất
            var bestSellingProducts = dtPieChart.AsEnumerable()
                .OrderByDescending(row => row.Field<int>("TongSoluong"))
                .Take(5) // Lấy top 5 sản phẩm bán chạy nhất
                .ToList();

            var leastSellingProducts = dtPieChart1.AsEnumerable()
                .OrderBy(row => row.Field<int>("TongSoluong"))
                .Take(5) // Lấy top 5 sản phẩm bán ít nhất
                .ToList();

            // Tạo SaveFileDialog để lưu báo cáo
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "PDF Files|*.pdf|All Files|*.*";
            saveFileDialog.Title = "Chọn nơi lưu báo cáo tài chính";
            saveFileDialog.FileName = "Báo_Cáo_Tài_Chính_" + selectedMonth.Month + "_" + selectedYear + ".pdf"; // Tên file mặc định

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Tạo file PDF
                    using (FileStream fs = new FileStream(saveFileDialog.FileName, FileMode.Create))
                    {
                        Document doc = new Document(PageSize.A4);
                        PdfWriter writer = PdfWriter.GetInstance(doc, fs);
                        doc.Open();

                        // Đường dẫn font Times New Roman
                        string fontPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "times.ttf");

                        if (!File.Exists(fontPath))
                        {
                            MessageBox.Show("Không tìm thấy phông Times New Roman! Vui lòng kiểm tra.");
                            return;
                        }

                        // Nhúng font vào tài liệu
                        BaseFont bf = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                        iTextSharp.text.Font titleFont = new iTextSharp.text.Font(bf, 16, iTextSharp.text.Font.BOLD);
                        iTextSharp.text.Font normalFont = new iTextSharp.text.Font(bf, 12, iTextSharp.text.Font.NORMAL);
                        iTextSharp.text.Font boldFont = new iTextSharp.text.Font(bf, 12, iTextSharp.text.Font.BOLD);
                        iTextSharp.text.Font italicFont = new iTextSharp.text.Font(bf, 12, iTextSharp.text.Font.ITALIC);

                        // Tiêu đề báo cáo
                        Paragraph title = new Paragraph("BÁO CÁO TÀI CHÍNH THÁNG " + selectedMonth.Month, titleFont)
                        {
                            Alignment = Element.ALIGN_CENTER
                        };
                        doc.Add(title);

                        // Thêm ngày tạo báo cáo
                        doc.Add(Chunk.NEWLINE);
                        Paragraph dateCreated = new Paragraph($"Ngày {DateTime.Now:dd/MM/yyyy}", italicFont)
                        {
                            Alignment = Element.ALIGN_CENTER
                        };
                        doc.Add(dateCreated);

                        // Thêm thông tin tổng quan tài chính
                        doc.Add(Chunk.NEWLINE);
                        doc.Add(new Paragraph($"Tổng Doanh Thu: {totalRevenue:N0} VND", normalFont));
                        doc.Add(new Paragraph($"Tổng Chi Phí: {totalExpense:N0} VND", normalFont));
                        doc.Add(new Paragraph($"Tổng Lợi Nhuận: {totalProfit:N0} VND", boldFont));

                        // Bảng doanh thu, chi phí, lợi nhuận từng tuần
                        doc.Add(Chunk.NEWLINE);
                        Paragraph weeklyReport = new Paragraph("Doanh Thu, Chi Phí, và Lợi Nhuận Theo Tuần", boldFont);
                        doc.Add(weeklyReport);

                        // Tạo bảng cho doanh thu, chi phí, lợi nhuận từng tuần
                        PdfPTable weeklyTable = new PdfPTable(4);
                        weeklyTable.WidthPercentage = 100;
                        weeklyTable.AddCell(new PdfPCell(new Phrase("Tuần", boldFont)));
                        weeklyTable.AddCell(new PdfPCell(new Phrase("Doanh Thu (VND)", boldFont)));
                        weeklyTable.AddCell(new PdfPCell(new Phrase("Chi Phí (VND)", boldFont)));
                        weeklyTable.AddCell(new PdfPCell(new Phrase("Lợi Nhuận (VND)", boldFont)));

                        // Thêm dữ liệu từng tuần vào bảng
                        for (int i = 0; i < 4; i++)
                        {
                            weeklyTable.AddCell(new PdfPCell(new Phrase("Tuần " + (i + 1), normalFont)));
                            weeklyTable.AddCell(new PdfPCell(new Phrase(revenueData[i].ToString("N0"), normalFont)));
                            weeklyTable.AddCell(new PdfPCell(new Phrase(expenseData[i].ToString("N0"), normalFont)));
                            weeklyTable.AddCell(new PdfPCell(new Phrase(profitData[i].ToString("N0"), normalFont)));
                        }

                        doc.Add(weeklyTable);

                        // Thêm thông tin chi tiết các sản phẩm bán chạy nhất
                        doc.Add(Chunk.NEWLINE);
                        Paragraph bestSelling = new Paragraph("Sản Phẩm Bán Chạy Nhất", boldFont);
                        doc.Add(bestSelling);
                        PdfPTable bestSellingTable = new PdfPTable(2);
                        bestSellingTable.WidthPercentage = 100;
                        bestSellingTable.AddCell(new PdfPCell(new Phrase("Tên Sản Phẩm", boldFont)));
                        bestSellingTable.AddCell(new PdfPCell(new Phrase("Tổng Số Lượng", boldFont)));

                        foreach (var row in bestSellingProducts)
                        {
                            bestSellingTable.AddCell(new PdfPCell(new Phrase(row["Tenhanghoa"].ToString(), normalFont)));
                            bestSellingTable.AddCell(new PdfPCell(new Phrase(row["TongSoluong"].ToString(), normalFont)));
                        }
                        doc.Add(bestSellingTable);

                        // Thêm thông tin chi tiết các sản phẩm bán ít nhất
                        doc.Add(Chunk.NEWLINE);
                        Paragraph leastSelling = new Paragraph("Sản Phẩm Bán Ít Nhất", boldFont);
                        doc.Add(leastSelling);
                        PdfPTable leastSellingTable = new PdfPTable(2);
                        leastSellingTable.WidthPercentage = 100;
                        leastSellingTable.AddCell(new PdfPCell(new Phrase("Tên Sản Phẩm", boldFont)));
                        leastSellingTable.AddCell(new PdfPCell(new Phrase("Tổng Số Lượng", boldFont)));

                        foreach (var row in leastSellingProducts)
                        {
                            leastSellingTable.AddCell(new PdfPCell(new Phrase(row["Tenhanghoa"].ToString(), normalFont)));
                            leastSellingTable.AddCell(new PdfPCell(new Phrase(row["TongSoluong"].ToString(), normalFont)));
                        }
                        doc.Add(leastSellingTable);
                        Paragraph section_1 = new Paragraph();
                        section_1.Alignment = Element.ALIGN_LEFT;

                        section_1.Add(new Chunk("Đơn vị: ", boldFont));
                        section_1.Add(new Chunk("Công ty Trách nhiệm Hữu Hạn AEON Việt Nam", normalFont));
                        section_1.Add(Chunk.NEWLINE);
                        section_1.Add(new Chunk("Địa chỉ: ", boldFont));
                        section_1.Add(new Chunk("243 Chu Văn An, P. 12, Q. Bình Thạnh, TP. HCM.", normalFont));
                        section_1.Add(Chunk.NEWLINE);
                        section_1.Add(new Chunk("Số điện thoại: ", boldFont));
                        section_1.Add(new Chunk("0366-565454", normalFont));
                        section_1.Add(Chunk.NEWLINE);
                        section_1.Add(new Chunk("Mã số thuế: ", boldFont));
                        section_1.Add(new Chunk("0311241512", normalFont));

                        // Thêm phần thông tin đơn vị vào cuối
                        doc.Add(section_1);
                        // Đóng tài liệu PDF
                        doc.Close();

                        // Thông báo thành công
                        MessageBox.Show("Báo cáo tài chính đã được tạo thành công!\n" + saveFileDialog.FileName);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi lưu báo cáo: " + ex.Message);
                }
            }
        }

        private void guna2Panel8_Paint(object sender, PaintEventArgs e)
        {

        }
        private void pn_infoChanges_Paint(object sender, PaintEventArgs e)
        {

        }

        // ---------------- Phần của Quang ----------------------
        bool menuExpand_3 = false;
        
        //Check Password hợp lệ
        public static bool IsValidPassword(string password)
        {
            string pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*])[A-Za-z\d!@#$%^&*]{8,}$";
            return Regex.IsMatch(password, pattern);
        }


        private void pb_Avata_Click_1(object sender, EventArgs e)
        {
            tm_InforChanges.Start();
            lb_Ma.Text = Mainpage.CurrentUser.MaNhanvien;
            lb_Hoten.Text = Mainpage.CurrentUser.Hoten;
            lb_Ngaysinh.Text = Mainpage.CurrentUser.Ngaysinh;
            lb_Gioitinh.Text = Mainpage.CurrentUser.Gioitinh;
            lb_sdt.Text = Mainpage.CurrentUser.Sodienthoai;
        }

        private void tm_InforChanges_Tick_1(object sender, EventArgs e)
        {
            if (menuExpand_3 == false)
            {
                pn_infoChanges.Height += 20;
                if (pn_infoChanges.Height >= 280)
                {
                    tm_InforChanges.Stop();
                    menuExpand_3 = true;
                }
            }
            else
            {
                pn_infoChanges.Height -= 20;
                if (pn_infoChanges.Height <= 0)
                {
                    tm_InforChanges.Stop();
                    menuExpand_3 = false;
                }
            }
        }
    }
}

