using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DAL_Customer
    {
        public DataTable loadCustomer()
        {
            return DataProvider.Instance.ExecuteQuery("Select Sodienthoai, Hoten, Diachi, Diemthuong, Gioitinh, Hang From Khachhang where Xoa = 1");
        }
    }
}
