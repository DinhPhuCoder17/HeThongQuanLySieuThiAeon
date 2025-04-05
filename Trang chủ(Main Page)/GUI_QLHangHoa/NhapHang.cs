using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;
using DTO;
using iTextSharp.text.pdf;
using iTextSharp.text;
using Font = iTextSharp.text.Font;
using System.Threading;
using System.Security.Policy;
using System.Globalization;
using static ClosedXML.Excel.XLPredefinedFormat;
using DocumentFormat.OpenXml.VariantTypes;
using System.Text.RegularExpressions;

namespace Trang_chủ_Main_Page_
{
    public partial class NhapHang : Form
    {
        private String soHDSelect;
        public NhapHang()
        {
            InitializeComponent();
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void guna2DataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2DataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void NhapHang_Load(object sender, EventArgs e)
        {
            DataTable dataTable = BLLQuanLyKho.Instance.xemDSNH();
            dgvNhapHang.DataSource = dataTable;
            if(Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
            {
                dgvNhapHang.Columns[0].HeaderText = "Mã đơn hàng";
                dgvNhapHang.Columns[1].HeaderText = "Thời gian đặt";
                dgvNhapHang.Columns[2].HeaderText = "Tổng tiền";
                dgvNhapHang.Columns[3].HeaderText = "Trạng Thái";
            }else
            {
                dgvNhapHang.Columns[0].HeaderText = "Order ID";
                dgvNhapHang.Columns[1].HeaderText = "Order Time";
                dgvNhapHang.Columns[2].HeaderText = "Total Price";
                dgvNhapHang.Columns[3].HeaderText = "Status";
            }

            foreach (DataGridViewColumn column in dgvNhapHang.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            foreach (DataGridViewColumn column in dgvNhapHang.Columns)
            {
                column.Resizable = DataGridViewTriState.False;
            }

            btn_HuyHD.Enabled = false;
            btn_PrintExportPDF.Enabled = false;
            btn_MoveOn.Enabled = false;

        }

        private void dgvNhapHang_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvNhapHang_CellClick(sender, e);
            if (e.RowIndex >= 0)
            {
                // Lấy giá trị cột đầu tiên (ví dụ: Mã đơn hàng)
                string maDH = dgvNhapHang.Rows[e.RowIndex].Cells[0].Value.ToString();
                String TrangThaiDHLon = dgvNhapHang.Rows[e.RowIndex].Cells[3].Value.ToString();

                // Tạo form CTDH và truyền Mã đơn hàng
                CTDH cTDH = new CTDH();
                cTDH.loadCTHDGridview(soHDSelect, TrangThaiDHLon);
                cTDH.UpdateMaDH(maDH, TrangThaiDHLon); // Gọi phương thức cập nhật trên CTDH
                cTDH.ShowDialog(); // Hiển thị form chi tiết
                dgvNhapHang.DataSource = BLLQuanLyKho.Instance.xemDSNH();
            }
        }

        private void dgvNhapHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if(dgvNhapHang.Rows[e.RowIndex].Cells[3].Value.ToString() == "Chờ Xác Nhận")
                {
                    btn_PrintExportPDF.Enabled = true;
                    btn_HuyHD.Enabled = true;
                } else
                {
                    btn_HuyHD.Enabled = false;
                    btn_PrintExportPDF.Enabled = false;
                }

                btn_MoveOn.Enabled = true;

                if (dgvNhapHang.Rows[e.RowIndex].Cells[3].Value.ToString() == "Đã Xử Lý")
                {
                    btn_MoveOn.Enabled = false;
                }

                soHDSelect = dgvNhapHang.Rows[e.RowIndex].Cells[0].Value.ToString();
            }

        }

