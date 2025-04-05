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
using System.Threading;
using Trang_chủ_Main_Page_;

namespace Trang_chu_Main_Page_.GUI_QLHangHoa
{
    public partial class QuanLyNhaCungCap : Form
    {
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
                if (Thread.CurrentThread.CurrentUICulture.Name == "en-US")
                {
                    dgvNhaCungCap.Columns["MaNCC"].HeaderText = "Supplier Code";
                    dgvNhaCungCap.Columns["TenNCC"].HeaderText = "Supplier Name";
                    dgvNhaCungCap.Columns["Diachi"].HeaderText = "Address";
                    dgvNhaCungCap.Columns["Masothue"].HeaderText = "Tax Code";
                    dgvNhaCungCap.Columns["Sodienthoai"].HeaderText = "Phone Number";
                }
                else if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                {
                    dgvNhaCungCap.Columns["MaNCC"].HeaderText = "Mã Nhà Cung Cấp";
                    dgvNhaCungCap.Columns["TenNCC"].HeaderText = "Tên Nhà Cung Cấp";
                    dgvNhaCungCap.Columns["Diachi"].HeaderText = "Địa Chỉ";
                    dgvNhaCungCap.Columns["Masothue"].HeaderText = "Mã Số Thuế";
                    dgvNhaCungCap.Columns["Sodienthoai"].HeaderText = "Số Điện Thoại";
                }

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
                if (Thread.CurrentThread.CurrentUICulture.Name == "en-US")
                {
                    MessageBox.Show("Please fill in all the information!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                return;
            }

            if (!Regex.IsMatch(tenNCC, "^[a-zA-Z0-9À-ỹ ,.-]+$"))
            {
                if (Thread.CurrentThread.CurrentUICulture.Name == "en-US")
                {
                    MessageBox.Show("Supplier name cannot contain numbers or special characters");
                }
                else if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                {
                    MessageBox.Show("Tên NCC không được chứa số và kí tự đặc biệt");
                }
                return;
            }

            if (!Regex.IsMatch(sdt, "^[0-9]+$"))
            {
                if (Thread.CurrentThread.CurrentUICulture.Name == "en-US")
                {
                    MessageBox.Show("Invalid phone number");
                }
                else if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                {
                    MessageBox.Show("Số điện thoại không hợp lệ");
                }
                return;
            }

            if (!Regex.IsMatch(diaChi, @"^[\p{L}0-9,.\- ]{5,500}$"))
            {
                if (Thread.CurrentThread.CurrentUICulture.Name == "en-US")
                {
                    MessageBox.Show("Address cannot contain special characters");
                }
                else if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                {
                    MessageBox.Show("Địa chỉ không chứa kí tự đặc biệt");
                }
                return;
            }

            if (!Regex.IsMatch(maSoThue, @"^\d+$"))
            {
                if (Thread.CurrentThread.CurrentUICulture.Name == "en-US")
                {
                    MessageBox.Show("Tax code must contain only numbers");
                }
                else if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                {
                    MessageBox.Show("Mã số thuế chỉ chứa số");
                }
                return;
            }

            if (sdt.Length != 10)
            {
                if (Thread.CurrentThread.CurrentUICulture.Name == "en-US")
                {
                    MessageBox.Show("Invalid phone number");
                }
                else if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                {
                    MessageBox.Show("Số điện thoại không hợp lệ");
                }
                return;
            }

            //Kết thúc check nhập dữ liệu

            if (BLLQuanLyKho.Instance.AddNCC(tenNCC, diaChi, maSoThue, sdt))
            {
                if (Thread.CurrentThread.CurrentUICulture.Name == "en-US")
                {
                    MessageBox.Show("Added successfully!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                {
                    MessageBox.Show("Thêm thành công!", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                this.Close();
            }
            else
            {
                if (Thread.CurrentThread.CurrentUICulture.Name == "en-US")
                {
                    MessageBox.Show("Error adding new supplier!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                {
                    MessageBox.Show("Lỗi thêm mới nhà cung cấp!", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        //Sự kiện nhấn nút Sửa

        bool anNutSua = false;

        private void btnSuaNCC_Click(object sender, EventArgs e)
        {
            if (anNutSua == false)
            {
                if (dgvNhaCungCap.SelectedRows.Count == 0)
                {
                    if (Thread.CurrentThread.CurrentUICulture.Name == "en-US")
                    {
                        MessageBox.Show("Please select a row to edit.", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                    {
                        MessageBox.Show("Vui lòng chọn một dòng để sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    return;
                }
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
                        if (Thread.CurrentThread.CurrentUICulture.Name == "en-US")
                        {
                            MessageBox.Show("Update successful!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                        {
                            MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        LoadNCCList(); // Cập nhật lại danh sách
                    }
                    else
                    {
                        if (Thread.CurrentThread.CurrentUICulture.Name == "en-US")
                        {
                            MessageBox.Show("Update failed!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                        {
                            MessageBox.Show("Cập nhật thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    if (Thread.CurrentThread.CurrentUICulture.Name == "en-US")
                    {
                        MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                    {
                        MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                dgvNhaCungCap.ReadOnly = true; // Không cho chỉnh sửa nữa
                anNutSua = false; // ấn nút sửa lần 2 là lưu
            }
        }


        //Sự kiện nhấn nút Xoá
        private void btnXoaNCC_Click(object sender, EventArgs e)
        {
            if (dgvNhaCungCap.SelectedRows.Count == 0)
            {
                if (Thread.CurrentThread.CurrentUICulture.Name == "en-US")
                {
                    MessageBox.Show("Please select a row to delete.", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                {
                    MessageBox.Show("Vui lòng chọn 1 dòng để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                return;
            }

            DialogResult dialog = MessageBox.Show(
                Thread.CurrentThread.CurrentUICulture.Name == "en-US" ?
                "Are you sure you want to delete this supplier?" :
                "Bạn có chắc chắn muốn xóa nhà cung cấp này?",
                "Confirmation",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (dialog == DialogResult.No) return;

            string maNCC = dgvNhaCungCap.SelectedRows[0].Cells["MaNCC"].Value.ToString();

            bool result = BLLQuanLyKho.Instance.DeleteNCC(maNCC);

            if (result)
            {
                if (Thread.CurrentThread.CurrentUICulture.Name == "en-US")
                {
                    MessageBox.Show("Supplier has been deleted (hidden).", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                {
                    MessageBox.Show("Nhà cung cấp đã được xóa (ẩn).", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                LoadNCCList(); // Cập nhật lại danh sách
            }
            else
            {
                if (Thread.CurrentThread.CurrentUICulture.Name == "en-US")
                {
                    MessageBox.Show("Deletion failed!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                {
                    MessageBox.Show("Xóa thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // ---------------- Phần của Quang ----------------------
        bool menuExpand_3 = false;
        //Check Password hợp lệ
        public static bool IsValidPassword(string password)
        {
            string pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*])[A-Za-z\d!@#$%^&*]{8,}$";
            return Regex.IsMatch(password, pattern);
        }
        private void tm_InforChanges_Tick_1(object sender, EventArgs e)
        {
            if (menuExpand_3 == false)
            {
                pn_infoChanges.Height += 20;
                if (pn_infoChanges.Height >= 280)
                {
                    tm_InforChanges.Stop();
                    menuExpand_3 = true;
                }
            }
            else
            {
                pn_infoChanges.Height -= 20;
                if (pn_infoChanges.Height <= 0)
                {
                    tm_InforChanges.Stop();
                    menuExpand_3 = false;
                }
            }
        }
        

        private void pn_infoChanges_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lb_Hoten_Click(object sender, EventArgs e)
        {

        }

        private void lb_Ma_Click(object sender, EventArgs e)
        {

        }

        private void lb_Ngaysinh_Click(object sender, EventArgs e)
        {

        }

        private void lb_Gioitinh_Click(object sender, EventArgs e)
        {

        }

        private void lb_sdt_Click(object sender, EventArgs e)
        {

        }

        private void guna2CirclePictureBox1_Click(object sender, EventArgs e)
        {
            tm_InforChanges.Start();
            lb_Ma.Text = Mainpage.CurrentUser.MaNhanvien;
            lb_Hoten.Text = Mainpage.CurrentUser.Hoten;
            lb_Ngaysinh.Text = Mainpage.CurrentUser.Ngaysinh;
            lb_Gioitinh.Text = Mainpage.CurrentUser.Gioitinh;
            lb_sdt.Text = Mainpage.CurrentUser.Sodienthoai;
        }

        private void btn_Xacnhan_Click(object sender, EventArgs e)
        {
            string mk1 = tb_mk1.Text;
            string mk2 = tb_mk2.Text;
            if (string.IsNullOrEmpty(mk1))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu mới!", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(mk2))
            {
                MessageBox.Show("Vui lòng nhập xác nhận mật khẩu mới!", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!mk1.Equals(mk2))
            {
                MessageBox.Show("Mật khẩu không trùng khớp!", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!IsValidPassword(mk1))
            {
                MessageBox.Show("Mật khẩu không hợp lệ!", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (BLL_Nhanvien.Instance.UpdatePassword(Mainpage.CurrentUser.MaNhanvien, mk1))
            {
                MessageBox.Show("Đổi mật khẩu thành công!", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Lỗi khi đổi mật khẩu!", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
