using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Trang_chủ_Main_Page_
{
    public partial class NhapHang : Form
    {
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
            dgvNhapHang.Rows.Add("DH001", "12/03/2025", 1500000, "Đã giao");
            dgvNhapHang.Rows.Add("DH002", "11/03/2025", 200000, "Chờ xác nhận");
            dgvNhapHang.Rows.Add("DH003", "10/03/2025", 750000, "Đang vận chuyển");
            dgvNhapHang.Rows.Add("DH004", "09/03/2025", 500000, "Đã hủy");
            dgvNhapHang.Rows.Add("DH005", "08/03/2025", 1250000, "Đã giao");
            dgvNhapHang.Rows.Add("DH006", "07/03/2025", 300000, "Chờ xác nhận");
            dgvNhapHang.Rows.Add("DH007", "06/03/2025", 950000, "Đang vận chuyển");
            dgvNhapHang.Rows.Add("DH008", "05/03/2025", 850000, "Đã giao");
            dgvNhapHang.Rows.Add("DH009", "04/03/2025", 650000, "Chờ xác nhận");
            dgvNhapHang.Rows.Add("DH010", "03/03/2025", 400000, "Đã giao");



        }
    }
}
