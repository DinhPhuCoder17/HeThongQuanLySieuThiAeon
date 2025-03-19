using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DTO;
using System.Text.Json;
using System.Windows.Forms;
using System.Data;
using System.IO;
namespace BLL
{
    public class BLL_predictor
    {

        private readonly DAL_predictor dAL_predictor = new DAL_predictor();
        /*        public static void Predict(DataGridView dgvPredictor)
                {

                    List<(string MaHang, int SoLuongCanDat)> predictedProducts = CallPythonPredictor();
                    dgvPredictor.Rows.Clear();
                    foreach (var product in predictedProducts)
                    {
                        if (product.SoLuongCanDat != 0)
                        {
                            dgvPredictor.Rows.Add(product.MaHang, product.SoLuongCanDat);
                        }
                    }
                }*/
        public static void Predict(DataGridView dgvPredictor)
        {
            List<(string MaHang, int SoLuongCanDat)> predictedProducts = CallPythonPredictor();

            List<DTO_predictorHelper> allProducts = DAL_predictor.GetAllProducts();

            dgvPredictor.Rows.Clear();

            foreach (var pred in predictedProducts)
            {

                if (pred.SoLuongCanDat == 0) continue;

                DTO_predictorHelper info = allProducts
                    .FirstOrDefault(x => x.Mahanghoa == pred.MaHang);

                if (info != null)
                {
                    decimal thanhTien = pred.SoLuongCanDat * info.Tiennhap;

                    dgvPredictor.Rows.Add(
                        info.MaNCC,
                        info.TenNCC,
                        info.Mahanghoa,
                        info.Tenhanghoa,
                        pred.SoLuongCanDat,
                        info.Tiennhap,
                        thanhTien
                    );
                }
            }
        }



        private static List<(string MaHang, int SoLuongCanDat)> CallPythonPredictor()
        {
            List<DTO_predictor> dB_products = DAL_predictor.GetProducts();
            string csvFile = Path.Combine(Path.GetTempPath(), "products.csv");

            using (var writer = new StreamWriter(csvFile))
            {
                writer.WriteLine("MaHang,SoLuongDaBan,SoLuongTon");

                foreach (var product in dB_products)
                {
                    writer.WriteLine($"{product.MaHang},{product.SoLuongDaBan},{product.SoLuongTon}");
                }
            }

            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = "python",
                Arguments = $"\"predictor.py\" \"{csvFile}\"", 
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true,
                WorkingDirectory = AppDomain.CurrentDomain.BaseDirectory
            };

            using (Process process = Process.Start(psi))
            {
                process.WaitForExit();
                string result = process.StandardOutput.ReadToEnd().Trim();
     
                if (result.StartsWith("[") && result.EndsWith("]"))
                {
                    result = result.Substring(1, result.Length - 2);
                }


                string[] tokens = result.Split(',');

                for (int i = 0; i < tokens.Length; i++)
                {
                    tokens[i] = tokens[i].Trim().Trim('\'');
                }

                List<(string, int)> products = new List<(string, int)>();

                for (int i = 0; i < tokens.Length; i += 2)
                {
                    string maHang = tokens[i];
                    int soLuongCanDat = int.Parse(tokens[i + 1]);
                    products.Add((maHang, soLuongCanDat));
                }

                return products;
            }
        }

    }
}
