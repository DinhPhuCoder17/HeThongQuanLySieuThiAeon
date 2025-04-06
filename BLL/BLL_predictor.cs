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
using Microsoft.ML;
using static PythonModel.LinearRegression;
namespace BLL
{
    public class BLL_predictor
    {
        DAL_predictor dAL_Predictor = new DAL_predictor();



            public static void Predict(DataGridView dgvPredictor, string timeRange = "week")
            {
                // Lấy danh sách các sản phẩm đã dự đoán
                List<(string MaHang, int SoLuongCanDat)> predictedProducts = CallPredictor(timeRange);

                // Lấy tất cả thông tin sản phẩm từ cơ sở dữ liệu
                List<DTO_predictorHelper> allProducts = DAL_predictor.GetAllProducts();

                // Xóa tất cả các dòng cũ trong DataGridView
                dgvPredictor.Rows.Clear();

                // Duyệt qua tất cả các sản phẩm đã dự đoán
                foreach (var pred in predictedProducts)
                {
                    // Nếu số lượng cần đặt là 0, bỏ qua sản phẩm này
                    if (pred.SoLuongCanDat == 0)
                        continue;

                    // Tìm thông tin sản phẩm tương ứng với mã hàng
                    DTO_predictorHelper info = allProducts.FirstOrDefault(x => x.Mahanghoa == pred.MaHang);
                    if (info != null)
                    {
                        // Tính toán thành tiền
                        decimal thanhTien = pred.SoLuongCanDat * info.Tiennhap;

                        // Thêm một dòng vào DataGridView với các thông tin sản phẩm
                        dgvPredictor.Rows.Add(
                            info.MaNCC,       // Mã nhà cung cấp
                            info.TenNCC,      // Tên nhà cung cấp
                            info.Mahanghoa,   // Mã hàng hóa
                            info.Tenhanghoa,  // Tên hàng hóa
                            pred.SoLuongCanDat,  // Số lượng cần đặt
                            info.Tiennhap,    // Giá nhập
                            thanhTien,        // Thành tiền
                            info.DanhMuc      // Danh mục
                        );
                    }
                }
            }

            public static List<(string MaHang, int SoLuongCanDat)> CallPredictor(string timeRange = "week")
            {
                // Lấy sản phẩm từ cơ sở dữ liệu
                List<DTO_predictor> dB_products = timeRange.ToLower() == "month"
                    ? DAL_predictor.GetProducts_Month()
                    : DAL_predictor.GetProducts_Week();

                // Tạo MLContext
                var context = new MLContext();

                // Tải mô hình đã huấn luyện
                var modelPath = "regression_model.zip";
                ITransformer model = context.Model.Load(modelPath, out var modelInputSchema);

                // Tạo prediction engine
                var predictionEngine = context.Model.CreatePredictionEngine<ProductData, ProductPrediction>(model);

                // Dự đoán số lượng cần đặt
                var result = new List<(string MaHang, int SoLuongCanDat)>();

                foreach (var product in dB_products)
                {
                    var productData = new ProductData { SoLuongDaBan = product.SoLuongDaBan };
                    var prediction = predictionEngine.Predict(productData);
                    var soLuongCanDat = Math.Max(prediction.SoLuongCanDat - product.SoLuongTon, 0);
                    result.Add((product.MaHang, (int)soLuongCanDat));
                }

                return result;
            }

            public DataTable timKiem(String tukhoa)
            {
                return dAL_Predictor.timKiem(tukhoa);
            }

            // Class dữ liệu cho việc huấn luyện
            public class ProductData
            {
                public float SoLuongDaBan { get; set; }  // Số lượng đã bán
                public float SoLuongBanTiepTheo { get; set; }  // Số lượng bán tiếp theo (label)
            }

            // Class dữ liệu cho việc dự đoán
            public class ProductPrediction
            {
                public float SoLuongCanDat { get; set; }  // Dự đoán số lượng cần đặt
            }
   



    
    }
}
