using System;
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
        public DataTable xemDSNV()
        {
            return dAL_QuanlyTCNS.xemDSNV();
        }

        public DataTable xemDSKH()
        {
            return dAL_QuanlyTCNS.xemDSKH();
        }

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

    }
}
