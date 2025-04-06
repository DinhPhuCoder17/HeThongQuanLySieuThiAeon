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
using System.Threading;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.IO;
using System.Text.RegularExpressions;
using ServiceStack;

namespace Trang_chủ_Main_Page_
{
    public partial class employeeFilterOut : Form
    {
        bool menuExpand = false;
        bool menuExpand_2=false;
        BLL_QuanlyTCNS bLL_QuanLyTCNS = new BLL_QuanlyTCNS();
        private readonly BLL_QuanlyTCNS bLL_QuanlyTCNS = new BLL_QuanlyTCNS();

        public employeeFilterOut()
        {
            InitializeComponent();
        }

        private void employeeFilterOut_Load(object sender, EventArgs e)
        {
            btn_Customer_LogBuCong.Enabled = false;
            btn_Employ_Report.Enabled = false;
            //Datagridview bảng nhân viên
            dtg_Employee.DataSource = bLL_QuanlyTCNS.xemDSNV();
            if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
            {
                dtg_Employee.Columns[0].HeaderText = "Mã nhân viên";
                dtg_Employee.Columns[1].HeaderText = "Họ tên";
                dtg_Employee.Columns[2].HeaderText = "CCCD";
                dtg_Employee.Columns[3].HeaderText = "Ngày sinh";
                dtg_Employee.Columns[4].HeaderText = "Giới tính";
                dtg_Employee.Columns[5].HeaderText = "Địa chỉ";
                dtg_Employee.Columns[6].HeaderText = "Số điện thoại";
            }
            else if (Thread.CurrentThread.CurrentUICulture.Name == "en-US")
            {
                dtg_Employee.Columns[0].HeaderText = "Employee ID";
                dtg_Employee.Columns[1].HeaderText = "Full Name";
                dtg_Employee.Columns[2].HeaderText = "Citizen ID";
                dtg_Employee.Columns[3].HeaderText = "Date of Birth";
                dtg_Employee.Columns[4].HeaderText = "Gender";
                dtg_Employee.Columns[5].HeaderText = "Address";
                dtg_Employee.Columns[6].HeaderText = "Phone Number";
            }

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

            //Datagridview bảng chấm công
            dtg_DSCC.DataSource = bLL_QuanlyTCNS.xemDSCC();
            dtg_DSCC.Columns[0].HeaderText = "ID";
            dtg_DSCC.Columns[1].HeaderText = "Thời Gian CN";
            dtg_DSCC.Columns[2].HeaderText = "CheckIn";
            dtg_DSCC.Columns[3].HeaderText = "CheckOut";
            dtg_DSCC.Columns[4].HeaderText = "Số Công";
            dtg_DSCC.Columns[5].HeaderText = "Trạng Thái";
            dtg_DSCC.Columns[6].HeaderText = "Mã Ca Làm";
            dtg_DSCC.Columns[7].HeaderText = "Mã Nhân Viên";
            logTransition_2.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (menuExpand_2 == false)
            {
                guna2Panel2.Height += 25;
                if (guna2Panel2.Height >= 900)
                {
                    logTransition_2.Stop();
                    menuExpand_2 = true;
                    btn_Customer_LogBuCong.Enabled = true;
                    btn_Employ_Report.Enabled = true;
                }
            }
            else
            {
                guna2Panel2.Height -= 25;
                if (guna2Panel2.Height <= 0)
                {
                    logTransition_2.Stop();
                    menuExpand_2 = false;
                    btn_Customer_LogBuCong.Enabled = false;
                    btn_Employ_Report.Enabled = false;
                }
            }
        }

        private void txt_Employee_SearchBar_TextChanged(object sender, EventArgs e)
        {
            if (txt_Employee_SearchBar.Text == "")
            {
                employeeFilterOut_Load(sender, e);
            }
            else
            {
                dtg_Employee.DataSource = bLL_QuanlyTCNS.timKiemNV(txt_Employee_SearchBar.Text);
            }
        }