        private void btn_HuyHD_Click(object sender, EventArgs e)
        {
            DialogResult result = DialogResult.None;

            if (Thread.CurrentThread.CurrentUICulture.Name == "en-US")
            {
                result = MessageBox.Show("Are you sure you want to cancel this order?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            }
            else if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
            {
                result = MessageBox.Show("Bạn có chắc chắn muốn hủy đơn hàng này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            }

            if (result == DialogResult.Yes)
            {
                if (BLLQuanLyKho.Instance.huyHD(soHDSelect))
                {
                    if (Thread.CurrentThread.CurrentUICulture.Name == "en-US")
                    {
                        MessageBox.Show("Order cancellation successful");
                    }
                    else if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                    {
                        MessageBox.Show("Hủy đơn hàng thành công");
                    }
                    dgvNhapHang.DataSource = BLLQuanLyKho.Instance.xemDSNH();
                }
                else
                {
                    if (Thread.CurrentThread.CurrentUICulture.Name == "en-US")
                    {
                        MessageBox.Show("Order cancellation failed");
                    }
                    else if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                    {
                        MessageBox.Show("Hủy đơn hàng thất bại");
                    }
                }
            }
        }


        private void btn_MoveOn_Click(object sender, EventArgs e)
        {
            DialogResult result=DialogResult.None;

            if (Thread.CurrentThread.CurrentUICulture.Name == "en-US")
            {
                result = MessageBox.Show("Do you want to forward the order status?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            }
            else if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
            {
                result = MessageBox.Show("Bạn có muốn chuyển tiếp trạng thái đơn hàng?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            }

            if (result == DialogResult.Yes)
            {
                if(dgvNhapHang.CurrentRow.Cells[3].Value.ToString() == "Chờ Xác Nhận")
                {
                    btn_PrintExportPDF_Click(sender, e);
                }

                DTO_HDNhapHang hDNhapHang = new DTO_HDNhapHang
                {
                    soHD = soHDSelect,
                    trangThai = dgvNhapHang.Rows[dgvNhapHang.CurrentCell.RowIndex].Cells[3].Value.ToString()
                };

                if (BLLQuanLyKho.Instance.capNhatTTDH(hDNhapHang))
                {
                    if (Thread.CurrentThread.CurrentUICulture.Name == "en-US")
                    {
                        MessageBox.Show("Order status forwarded successfully");
                    }
                    else if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                    {
                        MessageBox.Show("Chuyển tiếp trạng thái đơn hàng thành công");
                    }
                    dgvNhapHang.DataSource = BLLQuanLyKho.Instance.xemDSNH();
                }
                else
                {
                    if (Thread.CurrentThread.CurrentUICulture.Name == "en-US")
                    {
                        MessageBox.Show("Order status forwarding failed");
                    }
                    else if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                    {
                        MessageBox.Show("Chuyển tiếp trạng thái đơn hàng thất bại");
                    }
                }
            }
        }


        private void btn_PrintExportPDF_Click(object sender, EventArgs e)
        {
            if (dgvNhapHang.CurrentCell != null)
            {
                string SoHD = dgvNhapHang.CurrentRow.Cells[0].Value.ToString();
                xuatHoaDonNhapHang(SoHD);
            }
            else
            {
                if (Thread.CurrentThread.CurrentUICulture.Name == "en-US")
                {
                    MessageBox.Show("No cell is currently selected!");
                }
                else if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                {
                    MessageBox.Show("Không có ô nào đang được chọn!");
                }

            }
        }

        public static string FormatShortNumber(decimal number)
        {
            if (number >= 1_000_000_000_000)
                return (number / 1_000_000_000_000M).ToString("0.#") + "T";
            else if (number >= 1_000_000_000)
                return (number / 1_000_000_000M).ToString("0.#") + "B";
            else
                return number.ToString("0.#");
        }

        private void xuatHoaDonNhapHang(String Sohd)
        {

            DataTable dt = BLLQuanLyKho.Instance.xemDSNHvaNCC(Sohd);            
            dt.Columns["Sohd"].ColumnName = "Số hóa đơn";
            dt.Columns["Mahanghoa"].ColumnName = "Mã hàng hóa";
            dt.Columns["Tenhanghoa"].ColumnName = "Tên hàng hóa";
            dt.Columns["Soluongdat"].ColumnName = "Số lượng đặt";
            dt.Columns["Thanhtien"].ColumnName = "Thành tiền";
            dt.Columns["TenNCC"].ColumnName = "Tên nhà cung cấp";
            dt.Columns["Tiennhap"].ColumnName = "Giá";



            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "PDF Files|*.pdf|All Files|*.*";
            saveFileDialog.Title = "Chọn nơi lưu file";
            saveFileDialog.FileName = "HoaDonDatHang.pdf"; // Tên file mặc định

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
                        iTextSharp.text.Font titleFont = new Font(bf, 16, iTextSharp.text.Font.BOLD);
                        Font normalFont = new Font(bf, 12, iTextSharp.text.Font.NORMAL);
                        Font boldFont = new Font(bf, 12, iTextSharp.text.Font.BOLD);
                        Font italicFont = new Font(bf, 12, iTextSharp.text.Font.ITALIC);

                        //Title
                        Paragraph title = new Paragraph("HÓA ĐƠN ĐẶT HÀNG", titleFont);
                        title.Alignment = Element.ALIGN_CENTER;
                        doc.Add(title);

                        //Break
                        doc.Add(Chunk.NEWLINE);

                        //DateCreated
                        Paragraph dateCreate = new Paragraph(String.Format("Ngày {0} Tháng {1} Năm {2}", System.DateTime.Now.ToString("dd"), System.DateTime.Now.ToString("MM"), System.DateTime.Now.ToString("yyyy")), italicFont);
                        dateCreate.Alignment = Element.ALIGN_CENTER;
                        doc.Add(dateCreate);

                        //Break
                        doc.Add(Chunk.NEWLINE);

                        //Số hóa đơn
                        Paragraph soHD = new Paragraph();
                        soHD.Alignment = Element.ALIGN_RIGHT;
                        soHD.Add(new Chunk(String.Format("Số hóa đơn: {0}", dgvNhapHang.CurrentRow.Cells[0].Value.ToString()), boldFont));
                        soHD.Add(Chunk.NEWLINE);
// soHD.Add(new Chunk(String.Format("Ngày đặt: {0}", BLLQuanLyKho.Instance.xemNgayDatHang(dgvNhapHang.CurrentRow.Cells[0].Value.ToString()).ToString("dd/MM/yyyy")), boldFont));

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
                        PdfPTable table = new PdfPTable(6);
                        table.WidthPercentage = 100; // Bảng chiếm toàn bộ chiều rộng trang
                        foreach (DataColumn col in dt.Columns)
                        {
                            if(col.ColumnName != "Số hóa đơn")
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
                                if (col.ColumnName != "Số hóa đơn")
                                {
                                    PdfPCell cell = null;
                                    if(col.ColumnName == "Giá" || col.ColumnName == "Thành tiền")
                                    {
                                        decimal value = decimal.Parse(row[col].ToString(), new CultureInfo("vi-VN"));
                                        if (value >= 1_000_000_000)
                                        {
                                            cell = new PdfPCell(new Phrase(FormatShortNumber(value), normalFont));
                                        }else
                                        {
                                            string formatted = value.ToString("N2", new CultureInfo("vi-VN"));
                                            cell = new PdfPCell(new Phrase(formatted, normalFont));
                                        }
                                    }
                                    else
                                    {
                                        cell = new PdfPCell(new Phrase(row[col].ToString(), normalFont));
                                    }
                                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                                    cell.NoWrap = false; // Cho phép xuống dòng
                                    cell.Padding = 10f;
                                    table.AddCell(cell);
                                }
                            }
                        }

                        PdfPCell mergedCell = new PdfPCell(new Phrase("TỔNG CỘNG", boldFont));
                        mergedCell.Colspan = 4; // Gộp 4 cột
                        mergedCell.HorizontalAlignment = Element.ALIGN_CENTER; // Căn giữa
                        mergedCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                        mergedCell.BackgroundColor = BaseColor.LIGHT_GRAY;
                        mergedCell.Padding = 10f;
                        table.AddCell(mergedCell);

                        int Quantity = dt.AsEnumerable().Sum(row => row.Field<int>("Số lượng đặt"));
                        decimal Price = 0;
                        foreach(DataRow row in dt.Rows)
                        {
                            foreach (DataColumn col in dt.Columns)
                            {
                                if(col.ColumnName == "Thành tiền")
                                {
                                    Price += Convert.ToDecimal(row["Thành tiền"]);
                                }
                            }
                        }

                        string totalPriceString = "";
                        if(Price > 1_000_000_000)
                        {
                            totalPriceString = FormatShortNumber(Price);
                        }else
                        {
                            totalPriceString = Price.ToString();
                        }

                        PdfPCell totalQuantity = new PdfPCell(new Phrase(Quantity.ToString(), boldFont));
                        totalQuantity.HorizontalAlignment = Element.ALIGN_CENTER; // Căn giữa
                        totalQuantity.VerticalAlignment = Element.ALIGN_MIDDLE;
                        totalQuantity.BackgroundColor = BaseColor.LIGHT_GRAY;
                        totalQuantity.Padding = 10f;
                        table.AddCell(totalQuantity);

                        PdfPCell totalPrice = new PdfPCell(new Phrase(totalPriceString, boldFont));
                        totalPrice.HorizontalAlignment = Element.ALIGN_CENTER; // Căn giữa
                        totalPrice.VerticalAlignment = Element.ALIGN_MIDDLE;
                        totalPrice.BackgroundColor = BaseColor.LIGHT_GRAY;
                        totalPrice.Padding = 10f;
                        table.AddCell(totalPrice);

                        doc.Add(table);

                        doc.Add(Chunk.NEWLINE);

                        //Section 2
                        Paragraph section_2 = new Paragraph();
                        section_2.Alignment = Element.ALIGN_LEFT;
                        section_2.Add(new Chunk("Ghi chú:", boldFont));
                        section_2.Add(Chunk.NEWLINE);
                        section_2.Add(new Chunk("- Công nợ sẽ được thanh toán sau 30 ngày kể từ ngày đặt hàng", boldFont));
                        section_2.Add(Chunk.NEWLINE);
                        doc.Add(section_2);

                        doc.Add(Chunk.NEWLINE);

                        //Section 3
                        PdfPTable infoTable = new PdfPTable(3); // 3 cột
                        infoTable.WidthPercentage = 100;
                        infoTable.SetWidths(new float[] { 1f, 1f, 1f }); // Chia đều 3 cột

                        // Thêm ô "Người lập hóa đơn"
                        infoTable.AddCell(new PdfPCell(new Phrase("NGƯỜI LẬP HÓA ĐƠN", boldFont))
                        {
                            Border = iTextSharp.text.Rectangle.NO_BORDER,
                            HorizontalAlignment = Element.ALIGN_CENTER
                        });

                        // Thêm ô "Nhà cung cấp"
                        infoTable.AddCell(new PdfPCell(new Phrase("NHÀ CUNG CẤP", boldFont))
                        {
                            Border = iTextSharp.text.Rectangle.NO_BORDER,
                            HorizontalAlignment = Element.ALIGN_CENTER
                        });

                        // Thêm ô "Bộ phận kiểm tra"
                        infoTable.AddCell(new PdfPCell(new Phrase("BỘ PHẦN KIỂM TRA", boldFont))
                        {
                            Border = iTextSharp.text.Rectangle.NO_BORDER,
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
                    else if (Thread.CurrentThread.CurrentUICulture.Name == "en-US")
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
                    else if (Thread.CurrentThread.CurrentUICulture.Name == "en-US")
                    {
                        MessageBox.Show("Error saving file: " + ex.Message);
                    }
                }
            }

        }

        private void txt_Searching_HDNH_TextChanged(object sender, EventArgs e)
         {
             if (txt_Searching_HDNH.Text == null)
             {
                 NhapHang_Load(sender, e);
             }
             else
             {
                DataTable dataTable = BLLQuanLyKho.Instance.timKiemHDNH(txt_Searching_HDNH.Text);
                 dgvNhapHang.DataSource = dataTable;
                if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                {
                    dgvNhapHang.Columns[0].HeaderText = "Mã đơn hàng";
                    dgvNhapHang.Columns[1].HeaderText = "Thời gian đặt";
                    dgvNhapHang.Columns[2].HeaderText = "Tổng tiền";
                    dgvNhapHang.Columns[3].HeaderText = "Trạng Thái";
                }
                else
                {
                    dgvNhapHang.Columns[0].HeaderText = "Order ID";
                    dgvNhapHang.Columns[1].HeaderText = "Order Time";
                    dgvNhapHang.Columns[2].HeaderText = "Total Price";
                    dgvNhapHang.Columns[3].HeaderText = "Status";
                }

                foreach (DataGridViewColumn column in dgvNhapHang.Columns)
                 {
                     column.SortMode = DataGridViewColumnSortMode.NotSortable;
                 }

                 foreach (DataGridViewColumn column in dgvNhapHang.Columns)
                 {
                     column.Resizable = DataGridViewTriState.False;
                 }
             }
         }

        private void guna2Panel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void guna2Panel4_Paint(object sender, PaintEventArgs e)
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
