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

namespace Trang_chủ_Main_Page_
{
    public partial class customerControl : Form
    {
        String soDienThoaiSelected = "";
        bool menu_CusTomer_Add_Expand=false;
        BLL_QuanlyTCNS bLL_QuanlyTCNS = new BLL_QuanlyTCNS();
        bool isEdited = false;  // Biến kiểm tra xem có đang ở chế độ chỉnh sửa hay không
        DataGridViewRow rowEdited = null; // Dòng đang chỉnh sửa

        public customerControl()
        {
            InitializeComponent();
            dtg_CustomerList.ReadOnly = true;
        }

        //Load danh sách khách hàng
        private void customerControl_Load(object sender, EventArgs e)
        {
            dtg_CustomerList.DataSource = bLL_QuanlyTCNS.xemDSKH();
            dtg_CustomerList.Columns["Sodienthoai"].HeaderText = "Số điện thoại";
            dtg_CustomerList.Columns["Hoten"].HeaderText = "Họ tên";
            dtg_CustomerList.Columns["Diachi"].HeaderText = "Địa chỉ";
            dtg_CustomerList.Columns["Diemthuong"].HeaderText = "Điểm thưởng";
            dtg_CustomerList.Columns["Gioitinh"].HeaderText = "Giới tính";
            dtg_CustomerList.Columns["Hang"].HeaderText = "Hạng";
        }

        private void dtgCustomerList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void btnCustomerAdd_Click(object sender, EventArgs e)
        {
            Timer_Customer_Add.Start();
        }

        private void logTransition_Tick(object sender, EventArgs e)
        {

        }

        private void Timer_Customer_Add_Tick(object sender, EventArgs e)
        {
            if (menu_CusTomer_Add_Expand == false)
            {
                menu_CusTomer_Add.Height += 10;
                if (menu_CusTomer_Add.Height >= 400)
                {
                    Timer_Customer_Add.Stop();
                    menu_CusTomer_Add_Expand = true;
                }
            }
            else
            {
                menu_CusTomer_Add.Height -= 10;
                if (menu_CusTomer_Add.Height <= 0)
                {
                    Timer_Customer_Add.Stop();
                    menu_CusTomer_Add_Expand = false;
                }
            }
        }

        private void menu_CusTomer_Add_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnCustomerFilterOut_Click(object sender, EventArgs e)
        {

        }

        private void btn_Customer_Add_Click(object sender, EventArgs e)
        {
            Timer_Customer_Add.Start();
        }

