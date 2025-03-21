﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Trang_chủ_Main_Page_
{
    public partial class DSNhanVien : Form
    {
        public DSNhanVien()
        {
            InitializeComponent();
        }

        private void guna2GradientTileButton1_Click(object sender, EventArgs e)
        {

        }

        private void guna2GradientTileButton2_Click(object sender, EventArgs e)
        {

        }

        private void guna2DataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void DSNhanVien_Load(object sender, EventArgs e)
        {
            // Đảm bảo có đủ dòng trong DataGridView
            while (guna2DataGridView2.Rows.Count < 10)
            {
                guna2DataGridView2.Rows.Add();
            }

            // Dữ liệu mẫu cho 10 dòng
            string[,] data = new string[,]
            {
    { "1", "Nguyễn Văn A", "01-01-1990", "Nam", "Hà Nội", "001200012345", "nguyenvana@gmail.com", "0987654321", "Quản lý" },
    { "2", "Trần Thị B", "05-06-1992", "Nữ", "TP HCM", "001200045678", "tranthib@gmail.com", "0976543210", "Nhân viên" },
    { "3", "Lê Hoàng C", "15-09-1988", "Nam", "Đà Nẵng", "001200078912", "lehoangc@gmail.com", "0965432109", "Nhân viên" },
    { "4", "Phạm Thu D", "20-12-1995", "Nữ", "Hải Phòng", "001200034567", "phamthud@gmail.com", "0954321098", "Nhân viên" },
    { "5", "Hoàng Minh E", "02-03-1985", "Nam", "Cần Thơ", "001200056789", "hoangminhe@gmail.com", "0943210987", "Quản lý" },
    { "6", "Đặng Kim F", "18-07-1993", "Nữ", "Bình Dương", "001200067891", "dangkf@gmail.com", "0932109876", "Nhân viên" },
    { "7", "Võ Văn G", "30-10-1990", "Nam", "Nha Trang", "001200078912", "vovang@gmail.com", "0921098765", "Nhân viên" },
    { "8", "Bùi Hồng H", "12-04-1997", "Nữ", "Huế", "001200089123", "buihongh@gmail.com", "0910987654", "Nhân viên" },
    { "9", "Ngô Thanh I", "22-08-1986", "Nam", "Quảng Ninh", "001200091234", "ngothanhi@gmail.com", "0909876543", "Quản lý" },
    { "10", "Đinh Lan J", "07-11-1994", "Nữ", "Vũng Tàu", "001200012345", "dinhlanj@gmail.com", "0898765432", "Nhân viên" }
            };

            // Gán dữ liệu vào từng dòng của DataGridView
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 9; j++) // 9 cột
                {
                    guna2DataGridView2.Rows[i].Cells[j].Value = data[i, j];
                }
            }

        }
    }
}
