using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Data.SqlClient;

namespace DAL
{
    public class DAL_EmployeeFilterOut
    {
        public DataTable loadEmployeeList()
        {
            return DataProvider.Instance.ExecuteQuery("Select Manhanvien, Hoten, CCCD, Ngaysinh, Gioitinh, Diachi, Sodienthoai From Nhanvien where Xoa = 1");
        }
    }
}
