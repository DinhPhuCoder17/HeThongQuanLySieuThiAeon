﻿using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using DAL;
using DTO;

namespace BLL
{
    public class BLL_QuanlyTCNS
    {

        private readonly DAL_QuanlyTCNS dAL_QuanlyTCNS = new DAL_QuanlyTCNS();


        //Xem danh sách nhân viên
        public DataTable xemDSNV()
        {
            return dAL_QuanlyTCNS.xemDSNV();
        }

        //Xem danh sách khách hàng
        public DataTable xemDSKH()
        {
            return dAL_QuanlyTCNS.xemDSKH();
        }

        //Thêm khách hàng
        public Boolean themKH(String Hoten, String Sodienthoai, String Diachi, String Gioitinh)
        {
            if(Regex.IsMatch(Hoten, "[0-9]"))
            {
                MessageBox.Show("Họ tên không được chứa số");
                return false;
            }
            if(Regex.IsMatch(Sodienthoai, "[^0-9]"))
            {
                MessageBox.Show("Số điện thoại không được chứa kí tự");
                return false;
            }
            if (Sodienthoai.Length != 10)
            {
                MessageBox.Show("Số điện thoại không hợp lệ");
                return false;
            }
            DTO_Khachhang kh = new DTO_Khachhang(Hoten, Sodienthoai, Gioitinh, Diachi, 0, null, null);
            if(dAL_QuanlyTCNS.themKH(kh))
            {
                return true;
            }
            return false;
        }

        //Xóa khách hàng
        public Boolean xoaKH(String soDienThoai)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa khách hàng này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if(result == DialogResult.Yes)
            {
                if (dAL_QuanlyTCNS.xoaKH(soDienThoai))
                {
                    return true;
                }
                return false;
            }
            return false;
        }

        //Sửa khách hàng
        public bool suaKH(String Sodienthoai, String Hoten, String Diachi, int Diemthuong, String Gioitinh, String hang)
        {
            if (Regex.IsMatch(Hoten, "[0-9]"))
            {
                MessageBox.Show("Họ tên không được chứa số");
                return false;
            }
            else if (Diemthuong < 0)
            {
                MessageBox.Show("Điểm thưởng không hợp lệ");
                return false;
            }
            else
            {
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn sửa thông tin khách hàng này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (result == DialogResult.Yes) {
                    DTO_Khachhang kh = new DTO_Khachhang(Hoten, Sodienthoai, Gioitinh, Diachi, Diemthuong, hang, null);
                    if (dAL_QuanlyTCNS.suaKH(kh))
                    {
                        return true;
                    }
                }
            }
            return false;
        }


        //Tìm kiếm khách hàng
        public DataTable timKiemKH(String tukhoa)
        {
            return dAL_QuanlyTCNS.timKiemKH(tukhoa);
        }


        //Sắp xếp khách hàng
        public DataTable sapXepKH(int indexChon)
        {
            return dAL_QuanlyTCNS.sapXepKH(indexChon);
        }

        //Tìm kiếm nhân viên
        public DataTable timKiemNV(String tukhoa)
        {
            return dAL_QuanlyTCNS.timKiemNV(tukhoa);
        }
    }
}
