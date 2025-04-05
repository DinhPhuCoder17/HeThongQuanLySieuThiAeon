using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using static Jenga.Theme;
using DTO;
using BLL;
using Utilities.BunifuCheckBox.Transitions;
using iTextSharp.text.pdf;
using System.Windows.Media;
using iTextSharp.text;
using System.IO;
using Font = iTextSharp.text.Font;
using ServiceStack.Text.Common;
using System.Web.UI.WebControls;
using Rectangle = iTextSharp.text.Rectangle;
using System.Threading;
using System.Text.RegularExpressions;
using Trang_chủ_Main_Page_;


namespace Trang_chu_Main_Page_.GUI_QLHangHoa
{
    public partial class KhieuNai : Form
    {
        public KhieuNai()
        {
            InitializeComponent();
        }

        //Hàm truyền dữ liệu DataGridView từ form chính
        public void giveDataGridView(System.Data.DataTable dataTable)
        {
            dgv_KhieuNai.DataSource = dataTable;

            if(Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
            {
                dgv_KhieuNai.Columns["Sohd"].HeaderText = "Số hóa đơn";
                dgv_KhieuNai.Columns["Mahanghoa"].HeaderText = "Mã hàng hóa";
                dgv_KhieuNai.Columns["Tenhanghoa"].HeaderText = "Tên hàng hóa";
                dgv_KhieuNai.Columns["Ngaynhap"].HeaderText = "Ngày nhập";
                dgv_KhieuNai.Columns["Soluongdat"].HeaderText = "Số lượng đặt";
                dgv_KhieuNai.Columns["Soluongnhan"].HeaderText = "Số lượng nhận";
                dgv_KhieuNai.Columns["Lydochitiet"].HeaderText = "Lý do chi tiết";
                dgv_KhieuNai.Columns["Luongchenhlech"].HeaderText = "Lượng chênh lệch";
                dgv_KhieuNai.Columns["Yeucauxuly"].HeaderText = "Yêu cầu xử lý";
            }else
            {
                dgv_KhieuNai.Columns["Sohd"].HeaderText = "Invoice number";
                dgv_KhieuNai.Columns["Mahanghoa"].HeaderText = "Product code";
                dgv_KhieuNai.Columns["Tenhanghoa"].HeaderText = "Product name";
                dgv_KhieuNai.Columns["Ngaynhap"].HeaderText = "Date of import";
                dgv_KhieuNai.Columns["Soluongdat"].HeaderText = "Quantity ordered";
                dgv_KhieuNai.Columns["Soluongnhan"].HeaderText = "Quantity received";
                dgv_KhieuNai.Columns["Lydochitiet"].HeaderText = "Detailed reason";
                dgv_KhieuNai.Columns["Luongchenhlech"].HeaderText = "Difference amount";
                dgv_KhieuNai.Columns["Yeucauxuly"].HeaderText = "Request for processing";
            }
            
            dgv_KhieuNai.Columns["LoaiKhieuNai"].Visible = false;

            // Tạo cột ComboBox cho trường hợp nhận < đặt
            DataGridViewComboBoxColumn lessReceive = new DataGridViewComboBoxColumn();
            if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
            {
                lessReceive.HeaderText = "Loại khiếu nại"; // Tiêu đề cột
            }
            else
            {
                lessReceive.HeaderText = "Type of complaint"; // Tiêu đề cột
            }
            lessReceive.Name = "LoaiKhieuNaiView"; // Tên cột

            if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
            {
                lessReceive.DataSource = new string[] { "Sai hàng", "Thiếu hàng", "Hàng lỗi", "Dư hàng" }; // Danh sách lựa chọn
            }
            else
            {
                lessReceive.DataSource = new string[] { "Wrong Item", "Missing Item", "Damaged Item", "Extra Item" }; // Danh sách lựa chọn
            }

            lessReceive.AutoComplete = true; // Cho phép tự động điền
           
            // Thêm cột vào DataGridView
            dgv_KhieuNai.Columns.Add(lessReceive);
            foreach (DataGridViewRow row in dgv_KhieuNai.Rows)
            {
                // Điền dữ liệu vào cột ComboBox
                row.Cells["LoaiKhieuNaiView"].Value = row.Cells["LoaiKhieuNai"].Value;
                if (row.Cells["Luongchenhlech"].Value.ToString() == "")
                {
                    row.Cells["Luongchenhlech"].Value = int.Parse(row.Cells["Soluongnhan"].Value.ToString()) - int.Parse(row.Cells["Soluongdat"].Value.ToString());
                }
            }

            foreach (DataGridViewColumn col in dgv_KhieuNai.Columns)
            {
                if (col.Name != "LoaiKhieuNaiView" && col.Name != "Lydochitiet" && col.Name != "Luongchenhlech" && col.Name != "Yeucauxuly")
                {
                    col.ReadOnly = true;
                }

            }
        }

        private bool kiemTraOTrong()
        {
            foreach (DataGridViewRow row in dgv_KhieuNai.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.Value == null)
                    {
                        return false;
                    }else if(cell.Value.ToString() == "")
                    {
                        return false;
                    }

                }
            }
            return true;
        }

