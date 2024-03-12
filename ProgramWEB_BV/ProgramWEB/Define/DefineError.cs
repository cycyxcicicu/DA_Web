using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProgramWEB.Define
{
    public class DefineError
    {
        public static string loiHeThong { get; } = "Có lỗi xảy ra, vui lòng thử lại sau.";
        public static string loiDuLieuKhongHopLe { get; } = "Thông tin gửi lên không hợp lệ.";
        public static string khongCoQuyen { get; } = "Bạn không có quyền sử dụng chức năng này.";
        public static string khongTonTai { get; } = "Thông tin gửi lên không tồn tại trong hệ thống.";
        public static string daTonTai { get; } = "Thông tin gửi lên đã tồn tại trong hệ thống.";
        public static string canDangNhap { get; } = "Bạn cần phải đăng nhập mới có thể sử dụng chức năng này.";
    }
}