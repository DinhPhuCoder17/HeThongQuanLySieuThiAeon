using BLL;
using DTO;
using MySqlX.XDevAPI.Common;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Trang_chủ_Main_Page_
{
    public partial class ThemMatHang : Form
    {        public ThemMatHang()
        {
            List<DTO_NhaCungCap> dsNCC = BLLQuanLyKho.Instance.XemNCC();

            InitializeComponent();

            this.imgImage.Paint += new System.Windows.Forms.PaintEventHandler(this.imgImage_Paint);

            cmbTenNcc.DataSource = dsNCC;
            cmbTenNcc.DisplayMember = "TenNCC";
            cmbTenNcc.ValueMember = "MaNCC";

            lbTenNcc.Text = "NC0001";

            this.StartPosition = FormStartPosition.CenterScreen;
        }
        private void btnUpload_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "Chọn ảnh";
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string imagePath = openFileDialog.FileName;
                    // Tải ảnh lên PictureBox
                    imgImage.Image = Image.FromFile(imagePath);
                    imgImage.SizeMode = PictureBoxSizeMode.Zoom;
                    imgImage.BorderStyle = BorderStyle.FixedSingle;

                }
            }
        }
        private void cmbTenNcc_Chon(object sender, EventArgs e)
        {
            if (cmbTenNcc.SelectedValue != null)
            {

                lbTenNcc.Text = cmbTenNcc.SelectedValue.ToString();
            }
        }


        private void guna2Button1_Click(object sender, EventArgs e)
        {

        }

        private void btnThemMatHang_Click(object sender, EventArgs e)
        {
            try
            {
                string barcode = TxtBarcode.Text.Trim();
                if (!Regex.IsMatch(barcode, @"^[a-zA-Z0-9]{1,20}$"))
                {
                    if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                    {
                        MessageBox.Show("Barcode không hợp lệ! (Chỉ chứa chữ cái và số. Độ dài tối đa: 20 ký tự).");
                    }
                    else if (Thread.CurrentThread.CurrentUICulture.Name == "en-US")
                    {
                        MessageBox.Show("Invalid barcode! (Only letters and numbers are allowed. Maximum length: 20 characters).");
                    }

                    return;
                }

                // Kiểm tra Tên Hàng Hóa
                string tenHangHoa = txtTenHangHoa.Text.Trim();
                if (!Regex.IsMatch(tenHangHoa, @"^[\p{L}0-9\s\.,/-]{2,50}$"))
                {
                    if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                    {
                        MessageBox.Show("Tên hàng hóa không hợp lệ! (Chỉ chứa chữ cái, số, dấu phẩy, dấu chấm, gạch ngang, gạch chéo và khoảng trắng. Độ dài: 2-50 ký tự).");
                    }
                    else if (Thread.CurrentThread.CurrentUICulture.Name == "en-US")
                    {
                        MessageBox.Show("Invalid product name! (Only letters, numbers, commas, periods, hyphens, slashes, and spaces are allowed. Length: 2-50 characters).");
                    }

                    return;
                }

                // Kiểm tra Tên Danh Mục
                string tenDanhMuc = txtTenDanhMuc.Text.Trim();
                if (!Regex.IsMatch(tenDanhMuc, @"^[\p{L}0-9\s\.,/-]{2,50}$"))
                {
                    if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                    {
                        MessageBox.Show("Tên danh mục không hợp lệ! (Chỉ chứa chữ cái, số, dấu phẩy, dấu chấm, gạch ngang, gạch chéo và khoảng trắng. Độ dài: 2-50 ký tự).");
                    }
                    else if (Thread.CurrentThread.CurrentUICulture.Name == "en-US")
                    {
                        MessageBox.Show("Invalid category name! (Only letters, numbers, commas, periods, hyphens, slashes, and spaces are allowed. Length: 2-50 characters).");
                    }

                    return;
                }

                // Kiểm tra Giá Gốc (Chỉ chứa chữ số và chữ số thập phân)
                string giaNhapText = txtTienNhap.Text.Trim();
                if (!Regex.IsMatch(giaNhapText, @"^\d+(\.\d{1,2})?$"))
                {
                    if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                    {
                        MessageBox.Show("Giá nhập không hợp lệ! (Chỉ chứa chữ số và chữ số thập phân).");
                    }
                    else if (Thread.CurrentThread.CurrentUICulture.Name == "en-US")
                    {
                        MessageBox.Show("Invalid purchase price! (Only digits and decimal numbers are allowed).");
                    }

                    return;
                }
                float giaNhap = float.Parse(giaNhapText);

                // Kiểm tra Giá Bán (Chỉ chứa chữ số và chữ số thập phân)
                string giaBanText = txtTienBan.Text.Trim();
                if (!Regex.IsMatch(giaBanText, @"^\d+(\.\d{1,2})?$"))
                {
                    if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                    {
                        MessageBox.Show("Giá bán không hợp lệ! (Chỉ chứa chữ số và chữ số thập phân).");
                    }
                    else if (Thread.CurrentThread.CurrentUICulture.Name == "en-US")
                    {
                        MessageBox.Show("Invalid selling price! (Only digits and decimal numbers are allowed).");
                    }

                    return;
                }
                float giaBan = float.Parse(giaBanText);

                // Kiểm tra THSD (Chỉ là số)
                string thsdText = txtTHSD.Text.Trim();
                if (!int.TryParse(thsdText, out int thsd))
                {
                    if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                    {
                        MessageBox.Show("THSD không hợp lệ! (Chỉ chứa số).");
                    }
                    else if (Thread.CurrentThread.CurrentUICulture.Name == "en-US")
                    {
                        MessageBox.Show("Invalid shelf life! (Only numbers are allowed).");
                    }

                    return;
                }
                List<DTO_Hanghoa> dsTonKho = BLLQuanLyKho.Instance.XemDSTonKho();
                List<DTO_Hanghoa> Barcode_Cu = BLLQuanLyKho.Instance.xemBarcode();
                // Kiểm tra xem Barcode có tồn tại trong danh sách hàng hóa không
                bool isDuplicate = dsTonKho.Any(hh => hh.Barcode.Equals(barcode, StringComparison.OrdinalIgnoreCase));
                if (isDuplicate)
                {
                    if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                    {
                        DialogResult result = MessageBox.Show("Hàng hóa này đã tồn tại, bạn có muốn thay đổi thông tin?",
                                         "Xác nhận cập nhật",
                                         MessageBoxButtons.YesNo,
                                         MessageBoxIcon.Question);

                        if (result == DialogResult.Yes)
                        {
                            // Gọi phương thức UpdateHanghoa để cập nhật thông tin hàng hóa
                            UpdateHanghoa(tenHangHoa, tenDanhMuc, giaNhap, giaBan, thsd, barcode);
                            return;
                        }
                       
                    }
                    else if (Thread.CurrentThread.CurrentUICulture.Name == "en-US")
                    {
                        DialogResult result = MessageBox.Show("This item already exists. Do you want to update the information?",
                                         "Confirm update",
                                         MessageBoxButtons.YesNo,
                                         MessageBoxIcon.Question);

                        if (result == DialogResult.Yes)
                        {
                            // Gọi phương thức UpdateHanghoa để cập nhật thông tin hàng hóa

                            UpdateHanghoa(tenHangHoa, tenDanhMuc, giaNhap, giaBan, thsd, barcode);
                            return;
                        }
                    }

                    return;
                }
                else if (Barcode_Cu.Any(hh => hh.Barcode.Equals(barcode, StringComparison.OrdinalIgnoreCase)))
                {
                    string message, title;

                    if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                    {
                        message = "Hàng hóa này đã bị xóa trước đó. Bạn có muốn thêm lại hàng hóa không?";
                        title = "Xác nhận khôi phục";
                    }
                    else // Mặc định tiếng Anh
                    {
                        message = "This product was previously deleted. Do you want to restore it?";
                        title = "Restore Confirmation";
                    }

                    DialogResult result = MessageBox.Show(message,
                                                          title,
                                                          MessageBoxButtons.YesNo,
                                                          MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        KhoiPhucHangHoa(barcode);
                        this.Close();
                        return;
                    }
                    else
                    {
                        string _message, _title;

                        if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                        {
                            _message = "Không thể khôi phục hàng hóa. Vui lòng thử lại.";
                            _title = "Lỗi";
                        }
                        else // Mặc định tiếng Anh
                        {
                            _message = "Unable to restore the product. Please try again.";
                            _title = "Error";
                        }

                        MessageBox.Show(_message,
                                        _title,
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Error);

                    }

                }
               
                // Tạo đối tượng DTO_Hanghoa
                DTO_Hanghoa hangHoa = new DTO_Hanghoa
                {
                    TenHangHoa = tenHangHoa,
                    DanhMuc = tenDanhMuc,
                    THSD = thsd,
                    GiaNhap = giaNhap,
                    GiaBan = giaBan,
                    NhaCC = lbTenNcc.Text.Trim(),
                    Barcode = barcode
                };

                // Kiểm tra hình ảnh
                if (imgImage.Image != null)
                {
                    try
                    {
                        using (MemoryStream ms = new MemoryStream())
                        {
                            imgImage.Image.Save(ms, imgImage.Image.RawFormat);
                            hangHoa.HinhAnh = ms.ToArray();
                        }


                    }
                    catch (Exception ex)
                    {
                        // Thông báo lỗi khi có vấn đề khi lưu hình ảnh
                        if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                        {
                            MessageBox.Show("Có lỗi khi lưu hình ảnh: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else if (Thread.CurrentThread.CurrentUICulture.Name == "en-US")
                        {
                            MessageBox.Show("Error saving image: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    // Thông báo khi không có hình ảnh
                    if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                    {
                        MessageBox.Show("Vui lòng chọn hình ảnh để lưu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (Thread.CurrentThread.CurrentUICulture.Name == "en-US")
                    {
                        MessageBox.Show("Please select an image to save!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }


                // Thêm mặt hàng
                bool kq = BLLQuanLyKho.Instance.ThemMatHang(hangHoa);
                if (kq)
                {
                    if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                    {
                        MessageBox.Show("Thêm mặt hàng thành công!");
                        this.Close();
                    }
                    else if (Thread.CurrentThread.CurrentUICulture.Name == "en-US")
                    {
                        MessageBox.Show("Product added successfully!");
                        this.Close();
                    }

                }
                else
                {
                    if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                    {
                        MessageBox.Show("Thêm mặt hàng thất bại!");
                    }
                    else if (Thread.CurrentThread.CurrentUICulture.Name == "en-US")
                    {
                        MessageBox.Show("Adding product failed!");
                    }

                }
            }
            catch (Exception ex)
            {
                if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
                else if (Thread.CurrentThread.CurrentUICulture.Name == "en-US")
                {
                    MessageBox.Show("Error: " + ex.Message);
                }

            }
        }
        public void KhoiPhucHangHoa(string barcode)
        {
            // Giả sử phương thức này gọi đến BLL để khôi phục hàng hóa theo barcode
            bool success = BLLQuanLyKho.Instance.KhoiPhucHangHoa(barcode);
            if (success)
            {
                string message, title;

                if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                {
                    message = "Hàng hóa với Barcode " + barcode + " đã được khôi phục thành công!";
                    title = "Thông báo";
                }
                else // Mặc định tiếng Anh
                {
                    message = "The product with Barcode " + barcode + " has been successfully restored!";
                    title = "Notification";
                }

                MessageBox.Show(message,
                                title,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

            }
            else
            {
                string message, title;

                if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                {
                    message = "Không thể khôi phục hàng hóa với Barcode " + barcode + ". Vui lòng thử lại sau.";
                    title = "Thông báo lỗi";
                }
                else // Mặc định tiếng Anh
                {
                    message = "Unable to restore the product with Barcode " + barcode + ". Please try again later.";
                    title = "Error Notification";
                }

                MessageBox.Show(message,
                                title,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);

            }
        }
        public void UpdateHanghoa(string tenHangHoa, string tenDanhMuc, float giaNhapText, float giaBanText, int thsd, string barcode)
        {
            // Chuyển đổi giá nhập và giá bán từ string sang float
          

            // Tạo đối tượng DTO_Hanghoa
            DTO_Hanghoa hangHoa = new DTO_Hanghoa
            {
                TenHangHoa = tenHangHoa,
                DanhMuc = tenDanhMuc,
                THSD = thsd,
                GiaNhap = giaNhapText,
                GiaBan = giaBanText,
                NhaCC = lbTenNcc.Text.Trim(),
                Barcode = barcode
            };

            // Kiểm tra hình ảnh
            if (imgImage.Image != null)
            {
                try
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        imgImage.Image.Save(ms, imgImage.Image.RawFormat);
                        hangHoa.HinhAnh = ms.ToArray(); // Lưu hình ảnh vào thuộc tính HinhAnh
                    }
                }
                catch (Exception ex)
                {
                    // Thông báo lỗi khi có vấn đề khi lưu hình ảnh
                    if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                    {
                        MessageBox.Show("Có lỗi khi lưu hình ảnh: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (Thread.CurrentThread.CurrentUICulture.Name == "en-US")
                    {
                        MessageBox.Show("Error saving image: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    return;
                }
            }
            else
            {
                // Thông báo khi không có hình ảnh
                if(Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                    {
                    MessageBox.Show("Vui lòng chọn hình ảnh để lưu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                    else if (Thread.CurrentThread.CurrentUICulture.Name == "en-US")
                {
                    MessageBox.Show("Please select an image to save!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                return;
            }

            // Thêm mặt hàng vào cơ sở dữ liệu
            bool kq = BLLQuanLyKho.Instance.UpdateHanghoa(hangHoa);
            if (kq)
            {
                if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                {
                    MessageBox.Show("Thêm mặt hàng thành công!");
                    this.Close();
                }
                else if (Thread.CurrentThread.CurrentUICulture.Name == "en-US")
                {
                    MessageBox.Show("Product added successfully!");
                    this.Close();
                }
                this.Close(); // Đóng form sau khi thêm thành công
            }
            else
            {
                if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                {
                    MessageBox.Show("Thêm mặt hàng thất bại!");
                }
                else if (Thread.CurrentThread.CurrentUICulture.Name == "en-US")
                {
                    MessageBox.Show("Adding product failed!");
                }
            }
        }


        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void imgImage_Paint(object sender, PaintEventArgs e)
        {
            int radius = 1; // Bán kính bo tròn cho 4 góc là 5
            GraphicsPath gp = new GraphicsPath();

            ControlPaint.DrawBorder(e.Graphics, imgImage.ClientRectangle, Color.Black, ButtonBorderStyle.Solid);

            // Góc trên trái
            gp.AddArc(0, 0, radius * 2, radius * 2, 180, 90);
            // Góc trên phải
            gp.AddArc(imgImage.Width - radius * 2, 0, radius * 2, radius * 2, 270, 90);
            // Góc dưới phải
            gp.AddArc(imgImage.Width - radius * 2, imgImage.Height - radius * 2, radius * 2, radius * 2, 0, 90);
            // Góc dưới trái
            gp.AddArc(0, imgImage.Height - radius * 2, radius * 2, radius * 2, 90, 90);

            gp.CloseFigure(); // Đóng đường dẫn thành một hình liên tục
            imgImage.Region = new Region(gp); // Áp dụng vùng bo tròn cho PictureBox
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void txtTHSD_TextChanged(object sender, EventArgs e)
        {

        }

        private void ThemMatHang_Load(object sender, EventArgs e)
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


