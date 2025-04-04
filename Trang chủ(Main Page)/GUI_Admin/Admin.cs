using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;
using Guna.UI2.WinForms;
using Trang_chu_Main_Page_.GUI_Admin;

namespace Trang_chủ_Main_Page_
{
    public partial class Admin : Form
    {
        bool menuExpand_3=false;
        private bool isEditing = false; // Kiểm tra có đang sửa không
        private DataGridViewRow selectedRow;

        public Admin()
        {
            InitializeComponent();
           
        }

        private void container(object _form)
        {
            if (guna2Panel3.Controls.Count > 0)
                guna2Panel3.Controls.Clear();
            Form fm = _form as Form;
            fm.TopLevel = false;
            fm.FormBorderStyle = FormBorderStyle.None;
            fm.Dock = DockStyle.Fill;
            guna2Panel3.Controls.Add(fm);
            guna2Panel3.Tag = fm;
            fm.Show();

        }



        private void guna2ComboBoxVaiTro_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedRole = cb_role.SelectedItem.ToString();
            string role = "";

            if (selectedRole.Contains("Quản lý Kho")) role = "Kho";
            else if (selectedRole.Contains("Quản lý Nhân Sự/Tài Chính")) role = "TCNS";


            if (!string.IsNullOrEmpty(role))
            {
                container(new FormAddQL(role));
            }
            else if (selectedRole.Contains("Nhân Viên"))
            {
                container(new FormAddNV());
            }
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }
        private void btn_DSNhanVien_Click(object sender, EventArgs e)
        {
            menuTransition_2.Start();
        }

        private void guna2CirclePictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void guna2Panel3_Paint(object sender, PaintEventArgs e)
        {

        }


        private void LoadEmployeeList()
        {
            guna2DataGridView2.DataSource = BLL_Nhanvien.Instance.GetAllEmployees();
            if (guna2DataGridView2.DataSource != null)
            {
                if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                {
                    guna2DataGridView2.Columns["Manhanvien"].HeaderText = "Mã nhân viên";
                    guna2DataGridView2.Columns["Hoten"].HeaderText = "Họ Tên";
                    guna2DataGridView2.Columns["CCCD"].HeaderText = "CCCD";
                    guna2DataGridView2.Columns["Ngaysinh"].HeaderText = "Ngày sinh";
                    guna2DataGridView2.Columns["Gioitinh"].HeaderText = "Giới Tính";
                    guna2DataGridView2.Columns["Diachi"].HeaderText = "Địa Chỉ";
                    guna2DataGridView2.Columns["Sodienthoai"].HeaderText = "Số Điện Thoại";
                    guna2DataGridView2.Columns["VaiTro"].HeaderText = "Vai Trò";
                }
                else if (Thread.CurrentThread.CurrentUICulture.Name == "en-US")
                {
                    guna2DataGridView2.Columns["Manhanvien"].HeaderText = "Employee ID";
                    guna2DataGridView2.Columns["Hoten"].HeaderText = "Full Name";
                    guna2DataGridView2.Columns["CCCD"].HeaderText = "ID Card";
                    guna2DataGridView2.Columns["Ngaysinh"].HeaderText = "Date of Birth";
                    guna2DataGridView2.Columns["Gioitinh"].HeaderText = "Gender";
                    guna2DataGridView2.Columns["Diachi"].HeaderText = "Address";
                    guna2DataGridView2.Columns["Sodienthoai"].HeaderText = "Phone Number";
                    guna2DataGridView2.Columns["VaiTro"].HeaderText = "Role";
                }
            }
            guna2DataGridView2.ReadOnly = true; // Ban đầu không cho chỉnh sửa

        }
        private void Admin_Load(object sender, EventArgs e)
        {
            LoadEmployeeList();
            btnLuu.Enabled = false; // Nút Lưu bị vô hiệu hóa ban đầu
        }

        private void menuTransition_2_Tick(object sender, EventArgs e)
        {
            if (menuExpand_3 == false)
            {
                cb_role.Hide();
                guna2Panel1.Height += 25;
                if (guna2Panel1.Height >= 578)
                {
                    lbl_role.ForeColor =Color.FromArgb(255, 251, 234);
                    lbl_role_Add.ForeColor =Color.FromArgb(255, 251, 234);
                    cb_role.ForeColor =Color.FromArgb(255, 251, 234);
                    cb_role.FillColor = Color.FromArgb(255, 251, 234);
                  
                    menuTransition_2.Stop();
                    menuExpand_3 = true;
                }
            }
            else
            {
                cb_role.Show();
                guna2Panel1.Height -= 50;
                if (guna2Panel1.Height <= 0)
                {
                    lbl_role.ForeColor = Color.Black;
                    lbl_role_Add.ForeColor = Color.Black;
                    cb_role.ForeColor = Color.FromArgb(68, 88, 112);
                    cb_role.FillColor = Color.White;
                   
                    menuTransition_2.Stop();
                    menuExpand_3 = false;
                }
            }
        }

