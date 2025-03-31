using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DTO_Account
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Manhanvien { get; set; }

        public DTO_Account(string manhanvien, string username, string password)
        {
            this.Manhanvien = manhanvien;
            this.Username = username;
            this.Password = password;
        }

    }
}
