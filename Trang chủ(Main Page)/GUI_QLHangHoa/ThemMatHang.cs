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
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Trang_chủ_Main_Page_
{
    public partial class ThemMatHang : Form
    {
        BLLQuanLyKho bll = new BLLQuanLyKho();
        public ThemMatHang()
        {
            List<DTO_NhaCungCap> dsNCC = bll.XemNCC();

            InitializeComponent();

            this.imgImage.Paint += new System.Windows.Forms.PaintEventHandler(this.imgImage_Paint);

            cmbTenNcc.DataSource = dsNCC;
            cmbTenNcc.DisplayMember = "TenNCC";
            cmbTenNcc.ValueMember = "MaNCC";

            lbTenNcc.Text = "NCC0001";

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
                DTO_Hanghoa hangHoa = new DTO_Hanghoa
                {
                    TenHangHoa = txtTenHangHoa.Text,
                    DanhMuc = txtTenDanhMuc.Text,
                    THSD = int.Parse(txtTHSD.Text),
                    GiaNhap = float.Parse(txtTienNhap.Text),
                    GiaBan = float.Parse(txtTienBan.Text),
                    NhaCC = lbTenNcc.Text
                };

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

                bool kq = bll.ThemMatHang(hangHoa);
                if (kq)
                {
                    MessageBox.Show("Thêm mặt hàng thành công!");
                }
                else
                {
                    MessageBox.Show("Có lỗi khi thêm mặt hàng!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
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
    }
}


    }
}
