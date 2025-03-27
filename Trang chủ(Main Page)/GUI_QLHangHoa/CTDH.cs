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
using BLL;
using Trang_chu_Main_Page_.GUI_QLHangHoa;

namespace Trang_chủ_Main_Page_
{
    public partial class CTDH : Form
    {
        public BLLQuanLyKho bLL_QuanLyKho = new BLLQuanLyKho();
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
            dgvCTDH.Columns[0].HeaderText = "Mã hàng hóa";
            dgvCTDH.Columns[1].HeaderText = "Số hóa đơn";
            dgvCTDH.Columns[2].HeaderText = "Ngày nhập";
            dgvCTDH.Columns[3].HeaderText = "Số lượng đặt";
            dgvCTDH.Columns[4].HeaderText = "Số lượng nhận";
            dgvCTDH.Columns[6].HeaderText = "Ngày sản xuất";
            dgvCTDH.Columns[6].HeaderText = "Hạn sử dụng";
            dgvCTDH.Columns[7].HeaderText = "Thành tiền";
            
            //Khóa chức năng tự điều chỉnh bảng
            foreach (DataGridViewColumn column in dgvCTDH.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            foreach (DataGridViewColumn column in dgvCTDH.Columns)
            {
                column.Resizable = DataGridViewTriState.False;
            }
            
            //foreach (DataGridViewColumn column in dgvCTDH.Columns)




        }

        public void loadCTHDGridview(String soHD)
        {
            dgvCTDH.DataSource = bLL_QuanLyKho.xemCTDHBySohd(soHD);
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
