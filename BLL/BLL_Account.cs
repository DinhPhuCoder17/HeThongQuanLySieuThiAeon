using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DAL;

namespace BLL
{
    public class BLL_Account
    {
        private static BLL_Account instance;

        public static BLL_Account Instance
        {
            get
            {
                if (instance == null)
                    instance = new BLL_Account();
                return instance;
            }
        }

        private BLL_Account() { }

        // Lấy role từ database
        public string GetRole(string username, string password)
        {
            return DAL_Account.Instance.GetRole(username, password);
        }

        //Lấy mã nhân viên từ database
        public string GetMaNV(string username, string password)
        {
            return DAL_Account.Instance.GetMaNV(username, password);
        }

        // Lấy Xoá từ bảng nhân viên
        public string CheckXoa(string username, string password)
        {
            return DAL_Account.Instance.CheckXoa(username, password);
        }
    }
}