        private void btnCustomerAdd_Send_Click(object sender, EventArgs e)
        {
            if (txt_Customer_Hoten.Text == "")
            {
                MessageBox.Show("Vui lòng nhập họ tên khách hàng");
            }
            else if (txt_Customer_SDT.Text == "")
            {
                MessageBox.Show("Vui lòng nhập số điện thoại khách hàng");
            }
            else if (txt_DiaChiKH.Text == "")
            {
                MessageBox.Show("Vui lòng nhập địa chỉ khách hàng");
            }
            else
            {
                    bool result = bLL_QuanlyTCNS.themKH(txt_Customer_Hoten.Text, txt_Customer_SDT.Text, txt_DiaChiKH.Text, cb_GioiTinhKH.SelectedItem.ToString());
                    if (result)
                    {
                        MessageBox.Show("Thêm khách hàng thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        customerControl_Load(sender, e);
                        Timer_Customer_Add.Start();
                        txt_Customer_Hoten.Text = "";
                        txt_Customer_SDT.Text = "";
                        txt_DiaChiKH.Text = "";
                }
                else
                    {
                        MessageBox.Show("Thêm khách hàng thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txt_Customer_Hoten.Text = "";
                        txt_Customer_SDT.Text = "";
                        txt_DiaChiKH.Text = "";
                }
            }
        }

        private void cb_GioiTinhKH_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btn_Customer_Delete_Click(object sender, EventArgs e)
        {
            if (soDienThoaiSelected != "")
            {
                if (bLL_QuanlyTCNS.xoaKH(soDienThoaiSelected))
                {
                    MessageBox.Show("Xóa khách hàng thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    customerControl_Load(sender, e);
                }
                else
                {
                    MessageBox.Show("Xóa khách hàng thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn khách hàng cần xóa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        //Lấy số điện thoại đã chọn

        private void dtg_CustomerList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                soDienThoaiSelected = dtg_CustomerList.Rows[e.RowIndex].Cells[0].Value.ToString();
            }

            //if(isEdited)
            //{
            //    dtg_CustomerList.Rows[e.RowIndex].ReadOnly = false;
            //}
        }


        private DataGridViewComboBoxCell cbAltGender;
        private DataGridViewComboBoxCell cbAltRank;

        private void btn_Customer_Change_Click(object sender, EventArgs e)
        {
            if(isEdited == false)
            {
                if (dtg_CustomerList.CurrentRow != null) // Kiểm tra có dòng nào đang chọn không
                {
                    isEdited = true;

                    dtg_CustomerList.ReadOnly = false; // Mở khóa tất cả các dòng
                    dtg_CustomerList.Columns["Sodienthoai"].ReadOnly = true;

                    foreach (DataGridViewRow row in dtg_CustomerList.Rows)
                    {
                        foreach (DataGridViewCell cell in row.Cells)
                        {
                            if (row == dtg_CustomerList.CurrentRow && cell.OwningColumn.Name != "Sodienthoai")
                            {
                                cell.ReadOnly = false; // Mở khóa
                            }
                            else
                            {
                                cell.ReadOnly = true; // Khóa lại các ô khác
                            }
                        }
                    }
                    dtg_CustomerList.CurrentRow.DefaultCellStyle.BackColor = Color.DarkGray;
                    dtg_CustomerList.CurrentRow.DefaultCellStyle.SelectionBackColor = Color.Gray;
                    btn_Customer_Add.Enabled = false;
                    btn_Customer_Delete.Enabled = false;
                    rowEdited = dtg_CustomerList.CurrentRow;

                    //Thêm combobox vào ô giới tính
                    cbAltGender = new DataGridViewComboBoxCell();
                    cbAltGender.Items.AddRange("Nam", "Nữ");
                    cbAltGender.Value = rowEdited.Cells[4].Value;
                    rowEdited.Cells[4] = cbAltGender;

                    cbAltRank = new DataGridViewComboBoxCell();
                    cbAltRank.Items.AddRange("Thành viên", "Bạc", "Vàng", "Kim cương");
                    cbAltRank.Value = rowEdited.Cells[5].Value;
                    rowEdited.Cells[5] = cbAltRank;

                }
                else
                {
                    MessageBox.Show("Vui lòng chọn một khách hàng để chỉnh sửa.");
                }
            }else
            {
                if (Regex.IsMatch(rowEdited.Cells[3].Value.ToString(), "[^0-9]"))
                {
                    MessageBox.Show("Điểm thưởng không chứa kí tự");
                }
                else
                {
                    try
                    {
                        if (bLL_QuanlyTCNS.suaKH(rowEdited.Cells[0].Value.ToString(), rowEdited.Cells[1].Value.ToString(), rowEdited.Cells[2].Value.ToString(), int.Parse(rowEdited.Cells[3].Value.ToString()), rowEdited.Cells[4].Value.ToString(), rowEdited.Cells[5].Value.ToString()))
                        {
                            MessageBox.Show("Sửa thông tin khách hàng thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            customerControl_Load(sender, e);
                            isEdited = false;
                            dtg_CustomerList.ReadOnly = true;
                            btn_Customer_Add.Enabled = true;
                            btn_Customer_Delete.Enabled = true;
                            rowEdited.DefaultCellStyle.BackColor = Color.White;
                            rowEdited.DefaultCellStyle.SelectionBackColor = Color.FromArgb(255, 69, 0);
                        }
                        else
                        {
                            MessageBox.Show("Sửa thông tin khách hàng thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Điểm thưởng không hợp lệ");

                    }
                }
            }
            
        }

        //Ngăn hộp thoại lỗi mặc định khi nhập kí tự vào điểm thưởng
        private void dtg_CustomerList_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false;
            if (e.Context.HasFlag(DataGridViewDataErrorContexts.Commit))
            {
                MessageBox.Show("Điểm thưởng sai định dạng", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txt_Customer_SearchBar_TextChanged(object sender, EventArgs e)
        {
            if(txt_Customer_SearchBar.Text == null)
            {
                customerControl_Load(sender, e);
            }else
            {
                    dtg_CustomerList.DataSource = bLL_QuanlyTCNS.timKiemKH(txt_Customer_SearchBar.Text);
            }
        }
    }
}
