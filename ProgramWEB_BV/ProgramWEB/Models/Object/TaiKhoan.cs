using System;

namespace ProgramWEB.Models.Object
{
    public class TaiKhoan
    {
        public string TK_TenDangNhap { get; set; }
        public bool? TK_QuyenAdmin { get; set; }
        public bool? TK_QuyenQuanLy { get; set; }
        public bool? TK_BiKhoa { get; set; }
        public DateTime? TK_ThoiGianMoKhoa { get; set; }
        public string NS_Ma { get; set; }
        public string TK_AnhDaiDien { get; set; }
        public TaiKhoan()
        {
            this.TK_TenDangNhap = string.Empty;
            this.TK_QuyenAdmin = null;
            this.TK_BiKhoa = null;
            this.TK_QuyenQuanLy = null;
            this.TK_ThoiGianMoKhoa = null;
            this.NS_Ma = string.Empty;
            this.TK_AnhDaiDien = string.Empty;
        }
    }
}