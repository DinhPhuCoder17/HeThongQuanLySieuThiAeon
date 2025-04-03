using BLL;
using DTO;
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
                // Kiểm tra Tên Hàng Hóa
                string tenHangHoa = txtTenHangHoa.Text.Trim();
                if (!Regex.IsMatch(tenHangHoa, @"^[a-zA-Z0-9\s\.,/-]{2,20}$"))
                {
                    if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                    {
                        MessageBox.Show("Tên hàng hóa không hợp lệ! (Chỉ chứa chữ cái, số, dấu phẩy, dấu chấm, gạch ngang, gạch chéo và khoảng trắng. Độ dài: 2-20 ký tự).");
                    }
                    else if (Thread.CurrentThread.CurrentUICulture.Name == "en-US")
                    {
                        MessageBox.Show("Invalid product name! (Only letters, numbers, commas, periods, hyphens, slashes, and spaces are allowed. Length: 2-20 characters).");
                    }

                    return;
                }

                // Kiểm tra Tên Danh Mục
                string tenDanhMuc = txtTenDanhMuc.Text.Trim();
                if (!Regex.IsMatch(tenDanhMuc, @"^[a-zA-Z0-9\s\.,/-]{2,20}$"))
                {
                    if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                    {
                        MessageBox.Show("Tên danh mục không hợp lệ! (Chỉ chứa chữ cái, số, dấu phẩy, dấu chấm, gạch ngang, gạch chéo và khoảng trắng. Độ dài: 2-20 ký tự).");
                    }
                    else if (Thread.CurrentThread.CurrentUICulture.Name == "en-US")
                    {
                        MessageBox.Show("Invalid category name! (Only letters, numbers, commas, periods, hyphens, slashes, and spaces are allowed. Length: 2-20 characters).");
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

                // Tạo đối tượng DTO_Hanghoa
                DTO_Hanghoa hangHoa = new DTO_Hanghoa
                {
                    TenHangHoa = tenHangHoa,
                    DanhMuc = tenDanhMuc,
                    THSD = thsd,
                    GiaNhap = giaNhap,
                    GiaBan = giaBan,
                    NhaCC = lbTenNcc.Text.Trim()
                };

                // Kiểm tra hình ảnh
                if (imgImage.Image != null)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        imgImage.Image.Save(ms, imgImage.Image.RawFormat);
                        hangHoa.HinhAnh = ms.ToArray();
                    }
                }
                else
                {
                    hangHoa.HinhAnh = null;
                }

                // Thêm mặt hàng
                bool kq = BLLQuanLyKho.Instance.ThemMatHang(hangHoa);
                if (kq)
                {
                    if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                    {
                        MessageBox.Show("Thêm mặt hàng thành công!");
                    }
                    else if (Thread.CurrentThread.CurrentUICulture.Name == "en-US")
                    {
                        MessageBox.Show("Product added successfully!");
                    }

                }
                else
                {
                    if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                    {
                        MessageBox.Show("Thêm mặt hàng thành công!");
                    }
                    else if (Thread.CurrentThread.CurrentUICulture.Name == "en-US")
                    {
                        MessageBox.Show("Product added successfully!");
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
    }
}


