using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Trang_chu_Main_Page_.GUI_QLHangHoa;

namespace Trang_chủ_Main_Page_
{
    public partial class CTDH : Form
    {
        public CTDH()
        {
            InitializeComponent();
        }

        private void label1_Paint(object sender, PaintEventArgs e)
        {
           
        }

        private void guna2GradientButton3_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
            "Khi ấn hủy, đơn hàng sẽ bị xóa và hoàn lại đồ cho nhà cung cấp.",
            "Xác nhận hủy đơn hàng",
            MessageBoxButtons.OKCancel,
            MessageBoxIcon.Warning
            );
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {

        }

        private void guna2GradientButton2_Click(object sender, EventArgs e)
        {

        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void guna2GradientButton3_Click_1(object sender, EventArgs e)
        {

        }

        private void guna2Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void guna2TextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2CirclePictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void CTDH_Load(object sender, EventArgs e)
        {
            // Xóa dữ liệu cũ (nếu cần)
            dgvCTDH.Rows.Clear();

            // Thêm 10 dòng dữ liệu mẫu
            for (int i = 1; i <= 10; i++)
            {
                dgvCTDH.Rows.Add(
                    "DH00" + i,                   // Mã Đơn Hàng
                    "NCC00" + i,                  // Mã Nhà Cung Cấp
                    "Nhà Cung Cấp " + i,          // Tên Nhà Cung Cấp
                    "MST00" + i,                  // Mã Số Thuế
                    "Địa chỉ NCC " + i,           // Địa Chỉ
                    "012345678" + i,              // Số Điện Thoại
                    "MH00" + i,                   // Mã Hàng Hóa
                    "Hàng Hóa " + i,              // Tên Hàng Hóa
                    10 + i,                       // Số Lượng Đặt
                    8 + i,                        // Số Lượng Nhận
                    (10 + i) * 50000              // Thành Tiền (Giá giả định)
                );
            }

        }

        private void btnKhieuNai_Click(object sender, EventArgs e)
        {
            KhieuNai kN = new KhieuNai();
            kN.Show();
        }

        // Xử lý sự kiện khi người dùng chọn một dòng trên bảng hóa đơn đặt hàng
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //Cập nhật lại mã đặt hàng
        public void UpdateMaDH(string maDH)
        {
           lbMaDH.Text = maDH;
        }
    }
}
