import sys
import pandas as pd
import numpy as np
from sklearn.linear_model import LinearRegression
import joblib
import json

def main(csv_file):
    # -------------------------
    # PHAN 1: TAO MO HINH DU DOAN
    # -------------------------
    np.random.seed(42)
    so_luong_da_ban_train = np.random.randint(20, 100, 100)
    noise = np.random.uniform(-0.1, 0.1, 100)
    so_luong_ban_tiep_theo_train = so_luong_da_ban_train * (1 + noise)

    # Tao DataFrame chua du lieu huan luyen
    data_train = pd.DataFrame({
        'SoLuongDaBan': so_luong_da_ban_train,
        'SoLuongBanTiepTheo': so_luong_ban_tiep_theo_train
    })

    # Huan luyen mo hinh hoi quy tuyen tinh
    reg_model = LinearRegression()
    reg_model.fit(data_train[['SoLuongDaBan']], data_train['SoLuongBanTiepTheo'])

    # Luu mo hinh vao file (có thể sử dụng đường dẫn tương đối hoặc tuyệt đối)
    model_path = "C:\\Users\\ducx3\\Downloads\\HeThongQuanLySieuThiAeon\\regression_model.pkl"
    joblib.dump(reg_model, model_path)

    # -------------------------
    # PHAN 2: DU DOAN VA GOI Y NHAP HANG
    # -------------------------
    # Doc du lieu hang hoa tu file CSV duoc truyen vao
    data = pd.read_csv(csv_file)

    # Tham so lead_time (so ky de nhan hang)
    lead_time = 2

    # Tai mo hinh da huan luyen
    reg_model = joblib.load(model_path)

    # Ham tinh so luong can dat hang
    def tinh_goi_y(row):
        du_doan = reg_model.predict(pd.DataFrame([[row['SoLuongDaBan']]], columns=['SoLuongDaBan']))[0]
        so_luong_can_dat = max(du_doan * lead_time - row['SoLuongTon'], 0)
        return int(round(so_luong_can_dat)) 
    data['SoLuongCanDat'] = data.apply(tinh_goi_y, axis=1)

    # Chuyen doi DataFrame thanh mang 2 chieu [[MaHang, SoLuongCanDat], ...]
    result = data[['MaHang', 'SoLuongCanDat']].values.tolist()
    # Chuyen mang 2 chieu thanh mang phang: [MaHang1, SoLuongCanDat1, MaHang2, SoLuongCanDat2, ...]
    result_flat = [item for sublist in result for item in sublist]

    # Xuat ket qua (danh sach phang)
    print(result_flat)

if __name__ == "__main__":
    # Kiem tra doi so: can nhan duong dan file CSV
    if len(sys.argv) < 2:
        print("Chưa nhận được đường dẫn file CSV!")
        sys.exit(1)
    
    csv_file = sys.argv[1]
    main(csv_file)


