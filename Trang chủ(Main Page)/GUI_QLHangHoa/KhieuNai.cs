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

namespace Trang_chu_Main_Page_.GUI_QLHangHoa
{
    public partial class KhieuNai : Form
    {
        private readonly BLLQuanLyKho bLL_QuanLyKho = new BLLQuanLyKho();
        public KhieuNai()
        {
            InitializeComponent();
        }

        //Hàm truyền dữ liệu DataGridView từ form chính
        public void giveDataGridView(System.Data.DataTable dataTable)
        {
            dgv_KhieuNai.DataSource = dataTable;
            dgv_KhieuNai.Columns["Sohd"].HeaderText = "Số hóa đơn";
            dgv_KhieuNai.Columns["Mahanghoa"].HeaderText = "Mã hàng hóa";
            dgv_KhieuNai.Columns["Tenhanghoa"].HeaderText = "Tên hàng hóa";
            dgv_KhieuNai.Columns["Ngaynhap"].HeaderText = "Ngày nhập";
            dgv_KhieuNai.Columns["Soluongdat"].HeaderText = "Số lượng đặt";
            dgv_KhieuNai.Columns["Soluongnhan"].HeaderText = "Số lượng nhận";
            dgv_KhieuNai.Columns["Lydochitiet"].HeaderText = "Lý do chi tiết";
            dgv_KhieuNai.Columns["Luongchenhlech"].HeaderText = "Lượng chênh lệch";
            dgv_KhieuNai.Columns["LoaiKhieuNai"].Visible = false;

            // Tạo cột ComboBox cho trường hợp nhận < đặt
            DataGridViewComboBoxColumn lessReceive = new DataGridViewComboBoxColumn();
            lessReceive.HeaderText = "Loại khiếu nại"; // Tiêu đề cột
            lessReceive.Name = "LoaiKhieuNaiView"; // Tên cột
            lessReceive.DataSource = new string[] { "Sai hàng", "Thiếu hàng", "Hàng lỗi", "Dư hàng" }; // Danh sách lựa chọn
            lessReceive.AutoComplete = true; // Cho phép tự động điền

            // Thêm cột vào DataGridView
            dgv_KhieuNai.Columns.Add(lessReceive);
            foreach (DataGridViewRow row in dgv_KhieuNai.Rows)
            {
                // Điền dữ liệu vào cột ComboBox
                row.Cells["LoaiKhieuNaiView"].Value = row.Cells["LoaiKhieuNai"].Value;
            }

            foreach (DataGridViewColumn col in dgv_KhieuNai.Columns)
            {
                if (col.Name != "LoaiKhieuNaiView" && col.Name != "Lydochitiet" && col.Name != "Luongchenhlech")
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
                    }
                }
            }
            return true;
        }

        private void btnTaoPhieuBoSung_Click(object sender, EventArgs e)
        {
            if(kiemTraOTrong())
            {
                foreach(DataGridViewRow row in dgv_KhieuNai.Rows)
                {
                    DTO_Khieunai kn = new DTO_Khieunai()
                    {
                        SoHD = row.Cells[1].Value.ToString(),
                        MaHH = row.Cells[2].Value.ToString(),
                        Luongchenhlech = int.Parse(row.Cells[7].Value.ToString()),
                        Loaikhieunai = row.Cells[0].Value.ToString(),
                        Lydochitiet = row.Cells[9].Value.ToString(),
                    };

                    bLL_QuanLyKho.themKN(kn);
                    
                }
            }
            else
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgv_KhieuNai_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.ColumnIndex == 7)
            {
                // Lấy giá trị người dùng vừa nhập
                string newValue = e.FormattedValue.ToString();

                // Kiểm tra nếu không phải số nguyên
                if (!int.TryParse(newValue, out _))
                {
                    MessageBox.Show("Vui lòng nhập một số hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    e.Cancel = true; // Hủy bỏ thay đổi
                }
            }

            if (dgv_KhieuNai.Columns[e.ColumnIndex] is DataGridViewComboBoxColumn)
            {
                // Kiểm tra nếu giá trị ô đang nhập là null hoặc rỗng
                if (string.IsNullOrWhiteSpace(e.FormattedValue?.ToString()))
                {
                    MessageBox.Show("Vui lòng chọn một giá trị!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    e.Cancel = true; // Hủy bỏ thay đổi nếu không hợp lệ
                }
            }
        }
    }
}