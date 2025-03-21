using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;
using DTO;
using Guna.UI2.WinForms;
using ServiceStack.OrmLite.Converters;
using Trang_chủ_Main_Page_;
using Trang_chu_Main_Page_.GUI_QL_TC_NS;
using static Jenga.Theme;


namespace Trang_chủ_Main_Page_
{
    public partial class employeeShift : Form
    {
        private DateTime today = DateTime.Today;
        BLL_QuanlyTCNS bLL_QuanlyTCNS = new BLL_QuanlyTCNS();
        bool menu_ChooseEmployee = false;
        bool addMode = false;
        bool shiftClick = false;
        bool editMode = false;
        Panel editPanel;
        String maCaLamChoose = "";
        Color editColor;
        Panel chooseShiftPanel;

        public employeeShift()
        {
            InitializeComponent();
        }

        private void guna2HtmlLabel1_Click(object sender, EventArgs e)
        {

        }

        private void guna2VScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {

        }
        private void employeeShift_FormClose(object sender, FormClosedEventArgs e)
        {
      
        }
        private EmployeeMainPage employeeMainPageInstance;
        private void guna2Button2_Click(object sender, EventArgs e)
        {
           
        }

        private void guna2HtmlLabel1_Click_1(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel2_Click(object sender, EventArgs e)
        {

        }

        private void employeeShift_Load(object sender, EventArgs e)
        {
            displayDay();
            dtp_Shift_Start.Format = DateTimePickerFormat.Custom;
            dtp_Shift_Start.CustomFormat = "dd/MM/yyyy HH:mm:ss";
            dtp_Shift_End.Format = DateTimePickerFormat.Custom;
            dtp_Shift_End.CustomFormat = "dd/MM/yyyy HH:mm:ss";
            dtp_Shift_Start.ShowUpDown = true;
            dtp_Shift_End.ShowUpDown = true;
            toMauThoiKhoaBieu();

            //Load danh sách nhân viên làm việc
            dtg_ChooseEmployee.DataSource = bLL_QuanlyTCNS.xemDSNVLamViec();
            DataGridViewCheckBoxColumn checkColumn = new DataGridViewCheckBoxColumn
            {
                HeaderText = "Chọn",    // Tiêu đề cột
                Name = "chkSelect",     // Tên cột (dùng để truy cập)
                Width = 50,             // Độ rộng cột
                TrueValue = true,       // Giá trị khi tích
                FalseValue = false,     // Giá trị khi bỏ tích
            };

            // Thêm cột vào DataGridView
            dtg_ChooseEmployee.ReadOnly = false;
            dtg_ChooseEmployee.Columns.Insert(0, checkColumn);
            dtg_ChooseEmployee.Columns[1].ReadOnly = true;
            dtg_ChooseEmployee.Columns[2].ReadOnly = true;

            //Khóa các thêm, xóa, sửa
            btnChooseEmployee.Enabled = false;
            dtp_Shift_End.Enabled = false;
            dtp_Shift_Start.Enabled = false;
            btn_Shift_Confirm.Enabled = false;
            txt_TenCa.Enabled = false;
            txt_Shift_Number.Enabled = false;

        }

        private void displayDay()
        {
            container_Date.Controls.Clear();
            // Tìm ngày Thứ Hai của tuần hiện tại
            DateTime monday = today.AddDays(-(int)today.DayOfWeek + 1);

            // Nếu hôm nay là Chủ Nhật, lùi về Thứ Hai tuần trước
            if (today.DayOfWeek == DayOfWeek.Sunday)
            {
                monday = today.AddDays(-6);
            }
            DateTime tempTime = monday;

            for (int i = 0; i < 7; i++)
            {
                UserControlDateNum dateNum = new UserControlDateNum();
                dateNum.setDay(tempTime.ToString("dd/MM/yyyy"));
                container_Date.Controls.Add(dateNum);

                // Tăng ngày lên 1 cho vòng lặp tiếp theo
                tempTime = tempTime.AddDays(1);
            }

        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {

        }

        private void guna2Panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Panel3_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {

        }

        private void label24_Click(object sender, EventArgs e)
        {

        }

        private void guna2DateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void guna2Panel3_Paint_2(object sender, PaintEventArgs e)
        {

        }

        private void btnPrevCalendar_Click(object sender, EventArgs e)
        {
            container_Date.Controls.Clear();
            today = today.AddDays(-7);

            // Tìm ngày Thứ Hai của tuần hiện tại
            DateTime monday = today.AddDays(-(int)today.DayOfWeek + 1);

            // Nếu hôm nay là Chủ Nhật, lùi về Thứ Hai tuần trước
            if (today.DayOfWeek == DayOfWeek.Sunday)
            {
                monday = today.AddDays(-6);
            }
            DateTime tempTime = monday;

            for (int i = 0; i < 7; i++)
            {
                UserControlDateNum dateNum = new UserControlDateNum();
                dateNum.setDay(tempTime.ToString("dd/MM/yyyy"));
                container_Date.Controls.Add(dateNum);

                // Tăng ngày lên 1 cho vòng lặp tiếp theo
                tempTime = tempTime.AddDays(1);
            }
            toMauThoiKhoaBieu();

        }

        private void btn_NextCalendar_Click(object sender, EventArgs e)
        {
            container_Date.Controls.Clear();
            today = today.AddDays(7);

            // Tìm ngày Thứ Hai của tuần hiện tại
            DateTime monday = today.AddDays(-(int)today.DayOfWeek + 1);

            // Nếu hôm nay là Chủ Nhật, lùi về Thứ Hai tuần trước
            if (today.DayOfWeek == DayOfWeek.Sunday)
            {
                monday = today.AddDays(-6);
            }
            DateTime tempTime = monday;

            for (int i = 0; i < 7; i++)
            {
                UserControlDateNum dateNum = new UserControlDateNum();
                dateNum.setDay(tempTime.ToString("dd/MM/yyyy"));
                container_Date.Controls.Add(dateNum);

                // Tăng ngày lên 1 cho vòng lặp tiếp theo
                tempTime = tempTime.AddDays(1);
            }
            toMauThoiKhoaBieu();

        }

        //Hàm tô màu thời khóa biểu
        private void toMauThoiKhoaBieu()
        {
            tableLayoutPanel1.Controls.Clear();
            DateTime startDate = today.AddDays(-(int)today.DayOfWeek + 1);

            // Nếu hôm nay là Chủ Nhật, lùi về Thứ Hai tuần trước
            if (today.DayOfWeek == DayOfWeek.Sunday)
            {
                startDate = today.AddDays(-6);
            }

            DateTime endDate = startDate.AddDays(7);
            Dictionary<String, DTO_Calam> listPhanCong = bLL_QuanlyTCNS.toMauThoiKhoaBieu(startDate.ToString("yyyy/MM/dd"), endDate.ToString("yyyy/MM/dd"));
            foreach (var item in listPhanCong)
            {
                DTO_Calam calam = item.Value;
                DayOfWeek dayOfWeek = calam.tgBatDau.DayOfWeek;
                switch (dayOfWeek)
                {
                    case DayOfWeek.Monday:
                        AddShiftPanel(calam.maCaLam, dayOfWeek, calam.tenCaLam, calam.tgBatDau, calam.tgKetThuc, 0, calam.soLuongNhanVien, calam.PC_Nhanvien);
                        break;
                    case DayOfWeek.Tuesday:
                        AddShiftPanel(calam.maCaLam, dayOfWeek, calam.tenCaLam, calam.tgBatDau, calam.tgKetThuc, 1, calam.soLuongNhanVien, calam.PC_Nhanvien);
                        break;
                    case DayOfWeek.Wednesday:
                        AddShiftPanel(calam.maCaLam, dayOfWeek, calam.tenCaLam, calam.tgBatDau, calam.tgKetThuc, 2, calam.soLuongNhanVien, calam.PC_Nhanvien);
                        break;
                    case DayOfWeek.Thursday:
                        AddShiftPanel(calam.maCaLam, dayOfWeek, calam.tenCaLam, calam.tgBatDau, calam.tgKetThuc, 3, calam.soLuongNhanVien, calam.PC_Nhanvien);
                        break;
                    case DayOfWeek.Friday:
                        AddShiftPanel(calam.maCaLam, dayOfWeek, calam.tenCaLam, calam.tgBatDau, calam.tgKetThuc, 4, calam.soLuongNhanVien, calam.PC_Nhanvien);
                        break;
                    case DayOfWeek.Saturday:
                        AddShiftPanel(calam.maCaLam, dayOfWeek, calam.tenCaLam, calam.tgBatDau, calam.tgKetThuc, 5, calam.soLuongNhanVien, calam.PC_Nhanvien);
                        break;
                    case DayOfWeek.Sunday:
                        AddShiftPanel(calam.maCaLam, dayOfWeek, calam.tenCaLam, calam.tgBatDau, calam.tgKetThuc, 6, calam.soLuongNhanVien, calam.PC_Nhanvien);
                        break;
                }
            }
            
        }

        // Hàm giảm độ sáng (darken) của màu
        private Color DarkenColor(Color color, int amount)
        {
            int r = Math.Max(color.R - amount, 0); // Giảm đỏ
            int g = Math.Max(color.G - amount, 0); // Giảm xanh lá
            int b = Math.Max(color.B - amount, 0); // Giảm xanh dương
            return Color.FromArgb(r, g, b);
        }

        private void AddShiftPanel(String maCalam, DayOfWeek day, string name, DateTime start, DateTime end, int column, int soLuong, List<String> batBuoc)
        {
            int timeStart = int.Parse(start.ToString("HH"));
            int timeEnd = int.Parse(end.ToString("HH"));
            //Nếu sau 21:00 thì tự động fill dầy cột
            if(end.TimeOfDay > TimeSpan.Parse("21:00"))
            {
                timeEnd = 23;
            }
            int height = (timeEnd - timeStart)*33;
            Color color = Color.IndianRed;
            if(batBuoc.Count < soLuong)
            {
                color = Color.FromArgb(255, 69, 0);
            }else if(batBuoc.Count == soLuong)
            {
                color = Color.FromArgb(0, 210, 106);
            }
            Panel panelCover = new Panel
            {
                Size = new Size(159, height),
                BackColor = color,
                BorderStyle = BorderStyle.FixedSingle
            };

            //Thêm sự kiện click vào panel
            panelCover.Click += Panelcover_Click;
            //Thêm hover vào panel
            panelCover.MouseEnter += (sender, e) =>
            {
                if(!editMode)
                {
                    panelCover.BackColor = DarkenColor(color, 40);
                }
            };
            panelCover.MouseLeave += (sender, e) =>
            {
                if (!editMode)
                {
                    panelCover.BackColor = color;
                }
            };

            Label lbNameShift = new Label
            {
                Text = name,
                AutoSize = false,
                TextAlign = ContentAlignment.TopCenter,
                ForeColor = Color.White,
                Font = new Font("Microsoft Sans Serif", 11, FontStyle.Bold),
                Location = new Point(26, 20),              
            };

            Label lbDetailShift = new Label
            {
                Text = $"{start:HH:mm} - {end:HH:mm}",
                AutoSize = false,
                TextAlign = ContentAlignment.TopCenter,
                ForeColor = Color.White,
                Font = new Font("Microsoft Sans Serif", 9, FontStyle.Italic),
                Location = new Point(23, 40)
            };

            Label lbDetailQuantity = new Label
            {
                Text = $"Số lượng: {batBuoc.Count} / {soLuong}",
                AutoSize = false,
                TextAlign = ContentAlignment.TopCenter,
                ForeColor = Color.White,
                Font = new Font("Microsoft Sans Serif", 9, FontStyle.Underline),
                Location = new Point(23, 60)
            };

            Label lbUnvisibleMaCaLam = new Label
            {
                Text = maCalam,
                AutoSize = false,
                TextAlign = ContentAlignment.TopCenter,
                ForeColor = Color.White,
                Font = new Font("Microsoft Sans Serif", 9, FontStyle.Underline),
                Location = new Point(23, 80),
                Visible = false
            };
            
            //Thêm label thời gian bắt đầu
            DateTimePicker dtpInvisibleTGBD = new DateTimePicker
            {
                Value = start,
                Visible = false,
            };

            //Thêm label thời gian kết thúc
            DateTimePicker dtpInvisibleTGKT = new DateTimePicker
            {
                Value = end,
                Visible = false,
            };

            //Thêm sự kiện click vào các label
            lbNameShift.Click += allPanel_Click;
            lbDetailShift.Click += allPanel_Click;
            lbDetailQuantity.Click += allPanel_Click;

            //Thêm label tên Ca làm
            panelCover.Controls.Add(lbNameShift);
            //Thêm label chi tiết Ca làm
            panelCover.Controls.Add(lbDetailShift);
            //Thêm label số lượng nhân viên
            panelCover.Controls.Add(lbDetailQuantity);
            //Thêm label mã ca làm
            panelCover.Controls.Add(lbUnvisibleMaCaLam);
            //Thêm DateTimePicker thời gian bắt đầu
            panelCover.Controls.Add(dtpInvisibleTGBD);
            //Thêm DateTimePicker thời gian kết thúc
            panelCover.Controls.Add(dtpInvisibleTGKT);

            //Thêm panel vào tableLayoutPanel
            tableLayoutPanel1.Controls.Add(panelCover, column, timeStart - 6);
            tableLayoutPanel1.SetRowSpan(panelCover, height/33);
            panelCover.BringToFront();
        }

        //Su kien click vao panel
        private void Panelcover_Click(object sender, EventArgs e)
        {
            if(!editMode)
            {
                //Khóa chức năng chọn nhân viên trong trạng thái 
                dtg_ChooseEmployee.Columns["chkSelect"].ReadOnly = true;
                //Mở chức năng chọn nhân viên trong trạng thái thêm
                btnChooseEmployee.Enabled = true;
                shiftClick = true;
                btn_Shift_Edit.Enabled = true;
                if (!editMode)
                {
                    btn_Shift_Remove.Enabled = true;
                }
                Panel panel = (Panel)sender;
                maCaLamChoose = panel.Controls[3].Text;
                chooseShiftPanel = panel;
                List<String> listNhanVienHienTai = bLL_QuanlyTCNS.listNhanVienHienTai(maCaLamChoose);
                foreach (DataGridViewRow row in dtg_ChooseEmployee.Rows)
                {
                    if (listNhanVienHienTai.Contains(row.Cells[1].Value.ToString()))
                    {
                        row.Cells[0].Value = true;
                    }
                    else
                    {
                        row.Cells[0].Value = false;
                    }
                }

                txt_TenCa.Text = panel.Controls[0].Text;
                if (panel.Controls[4] is DateTimePicker dtpStart)
                {
                    dtp_Shift_Start.Value = dtpStart.Value;
                }
                if (panel.Controls[5] is DateTimePicker dtpEnd)
                {
                    dtp_Shift_End.Value = dtpEnd.Value;
                }
                String[] splitQuantity = panel.Controls[2].Text.Split('/');
                txt_Shift_Number.Text = splitQuantity[1].Trim();
            }
        }

        //Su kien click vao label của Panel Cover
        private void allPanel_Click(object sender, EventArgs e)
        {
            while (sender != null)
            {
                if (sender is Panel)
                {
                    Panelcover_Click(sender, e);
                    break;
                }else
                {
                    sender = ((Control)sender).Parent;
                }
            }
        }

        private void btnChooseEmployee_Click(object sender, EventArgs e)
        {
            timer_ChooseEmployee.Start();
        }

        //Hàm slide menu chọn nhân viên
        private void timer_ChooseEmployee_Tick(object sender, EventArgs e)
        {
            if (menu_ChooseEmployee == false)
            {
                pn_ChooseEmployee.Height += 20;
                if (pn_ChooseEmployee.Height >= 294)
                {
                    timer_ChooseEmployee.Stop();
                    menu_ChooseEmployee = true;
                }
            }
            else
            {
                pn_ChooseEmployee.Height -= 20;
                if (pn_ChooseEmployee.Height <= 0)
                {
                    timer_ChooseEmployee.Stop();
                    menu_ChooseEmployee = false;
                }
            }
        }


        //Chọn option Thêm nhân viên
        private void btn_Shift_Add_Click(object sender, EventArgs e)
        {
            addMode = true;

            //Vô hiệu hóa các chức năng khác
            btnChooseEmployee.Enabled = true;
            dtp_Shift_End.Enabled = true;
            dtp_Shift_Start.Enabled = true;
            btn_Shift_Confirm.Enabled = true;
            btn_Shift_Edit.Enabled = false;
            btn_Shift_Remove.Enabled = false;
            txt_TenCa.Enabled = true;
            txt_TenCa.Text = "";
            txt_Shift_Number.Enabled = true;
            txt_Shift_Number.Text = "";
            txt_TenCa.Focus();
            dtg_ChooseEmployee.Columns["chkSelect"].ReadOnly = false;

            foreach (DataGridViewRow row in dtg_ChooseEmployee.Rows)
            {
                if (Convert.ToBoolean(row.Cells[0].Value) == true)
                {
                    row.Cells[0].Value = false;
                }
            }

        }


        private void btn_Shift_Confirm_Click(object sender, EventArgs e)
        {
            //Nếu đang ở chế độ thêm
            if (addMode)
            {
                List<String> phanCongNhanVien = new List<string>();

                //Tạo list nhân viên phân công
                foreach (DataGridViewRow row in dtg_ChooseEmployee.Rows)
                {
                    if (Convert.ToBoolean(row.Cells[0].Value) == true)
                    {
                        phanCongNhanVien.Add(row.Cells[1].Value.ToString());
                    }
                }
                if (Regex.IsMatch(txt_Shift_Number.Text, "[^0-9]"))
                {
                    MessageBox.Show("Số lượng nhân viên không hợp lệ");
                    return;
                }
                else if (txt_TenCa.Text == "")
                {
                    MessageBox.Show("Tên ca làm không được để trống");
                }
                else if (dtp_Shift_Start.Value.ToString("dd/MM/yyyy") != dtp_Shift_End.Value.ToString("dd/MM/yyyy"))
                {
                    MessageBox.Show("Thời gian bắt đầu và kết thúc không hợp lệ");
                }
                else if (txt_Shift_Number.Text == "")
                {
                    MessageBox.Show("Số lượng nhân viên trống");
                }
                else if (int.Parse(txt_Shift_Number.Text) < 0)
                {
                    MessageBox.Show("Số lượng nhân viên không hợp lệ");
                }
                else if (dtp_Shift_Start.Value >= dtp_Shift_End.Value)
                {
                    MessageBox.Show("Thời gian bắt đầu phải nhỏ hơn thời gian kết thúc");
                }
                else if (phanCongNhanVien.Count == 0)
                {
                    MessageBox.Show("Chưa chọn nhân viên");
                }
                else
                {

                    DTO_Calam calam = new DTO_Calam(null, txt_TenCa.Text, dtp_Shift_Start.Value, dtp_Shift_End.Value, int.Parse(txt_Shift_Number.Text), phanCongNhanVien);
                    if (bLL_QuanlyTCNS.themCaLam(calam))
                    {
                        MessageBox.Show("Thêm ca làm thành công", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        toMauThoiKhoaBieu();
                    }
                    else
                    {
                        MessageBox.Show("Thêm ca làm thất bại", "Thất bại", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
                if (menu_ChooseEmployee)
                {
                    timer_ChooseEmployee.Start();
                }
                addMode = false;
                dtg_ChooseEmployee.Columns["chkSelect"].ReadOnly = true;
                foreach (DataGridViewRow row in dtg_ChooseEmployee.Rows)
                {
                    if (Convert.ToBoolean(row.Cells[0].Value) == true)
                    {
                        row.Cells[0].Value = false;
                    }
                }
            }
            else if(editMode)
            {
                if (txt_TenCa.Text == "")
                {
                    MessageBox.Show("Tên ca làm không được để trống");
                }
                else if (Regex.IsMatch(txt_Shift_Number.Text, "[^0-9]"))
                {
                    MessageBox.Show("Số lượng nhân viên không hợp lệ");
                }
                else if (txt_Shift_Number.Text == "")
                {
                    MessageBox.Show("Số lượng nhân viên trống");
                }
                else if (int.Parse(txt_Shift_Number.Text) < 0)
                {
                    MessageBox.Show("Số lượng nhân viên không hợp lệ");
                }
                else
                {
                    List<String> phanCongNhanVien = new List<string>();
                    foreach (DataGridViewRow row in dtg_ChooseEmployee.Rows)
                    {
                        if (Convert.ToBoolean(row.Cells[0].Value))
                        {
                            phanCongNhanVien.Add(row.Cells[1].Value.ToString());
                        }
                    }

                    //Kiểm tra số lượng nhân viên có khớp với số lượng nhân viên đã chọn không
                    
                    DTO_Calam calam = new DTO_Calam(editPanel.Controls[3].Text, txt_TenCa.Text, dtp_Shift_Start.Value, dtp_Shift_End.Value, int.Parse(txt_Shift_Number.Text), phanCongNhanVien);
                    if(bLL_QuanlyTCNS.suaCaLam(calam))
                    {
                        MessageBox.Show("Sửa ca làm thành công", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        toMauThoiKhoaBieu();
                    }else
                    {
                        MessageBox.Show("Sửa ca làm thất bại", "Thất bại", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        toMauThoiKhoaBieu();
                    }
                    editMode = false;
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn chức năng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            if(menu_ChooseEmployee)
            {
                timer_ChooseEmployee.Start();
            }
            dtg_ChooseEmployee.Columns["chkSelect"].ReadOnly = true;
            foreach (DataGridViewRow row in dtg_ChooseEmployee.Rows)
            {
                if (Convert.ToBoolean(row.Cells[0].Value) == true)
                {
                    row.Cells[0].Value = false;
                }
            }

            btn_Shift_Add.Enabled = true;
        }
        private void btn_Shift_Remove_Click(object sender, EventArgs e)
        {
            if (maCaLamChoose == "")
            {
                MessageBox.Show("Chưa chọn ca làm");
            }
            else
            {
                Color oldColor = chooseShiftPanel.BackColor;
                chooseShiftPanel.BackColor = Color.LightGray;
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa ca làm này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (result == DialogResult.Yes)
                {
                    if (bLL_QuanlyTCNS.xoaCaLam(maCaLamChoose))
                    {
                        MessageBox.Show("Xóa ca làm thành công", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        toMauThoiKhoaBieu();
                        btn_Shift_Add.Enabled = true;
                    }
                    else
                    {
                        MessageBox.Show("Xóa ca làm thất bại", "Thất bại", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        btn_Shift_Add.Enabled = true;
                    }
                }
                chooseShiftPanel.BackColor = oldColor;
            }
        }

        private void btn_Shift_Edit_Click(object sender, EventArgs e)
        {
            if(!addMode)
            {
                if (chooseShiftPanel != null)
                {
                    editMode = true;
                    addMode = false;
                    txt_TenCa.Enabled = true;
                    txt_Shift_Number.Enabled = true;
                    btn_Shift_Confirm.Enabled = true;
                    btn_Shift_Add.Enabled = false;
                    btn_Shift_Remove.Enabled = false;
                    dtg_ChooseEmployee.Columns["chkSelect"].ReadOnly = false;
                    editColor = chooseShiftPanel.BackColor;
                    editPanel = chooseShiftPanel;
                    editPanel.BackColor = Color.LightGray;
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn ca làm");
                }
            }else
            {
                MessageBox.Show("Đang ở chế độ thêm", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
