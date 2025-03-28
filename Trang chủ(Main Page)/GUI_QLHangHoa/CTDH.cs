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
using DTO;
using Microsoft.Office.Interop.Excel;
using Trang_chu_Main_Page_.GUI_QLHangHoa;

namespace Trang_chủ_Main_Page_
{
    public partial class CTDH : Form
    {
        private String Trangthai;
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

        private bool kiemTraOTrong()
        {
            foreach (DataGridViewRow row in dgvCTDH.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.Value.ToString() == "")
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private void nhapVaoKho_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
            "Khi ấn xác nhận, hàng hóa sẽ được nhập vào kho và không thể hủy.",
            "Xác nhận nhập hàng",
            MessageBoxButtons.OKCancel,
            MessageBoxIcon.Warning
            );
            if (result == DialogResult.OK)
            {
                if(kiemTraOTrong())
                {
                    String status = "Đã Xử Lý";
                    foreach (DataGridViewRow row in dgvCTDH.Rows)
                    {
                        if (int.Parse(row.Cells[4].Value.ToString()) != int.Parse(row.Cells[5].Value.ToString()))
                        {
                            status = "Nhập Kho Một Phần";
                        }
                    }
                    DTO_HDNhapHang hDNhapHang = new DTO_HDNhapHang()
                    {
                        soHD = lbMaDH.Text,
                        soLuong = 0,
                        trangThai = status,
                        ngayDat = DateTime.Now,
                        tongTien = 0,
                        CT_HDNH = new List<DTO_HH_HDNH>()
                    };

                    foreach (DataGridViewRow row in dgvCTDH.Rows)
                    {
                        DTO_HH_HDNH dto_HH_HDNH = new DTO_HH_HDNH()
                        {
                            HangHoa = new DTO_Hanghoa() { MaHangHoa = row.Cells[1].Value.ToString() },
                            SoLuongDat = int.Parse(row.Cells[4].Value.ToString()),
                            SoLuongNhan = int.Parse(row.Cells[5].Value.ToString()),
                            NSX = DateTime.Parse(row.Cells[6].Value.ToString()),
                            HSD = DateTime.Parse(row.Cells[7].Value.ToString()),
                            TrangThai = "Đã Nhập Kho"
                        };

                        hDNhapHang.CT_HDNH.Add(dto_HH_HDNH);
                    }

                    if (bLL_QuanLyKho.nhapKho(hDNhapHang))
                    {
                        MessageBox.Show("Nhập hàng thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnNhapVaoKho.Enabled = false;
                        btnKhieuNai.Enabled = false;
                        btnXacNhanDuTatCa.Enabled = false;
                        dgvCTDH.ReadOnly = true;

                        dgvCTDH.CellEndEdit -= dgvCTDH_CellEndEdit;
                        dgvCTDH.CellValidating -= dgvCTDH_CellValidating;

                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Nhập hàng thất bại", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }else
                {
                    MessageBox.Show("Vui lòng điền đầy đủ thông tin", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void btnXacNhanDuTatCa_Click(object sender, EventArgs e)
        {
            foreach(DataGridViewRow row in dgvCTDH.Rows)
            {
                row.Cells[5].Value = row.Cells[4].Value;
                row.Cells[6].Value = DateTime.Now.ToString("yyyy/MM/dd");
                row.Cells[7].Value = DateTime.Now.AddDays(int.Parse(row.Cells[9].Value.ToString())).ToString("yyyy/MM/dd");
            }
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
            dgvCTDH.Columns[0].HeaderText = "Số hóa đơn";
            dgvCTDH.Columns[1].HeaderText = "Mã hàng hóa";
            dgvCTDH.Columns[2].HeaderText = "Tên hàng hóa";
            dgvCTDH.Columns[3].HeaderText = "Ngày nhập";
            dgvCTDH.Columns[4].HeaderText = "Số lượng đặt";
            dgvCTDH.Columns[5].HeaderText = "Số lượng nhận";
            dgvCTDH.Columns[6].HeaderText = "Ngày sản xuất";
            dgvCTDH.Columns[7].HeaderText = "Hạn sử dụng";
            dgvCTDH.Columns[8].HeaderText = "Thành tiền";

            //Khóa chức năng tự điều chỉnh bảng
            foreach (DataGridViewColumn column in dgvCTDH.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
                column.Resizable = DataGridViewTriState.False;
                column.ReadOnly = true; // Khóa tất cả cột

            }

            // Mở khóa chỉnh sửa cột thứ 5 (chỉ số 4)
            dgvCTDH.Columns[5].ReadOnly = false;
            dgvCTDH.Columns[6].ReadOnly = false;

            if(Trangthai == "Nhập Kho Một Phần")
            {
                foreach (DataGridViewRow row in dgvCTDH.Rows)
                {
                    if (int.Parse(row.Cells[4].Value.ToString()) != int.Parse(row.Cells[5].Value.ToString()))
                    {
                        row.Cells[5].Style.BackColor = Color.DarkRed;
                        row.Cells[5].Style.ForeColor = Color.White;
                        row.Cells[5].Style.SelectionBackColor = Color.DarkRed;
                        row.Cells[5].Style.SelectionForeColor = Color.White;
                    }
                }
            }

        }

        public void loadCTHDGridview(String soHD, String Trangthai)
        {
            dgvCTDH.DataSource = bLL_QuanLyKho.xemCTDHBySohd(soHD);
            dgvCTDH.Columns["THSD"].Visible = false;
            dgvCTDH.Columns["Sohd"].Visible = false;
            this.Trangthai = Trangthai;
        }

        private void btnKhieuNai_Click(object sender, EventArgs e)
        {
            System.Data.DataTable dt = bLL_QuanLyKho.xemDSKN(lbMaDH.Text);
            foreach (DataRow row in dt.Select("Soluongnhan = Soluongdat"))
            {
                dt.Rows.Remove(row);
            }


            KhieuNai kN = new KhieuNai();
            kN.giveDataGridView(dt);
            kN.ShowDialog();
        }

        // Xử lý sự kiện khi người dùng chọn một dòng trên bảng hóa đơn đặt hàng
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //Cập nhật lại mã đặt hàng
        public void UpdateMaDH(string maDH, String TrangThaiDHLon)
        {
            btnKhieuNai.Enabled = false;
            lbMaDH.Text = maDH;
            if (TrangThaiDHLon == "Chờ Xác Nhận" || TrangThaiDHLon == "Đang Vận Chuyển" || TrangThaiDHLon == "Đã Xử Lý" || TrangThaiDHLon == "Đã Nhập Một Phần")
            {
                btnNhapVaoKho.Enabled = false;
                btnXacNhanDuTatCa.Enabled = false;
                btnKhieuNai.Enabled = false;
                dgvCTDH.ReadOnly = true;

                dgvCTDH.CellEndEdit -= dgvCTDH_CellEndEdit;
                dgvCTDH.CellValidating -= dgvCTDH_CellValidating;

            }

            if(TrangThaiDHLon == "Nhập Kho Một Phần")
            {
                btnNhapVaoKho.Enabled = false;
                btnKhieuNai.Enabled = true;
                btnXacNhanDuTatCa.Enabled = false;
                dgvCTDH.ReadOnly = true;
                dgvCTDH.CellEndEdit += dgvCTDH_CellEndEdit;
                dgvCTDH.CellValidating += dgvCTDH_CellValidating;
            }

        }

        private void dgvCTDH_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            //try
            //{
            //    dgvCTDH.CellValueChanged -= dgvCTDH_CellValueChanged; // Tạm thời ngừng sự kiện CellValueChanged
            //    dgvCTDH.Rows[e.RowIndex].Cells[6].Value = DateTime.Now.AddDays(int.Parse(dgvCTDH.Rows[e.RowIndex].Cells[8].Value.ToString())).ToString("yyyy/MM/dd");
            //    dgvCTDH.CellValueChanged += dgvCTDH_CellValueChanged; // Kích hoạt lại sự kiện CellValueChanged
            //}
            //catch
            //{
            //    MessageBox.Show("Có lỗi xảy ra khi cập nhật hạn sử dụng", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}
        }

        private void dgvCTDH_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if(e.ColumnIndex == 5)
            {
                if (!int.TryParse(e.FormattedValue.ToString(), out int result) || result < 0 )
                {
                    MessageBox.Show("Số lượng không hợp lệ", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    e.Cancel = true; // Ngăn không cho rời khỏi ô nếu nhập sai
                }
            }

            if (e.ColumnIndex == 6)
            {
                string input = e.FormattedValue.ToString(); // Lấy giá trị vừa nhập

                if (!DateTime.TryParse(input, out _)) // Kiểm tra có đúng định dạng ngày không
                {
                    MessageBox.Show("Vui lòng nhập đúng định dạng ngày (dd/MM/yyyy hoặc yyyy-MM-dd)!",
                        "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    e.Cancel = true; // Ngăn không cho rời khỏi ô nếu nhập sai
                }
            }
        }

        private void dgvCTDH_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                dgvCTDH.Rows[e.RowIndex].Cells[7].Value = DateTime.Parse(dgvCTDH.Rows[e.RowIndex].Cells[6].Value.ToString()).AddDays(int.Parse(dgvCTDH.Rows[e.RowIndex].Cells[9].Value.ToString())).ToString("yyyy/MM/dd");
            }
            catch
            {
                MessageBox.Show("Có lỗi xảy ra khi cập nhật hạn sử dụng", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