        private void dtg_DSCC_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btn_BuCong_Send_Click_Click(object sender, EventArgs e)
        {
            if (txt_ThoiGianCapNhat.Text == "")
            {
                MessageBox.Show("Vui lòng nhập ngày làm!");
            }
           
            else if (txt_CheckIn.Text == "")
            {
                MessageBox.Show("Vui lòng nhập thời gian check In");
            }
            else if (txt_CheckOut.Text == "")
            {
                MessageBox.Show("Vui lòng nhập thời gian check Out");
            }
            else if (txt_MaNhanVien.Text == "")
            {
                MessageBox.Show("Vui lòng nhập mã nhân viên");
            }
            else
            {
                // Chuyển đổi thời gian cập nhật (DateTime)
                if (!DateTime.TryParse(txt_ThoiGianCapNhat.Text, out DateTime thoiGianCN))
                {
                    MessageBox.Show("Thời gian cập nhật không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Chuyển đổi thời gian Check-in
                if (!TimeSpan.TryParse(txt_CheckIn.Text, out TimeSpan checkIn))
                {
                    MessageBox.Show("Giờ Check-in không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Chuyển đổi thời gian Check-out
                if (!TimeSpan.TryParse(txt_CheckOut.Text, out TimeSpan checkOut))
                {
                    MessageBox.Show("Giờ Check-out không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                // Chuyển đổi chuỗi thành TimeSpan
                if (TimeSpan.TryParse(txt_CheckIn.Text, out TimeSpan checkIn1) &&
                    TimeSpan.TryParse(txt_CheckOut.Text, out TimeSpan checkOut1))
                {
                    // Kiểm tra CheckIn phải nhỏ hơn CheckOut
                    if (checkIn1 >= checkOut1)
                    {
                        MessageBox.Show("Giờ Check-in phải nhỏ hơn giờ Check-out!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Giờ Check-in hoặc Check-out không hợp lệ! Định dạng hợp lệ: HH:mm:ss", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }


                // Gọi phương thức thêm chấm công với dữ liệu đúng kiểu
                bool result = bLL_QuanlyTCNS.ThemChamCong(thoiGianCN, checkIn, checkOut, txt_MaNhanVien.Text );

                // Kiểm tra kết quả
                if (result)
                {
                    MessageBox.Show("Thêm chấm công thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Thêm chấm công thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                if (result)
                {
                    MessageBox.Show("Thêm chấm công thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    employeeFilterOut_Load(sender, e);
                    logTransition.Start();
                    txt_CheckIn.Text = "";
                    txt_CheckOut.Text = "";
                   
                    txt_MaNhanVien.Text = "";
                    txt_ThoiGianCapNhat.Text = "";
                
                }
                else
                {
                    MessageBox.Show("Thêm chấm công thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    logTransition.Start();
                    txt_CheckIn.Text = "";
                    txt_CheckOut.Text = "";
                   
                    txt_MaNhanVien.Text = "";
                    txt_ThoiGianCapNhat.Text = "";
                }
            }
        }

        private void guna2HtmlLabel3_Click(object sender, EventArgs e)
        {

        }

        private void txt_Shift_Number_TextChanged(object sender, EventArgs e)
        {

        }

        private void btn_Employ_Report_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem DataGridView có dữ liệu không
            if (dtg_DSCC.DataSource == null || ((DataTable)dtg_DSCC.DataSource).Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Mở SaveFileDialog để chọn nơi lưu file
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "PDF Files|*.pdf|All Files|*.*";
            saveFileDialog.Title = "Chọn nơi lưu báo cáo chấm công";
            saveFileDialog.FileName = "Bao_Cao_Cham_Cong_" + DateTime.Now.ToString("dd_MM_yyyy") + ".pdf"; // Tên file mặc định

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    DataTable dt = (DataTable)dtg_DSCC.DataSource;

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

                        // Tiêu đề báo cáo
                        Paragraph title = new Paragraph("BÁO CÁO CHẤM CÔNG", titleFont)
                        {
                            Alignment = Element.ALIGN_CENTER
                        };
                        doc.Add(title);

                        // Thêm ngày tạo báo cáo
                        doc.Add(Chunk.NEWLINE);
                        Paragraph dateCreated = new Paragraph($"Ngày {DateTime.Now:dd/MM/yyyy}", normalFont)
                        {
                            Alignment = Element.ALIGN_CENTER
                        };
                        doc.Add(dateCreated);

                        // Thêm một khoảng cách giữa tiêu đề và bảng
                        doc.Add(Chunk.NEWLINE);

                        // Tạo bảng dữ liệu chấm công
                        PdfPTable timesheetTable = new PdfPTable(dt.Columns.Count); // Số cột bằng số cột trong DataTable
                        timesheetTable.WidthPercentage = 100; // Đặt bảng chiếm toàn bộ chiều rộng trang

                        // Tạo từ điển ánh xạ tên cột từ cũ sang mới
                        Dictionary<string, string> columnNames = new Dictionary<string, string>()
            {
                { "ID", "ID" },
                { "ThoigianCN", "Thời Gian Cập Nhật" },
                { "Checkin", "Giờ Vào" },
                { "Checkout", "Giờ Ra" },
                { "Socong", "Số Công" },
                { "Trangthai", "Trạng Thái" },
                { "Macalam", "Mã Ca Làm" },
                { "Manhanvien", "Mã Nhân Viên" }
            };

                        // Thêm tiêu đề cột vào bảng
                        foreach (DataColumn column in dt.Columns)
                        {
                            string columnName = column.ColumnName;

                            // Kiểm tra nếu tên cột có trong từ điển ánh xạ, nếu có thì thay thế
                            if (columnNames.ContainsKey(columnName))
                            {
                                columnName = columnNames[columnName];
                            }

                            // Thêm tên cột vào bảng với tên đã thay thế
                            PdfPCell cell = new PdfPCell(new Phrase(columnName, boldFont))
                            {
                                HorizontalAlignment = Element.ALIGN_CENTER,  // Căn giữa theo chiều ngang
                                VerticalAlignment = Element.ALIGN_MIDDLE,   // Căn giữa theo chiều dọc
                                BackgroundColor = BaseColor.LIGHT_GRAY      // Tạo màu nền nhẹ cho tiêu đề cột
                            };
                            timesheetTable.AddCell(cell);
                        }

                        // Thêm dữ liệu chấm công vào bảng
                        foreach (DataRow row in dt.Rows)
                        {
                            foreach (var cell in row.ItemArray)
                            {
                                timesheetTable.AddCell(new PdfPCell(new Phrase(cell.ToString(), normalFont))
                                {
                                    HorizontalAlignment = Element.ALIGN_CENTER,  // Căn giữa theo chiều ngang
                                    VerticalAlignment = Element.ALIGN_MIDDLE   // Căn giữa theo chiều dọc
                                });
                            }
                        }

                        // Thêm bảng vào tài liệu PDF
                        doc.Add(timesheetTable);

                        // Thêm thông tin đơn vị vào cuối tài liệu
                        doc.Add(Chunk.NEWLINE);
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

                        
                        doc.Close();

                        // Thông báo thành công
                        MessageBox.Show("Báo cáo chấm công đã được xuất thành công!\n" + saveFileDialog.FileName);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xuất báo cáo: " + ex.Message);
                }
            }



        }

        // ----------- Phần của Quang ---------------------------
        bool menuExpand_3 = false;
        private void tm_InforChanges_Tick(object sender, EventArgs e)
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
        private void pb_Avatar_Click(object sender, EventArgs e)
        {
            lb_Ma.Text = Mainpage.CurrentUser.MaNhanvien;
            lb_Hoten.Text = Mainpage.CurrentUser.Hoten;
            lb_Ngaysinh.Text = Mainpage.CurrentUser.Ngaysinh;
            lb_Gioitinh.Text = Mainpage.CurrentUser.Gioitinh;
            lb_sdt.Text = Mainpage.CurrentUser.Sodienthoai; 
            tm_InforChanges.Start();
        }
        private void btn_Xacnhan_Click(object sender, EventArgs e)
        {
            string oldPassword = lb_OldPassword.Text;
            string Password = tb_mk1.Text;
            string Repassword = tb_mk2.Text;
            if (string.IsNullOrEmpty(oldPassword))
            {
                if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                {
                    MessageBox.Show("Vui lòng nhập mật khẩu cũ!", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (Thread.CurrentThread.CurrentUICulture.Name == "en-US")
                {
                    MessageBox.Show("Please enter the old password!", "NOTIFICATION", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                return;
            }
            if (string.IsNullOrEmpty(Password))
            {
                if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                {
                    MessageBox.Show("Vui lòng nhập mật khẩu mới!", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (Thread.CurrentThread.CurrentUICulture.Name == "en-US")
                {
                    MessageBox.Show("Please enter the new password!", "NOTIFICATION", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                return;
            }
            if (string.IsNullOrEmpty(Repassword))
            {
                if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                {
                    MessageBox.Show("Vui lòng nhập xác nhận mật khẩu mới!", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (Thread.CurrentThread.CurrentUICulture.Name == "en-US")
                {
                    MessageBox.Show("Please enter the new password confirmation!", "NOTIFICATION", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                return;
            }
            if (!Password.Equals(Repassword))
            {
                if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                {
                    MessageBox.Show("Mật khẩu không trùng khớp!", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }else
                {
                    MessageBox.Show("Password does not match!", "NOTIFICATION", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                return;
            }
            if (!IsValidPassword(Password))
            {
                if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                {
                    MessageBox.Show("Mật khẩu không hợp lệ! Mật khẩu phải có ít nhất 8 ký tự, bao gồm chữ hoa, chữ thường, số và ký tự đặc biệt.", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (Thread.CurrentThread.CurrentUICulture.Name == "en-US")
                {
                    MessageBox.Show("Invalid password! Password must be at least 8 characters long and contain at least one uppercase letter, one lowercase letter, one digit, and one special character.", "NOTIFICATION", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                return;
            }
            if (BLL_Nhanvien.Instance.UpdatePassword(Mainpage.CurrentUser.MaNhanvien, oldPassword, Password))
            {
                if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                {
                    MessageBox.Show("Đổi mật khẩu thành công!", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (Thread.CurrentThread.CurrentUICulture.Name == "en-US")
                {
                    MessageBox.Show("Password changed successfully!", "NOTIFICATION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                return;
            }
            else
            {
                if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                {
                    MessageBox.Show("Lỗi khi đổi mật khẩu!", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (Thread.CurrentThread.CurrentUICulture.Name == "en-US")
                {
                    MessageBox.Show("Error changing password!", "NOTIFICATION", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return;
            }
        }
        //Check Password hợp lệ
        public static bool IsValidPassword(string password)
        {
            string pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*])[A-Za-z\d!@#$%^&*]{8,}$";
            return Regex.IsMatch(password, pattern);
        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}

