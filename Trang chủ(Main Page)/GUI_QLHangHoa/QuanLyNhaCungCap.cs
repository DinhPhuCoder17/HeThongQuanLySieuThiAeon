using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using BLL;
using ServiceStack.OrmLite.Converters;
using System.Text.RegularExpressions;
using static Jenga.Theme;
using OfficeOpenXml.FormulaParsing.LexicalAnalysis;

namespace Trang_chu_Main_Page_.GUI_QLHangHoa
{
    public partial class QuanLyNhaCungCap : Form
    {
        String maNCCSelected = "";
        bool menu_NCC_Add_Expand = false;
        bool isEdited = false;  // Biến kiểm tra xem có đang ở chế độ chỉnh sửa hay không
        DataGridViewRow rowEdited = null; // Dòng đang chỉnh sửa

        bool pn_supplier_Add_Expand=false;
        public QuanLyNhaCungCap()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        //Load danh sách nhà cung cấp
        private void LoadNCCList()
        {
            dgvNhaCungCap.DataSource = BLLQuanLyKho.Instance.GetAllNCC();
            if (dgvNhaCungCap.DataSource != null)
            {
                dgvNhaCungCap.Columns["MaNCC"].HeaderText = "Mã Nhà cung cấp";
                dgvNhaCungCap.Columns["TenNCC"].HeaderText = "Tên Nhà Cung Cấp";
                dgvNhaCungCap.Columns["Diachi"].HeaderText = "Địa Chỉ";
                dgvNhaCungCap.Columns["Masothue"].HeaderText = "Mã số thuế";
                dgvNhaCungCap.Columns["Sodienthoai"].HeaderText = "Số Điện Thoại";
            }
            dgvNhaCungCap.ReadOnly = true; // Ban đầu không cho chỉnh sửa

        }
        private void QuanLyNhaCungCap_Load(object sender, EventArgs e)
        {
            LoadNCCList();        
        }

        private void t_Supplier_Add_Tick(object sender, EventArgs e)
        {
            if (pn_supplier_Add_Expand == false)
            {
                pn_Supplier_Add.Height += 20;
                if (pn_Supplier_Add.Height >= 300)
                {
                    t_Supplier_Add.Stop();
                    pn_supplier_Add_Expand = true;
                }
            }
            else
            {
                pn_Supplier_Add.Height -= 20;
                if (pn_Supplier_Add.Height <= 0)
                {
                    t_Supplier_Add.Stop();
                    pn_supplier_Add_Expand = false;
                }
            }
        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            
        }

        private void guna2GradientButton1_Click_1(object sender, EventArgs e)
        {
            t_Supplier_Add.Start();
        }

        private void btnLuuNCC_Click(object sender, EventArgs e)
        {

        }

        private void guna2Panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pn_Supplier_Add_Paint(object sender, PaintEventArgs e)
        {
            //if (txt_maNCC.Text == "" || txt_tenNCC.Text == "" || txt_mst.Text == "" || txt_diachi.Text == "" || txt_sdt.Text == "")
            //{
            //    MessageBox.Show("Vui lòng nhập đầy đủ thông tin");
            //}
        }

        private void btn_Them_Click(object sender, EventArgs e)
        {
            string tenNCC = txt_tenNCC.Text;
            string diaChi = txt_diachi.Text;
            string maSoThue = txt_mst.Text;
            string sdt = txt_sdt.Text;

            //Bắt đầu check nhập dữ liệu
            if (string.IsNullOrEmpty(tenNCC) || string.IsNullOrEmpty(diaChi) || string.IsNullOrEmpty(maSoThue) || string.IsNullOrEmpty(sdt))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!Regex.IsMatch(tenNCC, "^[a-zA-Z0-9À-ỹ ,.-]+$"))
            {
                MessageBox.Show("Tên NCC không được chứa số và kí tự đặc biệt");
                return;
            }
            if (!Regex.IsMatch(sdt, "^[0-9]+$")) 
            {
                MessageBox.Show("Số điện thoại không hợp lệ");
                return;
            }
            if (!Regex.IsMatch(diaChi, @"^[\p{L}0-9,.\- ]{5,500}$"))
            {
                MessageBox.Show("Địa chỉ không chứa kí tự đặc biệt");
                return;
            }
            if (!Regex.IsMatch(maSoThue, @"^\d+$")) // chỉ chứa số từ đầu đến cuối
            {
                MessageBox.Show("Mã số thuế chỉ chứa số");
                return;
            }
            if (sdt.Length != 10)
            {
                MessageBox.Show("Số điện thoại không hợp lệ");
                return;
            }
            //Kết thúc check nhập dữ liệu

            if (BLLQuanLyKho.Instance.AddNCC(tenNCC, diaChi, maSoThue, sdt))
            {
                MessageBox.Show("Thêm thành công!", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Lỗi thêm mới nhà cung cấp!", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Sự kiện nhấn nút Sửa

        bool anNutSua = false;
        private void btnSuaNCC_Click(object sender, EventArgs e)
        {
            if(anNutSua == false)
            {
                if (dgvNhaCungCap.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Vui lòng chọn một dòng để sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                isEdited = true;
                dgvNhaCungCap.ReadOnly = false; // Cho phép chỉnh sửa
                anNutSua = true;
            }
            else // anNutSua = true
            {
                try
                {
                    DataGridViewRow row = dgvNhaCungCap.SelectedRows[0];
                    string maNCC = row.Cells["MaNCC"].Value.ToString();
                    string tenNCC = row.Cells["TenNCC"].Value.ToString();
                    string maSoThue = row.Cells["Masothue"].Value.ToString();
                    string diaChi = row.Cells["Diachi"].Value.ToString();
                    string sdt = row.Cells["Sodienthoai"].Value.ToString();

                    bool result = BLLQuanLyKho.Instance.UpdateNCC(maNCC, tenNCC, maSoThue, diaChi, sdt);

                    if (result)
                    {
                        MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadNCCList(); // Cập nhật lại danh sách
                    }
                    else
                    {
                        MessageBox.Show("Cập nhật thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                isEdited = false;
                dgvNhaCungCap.ReadOnly = true; // Không cho chỉnh sửa nữa
                anNutSua = false; // ấn nút sửa lần 2 là lưu
            }
            
        }

        //Sự kiện nhấn nút Xoá
        private void btnXoaNCC_Click(object sender, EventArgs e)
        {
            if (dgvNhaCungCap.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn 1 dòng để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult dialog = MessageBox.Show("Bạn có chắc chắn muốn xóa nhà cung cấp này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.No) return;

            string maNCC = dgvNhaCungCap.SelectedRows[0].Cells["MaNCC"].Value.ToString();

            bool result = BLLQuanLyKho.Instance.DeleteNCC(maNCC);

            if (result)
            {
                MessageBox.Show("Nhà cung cấp đã được xóa (ẩn).", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadNCCList(); // Cập nhật lại danh sách
            }
            else
            {
                MessageBox.Show("Xóa thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
