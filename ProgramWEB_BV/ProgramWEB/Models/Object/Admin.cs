using ProgramWEB.Define;
using ProgramWEB.Libary;
using ProgramWEB.Models.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Http.Results;

namespace ProgramWEB.Models.Object
{
    public class Admin : QuanLy
    {
        public Admin() : base()
        {

        }
        public Admin(string username, string maNhanSu, bool quyenAdmin, bool quyenQuanLy, string avatar) : base(username, maNhanSu, quyenAdmin, quyenQuanLy, avatar)
        {
        }
//Phong ban
        public string themPhongBan(Object.PhongBan result)
        {
            try
            {
                if (result == null)
                    return DefineError.loiDuLieuKhongHopLe;
                if (string.IsNullOrEmpty(result.PB_Ma) || string.IsNullOrEmpty(result.PB_Ten))
                    return DefineError.loiDuLieuKhongHopLe;
                Data.PhongBan old = context.PhongBans.Where(item => item.PB_Ma == result.PB_Ma).FirstOrDefault();
                if (old == null)
                {
                    if (!string.IsNullOrEmpty(result.NS_Ma)) {
                        if (context.NhanSus.Find(result.NS_Ma) == null)
                            return "Nhân sự không tồn tại.";
                        old = context.PhongBans.Where(item => item.NS_Ma == result.NS_Ma).FirstOrDefault();
                        if (old != null)
                            return "Một nhân sự không thể cai quản nhiều hơn một phòng ban.";
                    }
                    old = new Data.PhongBan();
                    Convert<Data.PhongBan, Object.PhongBan>.ConvertObj(ref old, result);
                    context.PhongBans.Add(old);
                    int check = context.SaveChanges();
                    if (check == 0)
                        return DefineError.loiHeThong;
                    return string.Empty;
                }
                return "Mã phòng ban đã tồn tại.";
            }
            catch { }
            return DefineError.loiHeThong;
        }
        public string suaPhongBan(Object.PhongBan New)
        {
            try
            {
                if (New == null)
                    return DefineError.loiDuLieuKhongHopLe;
                if (string.IsNullOrEmpty(New.PB_Ma) || string.IsNullOrEmpty(New.PB_Ten))
                    return DefineError.loiDuLieuKhongHopLe;
                Data.PhongBan old = context.PhongBans.Find(New.PB_Ma);
                if (old == null)
                    return "Phòng ban cần sửa không tồn tại.";
                if (!string.IsNullOrEmpty(New.NS_Ma))
                {
                    if (context.NhanSus.Find(New.NS_Ma) == null)
                        return "Nhân sự không tồn tại";
                    if (context.PhongBans.Where(
                        item => (item.PB_Ma != New.PB_Ma && (item.NS_Ma == New.NS_Ma))).Count() > 0)
                        return "Một nhân sự không thể cai quản nhiều hơn một phòng ban";
                }
                Convert<Data.PhongBan, Object.PhongBan>.ConvertObj(ref old, New);
                int checkInt = context.SaveChanges();
                if (checkInt == 0)
                    return DefineError.loiHeThong;
                return string.Empty;
            }
            catch
            {
                return DefineError.loiHeThong;
            }
        }
        public string xoaPhongBan(string ma)
        {
            try
            {
                if (string.IsNullOrEmpty(ma))
                    return DefineError.loiDuLieuKhongHopLe;
                Data.PhongBan del = context.PhongBans.Find(ma);
                if (del == null)
                    return DefineError.khongTonTai;
                context.PhongBans.Remove(del);
                int check = context.SaveChanges();
                if (check > 0)
                    return string.Empty;
            }
            catch { }
            return DefineError.loiHeThong;
        }
        public string[] xoaNhieuPhongBan(string[] mas)
        {
            string[] results = new string[] { string.Empty, string.Empty };
            try
            {
                if (mas == null)
                    return null;
                else
                {
                    string error = "";
                    string success = "";
                    int countSuccess = 0;
                    foreach (string s in mas)
                    {
                        string message = xoaPhongBan(s);
                        if (string.IsNullOrEmpty(message))
                            countSuccess++;
                    }
                    if (countSuccess > 0)
                        success = "Xoá thành công " + countSuccess + " phòng ban.";
                    if (countSuccess < mas.Length)
                        error = "Không thể xóa " + (mas.Length - countSuccess) + " phòng ban.";
                    results[0] = error;
                    results[1] = success;
                    return results;
                }
            }
            catch { }
            results[0] = DefineError.loiHeThong;
            return results;
        }
//Bo phan
        public string themBoPhan(Object.BoPhan result)
        {
            try
            {
                if (result == null)
                    return DefineError.loiDuLieuKhongHopLe;
                if (string.IsNullOrEmpty(result.PB_Ma) || string.IsNullOrEmpty(result.BP_Ma) || string.IsNullOrEmpty(result.BP_Ten))
                    return DefineError.loiDuLieuKhongHopLe;
                if (context.PhongBans.Find(result.PB_Ma) == null)
                    return "Phòng ban không tồn tại.";
                Data.BoPhan old = context.BoPhans.Where(item => item.BP_Ma == result.BP_Ma).FirstOrDefault();
                if (old == null)
                {
                    if (!string.IsNullOrEmpty(result.NS_Ma))
                    {
                        if (context.NhanSus.Find(result.NS_Ma) == null)
                            return "Nhân sự không tồn tại";
                        old = context.BoPhans.Where(item => item.NS_Ma == result.NS_Ma).FirstOrDefault();
                        if (old != null)
                            return "Một nhân sự không thể cai quản nhiều hơn một bộ phận.";
                    }
                    old = new Data.BoPhan();
                    Convert<Data.BoPhan, Object.BoPhan>.ConvertObj(ref old, result);
                    context.BoPhans.Add(old);
                    int check = context.SaveChanges();
                    if (check == 0)
                        return DefineError.loiHeThong;
                    return string.Empty;
                }
                return "Mã bộ phận đã tồn tại.";
            }
            catch { }
            return DefineError.loiHeThong;
        }
        public string suaBoPhan(Object.BoPhan New)
        {
            try
            {
                if (New == null)
                    return DefineError.loiDuLieuKhongHopLe;
                if (string.IsNullOrEmpty(New.PB_Ma) || string.IsNullOrEmpty(New.BP_Ma) || string.IsNullOrEmpty(New.BP_Ten))
                    return DefineError.loiDuLieuKhongHopLe;
                Data.BoPhan old = context.BoPhans.Find(New.BP_Ma);
                if (old == null)
                    return DefineError.khongTonTai;
                if (context.PhongBans.Find(New.PB_Ma) == null)
                    return "Phòng ban không tồn tại.";
                if (!string.IsNullOrEmpty(New.NS_Ma))
                {
                    if (context.NhanSus.Find(New.NS_Ma) == null)
                        return "Nhân sự không tồn tại";
                    if (context.BoPhans.Where(
                        item => (item.BP_Ma != New.BP_Ma && (item.NS_Ma == New.NS_Ma))).Count() > 0)
                        return "Một nhân sự không thể cai quản nhiều hơn một bộ phận";
                }
                Convert<Data.BoPhan, Object.BoPhan>.ConvertObj(ref old, New);
                int checkInt = context.SaveChanges();
                if (checkInt == 0)
                    return DefineError.loiHeThong;
                return string.Empty;
            }
            catch
            {
                return DefineError.loiHeThong;
            }
        }
        public string xoaBoPhan(string ma)
        {
            try
            {
                if (string.IsNullOrEmpty(ma))
                    return DefineError.loiDuLieuKhongHopLe;
                Data.BoPhan del = context.BoPhans.Find(ma);
                if (del == null)
                    return DefineError.khongTonTai;
                context.BoPhans.Remove(del);
                int check = context.SaveChanges();
                if (check > 0)
                    return string.Empty;
            }
            catch { }
            return DefineError.loiHeThong;
        }
        public string[] xoaNhieuBoPhan(string[] mas)
        {
            string[] results = new string[] { string.Empty, string.Empty };
            try
            {
                if (mas == null)
                    return null;
                else
                {
                    string error = "";
                    string success = "";
                    int countSuccess = 0;
                    foreach (string s in mas)
                    {
                        string message = xoaBoPhan(s);
                        if (string.IsNullOrEmpty(message))
                            countSuccess++;
                    }
                    if (countSuccess > 0)
                        success = "Xoá thành công " + countSuccess + " bộ phận.";
                    if (countSuccess < mas.Length)
                        error = "Không thể xóa " + (mas.Length - countSuccess) + " bộ phận.";
                    results[0] = error;
                    results[1] = success;
                    return results;
                }
            }
            catch { }
            results[0] = DefineError.loiHeThong;
            return results;
        }
//Tai khoan
        public string khoaTaiKhoan(string username, int timeLock = 0)
        {
            try
            {
                if (string.IsNullOrEmpty(username))
                    return DefineError.loiDuLieuKhongHopLe;
                Data.TaiKhoan rs = context.TaiKhoans.Find(username);
                if (rs == null)
                    return DefineError.khongTonTai;
                if (rs.TK_QuyenAdmin)
                    return "Bạn không thể khóa tài khoản của admin khác.";
                rs.TK_BiKhoa = true;
                if (timeLock > 0)
                {
                    DateTime now = DateTime.Now;
                    now = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, 0);
                    rs.TK_ThoiGianMoKhoa = now.AddHours(timeLock);
                }
                int check = context.SaveChanges();
                if (check == 0)
                    return DefineError.loiHeThong;
                return string.Empty;
            }
            catch { }
            return DefineError.loiHeThong;
        }
        public string capQuyenQuanLy(string username, bool huy = false)
        {
            try
            {
                if (string.IsNullOrEmpty(username))
                    return DefineError.loiDuLieuKhongHopLe;
                Data.TaiKhoan rs = context.TaiKhoans.Where(item => item.TK_TenDangNhap == username).FirstOrDefault();
                if (rs == null)
                    return DefineError.khongTonTai;
                if (!huy)
                    rs.TK_QuyenQuanLy = true;
                else
                {
                    if (rs.TK_QuyenAdmin)
                        return "Bạn không thể thao tác với tài khoản của admin khác.";
                    rs.TK_QuyenQuanLy = false;
                }
                int check = context.SaveChanges();
                if (check == 0)
                    return DefineError.loiHeThong;
                return string.Empty;
            }
            catch { }
            return DefineError.loiHeThong;
        }
//Ngay nghi
        public string themNgayNghi(NgayNghi New)
        {
            try
            {
                if (New == null || New.NN_Ngay == null)
                    return DefineError.loiDuLieuKhongHopLe;
                Data.NgayNghi old = context.NgayNghis.Where(o => o.NN_Ngay == New.NN_Ngay || o.NN_Ma == New.NN_Ma).FirstOrDefault();
                if (old == null)
                {
                    old = new Data.NgayNghi();
                    Convert<Data.NgayNghi, Object.NgayNghi>.ConvertObj(ref old, New);
                    context.NgayNghis.Add(old);
                    int check = context.SaveChanges();
                    if (check == 0)
                        return DefineError.loiHeThong;
                    return string.Empty;
                }
                string error = "";
                if (old.NN_Ma == New.NN_Ma)
                    error += "[Mã đã tồn tại]";
                if (old.NN_Ngay == New.NN_Ngay)
                    error += "[Ngày nghỉ này đã tồn tại]";
                return error;
            }
            catch { }
            return DefineError.loiHeThong;
        }
        public string suaNgayNghi(NgayNghi New)
        {
            try
            {
                if (New == null || New.NN_Ngay == null)
                    return DefineError.loiDuLieuKhongHopLe;
                Data.NgayNghi old = context.NgayNghis.Find(New.NN_Ma);
                if (old == null)
                    return DefineError.khongTonTai;
                if (context.NgayNghis.Where(item => (item.NN_Ma != New.NN_Ma && item.NN_Ngay == New.NN_Ngay)).Count() > 0)
                    return "[Ngày nghỉ này đã tồn tại]";
                old = new Data.NgayNghi();
                Convert<Data.NgayNghi, Object.NgayNghi>.ConvertObj(ref old, New);
                context.NgayNghis.AddOrUpdate(old);
                int check = context.SaveChanges();
                if (check == 0)
                    return DefineError.loiHeThong;
                return string.Empty;
            }
            catch { }
            return DefineError.loiHeThong; 
        }
        public string xoaNgayNghi(long ma)
        {
            try
            {
                if (ma < 0)
                    return DefineError.loiDuLieuKhongHopLe;
                Data.NgayNghi ngayNghi = context.NgayNghis.Find(ma);
                if (ngayNghi == null)
                    return DefineError.khongTonTai;
                context.NgayNghis.Remove(ngayNghi);
                int check = context.SaveChanges();
                if (check == 0)
                    return DefineError.loiHeThong;
                return string.Empty;
            }
            catch { }
            return DefineError.loiHeThong;
        }
        public string[] xoaNhieuNgayNghi(long[] mas)
        {
            string[] results = new string[] { string.Empty, string.Empty };
            try
            {
                if (mas == null)
                    return null;
                else
                {
                    string error = "";
                    string success = "";
                    int countSuccess = 0;
                    foreach (long s in mas)
                    {
                        string message = xoaNgayNghi(s);
                        if (string.IsNullOrEmpty(message))
                            countSuccess++;
                    }
                    if (countSuccess > 0)
                        success = "Xoá thành công " + countSuccess + " ngày nghỉ.";
                    if (countSuccess < mas.Length)
                        error = "Không thể xóa " + (mas.Length - countSuccess) + " ngày nghỉ.";
                    results[0] = error;
                    results[1] = success;
                    return results;
                }
            }
            catch { }
            results[0] = DefineError.loiHeThong;
            return results;
        }
//Ca lam
        public string themCaLam(CaLam New)
        {
            try
            {
                if (New == null || string.IsNullOrEmpty(New.CL_Ma) ||
                    string.IsNullOrEmpty(New.CL_TenCa) || New.CL_GioBatDau == null ||
                    New.CL_PhutBatDau == null || New.CL_GioKetThuc == null || 
                    New.CL_PhutKetThuc == null || New.CL_GioBatDau < 0 || New.CL_GioBatDau > 23 || New.CL_GioKetThuc < 0 || New.CL_GioKetThuc > 23 ||
                    New.CL_PhutBatDau < 0 || New.CL_PhutBatDau > 59 || New.CL_PhutKetThuc < 0 || New.CL_PhutKetThuc > 59)
                    return DefineError.loiDuLieuKhongHopLe;
                Data.CaLam old = context.CaLams.Where(o => (o.CL_Ma == New.CL_Ma || 
                                                        (o.CL_GioBatDau == New.CL_GioBatDau && 
                                                         o.CL_PhutBatDau == New.CL_PhutBatDau &&
                                                         o.CL_GioKetThuc == New.CL_GioKetThuc &&
                                                         o.CL_PhutKetThuc == New.CL_PhutKetThuc))
                                                         ).FirstOrDefault();
                if (old == null)
                {
                    old = new Data.CaLam();
                    Convert<Data.CaLam, CaLam>.ConvertObj(ref old, New);
                    context.CaLams.Add(old);
                    int check = context.SaveChanges();
                    if (check > 0)
                        return string.Empty;
                }
                if (New.CL_Ma == old.CL_Ma)
                    return "[Mã ca làm đã tồn tại]";
                return "[Thời gian của ca làm này đã tồn tại]";
            }
            catch { }
            return DefineError.loiHeThong;
        }
        public string suaCaLam(CaLam New)
        {
            try
            {
                if (New == null || string.IsNullOrEmpty(New.CL_Ma) ||
                    string.IsNullOrEmpty(New.CL_TenCa) || New.CL_GioBatDau == null ||
                    New.CL_PhutBatDau == null || New.CL_GioKetThuc == null ||
                    New.CL_PhutKetThuc == null || New.CL_GioBatDau < 0 || New.CL_GioBatDau > 23 || New.CL_GioKetThuc < 0 || New.CL_GioKetThuc > 23 ||
                    New.CL_PhutBatDau < 0 || New.CL_PhutBatDau > 59 || New.CL_PhutKetThuc < 0 || New.CL_PhutKetThuc > 59)
                    return DefineError.loiDuLieuKhongHopLe;
                Data.CaLam old = context.CaLams.Find(New.CL_Ma);
                if (old == null)
                    return DefineError.khongTonTai;
                if (context.CaLams.Where(o => o.CL_Ma != New.CL_Ma && 
                                                                (o.CL_GioBatDau == New.CL_GioBatDau &&
                                                                o.CL_PhutBatDau == New.CL_PhutBatDau &&
                                                                o.CL_GioKetThuc == New.CL_GioKetThuc &&
                                                                o.CL_PhutKetThuc == New.CL_PhutKetThuc)).Count() > 0)
                    return "[Thời gian của ca làm này đã tồn tại]";
                old = new Data.CaLam();
                Convert<Data.CaLam, Object.CaLam>.ConvertObj(ref old, New);
                context.CaLams.AddOrUpdate(old);
                int checkInt = context.SaveChanges();
                if (checkInt > 0)
                    return string.Empty;
            }
            catch { }
            return DefineError.loiHeThong;
        }
        public string xoaCaLam(string ma)
        {
            try
            {
                if (string.IsNullOrEmpty(ma))
                    return DefineError.loiDuLieuKhongHopLe;
                Data.CaLam caLam = context.CaLams.Find(ma);
                if (caLam == null)
                    return DefineError.khongTonTai;
                context.CaLams.Remove(caLam);
                int check = context.SaveChanges();
                if (check > 0)
                    return string.Empty;
            }
            catch { }
            return DefineError.loiHeThong; 
        }
        public string[] xoaNhieuCaLam(string[] mas)
        {
            string[] results = new string[] { string.Empty, string.Empty };
            try
            {
                if (mas == null)
                    return null;
                else
                {
                    string error = "";
                    string success = "";
                    int countSuccess = 0;
                    foreach (string s in mas)
                    {
                        string message = xoaCaLam(s);
                        if (string.IsNullOrEmpty(message))
                            countSuccess++;
                    }
                    if (countSuccess > 0)
                        success = "Xoá thành công " + countSuccess + " ca làm.";
                    if (countSuccess < mas.Length)
                        error = "Không thể xóa " + (mas.Length - countSuccess) + " ca làm.";
                    results[0] = error;
                    results[1] = success;
                    return results;
                }
            }
            catch { }
            results[0] = DefineError.loiHeThong;
            return results;
        }
    }
}