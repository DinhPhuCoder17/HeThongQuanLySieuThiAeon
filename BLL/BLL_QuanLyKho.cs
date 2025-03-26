using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace BLL
{
    public class BLLQuanLyKho
    {
        private readonly DAL_QuanLyKho dAL_QuanLyKho = new DAL_QuanLyKho();
        public List<DTO_Hanghoa> hangHoa_NhapHang()
        {
            DataTable dt = DAL_QuanLyKho.hangHoa_NhapHang();
            List<DTO_Hanghoa> list = new List<DTO_Hanghoa>();

            string defaultImagePath = "Resources/z6338454431504_88b6a5b9be1edce907298e1dbea998ea.jpg";
            byte[] defaultImage = File.Exists(defaultImagePath) ? File.ReadAllBytes(defaultImagePath) : null;

            foreach (DataRow row in dt.Rows)
            {
                DTO_Hanghoa hangHoa = new DTO_Hanghoa
                {
                    TenHangHoa = row["Tenhanghoa"].ToString(),
                    NhaCC = row["TenNCC"].ToString(),
                    GiaNhap = Convert.ToSingle(row["Tiennhap"]),
                    HinhAnh = row["ImageData"] != DBNull.Value ? (byte[])row["ImageData"] : defaultImage
                };

                list.Add(hangHoa);
            }

            return list;
        }

        // Auto update trạng thái nhập hàng
        public void AutoUpdateTrangThaiNhapHang()
        {
            dAL_QuanLyKho.AutoUpdateTrangThaiNhapHang();
        }

        // Xem danh sách nhập hàng
        public DataTable xemDSNH()
        {
            return dAL_QuanLyKho.xemDSNH();
        }
}
}