        private void guna2Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2DataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (isEditing)
            {
                string message, title;
                if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                {
                    message = "Vui lòng lưu lại thay đổi trước khi sửa tiếp.";
                    title = "Thông báo";
                }
                else // Mặc định là tiếng Anh
                {
                    message = "Please save changes before editing again.";
                    title = "Notification";
                }
                MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            btnXoa.Enabled = false;
            isEditing = true;
            selectedRow = guna2DataGridView2.CurrentRow; // Lưu dòng đang sửa

            //Thêm combobox vào ô giới tính
            cbAltGender = new DataGridViewComboBoxCell();
            cbAltGender.Items.AddRange("Nam", "Nữ");
            cbAltGender.Value = selectedRow.Cells[4].Value;
            selectedRow.Cells[4] = cbAltGender;

            if (selectedRow == null)
            {
                string message, title;

                if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                {
                    message = "Vui lòng chọn một nhân viên để sửa.";
                    title = "Thông báo";
                }
                else // Mặc định là tiếng Anh
                {
                    message = "Please select an employee to edit.";
                    title = "Notification";
                }

                MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else {
                guna2DataGridView2.ReadOnly = false;
                // Khóa tất cả các dòng trước khi mở dòng được chọn
                foreach (DataGridViewRow row in guna2DataGridView2.Rows)
                {
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        cell.ReadOnly = (row != selectedRow); // Chỉ mở khóa ô của dòng đang chỉnh sửa
                    }
                }


                guna2DataGridView2.Columns["Manhanvien"].ReadOnly = true;
                guna2DataGridView2.Columns["CCCD"].ReadOnly = true;
                selectedRow.DefaultCellStyle.BackColor = Color.DarkGray;
                selectedRow.DefaultCellStyle.SelectionBackColor = Color.Gray;
            }
            btnLuu.Enabled = true; // Kích hoạt nút Lưu
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (guna2DataGridView2.SelectedRows.Count == 0)
            {
                string _message, _title;

                if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                {
                    _message = "Vui lòng chọn một nhân viên để xóa.";
                    _title = "Thông báo";
                }
                else // Mặc định là tiếng Anh
                {
                    _message = "Please select an employee to delete.";
                    _title = "Notification";
                }

                MessageBox.Show(_message, _title, MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return;
            }

            string message, title;
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult dialog;

            if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
            {
                message = "Bạn có chắc chắn muốn xóa nhân viên này?";
                title = "Xác nhận";
            }
            else 
            {
                message = "Are you sure you want to delete this employee?";
                title = "Confirmation";
            }

            dialog = MessageBox.Show(message, title, buttons, MessageBoxIcon.Question);

            if (dialog == DialogResult.No)
            {
                return; 
            }


            string maNhanVien = guna2DataGridView2.SelectedRows[0].Cells["Manhanvien"].Value.ToString();

            bool result = BLL_Nhanvien.Instance.DeleteEmployee(maNhanVien);

            if (result)
            {
                if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                {
                    MessageBox.Show("Nhân viên đã được xóa (ẩn).", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else // Mặc định là tiếng Anh
                {
                    MessageBox.Show("The employee has been deleted (hidden).", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                LoadEmployeeList(); // Cập nhật lại danh sách
            }
            else
            {
                if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                {
                    MessageBox.Show("Xóa thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else // Mặc định là tiếng Anh
                {
                    MessageBox.Show("Deletion failed!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private DataGridViewComboBoxCell cbAltGender;
        // Sự kiện nhấn nút Lưu
        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (!isEditing) return;
            try
            {
                string maNhanVien = selectedRow.Cells["Manhanvien"].Value.ToString();
                string hoTen = selectedRow.Cells["Hoten"].Value.ToString();
                string cccd = selectedRow.Cells["CCCD"].Value.ToString();
                DateTime ngaySinh = Convert.ToDateTime(selectedRow.Cells["Ngaysinh"].Value);
                string gioiTinh = selectedRow.Cells["Gioitinh"].Value.ToString();
                string diaChi = selectedRow.Cells["Diachi"].Value.ToString();
                string soDienThoai = selectedRow.Cells["Sodienthoai"].Value.ToString();
                string vaiTro = selectedRow.Cells["Vaitro"].Value.ToString();
                DynamicCreateNhanVien dynamicCreateNhanVien = new DynamicCreateNhanVien();

                if (vaiTro == "Quản Lý" || vaiTro == "quản lý" || vaiTro == "Quản Lí" || vaiTro == "quản lí" || vaiTro == "quan ly" || vaiTro == "quan li" || vaiTro == "manager" || vaiTro =="Manager" || vaiTro == "Quan ly")
                {
                    dynamicCreateNhanVien.ShowDialog();

                    if(!dynamicCreateNhanVien.isFillingIn)
                    {
                        return;
                    }

                    vaiTro = dynamicCreateNhanVien.role;
                    BLL_Nhanvien.Instance.AddManager(maNhanVien, dynamicCreateNhanVien.userName, dynamicCreateNhanVien.passWord, dynamicCreateNhanVien.role);
                }



                if (string.IsNullOrEmpty(hoTen) || string.IsNullOrEmpty(cccd) || string.IsNullOrEmpty(diaChi) || string.IsNullOrEmpty(soDienThoai))
                {
                    if(Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                    {
                        MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Please fill in all the information!", "NOTIFICATION", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                if (!IsValidFullName(hoTen))
                {
                    if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                    {
                        MessageBox.Show("Họ tên không hợp lệ!", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }else
                    {
                        MessageBox.Show("Full name is not valid!", "NOTIFICATION", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                if (!IsValidCCCD(cccd))
                {
                    if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                    {
                        MessageBox.Show("CCCD không hợp lệ!", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }else
                    {
                        MessageBox.Show("ID card is not valid!", "NOTIFICATION", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                }
                if (!IsValidBirthDate(ngaySinh))
                {
                    if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                    {
                        MessageBox.Show("Tuổi không hợp lệ!", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Age is not valid!", "NOTIFICATION", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                        
                }
                if (!IsValidAddress(diaChi))
                {
                    if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                    {
                        MessageBox.Show("Địa chỉ không hợp lệ!", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Address is not valid!", "NOTIFICATION", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                if (!IsValidPhoneNumber(soDienThoai))
                {
                    if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                    {
                        MessageBox.Show("Số điện thoại không hợp lệ!", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Phone number is not valid!", "NOTIFICATION", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                if (!IsValidRoleNV(vaiTro))
                {
                    if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                    {
                        MessageBox.Show("Vai trò không hợp lệ!", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Role is not valid!", "NOTIFICATION", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                DialogResult askResult;
                if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                {
                    askResult = MessageBox.Show("Bạn có chắc chắn muốn sửa nhân viên này?",
                                         "Xác nhận",
                                         MessageBoxButtons.YesNo,
                                         MessageBoxIcon.Question);
                }
                else
                {
                    askResult = MessageBox.Show("Are you sure you want to edit this employee?",
                                         "Confirmation",
                                         MessageBoxButtons.YesNo,
                                         MessageBoxIcon.Question);
                }

                    
                if (askResult == DialogResult.Yes)
                {
                    bool result = BLL_Nhanvien.Instance.UpdateEmployee(maNhanVien, hoTen, cccd, ngaySinh, gioiTinh, diaChi, soDienThoai, vaiTro);

                    if (result)
                    {
                        if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                        {
                            MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else // Mặc định là tiếng Anh
                        {
                            MessageBox.Show("Update successful!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                        LoadEmployeeList(); // Cập nhật lại danh sách
                    }
                    else
                    {
                        if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                        {
                            MessageBox.Show("Cập nhật thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else // Mặc định là tiếng Anh
                        {
                            MessageBox.Show("Update failed!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                {
                    MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else // Mặc định là tiếng Anh
                {
                    MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }

            isEditing = false;
            guna2DataGridView2.ReadOnly = true; // Không cho chỉnh sửa nữa
            btnLuu.Enabled = false; // Vô hiệu hóa nút Lưu
            btnXoa.Enabled = true;
        }

        //Check Họ tên hợp lệ
        public static bool IsValidFullName(string fullName)
        {
            string pattern = @"^([A-ZĐ][a-zàáạảãâầấậẩẫăằắặẳẵèéẹẻẽêềếệểễìíịỉĩòóọỏõôồốộổỗơờớợởỡùúụủũưừứựửữỳýỵỷỹđ]+)(\s+[A-ZĐ][a-zàáạảãâầấậẩẫăằắặẳẵèéẹẻẽêềếệểễìíịỉĩòóọỏõôồốộổỗơờớợởỡùúụủũưừứựửữỳýỵỷỹđ]+){0,49}$";
            return Regex.IsMatch(fullName, pattern);
        }
        //Check tuổi
        public static bool IsValidBirthDate(DateTime birthDate)
        {
            int year = birthDate.Year;
            int currentYear = DateTime.Now.Year;
            int age = currentYear - year;
            return year >= 1900 && year <= currentYear && age >= 18;
        }
        //Check CCCD hợp lệ
        public static bool IsValidCCCD(string cccd)
        {
            string pattern = @"^\d{12}$";
            return Regex.IsMatch(cccd, pattern);
        }
        //Check SĐT hợp lệ
        public static bool IsValidPhoneNumber(string phoneNumber)
        {
            string pattern = @"^\d{10}$";
            return Regex.IsMatch(phoneNumber, pattern);
        }
        //Check địa chỉ hợp lệ
        public static bool IsValidAddress(string address)
        {
            string pattern = @"^[a-zA-ZĐđàáạảãâầấậẩẫăằắặẳẵèéẹẻẽêềếệểễìíịỉĩòóọỏõôồốộổỗơờớợởỡùúụủũưừứựửữỳýỵỷỹ0-9,./\- ]{5,500}$";
            return Regex.IsMatch(address, pattern);
        }

        //Check vai trò
        public static bool IsValidRoleNV(string address)
        {
            string pattern = @"^[a-zA-ZĐđàáạảãâầấậẩẫăằắặẳẵèéẹẻẽêềếệểễìíịỉĩòóọỏõôồốộổỗơờớợởỡùúụủũưừứựửữỳýỵỷỹ0-9,.\- ]{5,500}$";
            return Regex.IsMatch(address, pattern);
        }
    }
}
