using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;
using DTO;

namespace Trang_chủ_Main_Page_
{
    public partial class NhapHang : Form
    {
        private readonly BLLQuanLyKho bll_QuanLyKho = new BLLQuanLyKho();
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
            DataTable dataTable = bll_QuanLyKho.xemDSNH();
            dgvNhapHang.DataSource = dataTable;
            dgvNhapHang.Columns[0].HeaderText = "Mã đơn hàng";
            dgvNhapHang.Columns[1].HeaderText = "Ngày nhập";
            dgvNhapHang.Columns[2].HeaderText = "Tổng tiền";
            dgvNhapHang.Columns[3].HeaderText = "Trạng Thái";

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
            if (e.RowIndex >= 0)
            {
                // Lấy giá trị cột đầu tiên (ví dụ: Mã đơn hàng)
                string maDH = dgvNhapHang.Rows[e.RowIndex].Cells[0].Value.ToString();

                // Tạo form CTDH và truyền Mã đơn hàng
                CTDH cTDH = new CTDH();
                cTDH.loadCTHDGridview(soHDSelect);
                cTDH.UpdateMaDH(maDH); // Gọi phương thức cập nhật trên CTDH
                cTDH.ShowDialog(); // Hiển thị form chi tiết
            }
        }

        private void dgvNhapHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(dgvNhapHang.Rows[e.RowIndex].Cells[3].Value.ToString() == "Chờ Xác Nhận")
            {
                btn_HuyHD.Enabled = true;
            } else
            {
                btn_HuyHD.Enabled = false;
            }
            soHDSelect = dgvNhapHang.Rows[e.RowIndex].Cells[0].Value.ToString();
            btn_MoveOn.Enabled = true;
        }

        private void btn_HuyHD_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn hủy đơn hàng này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (result == DialogResult.Yes)
            {
                if (bll_QuanLyKho.huyHD(soHDSelect))
                {
                    MessageBox.Show("Hủy đơn hàng thành công");
                    dgvNhapHang.DataSource = bll_QuanLyKho.xemDSNH();
                }
                else
                {
                    MessageBox.Show("Hủy đơn hàng thất bại");
                }
            }
        }

        private void btn_MoveOn_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có muốn chuyển tiếp trạng thái đơn hàng?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (result == DialogResult.Yes)
            {
                DTO_HDNhapHang hDNhapHang = new DTO_HDNhapHang
                {
                    soHD = soHDSelect,
                    trangThai = dgvNhapHang.Rows[dgvNhapHang.CurrentCell.RowIndex].Cells[3].Value.ToString()
                };
                if (bll_QuanLyKho.capNhatTTDH(hDNhapHang))
                {
                    MessageBox.Show("Chuyển tiếp trạng thái đơn hàng thành công");
                    dgvNhapHang.DataSource = bll_QuanLyKho.xemDSNH();
                }
                else
                {
                    MessageBox.Show("Chuyển tiếp trạng thái đơn hàng thất bại");
                }
            }

        }
    }
}
