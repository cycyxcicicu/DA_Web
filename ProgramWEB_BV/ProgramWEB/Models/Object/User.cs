using ProgramWEB.Define;
using ProgramWEB.Libary;
using ProgramWEB.Models.Data;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProgramWEB.Models.Object
{
    public class User
    {
        public string username { set; get; }
        public string maNhanSu { set; get; }
        public bool quyenAdmin { set; get; }
        public bool quyenQuanLy { set; get; }
        public string avatar { set; get; }
        protected QuanLyNhanSuContext context { get; set; }
        public User() { init(); }
        public User(string username, string maNhanSu, bool quyenAdmin, bool quyenQuanLy, string avatar)
        {
            this.username = username;
            this.maNhanSu = maNhanSu;
            this.quyenAdmin = quyenAdmin;
            this.quyenQuanLy = quyenQuanLy;
            this.avatar = avatar;
            init();
        }
        private void init()
        {
            if (context == null)
                context = new QuanLyNhanSuContext();
        }
        public static string login(string username, string password)
        {
            try
            {
                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                    return DefineError.loiDuLieuKhongHopLe;
                Data.TaiKhoan taiKhoan = new QuanLyNhanSuContext().TaiKhoans.Find(username);
                if (taiKhoan == null)
                    return "Tài khoản không tồn tại.";
                if (!BCrypt.Net.BCrypt.Verify(password, taiKhoan.TK_MatKhau))
                    return "Mật khẩu đăng nhập không chính xác.";
                if (kiemTraBiKhoaVaMoKhoa(taiKhoan.TK_TenDangNhap)) 
                    return taiKhoan.TK_ThoiGianMoKhoa != null ? "Tài khoản đang bị khóa, hãy chờ đến " +
                        taiKhoan.TK_ThoiGianMoKhoa.ToString() + "." : "Tài khoản của bạn đã bị khóa vĩnh viễn.";
                return string.Empty;
            } catch { }
            return DefineError.loiHeThong;
        }
        public static bool kiemTraBiKhoaVaMoKhoa(string username)
        {
            if (string.IsNullOrEmpty(username))
                return true;
            QuanLyNhanSuContext con = new QuanLyNhanSuContext();
            Data.TaiKhoan taiKhoan = con.TaiKhoans.Find(username);
            if (taiKhoan == null)
                return true;
            if (taiKhoan.TK_BiKhoa == false)
                return false;
            if (taiKhoan.TK_ThoiGianMoKhoa == null || taiKhoan.TK_ThoiGianMoKhoa > DateTime.Now)
                return true;
            taiKhoan.TK_BiKhoa = !taiKhoan.TK_BiKhoa;
            int check = new QuanLyNhanSuContext().SaveChanges();
            if (check == 0)
                return true;
            return false;
        }
        public NhanSu getNhanSu()
        {
            try
            {
                Data.NhanSu nhanSuData = context.NhanSus.Where(item => item.NS_Ma.Trim() == this.maNhanSu).FirstOrDefault();
                if (nhanSuData == null)
                    return null;
                NhanSu nhanSu = new NhanSu();
                Libary.Convert<NhanSu, Data.NhanSu>.ConvertObj(ref nhanSu, nhanSuData);
                return nhanSu;
            }
            catch { }
            return null;
        }
        public TaiKhoan getTaiKhoan()
        {
            try
            {
                Data.TaiKhoan data = context.TaiKhoans.Find(this.username);
                if (data == null)
                    return null;
                TaiKhoan result = new TaiKhoan();
                Libary.Convert<TaiKhoan, Data.TaiKhoan>.ConvertObj(ref result, data);
                return result;
            }
            catch { }
            return null;
        }
        public IEnumerable<BaoHiem> getBaoHiems()
        {
            try
            {
                IEnumerable<Data.BaoHiem> datas = context.BaoHiems.Where(item => item.NS_Ma.Trim() == this.maNhanSu).OrderBy(item => item.BH_NgayCap);
                if (datas == null)
                    return null;
                return Libary.Convert<BaoHiem, Data.BaoHiem>.ConvertObjs(datas);
            }
            catch { }
            return null;
        }
        public IEnumerable<ChamCong> getChamCongs()
        {
            try
            {
                IEnumerable<Data.ChamCong> datas = context.ChamCongs.Where(item => item.NS_Ma.Trim() == this.maNhanSu).OrderBy(item => item.CC_Ngay);
                if (datas == null)
                    return null;
                return Libary.Convert<ChamCong, Data.ChamCong>.ConvertObjs(datas);
            }
            catch { }
            return null;
        } 
        public IEnumerable<KhenThuongKyLuat> getKhenThuongKyLuats()
        {
            try
            {
                IEnumerable<Data.KhenThuongKyLuat> datas = context.KhenThuongKyLuats.Where(item => item.NS_Ma.Trim() == this.maNhanSu).OrderBy(item => item.KTKL_HinhThuc);
                if (datas == null)
                    return null;
                return Libary.Convert<KhenThuongKyLuat, Data.KhenThuongKyLuat>.ConvertObjs(datas);
            }
            catch { }
            return null;
        }
        public IEnumerable<LichSuLamViec> getLichSuLamViecs()
        {
            try
            {
                IEnumerable<Data.LichSuLamViec> datas = context.LichSuLamViecs.Where(item => item.NS_Ma.Trim() == this.maNhanSu).OrderBy(item => item.LSLV_NgayBatDau);
                if (datas == null)
                    return null;
                return Libary.Convert<LichSuLamViec, Data.LichSuLamViec>.ConvertObjs(datas);
            }
            catch { }
            return null;
        }
        public IEnumerable<HopDong> getHopDongs()
        {
            try
            {
                IEnumerable<Data.HopDong> datas = context.HopDongs.Where(item => item.NS_Ma.Trim() == this.maNhanSu).OrderBy(item => item.HD_NgayBatDau);
                if (datas == null)
                    return null;
                return Libary.Convert<HopDong, Data.HopDong>.ConvertObjs(datas);
            }
            catch { }
            return null;
        }
        public bool thayDoiAnhDaiDien(string newUrl)
        {
            try
            {
                if (string.IsNullOrEmpty(newUrl))
                    return false;
                Data.TaiKhoan taiKhoan = context.TaiKhoans.Find(this.username);
                if (taiKhoan == null) return false;
                taiKhoan.TK_AnhDaiDien = newUrl;
                int check = context.SaveChanges();
                if (check == 0)
                    return false;
                return true;
            } catch { }
            return false;
        }
        public string doiMatKhau(string oldPassword, string newPassword)
        {
            if (string.IsNullOrEmpty(oldPassword) || string.IsNullOrEmpty(newPassword) || oldPassword.Length < 6 || newPassword.Length < 6)
                return DefineError.loiDuLieuKhongHopLe;
            if (oldPassword == newPassword)
                return "Mật khẩu mới và mật khẩu cũ không được trùng nhau";
            Data.TaiKhoan taiKhoan = context.TaiKhoans.Find(this.username);
            if (taiKhoan == null)
                return DefineError.khongTonTai;
            if (!BCrypt.Net.BCrypt.Verify(oldPassword, taiKhoan.TK_MatKhau))
                return "Mật khẩu cũ không chính xác.";
            taiKhoan.TK_MatKhau = BCrypt.Net.BCrypt.HashPassword(newPassword);
            int check = context.SaveChanges();
            if (check == 0)
                return DefineError.loiHeThong;
            return string.Empty;
        }

        public static string thayDoiMaXacThuc(string email)
        {
            try
            {
                if (string.IsNullOrEmpty(email) || !StringHelper.IsValidEmail(email))
                    return DefineError.loiDuLieuKhongHopLe;
                QuanLyNhanSuContext context = new QuanLyNhanSuContext();
                Data.TaiKhoan taiKhoan = context.TaiKhoans.Where(item => item.NhanSu.NS_Email == email).FirstOrDefault();
                if (taiKhoan == null)
                    return "Email của bạn không tồn tại trong hệ thống.";
                taiKhoan.TK_MaXacThuc = StringHelper.TaoMaXacThuc();
                DateTime now = DateTime.Now;
                now = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, 0);
                taiKhoan.TK_ThoiGianTaoMa = now;
                int check = context.SaveChanges();
                if (check == 0)
                    return DefineError.loiHeThong;
                return string.Empty;
            } catch { }
            return DefineError.loiHeThong;
        }

        public static string kiemTraMaXacThuc(string email, string maXacThuc)
        {
            try
            {
                if (string.IsNullOrEmpty(email) || 
                    !StringHelper.IsValidEmail(email) || 
                    string.IsNullOrEmpty(maXacThuc) || 
                    maXacThuc.Length != 6 ||
                    maXacThuc.Where(item => item > '9' || item < '0').Count() > 1)
                    return DefineError.loiDuLieuKhongHopLe;
                QuanLyNhanSuContext context = new QuanLyNhanSuContext();
                Data.TaiKhoan taiKhoan = context.TaiKhoans.Where(item => item.NhanSu.NS_Email == email).FirstOrDefault();
                if (taiKhoan == null)
                    return "Email của bạn không tồn tại trong hệ thống.";
                if (taiKhoan.TK_MaXacThuc != maXacThuc)
                    return "Mã xác thực không chính xác";
                if (taiKhoan.TK_ThoiGianTaoMa.Value.AddMinutes(5) < DateTime.Now)
                    return "Mã xác thực này đã hết hạn sử dụng. Vui lòng gửi lại mã xác thực khác.";
                return string.Empty;
            }
            catch { }
            return DefineError.loiHeThong;
        }

        public IEnumerable<NgayNghi> getNgayNghis(int year = 1, int month = 1)
        {
            try
            {
                if (year < 0 || month < 0 || month > 12)
                {
                    year = DateTime.Now.Year;
                    month = DateTime.Now.Month;
                }
                return Convert<NgayNghi, Data.NgayNghi>.ConvertObjs(context.NgayNghis
                    .Where(item => item.NN_Ngay.Year == year && item.NN_Ngay.Month == month).OrderBy(item => item.NN_Ngay));
            } catch { }
            return null;
        }
        public IEnumerable<CaLam> getCaLams()
        {
            try
            {
                return Convert<CaLam, Data.CaLam>.ConvertObjs(context.CaLams.OrderBy(item => item.CL_TenCa));
            }
            catch { }
            return null;
        }
    }
}