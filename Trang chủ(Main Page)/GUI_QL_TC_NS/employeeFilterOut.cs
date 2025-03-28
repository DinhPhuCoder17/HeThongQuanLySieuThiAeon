using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using BLL;
using static Jenga.Theme;

namespace Trang_chủ_Main_Page_
{
    public partial class employeeFilterOut : Form
    {
        bool menuExpand = false;
        bool menuExpand_2=false;
        BLL_QuanlyTCNS bLL_QuanLyTCNS = new BLL_QuanlyTCNS();
        private readonly BLL_QuanlyTCNS bLL_QuanlyTCNS = new BLL_QuanlyTCNS();

        public employeeFilterOut()
        {
            InitializeComponent();
        }

        private void employeeFilterOut_Load(object sender, EventArgs e)
        {
           
            //Datagridview bảng nhân viên
            dtg_Employee.DataSource = bLL_QuanlyTCNS.xemDSNV();
            dtg_Employee.Columns[0].HeaderText = "Mã nhân viên";
            dtg_Employee.Columns[1].HeaderText = "Họ tên";
            dtg_Employee.Columns[2].HeaderText = "CCCD";
            dtg_Employee.Columns[3].HeaderText = "Ngày sinh";
            dtg_Employee.Columns[4].HeaderText = "Giới tính";
            dtg_Employee.Columns[5].HeaderText = "Địa chỉ";
            dtg_Employee.Columns[6].HeaderText = "Số điện thoại";

        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void employeeFilterOut_BindingContextChanged(object sender, EventArgs e)
        {

        }

        private void employeeFilterOut_SizeChanged(object sender, EventArgs e)
        {
          
        }

        private void menuContainer_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
          logTransition.Start();
        }

        private void menuContainer_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void logTransition_Tick(object sender, EventArgs e)
        {
            if (menuExpand == false)
            {
                menuContainer.Height += 10;
                if (menuContainer.Height >= 290)
                {
                    logTransition.Stop();
                    menuExpand = true;
                }
            }
            else
            {
                menuContainer.Height -= 10;
                if (menuContainer.Height <= 0)
                {
                    logTransition.Stop();
                    menuExpand = false;
                }
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            logTransition.Start();
        }

        private void menuContainer_Paint_2(object sender, PaintEventArgs e)
        {

        }

        private void guna2TextBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            //Datagridview bảng chấm công
            dtg_DSCC.DataSource = bLL_QuanlyTCNS.xemDSCC();
            dtg_DSCC.Columns[0].HeaderText = "ID";
            dtg_DSCC.Columns[1].HeaderText = "Thời Gian CN";
            dtg_DSCC.Columns[2].HeaderText = "CheckIn";
            dtg_DSCC.Columns[3].HeaderText = "CheckOut";
            dtg_DSCC.Columns[4].HeaderText = "Số Công";
            dtg_DSCC.Columns[5].HeaderText = "Trạng Thái";
            dtg_DSCC.Columns[6].HeaderText = "Mã Ca Làm";
            dtg_DSCC.Columns[7].HeaderText = "Mã Nhân Viên";
            logTransition_2.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (menuExpand_2 == false)
            {
                guna2Panel2.Height += 25;
                if (guna2Panel2.Height >= 900)
                {
                    logTransition_2.Stop();
                    menuExpand_2 = true;
                }
            }
            else
            {
                guna2Panel2.Height -= 25;
                if (guna2Panel2.Height <= 0)
                {
                    logTransition_2.Stop();
                    menuExpand_2 = false;
                }
            }
        }

        private void txt_Employee_SearchBar_TextChanged(object sender, EventArgs e)
        {
            if (txt_Employee_SearchBar.Text == "")
            {
                employeeFilterOut_Load(sender, e);
            }
            else
            {
                dtg_Employee.DataSource = bLL_QuanlyTCNS.timKiemNV(txt_Employee_SearchBar.Text);
            }
        }

        private void dtg_DSCC_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btn_BuCong_Send_Click_Click(object sender, EventArgs e)
        {
            if (txt_ThoiGianCapNhat.Text == "")
            {
                MessageBox.Show("Vui lòng nhập thời gian cập nhật!");
            }
            else if (txt_MaCalam.Text == "")
            {
                MessageBox.Show("Vui lòng nhập mã ca làm!");
            }
            else if (txt_CheckIn.Text == "")
            {
                MessageBox.Show("Vui lòng nhập thời gian check In");
            }
            else if (txt_CheckOut.Text == "")
            {
                MessageBox.Show("Vui lòng nhập thời gian check Out");
            }
            else if (txt_MaNhanVien.Text == "")
            {
                MessageBox.Show("Vui lòng nhập mã nhân viên");
            }
            else
            {
                // Chuyển đổi thời gian cập nhật (DateTime)
                if (!DateTime.TryParse(txt_ThoiGianCapNhat.Text, out DateTime thoiGianCN))
                {
                    MessageBox.Show("Thời gian cập nhật không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Chuyển đổi thời gian Check-in
                if (!TimeSpan.TryParse(txt_CheckIn.Text, out TimeSpan checkIn))
                {
                    MessageBox.Show("Giờ Check-in không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Chuyển đổi thời gian Check-out
                if (!TimeSpan.TryParse(txt_CheckOut.Text, out TimeSpan checkOut))
                {
                    MessageBox.Show("Giờ Check-out không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                // Chuyển đổi chuỗi thành TimeSpan
                if (TimeSpan.TryParse(txt_CheckIn.Text, out TimeSpan checkIn1) &&
                    TimeSpan.TryParse(txt_CheckOut.Text, out TimeSpan checkOut1))
                {
                    // Kiểm tra CheckIn phải nhỏ hơn CheckOut
                    if (checkIn1 >= checkOut1)
                    {
                        MessageBox.Show("Giờ Check-in phải nhỏ hơn giờ Check-out!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Giờ Check-in hoặc Check-out không hợp lệ! Định dạng hợp lệ: HH:mm:ss", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }


                // Gọi phương thức thêm chấm công với dữ liệu đúng kiểu
                bool result = bLL_QuanlyTCNS.ThemChamCong(thoiGianCN, checkIn, checkOut, txt_MaCalam.Text, txt_MaNhanVien.Text );

                // Kiểm tra kết quả
                if (result)
                {
                    MessageBox.Show("Thêm chấm công thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Thêm chấm công thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                if (result)
                {
                    MessageBox.Show("Thêm chấm công thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    employeeFilterOut_Load(sender, e);
                    logTransition.Start();
                    txt_CheckIn.Text = "";
                    txt_CheckOut.Text = "";
                    txt_MaCalam.Text = "";
                    txt_MaNhanVien.Text = "";
                    txt_ThoiGianCapNhat.Text = "";
                
                }
                else
                {
                    MessageBox.Show("Thêm chấm công thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    logTransition.Start();
                    txt_CheckIn.Text = "";
                    txt_CheckOut.Text = "";
                    txt_MaCalam.Text = "";
                    txt_MaNhanVien.Text = "";
                    txt_ThoiGianCapNhat.Text = "";
                }
            }
        }
    }
}

