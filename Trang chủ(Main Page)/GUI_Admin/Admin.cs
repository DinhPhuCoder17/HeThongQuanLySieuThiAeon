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

namespace Trang_chủ_Main_Page_
{
    public partial class Admin : Form
    {
        bool menuExpand_3=false;
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
                FormAddQL form = new FormAddQL(role);
                container(form);
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
            //menuTransition_2.Start();
            // Kiểm tra nếu form đã mở rồi thì không mở nữa
            foreach (Form frm in Application.OpenForms)
            {
                if (frm is DSNhanVien)
                {
                    frm.Activate(); // Đưa form lên foreground
                    return;
                }
            }
            DSNhanVien ds = new DSNhanVien();
            ds.Show();
        }

        private void guna2CirclePictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void guna2Panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Admin_Load(object sender, EventArgs e)
        {
            //guna2DataGridView2.Rows.Add(1, "Nguyễn Văn B", "15/05/1990", "Nam", "Hà Nội", "012345678901", "nguyenvana@example.com", "0987654321", "Nhân viên");
            //guna2DataGridView2.Rows.Add(2, "Trần Thị B", "20/07/1995", "Nữ", "TP.HCM", "098765432109", "tranthib@example.com", "0912345678", "Nhân viên");
            //guna2DataGridView2.Rows.Add(3, "Lê Văn C", "10/02/1992", "Nam", "Đà Nẵng", "123456789012", "levanc@example.com", "0978123456", "Nhân viên");
            //guna2DataGridView2.Rows.Add(4, "Phạm Thị D", "05/11/1993", "Nữ", "Cần Thơ", "987654321098", "phamthid@example.com", "0965234789", "Nhân viên");
            //guna2DataGridView2.Rows.Add(5, "Hoàng Văn E", "25/12/1985", "Nam", "Hải Phòng", "567890123456", "hoangvane@example.com", "0956781234", "Nhân viên");
            //guna2DataGridView2.Rows.Add(6, "Đặng Thị F", "30/03/1998", "Nữ", "Bình Dương", "234567890123", "dangthif@example.com", "0945678123", "Nhân viên");
            //guna2DataGridView2.Rows.Add(7, "Vũ Văn G", "18/08/1996", "Nam", "Nghệ An", "345678901234", "vuvang@example.com", "0934567890", "Nhân viên");
            //guna2DataGridView2.Rows.Add(8, "Nguyễn Thị H", "09/09/1997", "Nữ", "Huế", "456789012345", "nguyenthih@example.com", "0923456789", "Nhân viên");
            //guna2DataGridView2.Rows.Add(9, "Bùi Văn I", "14/06/1994", "Nam", "Quảng Ninh", "567890123457", "buivani@example.com", "0912345670", "Nhân viên");
            //guna2DataGridView2.Rows.Add(10, "Dương Thị K", "22/04/1991", "Nữ", "Vũng Tàu", "678901234568", "duongthik@example.com", "0909876543", "Nhân viên");
            //guna2DataGridView2.Rows.Add(11, "Lý Văn L", "10/01/1987", "Nam", "Bắc Giang", "789012345678", "lyvanl@example.com", "0897654321", "Nhân viên");
            //guna2DataGridView2.Rows.Add(12, "Mai Thị M", "08/03/1992", "Nữ", "Bến Tre", "890123456789", "maithim@example.com", "0886543210", "Nhân viên");
            //guna2DataGridView2.Rows.Add(13, "Đỗ Hoàng N", "12/07/1995", "Nam", "Quảng Nam", "901234567890", "dohoangn@example.com", "0875432109", "Nhân viên");
            //guna2DataGridView2.Rows.Add(14, "Phan Thanh O", "28/09/1989", "Nam", "Gia Lai", "012345678901", "phanthanho@example.com", "0864321098", "Nhân viên");
            //guna2DataGridView2.Rows.Add(15, "Trịnh Thị P", "05/12/1994", "Nữ", "Kiên Giang", "123456789012", "trinhthip@example.com", "0853210987", "Nhân viên");

        }

        private void menuTransition_2_Tick(object sender, EventArgs e)
        {
            if (menuExpand_3 == false)
            {
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
    }
}
