using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DAL
{
    public class DAL_predictor
    {
        public static List<DTO_predictorHelper> GetAllProducts()
        {
            DataTable dt = DataProvider.Instance.ExecuteQuery("SELECT H.Mahanghoa, H.Tenhanghoa, N.MaNCC, N.TenNCC, H.Tiennhap FROM Hanghoa H JOIN Nhacungcap N ON H.MaNCC = N.MaNCC WHERE H.Xoa = 1");
            
            List <DTO_predictorHelper> list = new List<DTO_predictorHelper>();

            foreach (DataRow row in dt.Rows)
            {
                DTO_predictorHelper product = new DTO_predictorHelper
                {
                    Mahanghoa = row["Mahanghoa"].ToString(),
                    Tenhanghoa = row["Tenhanghoa"].ToString(),
                    MaNCC = row["MaNCC"].ToString(),
                    TenNCC = row["TenNCC"].ToString(),
                    Tiennhap = row.Field<decimal>("Tiennhap"),
                };

                list.Add(product);
            }

            return list;
        }




        public static List<DTO_predictor> GetProducts()
        {
            List<DTO_predictor> products = new List<DTO_predictor>();

            DataTable dtHangHoa = DataProvider.Instance.ExecuteQuery("SELECT Mahanghoa, Soluong FROM HangHoa where Xoa = 1");

            DataTable dtBanHang = DataProvider.Instance.ExecuteQuery("SELECT Mahanghoa, SUM(Soluong) AS TongSoLuong FROM HH_HDBH GROUP BY Mahanghoa");

            foreach (DataRow row in dtHangHoa.Rows)
            {
                string maHang = row["Mahanghoa"].ToString();
                int soLuongTon = Convert.ToInt32(row["Soluong"]);

                int soLuongDaBan = 0;
                DataRow[] foundRows = dtBanHang.Select($"Mahanghoa = '{maHang}'");
                if (foundRows.Length > 0)
                {
                    soLuongDaBan = Convert.ToInt32(foundRows[0]["TongSoLuong"]);
                }

                // Thêm vào danh sách
                products.Add(new DTO_predictor
                {
                    MaHang = maHang,
                    SoLuongDaBan = soLuongDaBan,
                    SoLuongTon = soLuongTon
                });
            }

            return products;
        }
    }
}
