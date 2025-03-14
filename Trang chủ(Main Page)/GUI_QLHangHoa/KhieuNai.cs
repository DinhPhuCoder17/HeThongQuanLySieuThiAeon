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

namespace Trang_chu_Main_Page_.GUI_QLHangHoa
{
    public partial class KhieuNai : Form
    {
        public KhieuNai()
        {
            InitializeComponent();
            InitializeDataGridView();
        }

        private void btnTaoPhieuBoSung_Click(object sender, EventArgs e)
        {

        }
        private void InitializeDataGridView()
        {
            dgv_KhieuNai.Columns.Clear();

            dgv_KhieuNai.ReadOnly = false;
            dgv_KhieuNai.AllowUserToAddRows = false;
            dgv_KhieuNai.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgv_KhieuNai.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular);

            var colMaHH = new DataGridViewTextBoxColumn();
            colMaHH.Name = "colMaHH";
            colMaHH.HeaderText = "Mã Hàng Hóa";
            dgv_KhieuNai.Columns.Add(colMaHH);
            var colMaNCC = new DataGridViewTextBoxColumn();
            colMaNCC.Name = "colMaNCC";
            colMaNCC.HeaderText = "Mã Nhà Cung Cấp";
            dgv_KhieuNai.Columns.Add(colMaNCC);
            var colTenHH = new DataGridViewTextBoxColumn();
            colTenHH.Name = "colTenHH";
            colTenHH.HeaderText = "Tên Hàng Hóa";
            dgv_KhieuNai.Columns.Add(colTenHH);
            var colTenNCC = new DataGridViewTextBoxColumn();
            colTenNCC.Name = "colTenNCC";
            colTenNCC.HeaderText = "Tên Nhà Cung Cấp";
            dgv_KhieuNai.Columns.Add(colTenNCC);
            var colSoLuongChenh = new DataGridViewTextBoxColumn();
            colSoLuongChenh.Name = "colSoLuongChenh";
            colSoLuongChenh.HeaderText = "Số Lượng Chênh Lệch";
            dgv_KhieuNai.Columns.Add(colSoLuongChenh);

            var colTinhTrang = new DataGridViewComboBoxColumn();
            colTinhTrang.Name = "colTinhTrang";
            colTinhTrang.HeaderText = "Tình trạng";
            colTinhTrang.Items.Add("Thiếu");
            colTinhTrang.Items.Add("Hỏng");
            colTinhTrang.Items.Add("Thừa");
            dgv_KhieuNai.Columns.Add(colTinhTrang);

            var colLyDo = new DataGridViewTextBoxColumn();
            colLyDo.Name = "colLyDoChiTiet";
            colLyDo.HeaderText = "Lý do chi tiết";
            dgv_KhieuNai.Columns.Add(colLyDo);


            AddRowData("HH001", "NCC001", "Bánh kẹo", "ABC Co.", -5);
            AddRowData("HH002", "NCC002", "Nước ngọt", "XYZ Co.", 10);
        }


        private void AddRowData(string maHH, string maNCC, string tenHH, string tenNCC, int soLuongChenh, string lyDo = "")
        {
            bool isNegative = soLuongChenh < 0;
            int displayedValue = Math.Abs(soLuongChenh);

            int rowIndex = dgv_KhieuNai.Rows.Add();
            DataGridViewRow row = dgv_KhieuNai.Rows[rowIndex];

            row.Cells["colMaHH"].Value = maHH;
            row.Cells["colMaNCC"].Value = maNCC;
            row.Cells["colTenHH"].Value = tenHH;
            row.Cells["colTenNCC"].Value = tenNCC;
            row.Cells["colSoLuongChenh"].Value = displayedValue.ToString();
            row.Cells["colLyDoChiTiet"].Value = lyDo;

            var comboCell = row.Cells["colTinhTrang"] as DataGridViewComboBoxCell;
            if (comboCell != null)
            {
                comboCell.Items.Clear(); 

                if (isNegative)
                {
                    comboCell.Items.Add("Thiếu");
                    comboCell.Items.Add("Hỏng");
                    comboCell.Value = "Thiếu";       
                    comboCell.ReadOnly = false;      
                }
                else
                {
                    comboCell.Items.Add("Thừa");
                    comboCell.Value = "Thừa";        
                    comboCell.ReadOnly = true;       
                }
            }
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, int wParam, string lParam);

        private const int EM_SETCUEBANNER = 0x1501;
        
    }
}