using System;

namespace ProgramWEB.Models.Object
{
    public class NhanSu
    {
        public string NS_Ma { get; set; }

        public string NS_HoVaTen { get; set; }

        public bool? NS_GioiTinh { get; set; }

        public DateTime? NS_NgaySinh { get; set; }

        public string NS_SoDienThoai { get; set; }

        public string NS_Email { get; set; }

        public string NS_DiaChi { get; set; }

        public string NS_SoCCCD { get; set; }

        public string NS_SoTaiKhoanNganHang { get; set; }

        public string NS_TenChuTaiKhoan { get; set; }

        public string NS_HocVan { get; set; }

        public DateTime? NS_NgayVao { get; set; }
        public NhanSu()
        {
            this.NS_Ma = string.Empty;
            this.NS_HoVaTen = string.Empty;
            this.NS_GioiTinh = null;
            this.NS_NgaySinh = null;
            this.NS_SoDienThoai = string.Empty;
            this.NS_Email = string.Empty;
            this.NS_DiaChi = string.Empty;
            this.NS_SoCCCD = string.Empty;
            this.NS_SoTaiKhoanNganHang = string.Empty;
            this.NS_TenChuTaiKhoan = string.Empty;
            this.NS_HocVan = string.Empty;
            this.NS_NgayVao = null;
        }
    }
}
