using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Management;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;
using ClosedXML.Excel;
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
            Dictionary<String, DTO_Calam> listPhanCong = bLL_QuanlyTCNS.toMauThoiKhoaBieu(startDate.ToString("yyyy-MM-dd"), endDate.ToString("yyyy-MM-dd"));
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

            Label lbDetailQuantity = new Label();

            if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
            {
                lbDetailQuantity.Text = $"Số lượng: {batBuoc.Count} / {soLuong}";
                lbDetailQuantity.AutoSize = false;
                lbDetailQuantity.TextAlign = ContentAlignment.TopCenter;
                lbDetailQuantity.ForeColor = Color.White;
                lbDetailQuantity.Font = new Font("Microsoft Sans Serif", 9, FontStyle.Underline);
                lbDetailQuantity.Location = new Point(23, 60);
            }else
            {
                lbDetailQuantity.Text = $"Quantity: {batBuoc.Count} / {soLuong}";
                lbDetailQuantity.AutoSize = false;
                lbDetailQuantity.TextAlign = ContentAlignment.TopCenter;
                lbDetailQuantity.ForeColor = Color.White;
                lbDetailQuantity.Font = new Font("Microsoft Sans Serif", 9, FontStyle.Underline);
                lbDetailQuantity.Location = new Point(23, 60);
            }


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
            if(editMode)
            {
                if(Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                {
                    MessageBox.Show("Trong chế độ sửa");
                    return;
                }else
                {
                    MessageBox.Show("In editing mode");
                    return;
                }
            }

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
                    if(Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                    {
                        MessageBox.Show("Số lượng nhân viên không hợp lệ");
                        return;
                    }else
                    {
                        MessageBox.Show("Number of employees is not valid");
                        return;
                    }
                    
                }
                else if (txt_TenCa.Text == "")
                {
                    if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                    {
                        MessageBox.Show("Tên ca làm không được để trống");
                    }
                    else
                    {
                        MessageBox.Show("Shift name cannot be empty");
                    }
                }
                else if (dtp_Shift_Start.Value.ToString("dd/MM/yyyy") != dtp_Shift_End.Value.ToString("dd/MM/yyyy"))
                {
                    if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                    {
                        MessageBox.Show("Thời gian bắt đầu và kết thúc không hợp lệ");
                    }
                    else
                    {
                        MessageBox.Show("Start and end time are invalid");
                    }
                }
                else if (dtp_Shift_Start.Value < DateTime.Now)
                {
                    if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                    {
                        MessageBox.Show("Không thể xếp ca làm trong quá khứ");
                    }
                    else
                    {
                        MessageBox.Show("Cannot assign shifts in the past");
                    }
                }
                else if (txt_Shift_Number.Text == "")
                {
                    if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                    {
                        MessageBox.Show("Số lượng nhân viên trống");
                    }
                    else
                    {
                        MessageBox.Show("Number of employees is empty");
                    }
                }
                else if (int.Parse(txt_Shift_Number.Text) < 0)
                {
                    if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                    {
                        MessageBox.Show("Số lượng nhân viên không hợp lệ");
                    }
                    else
                    {
                        MessageBox.Show("Number of employees is not valid");
                    }
                }
                else if (dtp_Shift_Start.Value >= dtp_Shift_End.Value)
                {
                    if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                    {
                        MessageBox.Show("Thời gian bắt đầu phải nhỏ hơn thời gian kết thúc");
                    }
                    else
                    {
                        MessageBox.Show("Start time must be less than end time");
                    }
                }
                else if (phanCongNhanVien.Count == 0)
                {
                    if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                    {
                        MessageBox.Show("Chưa chọn nhân viên");
                    }
                    else
                    {
                        MessageBox.Show("No employee selected");
                    }
                }else if(int.Parse(txt_Shift_Number.Text) < phanCongNhanVien.Count)
                {
                    if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                    {
                        MessageBox.Show("Số lượng phân công nhân viên không hợp lệ");
                    }
                    else
                    {
                        MessageBox.Show("The assigned employee quantity is invalid");
                    }
                }else if(dtp_Shift_Start.Value.TimeOfDay < TimeSpan.Parse("06:00"))
                {
                    if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                    {
                        MessageBox.Show("Thời gian không hợp lệ - Trước 6:00");
                    }
                    else
                    {
                        MessageBox.Show("The time is invalid - Before 6:00");
                    }
                }

                else
                {
                    DialogResult result;
                    if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                    {
                        result = MessageBox.Show("Bạn có chắc chắn muốn thêm ca làm này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    }
                    else
                    {
                        result = MessageBox.Show("Are you sure you want to add this shift?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    }
                    if(result == DialogResult.No)
                    {
                        addMode = false;
                        dtg_ChooseEmployee.Columns["chkSelect"].ReadOnly = true;
                        foreach (DataGridViewRow row in dtg_ChooseEmployee.Rows)
                        {
                            if (Convert.ToBoolean(row.Cells[0].Value) == true)
                            {
                                row.Cells[0].Value = false;
                            }
                        }
                        return;
                    }
                    DTO_Calam calam = new DTO_Calam(null, txt_TenCa.Text, dtp_Shift_Start.Value, dtp_Shift_End.Value, int.Parse(txt_Shift_Number.Text), phanCongNhanVien);
                    if (bLL_QuanlyTCNS.themCaLam(calam))
                    {
                        if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                        {
                            MessageBox.Show("Thêm ca làm thành công", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            addMode = false;
                            toMauThoiKhoaBieu();
                        }
                        else
                        {
                            MessageBox.Show("Add shift successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            addMode = false;
                            toMauThoiKhoaBieu();
                        }
                    }
                    else
                    {
                        if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                        {
                            addMode = false;
                            MessageBox.Show("Thêm ca làm thất bại", "Thất bại", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            addMode = false;
                            MessageBox.Show("Add shift failed", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                if (menu_ChooseEmployee)
                {
                    timer_ChooseEmployee.Start();
                }
                //addMode = false;
                //dtg_ChooseEmployee.Columns["chkSelect"].ReadOnly = true;
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
                List<String> phanCongNhanVien = new List<string>();

                //Tạo list nhân viên phân công
                foreach (DataGridViewRow row in dtg_ChooseEmployee.Rows)
                {
                    if (Convert.ToBoolean(row.Cells[0].Value) == true)
                    {
                        phanCongNhanVien.Add(row.Cells[1].Value.ToString());
                    }
                }
                if (txt_TenCa.Text == "")
                {
                    if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                    {
                        MessageBox.Show("Tên ca làm không được để trống");
                    }
                    else
                    {
                        MessageBox.Show("Shift name cannot be empty");
                    }
                }
                else if (Regex.IsMatch(txt_Shift_Number.Text, "[^0-9]"))
                {
                    if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                    {
                        MessageBox.Show("Số lượng nhân viên không hợp lệ");
                    }
                    else
                    {
                        MessageBox.Show("Number of employees is not valid");
                    }
                }
                else if (txt_Shift_Number.Text == "")
                {
                    if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                    {
                        MessageBox.Show("Số lượng nhân viên trống");
                    }
                    else
                    {
                        MessageBox.Show("Number of employees is empty");
                    }
                }
                else if (int.Parse(txt_Shift_Number.Text) < 0)
                {
                    if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                    {
                        MessageBox.Show("Số lượng nhân viên không hợp lệ");
                    }
                    else
                    {
                        MessageBox.Show("Number of employees is not valid");
                    }
                }
                else if (int.Parse(txt_Shift_Number.Text) < phanCongNhanVien.Count)
                {
                    if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                    {
                        MessageBox.Show("Số lượng phân công nhân viên không hợp lệ");
                    }
                    else
                    {
                        MessageBox.Show("The assigned employee quantity is invalid");
                    }
                }   
                else
                {
                    DialogResult result;
                    if(Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                    {

                        result = MessageBox.Show("Bạn có chắc chắn muốn sửa ca làm này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    }else
                    {
                        result = MessageBox.Show("Are you sure you want to edit this shift?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    }

                    if (result == DialogResult.No)
                    {
                        editMode = false;
                        dtg_ChooseEmployee.Columns["chkSelect"].ReadOnly = true;
                        return;
                    }
                        DTO_Calam calam = new DTO_Calam(editPanel.Controls[3].Text, txt_TenCa.Text, dtp_Shift_Start.Value, dtp_Shift_End.Value, int.Parse(txt_Shift_Number.Text), phanCongNhanVien);
                    if (bLL_QuanlyTCNS.suaCaLam(calam))
                    {
                        if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                        {
                            MessageBox.Show("Sửa ca làm thành công", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            toMauThoiKhoaBieu();
                        }
                        else
                        {
                            MessageBox.Show("Edit shift successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            toMauThoiKhoaBieu();
                        }
                    }
                    else
                    {
                        if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                        {
                            MessageBox.Show("Sửa ca làm thất bại", "Thất bại", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            toMauThoiKhoaBieu();
                        }
                        else
                        {
                            MessageBox.Show("Edit shift failed", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            toMauThoiKhoaBieu();
                        }
                    }
                    editMode = false;
                }
            }
            else
            {
                if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                {
                    MessageBox.Show("Vui lòng chọn chức năng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Please select a function", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            if(menu_ChooseEmployee)
            {
                timer_ChooseEmployee.Start();
            }
            //dtg_ChooseEmployee.Columns["chkSelect"].ReadOnly = true;
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
            if (dtp_Shift_Start.Value < DateTime.Now)
            {
                if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                {
                    MessageBox.Show("Không thể xóa ca làm trong quá khứ");
                    return;
                }
                else
                {
                    MessageBox.Show("Can't remove shift in the past");
                    return;
                }
            }
            if (maCaLamChoose == "")
            {
                if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                {
                    MessageBox.Show("Chưa chọn ca làm");
                }
                else
                {
                    MessageBox.Show("No shift selected");
                }
            }
            else
            {
                Color oldColor = chooseShiftPanel.BackColor;
                chooseShiftPanel.BackColor = Color.LightGray;
                DialogResult result;
                if(Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                {
                    result = MessageBox.Show("Bạn có chắc chắn muốn xóa ca làm này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                }else
                {
                    result = MessageBox.Show("Are you sure you want to delete this shift?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                }
                if (result == DialogResult.Yes)
                {
                    if (bLL_QuanlyTCNS.xoaCaLam(maCaLamChoose))
                    {
                        if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                        {
                            MessageBox.Show("Xóa ca làm thành công", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Delete shift successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        toMauThoiKhoaBieu();
                        btn_Shift_Add.Enabled = true;
                    }
                    else
                    {
                        if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                        {
                            MessageBox.Show("Xóa ca làm thất bại", "Thất bại", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            MessageBox.Show("Delete shift failed", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        btn_Shift_Add.Enabled = true;
                    }
                }
                chooseShiftPanel.BackColor = oldColor;
            }
        }

        private void btn_Shift_Edit_Click(object sender, EventArgs e)
        {
            if (dtp_Shift_Start.Value < DateTime.Now)
            {
                if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                {
                    MessageBox.Show("Không thể sửa ca làm trong quá khứ");
                    return;
                }
                else
                {
                    MessageBox.Show("Can't change shift in the past");
                    return;
                }
            }
            if (!addMode)
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
                    if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                    {
                        MessageBox.Show("Vui lòng chọn ca làm");
                    }
                    else
                    {
                        MessageBox.Show("Please select a shift");
                    }
                }
            }else
            {
                if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                {
                    MessageBox.Show("Đang ở chế độ thêm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("In adding mode", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btn_ExportCalendar_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel Files|*.xlsx";
            saveFileDialog.Title = "Chọn nơi lưu file Excel";
            saveFileDialog.FileName = "DanhSach.xlsx"; // Tên file mặc địn
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (var workbook = new XLWorkbook())
                    {
                        var worksheet = workbook.Worksheets.Add("Sheet1");

                        

                        String[] columnLetterInfo = { "A", "B", "C" };
                        String[] columnLetter = { "D", "E", "F", "G", "H", "I", "J" };
                        String[] columnLetterGeneral = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" };

                        String[] columnDayOfWeek = { "Mon", "Tue", "Wed", "Thur", "Fri", "Sat", "Sun" };
                        String[] columnInfor = { "STT", "Họ Tên", "Vai Trò"};


                        DateTime monday = today.AddDays(-(int)today.DayOfWeek + 1);

                        // Nếu hôm nay là Chủ Nhật, lùi về Thứ Hai tuần trước
                        if (today.DayOfWeek == DayOfWeek.Sunday)
                        {
                            monday = today.AddDays(-6);
                        }

                        DateTime sunday = monday;

                        var range = worksheet.Range("A1:J1"); // Gộp ô A1 đến C1
                        range.Merge(); // Gộp ô
                                       // Thêm nội dung
                        range.Value = $"{monday.ToString("dd/MM/yyyy")} - {sunday.AddDays(7).ToString("dd/MM/yyyy")}";

                        // Căn giữa nội dung
                        range.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        range.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                        // In đậm chữ
                        range.Style.Font.Bold = true;

                        // Tăng cỡ chữ (tuỳ chọn)
                        range.Style.Font.FontSize = 14;
                        String cellAddress = "";
                        int i = 0;

                        //Add các ngày trong thứ của 1 tuần
                        foreach(String text in columnLetter)
                        {
                            cellAddress = $"{text}2";
                            worksheet.Cell(cellAddress).Value = columnDayOfWeek[i];
                            worksheet.Cell(cellAddress).Style.Font.Bold = true;
                            worksheet.Cell(cellAddress).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center; // Căn giữa ngang
                            worksheet.Cell(cellAddress).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center; // Căn giữa dọc
                            worksheet.Cell(cellAddress).Style.Border.OutsideBorder = XLBorderStyleValues.Thin; // Thêm viền ngoài

                            cellAddress = $"{text}3";
                            worksheet.Cell(cellAddress).Value = sunday.ToString("MMM dd");
                            worksheet.Cell(cellAddress).Style.Font.Bold = true;
                            worksheet.Cell(cellAddress).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center; // Căn giữa ngang
                            worksheet.Cell(cellAddress).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center; // Căn giữa dọc
                            worksheet.Cell(cellAddress).Style.Border.OutsideBorder = XLBorderStyleValues.Thin; // Thêm viền ngoài
                            sunday = sunday.AddDays(1);
                            i++;
                        }

                        i = 0;
                        //Add cột STT, Họ tên, Vai trò
                        foreach(String text in columnLetterInfo)
                        {
                            cellAddress = $"{text}3";
                            worksheet.Cell(cellAddress).Value = columnInfor[i];
                            worksheet.Cell(cellAddress).Style.Font.Bold = true;
                            worksheet.Cell(cellAddress).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center; // Căn giữa ngang
                            worksheet.Cell(cellAddress).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center; // Căn giữa dọc
                            worksheet.Cell(cellAddress).Style.Border.OutsideBorder = XLBorderStyleValues.Thin; // Thêm viền ngoài
                            i++;
                        }

                        DataTable dt = bLL_QuanlyTCNS.xemLichLamViecTheoNV(monday, sunday);
                        List<DTO_Nhanvien> listNV = new List<DTO_Nhanvien>();

                        foreach(DataRow row in dt.Rows)
                        {
                            DTO_Calam cl = new DTO_Calam()
                            {
                                tgBatDau = DateTime.Parse(row[3].ToString()),
                                tgKetThuc = DateTime.Parse(row[4].ToString()),
                            };

                            DTO_Nhanvien nv = new DTO_Nhanvien();
                            {
                                nv.maNhanvien = row[0].ToString();
                                nv.hoTen = row[1].ToString();
                                nv.vaiTro = row[2].ToString();
                                nv.lichLam.Add(cl);
                            }

                            if(listNV.Count != 0)
                            {
                                bool isExists = false;
                                foreach (DTO_Nhanvien nvCursor in listNV)
                                {
                                    if (nvCursor.MaNhanvien == nv.maNhanvien)
                                    {
                                        nvCursor.lichLam.Add(cl);
                                        isExists = true;
                                        break;
                                    }
                                }
                                if(!isExists)
                                {
                                    listNV.Add(nv);
                                }
                            }
                            else
                            {
                                listNV.Add(nv);
                            }
                        }

                        int stt = 1;
                        int j = 4;
                        i = 0;
                        int maxRow = 4;
                        foreach(DTO_Nhanvien nvWrite in listNV)
                        {
                            j = maxRow;
                            worksheet.Cell($"A{j}").Value = stt;
                            worksheet.Cell($"B{j}").Value = nvWrite.HoTen;
                            worksheet.Cell($"C{j}").Value = nvWrite.vaiTro;

                            foreach(DTO_Calam cl in nvWrite.lichLam)
                            {
                                int tmp = j;
                                var cell = worksheet.Cell(addressExcel(cl.tgBatDau, j));
                                while (!cell.IsEmpty())
                                {
                                    j++;
                                    cell = worksheet.Cell(addressExcel(cl.tgBatDau, j));
                                }
                                worksheet.Cell(addressExcel(cl.tgBatDau, j)).Value = $"{cl.tgBatDau.ToString("HH:mm")} - {cl.tgKetThuc.ToString("HH:mm")}";
                                if(maxRow < j)
                                {
                                    maxRow = j;
                                }
                                j = tmp;
                            }
                            worksheet.Range($"A{stt}:J{maxRow}").Style.Border.OutsideBorder = XLBorderStyleValues.Thin; // Thêm viền ngoài
                            
                            foreach(String key in columnLetterGeneral)
                            {
                                worksheet.Range($"{key}3:{key}{maxRow}").Style.Border.OutsideBorder = XLBorderStyleValues.Thin; // Thêm viền ngoài
                            }
                            stt++;
                            maxRow++;
                        }

                        worksheet.Columns().AdjustToContents(); // Tự động điều chỉnh chiều rộng cột
                        worksheet.Rows().AdjustToContents();
                        workbook.SaveAs(saveFileDialog.FileName);

                        if(Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                        {
                            MessageBox.Show("Xuất Excel thành công" + saveFileDialog.FileName, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }else
                        {
                            MessageBox.Show("Export Excel successfully" + saveFileDialog.FileName, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                catch(Exception ex)
                {
                    if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                    {
                        MessageBox.Show("Lỗi khi xuất Excel: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show("Error exporting Excel: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

        }
        private String addressExcel(DateTime date,int row)
        {
            switch(date.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    return $"D{row}";
                case DayOfWeek.Tuesday:
                    return $"E{row}";
                case DayOfWeek.Wednesday:
                    return $"F{row}";
                case DayOfWeek.Thursday:
                    return $"G{row}";
                case DayOfWeek.Friday:
                    return $"H{row}";
                case DayOfWeek.Saturday:
                    return $"I{row}";
                case DayOfWeek.Sunday:
                    return $"J{row}";
            }
            return "";
        }

        // ---------------- Phần của Quang ----------------------
        bool menuExpand_3 = false;
        private void tm_InforChanges_Tick(object sender, EventArgs e)
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
        private void pb_Avata_Click(object sender, EventArgs e)
        {
            tm_InforChanges.Start();
            lb_Ma.Text = Mainpage.CurrentUser.MaNhanvien;
            lb_Hoten.Text = Mainpage.CurrentUser.Hoten;
            lb_Ngaysinh.Text = Mainpage.CurrentUser.Ngaysinh;
            lb_Gioitinh.Text = Mainpage.CurrentUser.Gioitinh;
            lb_sdt.Text = Mainpage.CurrentUser.Sodienthoai;
        }

        
        //Check Password hợp lệ
        public static bool IsValidPassword(string password)
        {
            string pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*])[A-Za-z\d!@#$%^&*]{8,}$";
            return Regex.IsMatch(password, pattern);
        }

    }
}
