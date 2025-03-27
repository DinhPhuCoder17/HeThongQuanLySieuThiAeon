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
        private DAL_Account dal_account = new DAL_Account();

        public bool Login (string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return false;
            return dal_account.Login(username, password);
        }

        public DTO_Account GetAccountInfo(string username)
        {
            return dal_account.GetAccountByUsername(username);
        }

    }
}
