using ProgramWEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProgramWEB.Define
{
    public class DefinePage
    {
        public static Page home { get; } = new Page("Trang chủ", "/Home/Index");
        public static Page home_XemNgayNghi { get; } = new Page("Ngày nghỉ", "/Home/XemNgayNghi");
        public static Page home_XemCaLam { get; } = new Page("Ca làm", "/Home/XemCaLam");
        public static Page user { get; } = new Page("Tài khoản của tôi", "/User/Index");
        public static Page user_ChangePassword { get; } = new Page("Đổi mật khẩu", "/User/ChangePassword");
        public static Page user_ForgetPassword { get; } = new Page("Quên mật khẩu", "/User/ForgetPassword");
        public static Page user_Authentication { get; } = new Page("Xác thực tài khoản", "/User/Authentication");
        public static Page user_Logout { get; } = new Page("Đăng xuất", "/User/Logout");
        public static Page user_Login { get; } = new Page("Đăng nhập", "/User/Login");
        public static Page user_Profile { get; } = new Page("Thông tin cá nhân", "/User/Profile");
        public static Page user_XacThucTaiKhoan { get; } = new Page("Xác thực tài khoản", "/User/XacThucTaiKhoan");
        public static Page management { get; } = new Page("Quản lý", "/Management/Index");
        public static Page management_NhanSu { get; } = new Page("Nhân sự", "/Management/NhanSu");
        public static Page management_PhongBan { get; } = new Page("Phòng ban", "/Management/PhongBan");
        public static Page management_BoPhan { get; } = new Page("Bộ phận", "/Management/BoPhan");
        public static Page management_KhenThuongKyLuat { get; } = new Page("Khen thưởng kỷ luật", "/Management/KhenThuongKyLuat");
        public static Page management_TaiKhoan { get; } = new Page("Tài khoản", "/Management/TaiKhoan");
        public static Page management_BaoHiem { get; } = new Page("Bảo hiểm", "/Management/BaoHiem");
        public static Page management_ChamCong { get; } = new Page("Chấm công", "/Management/ChamCong");
        public static Page management_HopDong { get; } = new Page("Hợp đồng", "/Management/HopDong");
        public static Page management_LichSuLamViec { get; } = new Page("Lịch sử làm việc", "/Management/LichSuLamViec");
        public static Page management_CaLam { get; } = new Page("Ca làm", "/Management/CaLam");
        public static Page management_NgayNghi { get; } = new Page("Ngày nghỉ", "/Management/NgayNghi");
        public static Page profile_NhanSu { get; } = new Page("Thông tin nhân sự", "/NhanSu/Profile");
        public static Page chamCong { get; } = new Page("Chấm công", "/ChamCong/Index");
    }
}