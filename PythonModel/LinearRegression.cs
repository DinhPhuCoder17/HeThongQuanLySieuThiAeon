using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Trainers;
using System.Linq;
using DTO;

namespace PythonModel
{
    public class LinearRegression
    {
        public class ProductData
        {
            public float SoLuongDaBan { get; set; }  // Số lượng đã bán
            public float SoLuongBanTiepTheo { get; set; }  // Số lượng bán tiếp theo (label)
        }

        public class ProductPrediction
        {
            public float SoLuongCanDat { get; set; }  // Dự đoán số lượng cần đặt
        }
        public static void TrainModel(List<DTO_predictor> dB_products)
        {
            var context = new MLContext();

            // Tạo dữ liệu huấn luyện từ danh sách các sản phẩm
            var trainingData = new List<ProductData>();
            foreach (var product in dB_products)
            {
                trainingData.Add(new ProductData { SoLuongDaBan = product.SoLuongDaBan, SoLuongBanTiepTheo = product.SoLuongTon });
            }

            // Chuyển dữ liệu thành IDataView
            var trainData = context.Data.LoadFromEnumerable(trainingData);

            // Xây dựng pipeline với hồi quy tuyến tính
            var pipeline = context.Regression.Trainers.Sdca(labelColumnName: "SoLuongBanTiepTheo", featureColumnName: "SoLuongDaBan");

            // Huấn luyện mô hình
            var model = pipeline.Fit(trainData);

            // Lưu mô hình đã huấn luyện vào tệp
            context.Model.Save(model, trainData.Schema, "regression_model.zip");
        }


     
    }
}