        private void btnTaoPhieuBoSung_Click(object sender, EventArgs e)
        {
            if (kiemTraOTrong())
            {
                foreach (DataGridViewRow row in dgv_KhieuNai.Rows)
                {
                    String loaiKhieuNai = "";
                    switch(row.Cells[0].Value.ToString())
                    {
                        case "Wrong Item":
                            loaiKhieuNai = "Sai hàng";
                            break;
                        case "Missing Item":
                            loaiKhieuNai = "Thiếu hàng";
                            break;
                        case "Damaged Item":
                            loaiKhieuNai = "Hàng lỗi";
                            break;
                        case "Extra Item":
                            loaiKhieuNai = "Dư hàng";
                            break;
                    }

                    DTO_Khieunai kn = new DTO_Khieunai()
                    {
                        SoHD = row.Cells[1].Value.ToString(),
                        MaHH = row.Cells[2].Value.ToString(),
                        Luongchenhlech = int.Parse(row.Cells[7].Value.ToString()),
                        Loaikhieunai = loaiKhieuNai,
                        Lydochitiet = row.Cells[9].Value.ToString(),
                        Yeucauxuly = row.Cells[10].Value.ToString(),
                    };

                    BLLQuanLyKho.Instance.themKN(kn);

                }

                xuatHoaDonKhieuNai();
            }
            else
            {
                if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                {
                    MessageBox.Show("Vui lòng điền đầy đủ thông tin!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show("Please fill in all the information!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void xuatHoaDonKhieuNai()
        {
            
            DataTable dt = BLLQuanLyKho.Instance.xemDSKNvaNCC(dgv_KhieuNai.Rows[0].Cells[1].Value.ToString());
            var rowsToDelete = dt.Select("Soluongnhan = Soluongdat"); // Chọn các dòng cần xóa
            foreach (var row in rowsToDelete)
            {
                dt.Rows.Remove(row); // Xóa từng dòng
            }
            dt.Columns["Mahanghoa"].ColumnName = "Mã hàng hóa";
            dt.Columns["Tenhanghoa"].ColumnName = "Tên hàng hóa";
            dt.Columns["Soluongnhan"].ColumnName = "Số lượng nhận";
            dt.Columns["Soluongdat"].ColumnName = "Số lượng đặt";
            dt.Columns["Luongchenhlech"].ColumnName = "Lượng chênh lệch";
            dt.Columns["Loaikhieunai"].ColumnName = "Loại khiếu nại";
            dt.Columns["Lydochitiet"].ColumnName = "Lý do chi tiết";
            dt.Columns["Ngaynhap"].ColumnName = "Ngày nhập";
            dt.Columns["TenNCC"].ColumnName = "Tên nhà cung cấp";


            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "PDF Files|*.pdf|All Files|*.*";
            saveFileDialog.Title = "Chọn nơi lưu file";
            saveFileDialog.FileName = "HoaDonKhieuNai.pdf"; // Tên file mặc định

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (FileStream fs = new FileStream(saveFileDialog.FileName, FileMode.Create))
                    {
                        Document doc = new Document(PageSize.A4.Rotate(), 30, 30, 20, 20);
                        PdfWriter writer = PdfWriter.GetInstance(doc, fs);
                        doc.Open();

                        //Lấy đường dẫn FOnt Time New Roman
                        string fontPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "times.ttf");

                        if (!File.Exists(fontPath))
                        {
                            MessageBox.Show("Không tìm thấy phông Times New Roman! Vui lòng kiểm tra.");
                            return;
                        }

                        // ✅ Đúng cách để nhúng font Unicode
                        BaseFont bf = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                        Font titleFont = new Font(bf, 16, iTextSharp.text.Font.BOLD);
                        Font normalFont = new Font(bf, 12, iTextSharp.text.Font.NORMAL);
                        Font boldFont = new Font(bf, 12, iTextSharp.text.Font.BOLD);
                        Font italicFont = new Font(bf, 12, iTextSharp.text.Font.ITALIC);

                        //Title
                        Paragraph title = new Paragraph("HÓA ĐƠN KHIẾU NẠI", titleFont);
                        title.Alignment = Element.ALIGN_CENTER;
                        doc.Add(title);

                        //Break
                        doc.Add(Chunk.NEWLINE);

                        //DateCreated
                        Paragraph dateCreate = new Paragraph(String.Format("Ngày {0} Tháng {1} Năm {2}", DateTime.Now.ToString("dd"), DateTime.Now.ToString("MM"), DateTime.Now.ToString("yyyy")), italicFont);
                        dateCreate.Alignment = Element.ALIGN_CENTER;
                        doc.Add(dateCreate);

                        //Break
                        doc.Add(Chunk.NEWLINE);

                        //Số hóa đơn
                        Paragraph soHD = new Paragraph();
                        soHD.Alignment = Element.ALIGN_RIGHT;
                        soHD.Add(new Chunk(String.Format("Số hóa đơn: {0}", dgv_KhieuNai.Rows[0].Cells[1].Value.ToString()), boldFont));
                        soHD.Add(Chunk.NEWLINE);
// soHD.Add(new Chunk(String.Format("Ngày đặt: {0}", BLLQuanLyKho.Instance.xemNgayDatHang(dgv_KhieuNai.Rows[0].Cells[1].Value.ToString()).ToString("dd/MM/yyyy")), boldFont));

                        doc.Add(soHD);
                        
                        //Break
                        doc.Add(Chunk.NEWLINE);

                        //Section 1
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
                        doc.Add(section_1);

                        //Break
                        doc.Add(Chunk.NEWLINE);


                        //Table
                        PdfPTable table = new PdfPTable(9);
                        table.WidthPercentage = 100; // Bảng chiếm toàn bộ chiều rộng trang
                        foreach (DataColumn col in dt.Columns)
                        {
                            if (col.ColumnName != "Sohd" && col.ColumnName != "Yeucauxuly")
                            {
                                PdfPCell cell = new PdfPCell(new Phrase(col.ColumnName, boldFont));
                                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                                cell.BackgroundColor = BaseColor.LIGHT_GRAY; // Đổi màu nền tiêu đề
                                cell.NoWrap = false; // Cho phép xuống dòng
                                cell.Padding = 10f;
                                table.AddCell(cell);

                            }
                        }

                        foreach (DataRow row in dt.Rows)
                        {
                            foreach (DataColumn col in dt.Columns)
                            {
                                if (col.ColumnName != "Sohd" && col.ColumnName != "Yeucauxuly")
                                {
                                    PdfPCell cell;
                                    if (col.ColumnName == "Ngày nhập")
                                    {
                                        DateTime ngayNhap = DateTime.Parse(row[col].ToString());
                                        cell = new PdfPCell(new Phrase(ngayNhap.ToString("dd/MM/yyyy"), normalFont));
                                    }else
                                    {
                                        cell = new PdfPCell(new Phrase(row[col].ToString(), normalFont));
                                    }

                                    if(col.ColumnName == "Lý do chi tiết")
                                    {
                                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                                        cell.VerticalAlignment = Element.ALIGN_LEFT;
                                    }
                                    else
                                    {
                                        cell.HorizontalAlignment = Element.ALIGN_CENTER;
                                        cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                                    }

                                    
                                    cell.NoWrap = false; // Cho phép xuống dòng
                                    cell.Padding = 10f;
                                    table.AddCell(cell);
                                }
                            }
                        }

                        doc.Add(table);

                        doc.Add(Chunk.NEWLINE);

                        //Section 2
                        Paragraph section_2 = new Paragraph();
                        section_2.Alignment = Element.ALIGN_LEFT;
                        section_2.Add(new Chunk("Yêu cầu xử lý:", titleFont));
                        section_2.Add(Chunk.NEWLINE);
                        foreach(DataRow row in dt.Rows)
                        {
                            section_2.Add(new Chunk(row["Mã hàng hóa"].ToString() + ": ", normalFont));
                            section_2.Add(new Chunk(row[10].ToString(), normalFont));
                            section_2.Add(Chunk.NEWLINE);
                        }
                        doc.Add(section_2);

                        doc.Add(Chunk.NEWLINE);

                        //Section 3
                        PdfPTable infoTable = new PdfPTable(3); // 3 cột
                        infoTable.WidthPercentage = 100;
                        infoTable.SetWidths(new float[] { 1f, 1f, 1f }); // Chia đều 3 cột

                        // Thêm ô "Người lập hóa đơn"
                        infoTable.AddCell(new PdfPCell(new Phrase("NGƯỜI LẬP HÓA ĐƠN", boldFont))
                        {
                            Border = Rectangle.NO_BORDER,
                            HorizontalAlignment = Element.ALIGN_CENTER
                        });

                        // Thêm ô "Nhà cung cấp"
                        infoTable.AddCell(new PdfPCell(new Phrase("NHÀ CUNG CẤP", boldFont))
                        {
                            Border = Rectangle.NO_BORDER,
                            HorizontalAlignment = Element.ALIGN_CENTER
                        });

                        // Thêm ô "Bộ phận kiểm tra"
                        infoTable.AddCell(new PdfPCell(new Phrase("BỘ PHẦN KIỂM TRA", boldFont))
                        {
                            Border = Rectangle.NO_BORDER,
                            HorizontalAlignment = Element.ALIGN_CENTER
                        });

                        // Thêm bảng vào tài liệu
                        doc.Add(infoTable);

                        doc.Close();
                    }
                    if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                    {
                        MessageBox.Show("File đã được lưu thành công: " + saveFileDialog.FileName);
                    }
                    else
                    {
                        MessageBox.Show("File has been saved successfully: " + saveFileDialog.FileName);
                    }
                }
                catch (Exception ex)
                {
                    if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                    {
                        MessageBox.Show("Lỗi khi lưu file: " + ex.Message);
                    }
                    else
                    {
                        MessageBox.Show("Error saving file: " + ex.Message);
                    }
                }
            }

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgv_KhieuNai_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            string input = e.FormattedValue.ToString().Trim(); // Lấy giá trị người dùng nhập vào

            // ✅ Nếu ô trống, không cần kiểm tra gì thêm
            if (string.IsNullOrEmpty(input))
            {
                return;
            }


            if (e.ColumnIndex == 7)
            {
                // Lấy giá trị người dùng vừa nhập
                string newValue = e.FormattedValue.ToString();

                // Kiểm tra nếu không phải số nguyên
                if (!int.TryParse(newValue, out _))
                {
                    if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                    {
                        MessageBox.Show("Vui lòng nhập một số nguyên hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        MessageBox.Show("Please enter a valid integer!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    e.Cancel = true; // Hủy bỏ thay đổi
                }
            }

            if (dgv_KhieuNai.Columns[e.ColumnIndex] is DataGridViewComboBoxColumn)
            {
                // Kiểm tra nếu giá trị ô đang nhập là null hoặc rỗng
                if (string.IsNullOrWhiteSpace(e.FormattedValue?.ToString()))
                {
                    if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                    {
                        MessageBox.Show("Vui lòng chọn một giá trị!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        MessageBox.Show("Please select a value!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    e.Cancel = true; // Hủy bỏ thay đổi nếu không hợp lệ
                }
            }
        }

        private void dgv_KhieuNai_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // Chặn xuống dòng
            }
        }

        private void KhieuNai_Load(object sender, EventArgs e)
        {

        }

        // ---------------- Phần của Quang ----------------------
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
        private void pb_Avata_Click(object sender, EventArgs e)
        {
            tm_InforChanges.Start();
            lb_Ma.Text = Mainpage.CurrentUser.MaNhanvien;
            lb_Hoten.Text = Mainpage.CurrentUser.Hoten;
            lb_Ngaysinh.Text = Mainpage.CurrentUser.Ngaysinh;
            lb_Gioitinh.Text = Mainpage.CurrentUser.Gioitinh;
            lb_sdt.Text = Mainpage.CurrentUser.Sodienthoai;
        }

        private void btn_Xacnhan_Click(object sender, EventArgs e)
        {
            string mk1 = tb_mk1.Text;
            string mk2 = tb_mk2.Text;
            if (string.IsNullOrEmpty(mk1))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu mới!", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(mk2))
            {
                MessageBox.Show("Vui lòng nhập xác nhận mật khẩu mới!", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!mk1.Equals(mk2))
            {
                MessageBox.Show("Mật khẩu không trùng khớp!", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!IsValidPassword(mk1))
            {
                MessageBox.Show("Mật khẩu không hợp lệ!", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (BLL_Nhanvien.Instance.UpdatePassword(Mainpage.CurrentUser.MaNhanvien, mk1))
            {
                MessageBox.Show("Đổi mật khẩu thành công!", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Lỗi khi đổi mật khẩu!", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
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