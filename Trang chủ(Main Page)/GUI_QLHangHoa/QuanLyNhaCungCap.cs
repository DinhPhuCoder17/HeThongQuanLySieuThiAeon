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
using DTO;

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
        private DataTable originalNCCData; // Lưu dữ liệu gốc để lọc

        //Load danh sách nhà cung cấp
        private void LoadNCCList()
        {
            //dgvNhaCungCap.DataSource = BLLQuanLyKho.Instance.GetAllNCC();
            originalNCCData = BLLQuanLyKho.Instance.GetAllNCC();
            dgvNhaCungCap.DataSource = originalNCCData;

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
            foreach (DataGridViewColumn column in dgvNhaCungCap.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            foreach (DataGridViewColumn column in dgvNhaCungCap.Columns)
            {
                column.Resizable = DataGridViewTriState.False;
            }

            //dgvNhaCungCap.DataSource = originalNCCData;
            dgvNhaCungCap.ReadOnly = true; // Ban đầu không cho chỉnh sửa

        }
        private void QuanLyNhaCungCap_Load(object sender, EventArgs e)
        {
            LoadNCCList();
            dgvNhaCungCap.DataSource = originalNCCData;
        }
        private void txtSearchNCC_TextChanged(object sender, EventArgs e)
        {
            string keyword = txtSearchNCC.Text.Trim().ToLower();

            if (originalNCCData == null)
                return;

            if (string.IsNullOrEmpty(keyword))
            {
                dgvNhaCungCap.DataSource = originalNCCData;
            }
            else
            {
                DataView dv = originalNCCData.DefaultView;
                dv.RowFilter = $"MaNCC LIKE '%{keyword}%' OR " +
                               $"TenNCC LIKE '%{keyword}%' OR " +
                               $"Diachi LIKE '%{keyword}%' OR " +
                               $"Masothue LIKE '%{keyword}%' OR " +
                               $"Sodienthoai LIKE '%{keyword}%'";
                dgvNhaCungCap.DataSource = dv;
            }
        }

        private void t_Supplier_Add_Tick(object sender, EventArgs e)
        {
            if (pn_supplier_Add_Expand == false)
            {
                pn_Supplier_Add.Height += 20;
                if (pn_Supplier_Add.Height >= 280)
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

        //Nut them NCC moi 
        private void guna2GradientButton1_Click_1(object sender, EventArgs e)
        {
            t_Supplier_Add.Start();
        }

        private void guna2Panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pn_Supplier_Add_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void btn_Them_Click(object sender, EventArgs e)
        {
            // 1. Đọc input
            string tenNCC = txt_tenNCC.Text.Trim();
            string diaChi = txt_diachi.Text.Trim();
            string maSoThue = txt_mst.Text.Trim();
            string sdt = txt_sdt.Text.Trim();

            // 2. Validate nhập liệu
            if (string.IsNullOrEmpty(tenNCC) ||
                string.IsNullOrEmpty(diaChi) ||
                string.IsNullOrEmpty(maSoThue) ||
                string.IsNullOrEmpty(sdt))
            {
                ShowMsg("Please fill in all the information!", "Notification",
                        "Vui lòng nhập đầy đủ thông tin!", "THÔNG BÁO",
                        MessageBoxIcon.Warning);
                return;
            }
            if (!Regex.IsMatch(tenNCC, "^[a-zA-Z0-9À-ỹ ,.-]+$"))
            {
                ShowMsg("Supplier name cannot contain numbers or special characters", null,
                        "Tên NCC không được chứa số và kí tự đặc biệt", null,
                        MessageBoxIcon.Warning);
                return;
            }
            if (!Regex.IsMatch(sdt, "^[0-9]{10}$"))
            {
                ShowMsg("Invalid phone number", null,
                        "Số điện thoại không hợp lệ", null,
                        MessageBoxIcon.Warning);
                return;
            }
            if (!Regex.IsMatch(diaChi, @"^[\p{L}0-9,.\- ]{5,500}$"))
            {
                ShowMsg("Address cannot contain special characters", null,
                        "Địa chỉ không chứa kí tự đặc biệt", null,
                        MessageBoxIcon.Warning);
                return;
            }
            if (!Regex.IsMatch(maSoThue, @"^\d{10}$"))
            {
                ShowMsg("Tax code must contain only 10 numbers", null,
                        "Mã số thuế chỉ chứa 10 số", null,
                        MessageBoxIcon.Warning);
                return;
            }

            // 3. Lấy danh sách NCC active và danh sách mã số thuế đã xóa
            DataTable dtActive = BLLQuanLyKho.Instance.GetAllNCC();
            var dsActive = dtActive.AsEnumerable()
                .Select(r => new DTO_NhaCungCap
                {
                    MaNCC = r["MaNCC"].ToString(),
                    MaSoThue = r["Masothue"].ToString()
                })
                .ToList();

            var dsDeleted = BLLQuanLyKho.Instance.xemMaSoThue();

            // 4. Kiểm tra trùng với NCC đang active
            bool isDuplicate = dsActive
                .Any(ncc => ncc.MaSoThue.Equals(maSoThue, StringComparison.OrdinalIgnoreCase));

            if (isDuplicate)
            {
                // Hỏi có update không
                if (ConfirmYesNo(
                    "This supplier already exists. Do you want to update the information?",
                    "Confirm update",
                    "Nhà cung cấp này đã tồn tại, bạn có muốn thay đổi thông tin?",
                    "Xác nhận cập nhật"))
                {
                    // Lấy MaNCC cũ và update
                    string existingMaNCC = dsActive
                        .First(n => n.MaSoThue.Equals(maSoThue, StringComparison.OrdinalIgnoreCase))
                        .MaNCC;
                    BLLQuanLyKho.Instance.UpdateNCC(existingMaNCC, tenNCC, maSoThue, diaChi, sdt);
                    LoadNCCList();
                }
                return;
            }

            // 5. Kiểm tra mã số thuế đã xóa trước đó
            bool wasDeleted = dsDeleted
                .Any(ncc => ncc.MaSoThue.Equals(maSoThue, StringComparison.OrdinalIgnoreCase));

            if (wasDeleted)
            {
                if (ConfirmYesNo(
                    "This supplier was previously deleted. Do you want to restore it?",
                    "Restore Confirmation",
                    "Nhà cung cấp này đã bị xóa trước đó. Bạn có muốn khôi phục lại không?",
                    "Xác nhận khôi phục"))
                {
                    BLLQuanLyKho.Instance.KhoiPhucNCC(maSoThue);
                    LoadNCCList();
                }
                else
                {
                    ShowMsg("Unable to restore the supplier. Please try again.",
                            "Error",
                            "Không thể khôi phục nhà cung cấp. Vui lòng thử lại.",
                            "Lỗi",
                            MessageBoxIcon.Error);
                }
                return;
            }

            // 6. Thêm mới
            bool added = BLLQuanLyKho.Instance.AddNCC(tenNCC, diaChi, maSoThue, sdt);
            if (added)
            {
                ShowMsg("Added successfully!", "Notification",
                        "Thêm thành công!", "THÔNG BÁO",
                        MessageBoxIcon.Information);
                LoadNCCList();
            }
            else
            {
                ShowMsg("Error adding new supplier!", "Notification",
                        "Lỗi thêm mới nhà cung cấp!", "THÔNG BÁO",
                        MessageBoxIcon.Error);
            }
        }

        // Helper: Hiển thị MessageBox đa ngôn ngữ
        private void ShowMsg(string enMsg, string enTitle,
                             string viMsg, string viTitle,
                             MessageBoxIcon icon)
        {
            if (Thread.CurrentThread.CurrentUICulture.Name == "en-US")
                MessageBox.Show(enMsg, enTitle, MessageBoxButtons.OK, icon);
            else
                MessageBox.Show(viMsg, viTitle, MessageBoxButtons.OK, icon);
        }

        // Helper: Hỏi Yes/No đa ngôn ngữ, trả về true nếu Yes
        private bool ConfirmYesNo(string enMsg, string enTitle,
                                  string viMsg, string viTitle)
        {
            var (msg, title) = Thread.CurrentThread.CurrentUICulture.Name == "en-US"
                ? (enMsg, enTitle)
                : (viMsg, viTitle);

            return MessageBox.Show(msg, title,
                       MessageBoxButtons.YesNo,
                       MessageBoxIcon.Question) == DialogResult.Yes;
        }


        //Sự kiện nhấn nút Sửa

        // 1. Khai báo cấp class
        private bool isEditingNCC = false;
        private DataGridViewRow rowEditingNCC;
        private object[] originalNCCValues;

        // 2. Event cho nút Sửa/Cancel/Save
        private void btnSuaNCC_Click(object sender, EventArgs e)
        {
            txtSearchNCC.Enabled = false;
            if (!isEditingNCC)
            {
                if (dgvNhaCungCap.SelectedRows.Count == 0)
                {
                    string msg = Thread.CurrentThread.CurrentUICulture.Name == "vi-VN"
                        ? "Vui lòng chọn một dòng để sửa!"
                        : "Please select a row to edit!";
                    string title = Thread.CurrentThread.CurrentUICulture.Name == "vi-VN" ? "Thông báo" : "Notification";
                    MessageBox.Show(msg, title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                isEditingNCC = true;
                dgvNhaCungCap.ReadOnly = false;

                foreach (DataGridViewColumn col in dgvNhaCungCap.Columns)
                    col.ReadOnly = (col.Name == "MaNCC" || col.Name == "Masothue");

                rowEditingNCC = dgvNhaCungCap.SelectedRows[0];
                originalNCCValues = new object[rowEditingNCC.Cells.Count];
                for (int i = 0; i < rowEditingNCC.Cells.Count; i++)
                    originalNCCValues[i] = rowEditingNCC.Cells[i].Value;

                rowEditingNCC.DefaultCellStyle.BackColor = Color.DarkGray;
                rowEditingNCC.DefaultCellStyle.SelectionBackColor = Color.Gray;

                btnXoaNCC.Enabled = false;
                btnSuaNCC.Text = Thread.CurrentThread.CurrentUICulture.Name == "vi-VN" ? "Lưu" : "Save";
            }
            else
            {
                try
                {
                    string maNCC = rowEditingNCC.Cells["MaNCC"].Value.ToString();
                    string tenNCC = rowEditingNCC.Cells["TenNCC"].Value.ToString();
                    string maSoThue = rowEditingNCC.Cells["Masothue"].Value.ToString();
                    string diaChi = rowEditingNCC.Cells["Diachi"].Value.ToString();
                    string sdt = rowEditingNCC.Cells["Sodienthoai"].Value.ToString();

                    // Kiểm tra điều kiện số điện thoại
                    if (!Regex.IsMatch(sdt, @"^0\d{9}$"))
                    {
                        string errorMsg = Thread.CurrentThread.CurrentUICulture.Name == "vi-VN"
                            ? "Số điện thoại phải có 10 chữ số và bắt đầu bằng số 0!"
                            : "Phone number must be 10 digits and start with 0!";
                        MessageBox.Show(errorMsg, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    bool result = BLLQuanLyKho.Instance.UpdateNCC(maNCC, tenNCC, maSoThue, diaChi, sdt);
                    if (result)
                    {
                        string msg = Thread.CurrentThread.CurrentUICulture.Name == "vi-VN"
                            ? "Cập nhật thành công!"
                            : "Update successful!";
                        MessageBox.Show(msg, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtSearchNCC.Enabled = true;
                        LoadNCCList();
                    }
                    else
                    {
                        string err = Thread.CurrentThread.CurrentUICulture.Name == "vi-VN"
                            ? "Cập nhật thất bại!" : "Update failed!";
                        MessageBox.Show(err, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        for (int i = 0; i < rowEditingNCC.Cells.Count; i++)
                            rowEditingNCC.Cells[i].Value = originalNCCValues[i];
                    }
                }
                catch (Exception ex)
                {
                    string msg = Thread.CurrentThread.CurrentUICulture.Name == "vi-VN"
                        ? "Lỗi: " + ex.Message
                        : "Error: " + ex.Message;
                    MessageBox.Show(msg, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    for (int i = 0; i < rowEditingNCC.Cells.Count; i++)
                        rowEditingNCC.Cells[i].Value = originalNCCValues[i];
                }

                isEditingNCC = false;
                dgvNhaCungCap.ReadOnly = true;
                foreach (DataGridViewColumn col in dgvNhaCungCap.Columns)
                    col.ReadOnly = true;

                btnXoaNCC.Enabled = true;
                btnSuaNCC.Text = Thread.CurrentThread.CurrentUICulture.Name == "vi-VN" ? "Sửa" : "Edit";
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

        private void dgvNhaCungCap_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
