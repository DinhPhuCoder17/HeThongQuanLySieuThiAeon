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
namespace Trang_chủ_Main_Page_
{
    public partial class FormAddNV : Form
    {
        
        public FormAddNV()
        {
            InitializeComponent();
        }

        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {

        }

        private void btnTaoTaiKhoan_Click(object sender, EventArgs e)
        {
            string hoTen = txtHoTen.Text;
            string cccd = txtCCCD.Text;
            DateTime ngaySinh = guna2DateTimePicker1.Value;
            string gioiTinh = cmbGioiTinh.SelectedItem.ToString();
            string diaChi = txtDiaChi.Text;
            string sdt = txtSDT.Text;

            if (string.IsNullOrEmpty(hoTen) || string.IsNullOrEmpty(cccd) || string.IsNullOrEmpty(diaChi) || string.IsNullOrEmpty(sdt) ||
    cmbGioiTinh.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (BLL_Nhanvien.Instance.AddEmployee(hoTen, cccd, ngaySinh, gioiTinh, diaChi, sdt, "","",""))
            {
                MessageBox.Show("Thêm nhân viên thành công!", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Lỗi khi thêm nhân viên!", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
