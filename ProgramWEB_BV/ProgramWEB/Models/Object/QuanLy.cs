using PagedList;
using ProgramWEB.Define;
using ProgramWEB.Libary;
using ProgramWEB.Models.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Migrations;
using System.Linq;

namespace ProgramWEB.Models.Object
{
    public class QuanLy : User
    {
        public QuanLy() : base() 
        { 
            
        }
        public QuanLy(string username, string maNhanSu, bool quyenAdmin, bool quyenQuanLy, string avatar) : base(username, maNhanSu, quyenAdmin, quyenQuanLy, avatar)
        {
        }
//Nhan su
        public int demNhanSu()
        {
            try
            {
                return context.NhanSus.Count();
            } catch { }
            return 0;
        }
        public IEnumerable<Object.NhanSu> layDanhSachNhanSu()
        {
            try
            {
                
                if (context != null) 
                    return Convert<Object.NhanSu, Data.NhanSu>.ConvertObjs(context.NhanSus);      
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
        public List<IEnumerable<Object.NhanSu>> layDanhSachNhanSu(Object.NhanSu findBy = null, int page = 1, int pageSize = 10, string sortBy = "NS_Ma", bool sortTangDan = true)
        {
            try
            {
                IEnumerable<Object.NhanSu> results =
                    (findBy != null ? timKiemNhieuNhanSu(findBy)
                    : Convert<Object.NhanSu, Data.NhanSu>.ConvertObjs(from NhanSu in context.NhanSus select NhanSu));
                if (results == null)
                    return new List<IEnumerable<Object.NhanSu>>()
                    {
                        new List<Object.NhanSu>(),
                        new List<Object.NhanSu>()
                    };
                results = ObjectHelper.OrderByDynamic<Object.NhanSu>(results, sortBy, sortTangDan ? "asc" : "desc");
                int p = results.Count();
                if (p == 0)
                    return new List<IEnumerable<Object.NhanSu>>()
                    {
                        new List<Object.NhanSu>(),
                        new List<Object.NhanSu>()
                    };
                else
                    pageSize = pageSize < p ? pageSize : p;
                p = (p % pageSize) == 0 ? (p / pageSize) : (p / pageSize + 1);
                page = page > p ? p : page <= 0 ? 1 : page;
                return new List<IEnumerable<Object.NhanSu>>()
                {
                    results,
                    results.Count() > pageSize ? results.ToPagedList(page, pageSize) : results
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
        public IEnumerable<Object.NhanSu> timKiemNhieuNhanSu(Object.NhanSu findNhanSu = null)
        {
            try
            {
                if (findNhanSu != null)
                {
                    return Convert<Object.NhanSu, Data.NhanSu>.ConvertObjs(from NhanSu in context.NhanSus
                            where
                            (!string.IsNullOrEmpty(findNhanSu.NS_Ma) ? NhanSu.NS_Ma.StartsWith(findNhanSu.NS_Ma) : NhanSu != null) &&
                            (!string.IsNullOrEmpty(findNhanSu.NS_HoVaTen) ? NhanSu.NS_HoVaTen.StartsWith(findNhanSu.NS_HoVaTen) : NhanSu != null) &&
                            (findNhanSu.NS_GioiTinh != null ? NhanSu.NS_GioiTinh == findNhanSu.NS_GioiTinh : NhanSu != null) &&
                            (findNhanSu.NS_NgaySinh != null ? NhanSu.NS_NgaySinh == findNhanSu.NS_NgaySinh : NhanSu != null) &&
                            (!string.IsNullOrEmpty(findNhanSu.NS_SoDienThoai) ? NhanSu.NS_SoDienThoai.StartsWith(findNhanSu.NS_SoDienThoai) : NhanSu != null) &&
                            (!string.IsNullOrEmpty(findNhanSu.NS_Email) ? NhanSu.NS_Email.StartsWith(findNhanSu.NS_Email) : NhanSu != null) &&
                            (!string.IsNullOrEmpty(findNhanSu.NS_DiaChi) ? NhanSu.NS_DiaChi.StartsWith(findNhanSu.NS_DiaChi) : NhanSu != null) &&
                            (!string.IsNullOrEmpty(findNhanSu.NS_SoCCCD) ? NhanSu.NS_SoCCCD.StartsWith(findNhanSu.NS_SoCCCD) : NhanSu != null) &&
                            (!string.IsNullOrEmpty(findNhanSu.NS_SoTaiKhoanNganHang) ? NhanSu.NS_SoTaiKhoanNganHang.StartsWith(findNhanSu.NS_SoTaiKhoanNganHang) : NhanSu != null) &&
                            (!string.IsNullOrEmpty(findNhanSu.NS_TenChuTaiKhoan) ? NhanSu.NS_TenChuTaiKhoan.StartsWith(findNhanSu.NS_TenChuTaiKhoan) : NhanSu != null) &&
                            (!string.IsNullOrEmpty(findNhanSu.NS_HocVan) ? NhanSu.NS_HocVan.StartsWith(findNhanSu.NS_HocVan) : NhanSu != null) &&
                            (findNhanSu.NS_NgayVao != null ? NhanSu.NS_NgayVao == findNhanSu.NS_NgayVao : NhanSu != null)
                            select NhanSu);
                }
                return Convert<Object.NhanSu,Data.NhanSu>.ConvertObjs(context.NhanSus);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
        public Object.NhanSu timKiemMotNhanSu(string maNhanSu)
        {
            try
            {
                Object.NhanSu convert = new NhanSu();
                if (string.IsNullOrEmpty(maNhanSu))
                    return null;
                Convert<Object.NhanSu, Data.NhanSu>.ConvertObj(ref convert, context.NhanSus.Find(maNhanSu));
                return convert;
            } catch { }
            return null;
        }
        public string themNhanSu(Object.NhanSu nhanSu)
        {
            try
            {
                if (nhanSu == null)
                    return DefineError.loiDuLieuKhongHopLe;
                if (string.IsNullOrEmpty(nhanSu.NS_Ma) || string.IsNullOrEmpty(nhanSu.NS_HoVaTen) ||
                    nhanSu.NS_GioiTinh == null || nhanSu.NS_NgaySinh == null ||
                    string.IsNullOrEmpty(nhanSu.NS_SoDienThoai) || string.IsNullOrEmpty(nhanSu.NS_Email) ||
                    string.IsNullOrEmpty(nhanSu.NS_DiaChi) || string.IsNullOrEmpty(nhanSu.NS_SoCCCD) ||
                    nhanSu.NS_NgayVao == null)
                    return DefineError.loiDuLieuKhongHopLe;
                string error = "";
                if (!StringHelper.IsValidEmail(nhanSu.NS_Email))
                    error += "[Email không đúng định dạng]";
                if (!StringHelper.IsPhoneNbr(nhanSu.NS_SoDienThoai))
                    error += "[Số điện thoại không đúng định dạng]";
                if (!StringHelper.IsValidCCCD(nhanSu.NS_SoCCCD))
                    error += "[Số căn cước công dân không đúng định dạng]";
                if (DateTime.Now.Year - nhanSu.NS_NgaySinh.Value.Year < 16)
                    error += "[Nhân sự phải đủ từ 16 tuổi trở lên]";
                if (error.Length > 0)
                    return error;

                Data.NhanSu nhanSu1 = context.NhanSus.Where(
                    item => item.NS_Ma == nhanSu.NS_Ma || item.NS_SoCCCD == nhanSu.NS_SoCCCD ||
                    item.NS_Email == nhanSu.NS_Email || item.NS_SoDienThoai == nhanSu.NS_SoDienThoai).FirstOrDefault();
                if (nhanSu1 == null)
                {
                    nhanSu1 = new Data.NhanSu();
                    Convert<Data.NhanSu, Object.NhanSu>.ConvertObj(ref nhanSu1, nhanSu);
                    context.NhanSus.Add(nhanSu1);
                    int check = context.SaveChanges();
                    if (check == 0)
                        return DefineError.loiHeThong;
                    return string.Empty;
                }
                error = "";
                if (nhanSu1.NS_Ma == nhanSu.NS_Ma)
                    error += "[Mã nhân sự đã tồn tại]";
                if (nhanSu1.NS_SoCCCD == nhanSu.NS_SoCCCD)
                    error += "[Số căn cước công dân đã tồn tại]";
                if (nhanSu1.NS_Email == nhanSu.NS_Email)
                    error += "[Email đã tồn tại]";
                if (nhanSu1.NS_SoDienThoai == nhanSu.NS_SoDienThoai)
                    error += "[Số điện thoại đã tồn tại]";
                return error;
            }
            catch { }
            return DefineError.loiHeThong;
        }
        public string suaNhanSu(Object.NhanSu nhanSuNew)
        {
            try
            {
                if (nhanSuNew == null)
                    return DefineError.loiDuLieuKhongHopLe;
                if (string.IsNullOrEmpty(nhanSuNew.NS_Ma) || string.IsNullOrEmpty(nhanSuNew.NS_HoVaTen) ||
                    nhanSuNew.NS_GioiTinh == null || nhanSuNew.NS_NgaySinh == null ||
                    string.IsNullOrEmpty(nhanSuNew.NS_SoDienThoai) || string.IsNullOrEmpty(nhanSuNew.NS_Email) ||
                    string.IsNullOrEmpty(nhanSuNew.NS_DiaChi) || string.IsNullOrEmpty(nhanSuNew.NS_SoCCCD) ||
                    nhanSuNew.NS_NgayVao == null)
                    return DefineError.loiDuLieuKhongHopLe;
                string error = "";
                if (!StringHelper.IsValidEmail(nhanSuNew.NS_Email))
                    error += "[Email không đúng định dạng]";
                if (!StringHelper.IsPhoneNbr(nhanSuNew.NS_SoDienThoai))
                    error += "[Số điện thoại không đúng định dạng]";
                if (!StringHelper.IsValidCCCD(nhanSuNew.NS_SoCCCD))
                    error += "[Số căn cước công dân không đúng định dạng]";
                if (DateTime.Now.Year - nhanSuNew.NS_NgaySinh.Value.Year < 16)
                    error += "[Nhân sự phải đủ từ 16 tuổi trở lên]";
                if (error.Length > 0)
                    return error;

                Data.NhanSu nhanSu = context.NhanSus.Find(nhanSuNew.NS_Ma);
                if (nhanSu == null)
                    return DefineError.khongTonTai;
                Data.NhanSu nhanSuCheck = context.NhanSus.Where(
                    item => item.NS_Ma != nhanSuNew.NS_Ma && (item.NS_SoCCCD == nhanSuNew.NS_SoCCCD ||
                    item.NS_Email == nhanSuNew.NS_Email || item.NS_SoDienThoai == nhanSuNew.NS_SoDienThoai)).FirstOrDefault();
                if (nhanSuCheck == null)
                {
                    Convert<Data.NhanSu, Object.NhanSu>.ConvertObj(ref nhanSu, nhanSuNew);
                    context.NhanSus.AddOrUpdate(nhanSu);
                    int check = context.SaveChanges();
                    if (check == 0)
                        return DefineError.loiHeThong;
                    return string.Empty;
                }
                error = "";
                if (nhanSu.NS_SoCCCD == nhanSuNew.NS_SoCCCD)
                    error += "[Số căn cước công dân đã tồn tại]";
                if (nhanSu.NS_Email == nhanSuNew.NS_Email)
                    error += "[Email đã tồn tại]";
                if (nhanSu.NS_SoDienThoai == nhanSuNew.NS_SoDienThoai)
                    error += "[Số điện thoại đã tổn tại]";
                return error;
            }
            catch { }
            return DefineError.loiHeThong;
        }
        public string xoaNhanSu(string maNhanSu)
        {
            try
            {
                if (string.IsNullOrEmpty(maNhanSu))
                    return DefineError.loiDuLieuKhongHopLe;
                if (maNhanSu == this.maNhanSu)
                    return "Bạn không thể tự xóa chính bản thân mình.";
                Data.NhanSu nhanSu = context.NhanSus.Find(maNhanSu);
                if (nhanSu == null)
                    return DefineError.khongTonTai;
                TaiKhoan taiKhoan = timTaiKhoanBangMaNhanSu(nhanSu.NS_Ma);
                if (taiKhoan != null && (taiKhoan.TK_QuyenQuanLy == true || taiKhoan.TK_QuyenAdmin == true) && !this.quyenAdmin)
                    return "Bạn không thể xóa người có quyền quản lý hệ thống khác.";
                if (taiKhoan != null && taiKhoan.TK_QuyenAdmin == true && this.quyenAdmin)
                    return "Bạn không thể xóa admin của hệ thống.";
                context.NhanSus.Remove(nhanSu);
                int check = context.SaveChanges();
                if (check > 0)
                    return string.Empty;
            }
            catch {}
            return DefineError.loiHeThong;
        }
        public string[] xoaNhieuNhanSu(string[]maNhanSus)
        {
            string[] results = new string[] {string.Empty, string.Empty};
            try
            {
                if (maNhanSus == null)
                    return null;
                else
                {
                    string error = "";
                    string success = "";
                    int countSuccess = 0;
                    foreach (string s in maNhanSus)
                    {
                        string message = xoaNhanSu(s);
                        if (string.IsNullOrEmpty(message))
                            countSuccess++;
                    }
                    if (countSuccess > 0)
                        success = "Xoá thành công " + countSuccess + " nhân sự.";
                    if (countSuccess < maNhanSus.Length)
                        error = "Không thể xóa " + (maNhanSus.Length - countSuccess) + " nhân sự.";
                    results[0] = error;
                    results[1] = success;
                    return results;
                }
            } catch { }
            results[0] = DefineError.loiHeThong;
            return results;
        }
//Bao Hiem
        public int demBaoHiem()
        {
            try
            {
                return context.BaoHiems.Count();
            } catch { }
            return 0;
        }
        public IEnumerable<Object.BaoHiem> layDanhSachBaoHiem()
        {
            try
            {
                
                if (context != null)
                    return Convert<Object.BaoHiem, Data.BaoHiem>.ConvertObjs(context.BaoHiems);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
        public List<IEnumerable<Object.BaoHiem>> layDanhSachBaoHiem(Object.BaoHiem findBy = null, int page = 1, int pageSize = 10, string sortBy = "BH_Ma", bool sortTangDan = true)
        {
            try
            {
                
                IEnumerable<Object.BaoHiem> results =
                    (findBy != null ? timKiemNhieuBaoHiem(findBy)
                    : Convert<Object.BaoHiem, Data.BaoHiem>.ConvertObjs(from BaoHiem in context.BaoHiems select BaoHiem));
                if (results == null)
                    return new List<IEnumerable<Object.BaoHiem>>()
                    {
                        new List<Object.BaoHiem>(),
                        new List<Object.BaoHiem>()
                    };
                results = ObjectHelper.OrderByDynamic<Object.BaoHiem>(results, sortBy, sortTangDan ? "asc" : "desc");
                int p = results.Count();
                if (p == 0)
                    return new List<IEnumerable<Object.BaoHiem>>()
                    {
                        new List<Object.BaoHiem>(),
                        new List<Object.BaoHiem>()
                    };
                else
                    pageSize = pageSize < p ? pageSize : p;
                p = (p % pageSize) == 0 ? (p / pageSize) : (p / pageSize + 1);
                page = page > p ? p : page <= 0 ? 1 : page;
                return new List<IEnumerable<BaoHiem>>()
                {
                    results,
                    results.Count() > pageSize ? results.ToPagedList(page, pageSize) : results
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
        public IEnumerable<Object.BaoHiem> timKiemNhieuBaoHiem(Object.BaoHiem find = null)
        {
            try
            {
                
                if (find != null)
                {
                    return Convert<Object.BaoHiem, Data.BaoHiem>.ConvertObjs(from BaoHiem in context.BaoHiems
                            where
                            (find.BH_Ma != null ? BaoHiem.BH_Ma == find.BH_Ma : BaoHiem != null) &&
                            (!string.IsNullOrEmpty(find.BH_SoBaoHiem) ? BaoHiem.BH_SoBaoHiem.StartsWith(find.BH_SoBaoHiem) : BaoHiem != null) &&
                            (find.BH_NgayCap != null ? BaoHiem.BH_NgayCap == find.BH_NgayCap : BaoHiem != null) &&
                            (find.BH_NgayHetHan != null ? BaoHiem.BH_NgayHetHan == find.BH_NgayHetHan : BaoHiem != null) &&
                            (!string.IsNullOrEmpty(find.BH_NoiCap) ? BaoHiem.BH_NoiCap.StartsWith(find.BH_NoiCap) : BaoHiem != null) &&
                            (!string.IsNullOrEmpty(find.BH_NoiKhamBenh) ? BaoHiem.BH_NoiKhamBenh.StartsWith(find.BH_NoiKhamBenh) : BaoHiem != null) &&
                            (!string.IsNullOrEmpty(find.NS_Ma) ? BaoHiem.NS_Ma.StartsWith(find.NS_Ma) : BaoHiem != null)
                            select BaoHiem);
                }
                return Convert<Object.BaoHiem, Data.BaoHiem>.ConvertObjs(context.BaoHiems);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
        public Object.BaoHiem timKiemMotBaoHiem(long ma)
        {
            try
            {
                if (ma < 0)
                    return null;
                
                Object.BaoHiem convert = new BaoHiem();
                Convert<Object.BaoHiem, Data.BaoHiem>.ConvertObj(ref convert, context.BaoHiems.Find(ma));
                return convert;
            }
            catch { }
            return null;
        }
        public string themBaoHiem(Object.BaoHiem baoHiem)
        {
            try
            {
                if (baoHiem == null || string.IsNullOrEmpty(baoHiem.BH_SoBaoHiem) ||
                    baoHiem.BH_NgayCap == null || baoHiem.BH_NgayHetHan == null ||
                    string.IsNullOrEmpty(baoHiem.BH_NoiCap) || string.IsNullOrEmpty(baoHiem.NS_Ma))
                    return DefineError.loiDuLieuKhongHopLe;

                if (context.NhanSus.Find(baoHiem.NS_Ma) == null)
                    return "Nhân sự không tồn tại.";
                Data.BaoHiem BaoHiem1 = context.BaoHiems.Where(
                    item => (item.BH_Ma == baoHiem.BH_Ma ||
                    item.BH_SoBaoHiem == baoHiem.BH_SoBaoHiem)).FirstOrDefault();
                if (BaoHiem1 == null)
                {
                    BaoHiem1 = new Data.BaoHiem();
                    Convert<Data.BaoHiem, Object.BaoHiem>.ConvertObj(ref BaoHiem1, baoHiem);
                    context.BaoHiems.Add(BaoHiem1);
                    int check = context.SaveChanges();
                    if (check == 0)
                        return DefineError.loiHeThong;
                    return string.Empty;
                }
                string error = "";
                if (BaoHiem1.BH_Ma == baoHiem.BH_Ma)
                    error += "[Mã bảo hiểm đã tồn tại]";
                if (BaoHiem1.BH_SoBaoHiem == baoHiem.BH_SoBaoHiem)
                    error += "[Số bảo hiểm đã tồn tại]";
                return error;
            }
            catch { }
            return DefineError.loiHeThong;
        }
        public string suaBaoHiem(Object.BaoHiem New)
        {
            try
            {
                if (New == null || string.IsNullOrEmpty(New.BH_SoBaoHiem) ||
                    New.BH_NgayCap == null || New.BH_NgayHetHan == null ||
                    string.IsNullOrEmpty(New.BH_NoiCap) || string.IsNullOrEmpty(New.NS_Ma))
                    return DefineError.loiDuLieuKhongHopLe;
                if (context.NhanSus.Find(New.NS_Ma) == null)
                    return "Nhân sự không tồn tại.";
                Data.BaoHiem Old = context.BaoHiems.Find(New.BH_Ma);
                if (Old == null)
                    return DefineError.khongTonTai;
                Data.BaoHiem check = context.BaoHiems.Where(
                    item => item.BH_Ma != New.BH_Ma
                    && (item.BH_SoBaoHiem == New.BH_SoBaoHiem)).FirstOrDefault();
                if (check == null)
                {
                    Convert<Data.BaoHiem, Object.BaoHiem>.ConvertObj(ref Old, New);
                    context.BaoHiems.AddOrUpdate(check);
                    int checkSuccess = context.SaveChanges();
                    if (checkSuccess == 0)
                        return DefineError.loiHeThong;
                    return string.Empty;
                }
                string error = "";
                if (check.BH_SoBaoHiem == check.BH_SoBaoHiem)
                    error += "[Số bảo hiểm đã tồn tại]";
                return error;
            }
            catch { }
            return DefineError.loiHeThong;
        }
        public string xoaBaoHiem(long ma)
        {
            try
            {
                if (ma < 0)
                    return DefineError.loiDuLieuKhongHopLe; 
                Data.BaoHiem xoa = context.BaoHiems.Find(ma);
                if (xoa == null)
                    return DefineError.khongTonTai;
                context.BaoHiems.Remove(xoa);
                int check = context.SaveChanges();
                if (check > 0)
                    return string.Empty;
            }
            catch { }
            return DefineError.loiHeThong;
        }
        public string[] xoaNhieuBaoHiem(long[] mas)
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
                        string message = xoaBaoHiem(s);
                        if (string.IsNullOrEmpty(message))
                            countSuccess++;
                    }
                    if (countSuccess > 0)
                        success = "Xoá thành công " + countSuccess + " bảo hiểm.";
                    if (countSuccess < mas.Length)
                        error = "Không thể xóa " + (mas.Length - countSuccess) + " bảo hiểm.";
                    results[0] = error;
                    results[1] = success;
                    return results;
                }
            }
            catch { }
            results[0] = DefineError.loiHeThong;
            return results;
        }
//Hop Dong
        public int demHopDong()
        {
            try
            {
                return context.HopDongs.Count();
            } catch { }
            return 0;
        }
        public IEnumerable<Object.HopDong> layDanhSachHopDong()
        {
            try
            { 
                if (context != null)
                    return Convert<Object.HopDong, Data.HopDong>.ConvertObjs(context.HopDongs);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
        public List<IEnumerable<Object.HopDong>> layDanhSachHopDong(Object.HopDong findBy = null, int page = 1, int pageSize = 10, string sortBy = "HD_Ma", bool sortTangDan = true)
        {
            try
            {
                IEnumerable<Object.HopDong> results =
                    (findBy != null ? timKiemNhieuHopDong(findBy)
                    : Convert<Object.HopDong, Data.HopDong>.ConvertObjs(from HopDong in context.HopDongs select HopDong));
                if (results == null)
                    return new List<IEnumerable<Object.HopDong>>()
                    {
                        new List<Object.HopDong>(),
                        new List<Object.HopDong>()
                    };
                results = ObjectHelper.OrderByDynamic<Object.HopDong>(results, sortBy, sortTangDan ? "asc" : "desc");
                int p = results.Count();
                if (p == 0)
                    return new List<IEnumerable<Object.HopDong>>()
                    {
                        new List<Object.HopDong>(),
                        new List<Object.HopDong>()
                    };
                else
                    pageSize = pageSize < p ? pageSize : p;
                p = (p % pageSize) == 0 ? (p / pageSize) : (p / pageSize + 1);
                page = page > p ? p : page <= 0 ? 1 : page;
                return new List<IEnumerable<Object.HopDong>>()
                {
                    results,
                    results.Count() > pageSize ? results.ToPagedList(page, pageSize) : results
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
        public IEnumerable<Object.HopDong> timKiemNhieuHopDong(Object.HopDong find = null)
        {
            try
            {
                if (find != null)
                {
                    return Convert<Object.HopDong, Data.HopDong>.ConvertObjs(from HopDong in context.HopDongs
                        where
                        (find.HD_Ma != null ? HopDong.HD_Ma == find.HD_Ma : HopDong != null) &&
                        (find.HD_NgayBatDau != null ? HopDong.HD_NgayBatDau == find.HD_NgayBatDau : HopDong != null) &&
                        (find.HD_NgayKetThuc != null ? HopDong.HD_NgayKetThuc == find.HD_NgayKetThuc : HopDong != null) &&
                        (!string.IsNullOrEmpty(find.HD_HinhThucLamViec) ? HopDong.HD_HinhThucLamViec.StartsWith(find.HD_HinhThucLamViec) : HopDong != null) &&
                        (find.HD_Luong != null ? HopDong.HD_Luong == find.HD_Luong : HopDong != null) &&
                        (!string.IsNullOrEmpty(find.HD_DonViTinhuong) ? HopDong.HD_DonViTinhuong.StartsWith(find.HD_DonViTinhuong) : HopDong != null) &&
                        (!string.IsNullOrEmpty(find.HD_CongViec) ? HopDong.HD_CongViec.StartsWith(find.HD_CongViec) : HopDong != null) &&
                        (!string.IsNullOrEmpty(find.NS_Ma) ? HopDong.NS_Ma.StartsWith(find.NS_Ma) : HopDong != null) 
                        select HopDong);
                }
                return Convert<Object.HopDong, Data.HopDong>.ConvertObjs(context.HopDongs);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
        public Object.HopDong timKiemMotHopDong(long ma)
        {
            try
            {
                if (ma < 0)
                    return null; 
                Object.HopDong convert = new HopDong();
                Convert<Object.HopDong, Data.HopDong>.ConvertObj(ref convert, context.HopDongs.Find(ma));
                return convert;
            }
            catch { }
            return null;
        }
        public string themHopDong(Object.HopDong New)
        {
            try
            {
                if (New == null || New.HD_NgayBatDau == null ||
                    string.IsNullOrEmpty(New.HD_HinhThucLamViec) || New.HD_Luong == null ||
                    string.IsNullOrEmpty(New.HD_DonViTinhuong) || string.IsNullOrEmpty(New.HD_CongViec) ||
                    string.IsNullOrEmpty(New.NS_Ma))
                    return DefineError.loiDuLieuKhongHopLe;
                if (context.NhanSus.Find(New.NS_Ma) == null)
                    return "Nhân sự không tồn tại.";
                Data.HopDong vali = context.HopDongs.Where(
                    item => item.HD_Ma == New.HD_Ma ||
                    (
                        item.NS_Ma == New.NS_Ma &&
                        (
                            item.HD_NgayKetThuc == null ||
                            item.HD_NgayKetThuc > New.HD_NgayBatDau
                        )    
                    )
                ).FirstOrDefault();
                if (vali == null)
                {
                    vali = new Data.HopDong();
                    Convert<Data.HopDong, Object.HopDong>.ConvertObj(ref vali, New);
                    context.HopDongs.Add(vali);
                    int check = context.SaveChanges();
                    if (check == 0)
                        return DefineError.loiHeThong;
                    return string.Empty;
                }
                string error = "";
                if (vali.HD_Ma == New.HD_Ma)
                    error += "[Mã hợp đồng đã tồn tại]";
                else
                    error += "[Hợp đồng cũ của người này chưa kết thúc, không thể thêm mới]";
                return error;
            }
            catch
            {
                return DefineError.loiHeThong;
            }
        }
        public string xoaHopDong(long ma)
        {
            try
            {
                if (ma < 0)
                    return DefineError.loiDuLieuKhongHopLe;
                Data.HopDong xoa = context.HopDongs.Find(ma);
                if (xoa == null)
                    return DefineError.khongTonTai;
                context.HopDongs.Remove(xoa);
                int check = context.SaveChanges();
                if (check > 0)
                    return string.Empty;
            }
            catch { }
            return DefineError.loiHeThong;
        }
        public string[] xoaNhieuHopDong(long[] mas)
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
                        string message = xoaHopDong(s);
                        if (string.IsNullOrEmpty(message))
                            countSuccess++;
                    }
                    if (countSuccess > 0)
                        success = "Xoá thành công " + countSuccess + " hợp đồng.";
                    if (countSuccess < mas.Length)
                        error = "Không thể xóa " + (mas.Length - countSuccess) + " hợp đồng.";
                    results[0] = error;
                    results[1] = success;
                    return results;
                }
            }
            catch { }
            results[0] = DefineError.loiHeThong;
            return results;
        }
//Lich su lam viec
        public int demLichSuLamViec()
        {
            try
            {
                return context.LichSuLamViecs.Count();
            } catch { }
            return 0;
        }
        public IEnumerable<Object.LichSuLamViec> layDanhSachLichSuLamViec()
        {
            try
            {
                
                if (context != null)
                    return Convert<Object.LichSuLamViec, Data.LichSuLamViec>.ConvertObjs(context.LichSuLamViecs);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
        public List<IEnumerable<Object.LichSuLamViec>> layDanhSachLichSuLamViec(Object.LichSuLamViec findBy = null, int page = 1, int pageSize = 10, string sortBy = "LSLV_Ma", bool sortTangDan = true)
        {
            try
            {
                IEnumerable<Object.LichSuLamViec> results =
                    (findBy != null ? timKiemNhieuLichSuLamViec(findBy)
                    : Convert<Object.LichSuLamViec, Data.LichSuLamViec>.ConvertObjs(from LichSuLamViec in context.LichSuLamViecs select LichSuLamViec));
                if (results == null)
                    return new List<IEnumerable<Object.LichSuLamViec>>()
                    {
                        new List<Object.LichSuLamViec>(),
                        new List<Object.LichSuLamViec>()
                    };
                results = ObjectHelper.OrderByDynamic<Object.LichSuLamViec>(results, sortBy, sortTangDan ? "asc" : "desc");
                int p = results.Count();
                if (p == 0)
                    return new List<IEnumerable<Object.LichSuLamViec>>()
                    {
                        new List<Object.LichSuLamViec>(),
                        new List<Object.LichSuLamViec>()
                    };
                else
                    pageSize = pageSize < p ? pageSize : p;
                p = (p % pageSize) == 0 ? (p / pageSize) : (p / pageSize + 1);
                page = page > p ? p : page <= 0 ? 1 : page;
                return new List<IEnumerable<Object.LichSuLamViec>>()
                {
                    results,
                    results.Count() > pageSize ? results.ToPagedList(page, pageSize) : results
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
        public IEnumerable<Object.LichSuLamViec> timKiemNhieuLichSuLamViec(Object.LichSuLamViec find = null)
        {
            try
            {
                if (find != null)
                {
                    return Convert<Object.LichSuLamViec, Data.LichSuLamViec>.ConvertObjs(from LichSuLamViec in context.LichSuLamViecs
                        where
                        (find.LSLV_Ma != null ? LichSuLamViec.LSLV_Ma == find.LSLV_Ma : LichSuLamViec != null) &&
                        (find.LSLV_NgayBatDau != null ? LichSuLamViec.LSLV_NgayBatDau == find.LSLV_NgayBatDau : LichSuLamViec != null) &&
                        (find.LSLV_NgayKetThuc != null ? LichSuLamViec.LSLV_NgayKetThuc == find.LSLV_NgayKetThuc : LichSuLamViec != null) &&
                        (!string.IsNullOrEmpty(find.LSLV_ChucVu) ? LichSuLamViec.LSLV_ChucVu.StartsWith(find.LSLV_ChucVu) : LichSuLamViec != null) &&
                        (!string.IsNullOrEmpty(find.NS_Ma) ? LichSuLamViec.NS_Ma.StartsWith(find.NS_Ma) : LichSuLamViec != null) &&
                        (!string.IsNullOrEmpty(find.BP_Ma) ? LichSuLamViec.BP_Ma.StartsWith(find.BP_Ma) : LichSuLamViec != null) 
                        select LichSuLamViec);
                }
                return Convert<Object.LichSuLamViec, Data.LichSuLamViec>.ConvertObjs(context.LichSuLamViecs);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
        public Object.LichSuLamViec timKiemMotLichSuLamViec(long ma)
        {
            try
            {
                if (ma < 0)
                    return null;
                
                Object.LichSuLamViec convert = new LichSuLamViec();
                Convert<Object.LichSuLamViec, Data.LichSuLamViec>.ConvertObj(ref convert, context.LichSuLamViecs.Find(ma));
                return convert;
            }
            catch { }
            return null;
        }
        public string themLichSuLamViec(Object.LichSuLamViec New)
        {
            try
            {
                if (New == null || New.LSLV_NgayBatDau == null ||
                    string.IsNullOrEmpty(New.LSLV_ChucVu) || string.IsNullOrEmpty(New.NS_Ma))
                    return DefineError.loiDuLieuKhongHopLe;
                if (context.NhanSus.Find(New.NS_Ma) == null)
                    return "Nhân sự không tồn tại.";
                if (context.BoPhans.Find(New.BP_Ma) == null)
                    return "Bộ phận không tồn tại.";
                Data.LichSuLamViec vali = context.LichSuLamViecs
                    .Where(item => item.LSLV_Ma == New.LSLV_Ma ||
                    (
                        item.NS_Ma == New.NS_Ma && (
                            item.LSLV_NgayKetThuc == null ||
                            item.LSLV_NgayKetThuc > New.LSLV_NgayBatDau
                        )
                    )).FirstOrDefault();
                if (vali == null)
                {
                    vali = new Data.LichSuLamViec();
                    Convert<Data.LichSuLamViec, Object.LichSuLamViec>.ConvertObj(ref vali, New);
                    context.LichSuLamViecs.Add(vali);
                    int check = context.SaveChanges();
                    if (check == 0)
                        return DefineError.loiHeThong;
                    return string.Empty;
                }
                string error = "";
                if (vali.LSLV_Ma == New.LSLV_Ma)
                    error += "[Mã đã tồn tại]";
                else
                    error += "[Một nhân sự không thể có nhiều hơn một lịch sử làm việc trong một thời điểm]";
                return error;
            }
            catch { }
            return DefineError.loiHeThong;

        }
        public string suaLichSuLamViec(Object.LichSuLamViec New)
        {
            try
            {
                if (New == null || New.LSLV_NgayBatDau == null ||
                    string.IsNullOrEmpty(New.LSLV_ChucVu) || string.IsNullOrEmpty(New.NS_Ma))
                    return DefineError.loiDuLieuKhongHopLe;
                if (context.NhanSus.Find(New.NS_Ma) == null)
                    return "Nhân sự không tồn tại.";
                if (context.BoPhans.Find(New.BP_Ma) == null)
                    return "Bộ phận không tồn tại.";
                Data.LichSuLamViec Old = context.LichSuLamViecs.Find(New.LSLV_Ma);
                if (Old == null)
                    return DefineError.khongTonTai;
                Data.LichSuLamViec check = context.LichSuLamViecs.Where(
                    item => item.LSLV_Ma != New.LSLV_Ma && (
                        item.NS_Ma == New.NS_Ma && (
                            item.LSLV_NgayKetThuc == null ||
                            item.LSLV_NgayKetThuc > New.LSLV_NgayBatDau
                        )
                    )
                ).FirstOrDefault();
                if (check == null)
                {
                    Convert<Data.LichSuLamViec, Object.LichSuLamViec>.ConvertObj(ref Old, New);
                    context.LichSuLamViecs.AddOrUpdate(check);
                    int checkSuccess = context.SaveChanges();
                    if (checkSuccess == 0)
                        return DefineError.loiHeThong;
                    return string.Empty;
                }
                return "[Một nhân sự không thể có nhiều hơn một lịch sử làm việc trong một thời điểm]";
            }
            catch { }
            return DefineError.loiHeThong;
        }
        public string xoaLichSuLamViec(long ma)
        {
            try
            {
                if (ma < 0)
                    return DefineError.loiDuLieuKhongHopLe;
                Data.LichSuLamViec xoa = context.LichSuLamViecs.Find(ma);
                if (xoa == null)
                    return DefineError.khongTonTai;
                context.LichSuLamViecs.Remove(xoa);
                int check = context.SaveChanges();
                if (check > 0)
                    return string.Empty;
            }
            catch { }
            return DefineError.loiHeThong;
        }
        public string[] xoaNhieuLichSuLamViec(long[] mas)
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
                        string message = xoaLichSuLamViec(s);
                        if (string.IsNullOrEmpty(message))
                            countSuccess++;
                    }
                    if (countSuccess > 0)
                        success = "Xoá thành công " + countSuccess + " lịch sử làm việc.";
                    if (countSuccess < mas.Length)
                        error = "Không thể xóa " + (mas.Length - countSuccess) + " lịch sử làm việc.";
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
        public int demTaiKhoan()
        {
            try
            {
                return context.TaiKhoans.Count();
            } catch { }
            return 0;
        }
        public Object.TaiKhoan timTaiKhoanBangMaNhanSu(string maNhanSu)
        {
            try
            {
                if (string.IsNullOrEmpty(maNhanSu))
                    return null;
                Object.TaiKhoan convert = new Object.TaiKhoan();
                Convert<Object.TaiKhoan, Data.TaiKhoan>.ConvertObj(ref convert, context.TaiKhoans.Where(item => item.NS_Ma == maNhanSu).FirstOrDefault());
                return convert;
            }
            catch { }
            return null;
        }
        public IEnumerable<Object.TaiKhoan> layDanhSachTaiKhoan()
        {
            try
            {
                
                if (context != null)
                    return Convert<Object.TaiKhoan, Data.TaiKhoan>.ConvertObjs(context.TaiKhoans);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
        public List<IEnumerable<Object.TaiKhoan>> layDanhSachTaiKhoan(Object.TaiKhoan findBy = null, int page = 1, int pageSize = 10, string sortBy = "TK_TenDangNhap", bool sortTangDan = true)
        {
            try
            {
                IEnumerable<TaiKhoan> results =
                    (findBy != null ? timKiemNhieuTaiKhoan(findBy)
                    : Convert<Object.TaiKhoan, Data.TaiKhoan>.ConvertObjs(from TaiKhoan in context.TaiKhoans select TaiKhoan));
                if (results == null)
                    return new List<IEnumerable<TaiKhoan>>()
                    {
                        new List<TaiKhoan>(),
                        new List<TaiKhoan>()
                    };
                results = ObjectHelper.OrderByDynamic<TaiKhoan>(results, sortBy, sortTangDan ? "asc" : "desc");
                int p = results.Count();
                if (p == 0)
                    return new List<IEnumerable<TaiKhoan>>()
                    {
                        new List<TaiKhoan>(),
                        new List<TaiKhoan>() 
                    };
                else
                    pageSize = pageSize < p ? pageSize : p;
                p = (p % pageSize) == 0 ? (p / pageSize) : (p / pageSize + 1);
                page = page > p ? p : page <= 0 ? 1 : page;
                return new List<IEnumerable<TaiKhoan>>()
                {
                    results,
                    results.Count() > pageSize ? results.ToPagedList(page, pageSize) : results
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
        public IEnumerable<Object.TaiKhoan> timKiemNhieuTaiKhoan(Object.TaiKhoan findTaiKhoan = null)
        {
            try
            { 
                if (findTaiKhoan != null)
                {
                    return Convert<Object.TaiKhoan, Data.TaiKhoan>.ConvertObjs(from TaiKhoan in context.TaiKhoans
                            where
                            (!string.IsNullOrEmpty(findTaiKhoan.TK_TenDangNhap) ? TaiKhoan.TK_TenDangNhap.StartsWith(findTaiKhoan.TK_TenDangNhap) : TaiKhoan != null) &&
                            (findTaiKhoan.TK_ThoiGianMoKhoa != null ? TaiKhoan.TK_ThoiGianMoKhoa == findTaiKhoan.TK_ThoiGianMoKhoa : TaiKhoan != null) &&
                            (findTaiKhoan.TK_BiKhoa != null ? TaiKhoan.TK_BiKhoa == findTaiKhoan.TK_BiKhoa : TaiKhoan != null) &&
                            (findTaiKhoan.TK_QuyenAdmin != null ? TaiKhoan.TK_QuyenAdmin == findTaiKhoan.TK_QuyenAdmin : TaiKhoan != null) &&
                            (findTaiKhoan.TK_QuyenQuanLy != null ? TaiKhoan.TK_QuyenQuanLy == findTaiKhoan.TK_QuyenQuanLy : TaiKhoan != null) &&
                            (!string.IsNullOrEmpty(findTaiKhoan.NS_Ma) ? TaiKhoan.NS_Ma.StartsWith(findTaiKhoan.NS_Ma) : TaiKhoan != null) 
                            select TaiKhoan);
                }
                return Convert<Object.TaiKhoan, Data.TaiKhoan>.ConvertObjs(context.TaiKhoans);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
        public Object.TaiKhoan timKiemMotTaiKhoan(string username)
        {
            try
            {
                if (string.IsNullOrEmpty(maNhanSu))
                    return null;
                Object.TaiKhoan convert = new TaiKhoan();
                Convert<Object.TaiKhoan, Data.TaiKhoan>.ConvertObj(ref convert, context.TaiKhoans.Find(username));
                return convert;
            }
            catch { }
            return null;
        }
//Khen thuong ky luat
        public int demKhenThuongKyLuat()
        {
            try
            {
                return context.KhenThuongKyLuats.Count();
            } catch { }
            return 0;
        }
        public IEnumerable<Object.KhenThuongKyLuat> layDanhSachKhenThuongKyLuat()
        {
            try
            {
                
                if (context != null)
                    return Convert<Object.KhenThuongKyLuat, Data.KhenThuongKyLuat>.ConvertObjs(context.KhenThuongKyLuats);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
        public List<IEnumerable<Object.KhenThuongKyLuat>> layDanhSachKhenThuongKyLuat(Object.KhenThuongKyLuat findBy = null, int page = 1, int pageSize = 10, string sortBy = "KTKL_Ma", bool sortTangDan = true)
        {
            try
            {
                IEnumerable<Object.KhenThuongKyLuat> results =
                    (findBy != null ? timKiemNhieuKhenThuongKyLuat(findBy)
                    : Convert<Object.KhenThuongKyLuat, Data.KhenThuongKyLuat>.ConvertObjs(from KhenThuongKyLuat in context.KhenThuongKyLuats select KhenThuongKyLuat));
                if (results == null)
                    return new List<IEnumerable<Object.KhenThuongKyLuat>>()
                    {
                        new List<Object.KhenThuongKyLuat>(),
                        new List<Object.KhenThuongKyLuat>()
                    };
                results = ObjectHelper.OrderByDynamic<Object.KhenThuongKyLuat>(results, sortBy, sortTangDan ? "asc" : "desc");
                int p = results.Count();
                if (p == 0)
                    return new List<IEnumerable<Object.KhenThuongKyLuat>>()
                    {
                        new List<Object.KhenThuongKyLuat>(),
                        new List<Object.KhenThuongKyLuat>()
                    };
                else
                    pageSize = pageSize < p ? pageSize : p;
                p = (p % pageSize) == 0 ? (p / pageSize) : (p / pageSize + 1);
                page = page > p ? p : page <= 0 ? 1 : page;
                return new List<IEnumerable<Object.KhenThuongKyLuat>>()
                {
                    results,
                    results.Count() > pageSize ? results.ToPagedList(page, pageSize) : results
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
        public IEnumerable<Object.KhenThuongKyLuat> timKiemNhieuKhenThuongKyLuat(Object.KhenThuongKyLuat find = null)
        {
            try
            {
                if (find != null)
                {
                    return Convert<Object.KhenThuongKyLuat, Data.KhenThuongKyLuat>.ConvertObjs(from KhenThuongKyLuat in context.KhenThuongKyLuats
                        where
                        (find.KTKL_Ma != null ? KhenThuongKyLuat.KTKL_Ma == find.KTKL_Ma : KhenThuongKyLuat != null) &&
                        (!string.IsNullOrEmpty(find.KTKL_MoTa) ? KhenThuongKyLuat.KTKL_MoTa.StartsWith(find.KTKL_MoTa) : KhenThuongKyLuat != null) &&
                        (find.KTKL_ThoiGian != null ? KhenThuongKyLuat.KTKL_ThoiGian == find.KTKL_ThoiGian : KhenThuongKyLuat != null) &&
                        (!string.IsNullOrEmpty(find.KTKL_HinhThuc) ? KhenThuongKyLuat.KTKL_HinhThuc.StartsWith(find.KTKL_HinhThuc) : KhenThuongKyLuat != null) &&
                        (find.KTKL_SoTien != null ? KhenThuongKyLuat.KTKL_SoTien == find.KTKL_SoTien : KhenThuongKyLuat != null) &&
                        (!string.IsNullOrEmpty(find.NS_Ma) ? KhenThuongKyLuat.NS_Ma.StartsWith(find.NS_Ma) : KhenThuongKyLuat != null) 
                        select KhenThuongKyLuat);
                }
                return Convert<Object.KhenThuongKyLuat, Data.KhenThuongKyLuat>.ConvertObjs(context.KhenThuongKyLuats);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
        public Object.KhenThuongKyLuat timKiemMotKhenThuongKyLuat(long ma)
        {
            try
            {
                if (ma < 0)
                    return null;
                Object.KhenThuongKyLuat convert = new KhenThuongKyLuat();
                Convert<Object.KhenThuongKyLuat, Data.KhenThuongKyLuat>.ConvertObj(ref convert, context.KhenThuongKyLuats.Find(ma));
                return convert;
            }
            catch { }
            return null;
        }
        public string themKhenThuongKyLuat(Object.KhenThuongKyLuat New)
        {
            try
            {
                if (New == null || string.IsNullOrEmpty(New.KTKL_HinhThuc) ||
                    New.KTKL_SoTien == null || string.IsNullOrEmpty(New.NS_Ma))
                    return DefineError.loiDuLieuKhongHopLe;
                if (context.NhanSus.Find(New.NS_Ma) == null)
                    return "Nhân sự không tồn tại.";
                Data.KhenThuongKyLuat vali = context.KhenThuongKyLuats
                    .Where(item => item.KTKL_Ma == New.KTKL_Ma).FirstOrDefault();
                if (vali == null)
                {
                    vali = new Data.KhenThuongKyLuat();
                    Convert<Data.KhenThuongKyLuat, Object.KhenThuongKyLuat>.ConvertObj(ref vali, New);
                    context.KhenThuongKyLuats.Add(vali);
                    int check = context.SaveChanges();
                    if (check == 0)
                        return DefineError.loiHeThong;
                    return string.Empty;
                }
                if (vali.KTKL_Ma == New.KTKL_Ma)
                    return "[Mã đã tồn tại]";
            }
            catch { }
            return DefineError.loiHeThong;
        }
        public string suaKhenThuongKyLuat(Object.KhenThuongKyLuat New)
        {
            try
            {
                if (New == null || string.IsNullOrEmpty(New.KTKL_HinhThuc) ||
                    New.KTKL_SoTien == null || string.IsNullOrEmpty(New.NS_Ma))
                    return DefineError.loiDuLieuKhongHopLe;
                if (context.NhanSus.Find(New.NS_Ma) == null)
                    return "Nhân sự không tồn tại.";
                Data.KhenThuongKyLuat Old = context.KhenThuongKyLuats.Find(New.KTKL_Ma);
                if (Old == null)
                    return DefineError.khongTonTai;
                Old = new Data.KhenThuongKyLuat();
                Convert<Data.KhenThuongKyLuat, Object.KhenThuongKyLuat>.ConvertObj(ref Old, New);
                context.KhenThuongKyLuats.AddOrUpdate(Old);
                int checkSuccess = context.SaveChanges();
                if (checkSuccess == 0)
                    return DefineError.loiHeThong;
                return string.Empty;
            }
            catch
            {
                return DefineError.loiHeThong;
            }
        }
        public string xoaKhenThuongKyLuat(long ma)
        {
            try
            {
                if (ma < 0)
                    return DefineError.loiDuLieuKhongHopLe;
                Data.KhenThuongKyLuat xoa = context.KhenThuongKyLuats.Find(ma);
                if (xoa == null)
                    return DefineError.khongTonTai;
                context.KhenThuongKyLuats.Remove(xoa);
                int check = context.SaveChanges();
                if (check > 0)
                    return string.Empty;
            }
            catch { }
            return DefineError.loiHeThong;
        }
        public string[] xoaNhieuKhenThuongKyLuat(long[] mas)
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
                        string message = xoaKhenThuongKyLuat(s);
                        if (string.IsNullOrEmpty(message))
                            countSuccess++;
                    }
                    if (countSuccess > 0)
                        success = "Xoá thành công " + countSuccess + " khen thưởng kỷ luật.";
                    if (countSuccess < mas.Length)
                        error = "Không thể xóa " + (mas.Length - countSuccess) + " khen thưởng kỷ luật.";
                    results[0] = error;
                    results[1] = success;
                    return results;
                }
            }
            catch { }
            results[0] = DefineError.loiHeThong;
            return results;
        }
//Phong ban
        public int demPhongBan()
        {
            try
            {
                return context.PhongBans.Count();
            } catch { }
            return 0;
        }
        public IEnumerable<Object.PhongBan> layDanhSachPhongBan()
        {
            try
            {
                
                if (context != null)
                    return Convert<Object.PhongBan, Data.PhongBan>.ConvertObjs(context.PhongBans);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
        public List<IEnumerable<Object.PhongBan>> layDanhSachPhongBan(Object.PhongBan findBy = null, int page = 1, int pageSize = 10, string sortBy = "PB_Ma", bool sortTangDan = true)
        {
            try
            {
                IEnumerable<Object.PhongBan> results =
                    (findBy != null ? timKiemNhieuPhongBan(findBy)
                    : Convert<Object.PhongBan, Data.PhongBan>.ConvertObjs(from PhongBan in context.PhongBans select PhongBan));
                if (results == null)
                    return new List<IEnumerable<Object.PhongBan>>()
                    {
                        new List<Object.PhongBan>(),
                        new List<Object.PhongBan>()
                    };
                results = ObjectHelper.OrderByDynamic<Object.PhongBan>(results, sortBy, sortTangDan ? "asc" : "desc");
                int p = results.Count();
                if (p == 0)
                    return new List<IEnumerable<Object.PhongBan>>()
                    {
                        new List<Object.PhongBan>(),
                        new List<Object.PhongBan>()
                    };
                else
                    pageSize = pageSize < p ? pageSize : p;
                p = (p % pageSize) == 0 ? (p / pageSize) : (p / pageSize + 1);
                page = page > p ? p : page <= 0 ? 1 : page;
                return new List<IEnumerable<Object.PhongBan>>()
                {
                    results,
                    results.Count() > pageSize ? results.ToPagedList(page, pageSize) : results
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
        public IEnumerable<Object.PhongBan> timKiemNhieuPhongBan(Object.PhongBan find = null)
        {
            try
            {
                if (find != null)
                {
                    return Convert<Object.PhongBan, Data.PhongBan>.ConvertObjs(from PhongBan in context.PhongBans
                        where
                        (!string.IsNullOrEmpty(find.PB_Ma) ? PhongBan.PB_Ma.StartsWith(find.PB_Ma) : PhongBan != null) &&
                        (!string.IsNullOrEmpty(find.PB_Ten) ? PhongBan.PB_Ten.StartsWith(find.PB_Ten) : PhongBan != null) &&
                        (!string.IsNullOrEmpty(find.PB_VaiTro) ? PhongBan.PB_VaiTro.StartsWith(find.PB_VaiTro) : PhongBan != null) &&
                        (!string.IsNullOrEmpty(find.NS_Ma) ? PhongBan.NS_Ma.StartsWith(find.NS_Ma) : PhongBan != null)
                        select PhongBan);
                }
                return Convert<Object.PhongBan, Data.PhongBan>.ConvertObjs(context.PhongBans);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
        public Object.PhongBan timKiemMotPhongBan(string ma)
        {
            try
            {
                if (string.IsNullOrEmpty(ma))
                    return null;
                Object.PhongBan convert = new PhongBan();
                Convert<Object.PhongBan, Data.PhongBan>.ConvertObj(ref convert, context.PhongBans.Find(ma));
                return convert;
            }
            catch { }
            return null;
        }
//Bo Phan
        public int demBoPhan()
        {
            try
            {
                return context.BoPhans.Count();
            } catch { }
            return 0;
        }
        public IEnumerable<Object.BoPhan> layDanhSachBoPhan()
        {
            try
            {
                
                if (context != null)
                    return Convert<Object.BoPhan, Data.BoPhan>.ConvertObjs(context.BoPhans);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
        public List<IEnumerable<Object.BoPhan>> layDanhSachBoPhan(Object.BoPhan findBy = null, int page = 1, int pageSize = 10, string sortBy = "BP_Ma", bool sortTangDan = true)
        {
            try
            {
                IEnumerable<Object.BoPhan> results =
                    (findBy != null ? timKiemNhieuBoPhan(findBy)
                    : Convert<Object.BoPhan, Data.BoPhan>.ConvertObjs(from BoPhan in context.BoPhans select BoPhan));
                if (results == null)
                    return new List<IEnumerable<Object.BoPhan>>()
                    {
                        new List<Object.BoPhan>(),
                        new List<Object.BoPhan>()
                    };
                results = ObjectHelper.OrderByDynamic<Object.BoPhan>(results, sortBy, sortTangDan ? "asc" : "desc");
                int p = results.Count();
                if (p == 0)
                    return new List<IEnumerable<Object.BoPhan>>()
                    {
                        new List<Object.BoPhan>(),
                        new List<Object.BoPhan>()
                    };
                else
                    pageSize = pageSize < p ? pageSize : p;
                p = (p % pageSize) == 0 ? (p / pageSize) : (p / pageSize + 1);
                page = page > p ? p : page <= 0 ? 1 : page;
                return new List<IEnumerable<Object.BoPhan>>()
                {
                    results,
                    results.Count() > pageSize ? results.ToPagedList(page, pageSize) : results
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
        public IEnumerable<Object.BoPhan> timKiemNhieuBoPhan(Object.BoPhan find = null)
        {
            try
            {
                if (find != null)
                {
                    return Convert<Object.BoPhan, Data.BoPhan>.ConvertObjs(from BoPhan in context.BoPhans
                        where
                        (!string.IsNullOrEmpty(find.BP_Ma) ? BoPhan.BP_Ma.StartsWith(find.BP_Ma) : BoPhan != null) &&
                        (!string.IsNullOrEmpty(find.BP_Ten) ? BoPhan.BP_Ten.StartsWith(find.BP_Ten) : BoPhan != null) &&
                        (!string.IsNullOrEmpty(find.BP_ChuyenMon) ? BoPhan.BP_ChuyenMon.StartsWith(find.BP_ChuyenMon) : BoPhan != null) &&
                        (!string.IsNullOrEmpty(find.PB_Ma) ? BoPhan.PB_Ma.StartsWith(find.PB_Ma) : BoPhan != null) &&
                        (!string.IsNullOrEmpty(find.NS_Ma) ? BoPhan.NS_Ma.StartsWith(find.NS_Ma) : BoPhan != null)
                        select BoPhan);
                }
                return Convert<Object.BoPhan, Data.BoPhan>.ConvertObjs(context.BoPhans);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
        public Object.BoPhan timKiemMotBoPhan(string ma)
        {
            try
            {
                if (string.IsNullOrEmpty(ma))
                    return null;
                Object.BoPhan convert = new BoPhan();
                Convert<Object.BoPhan, Data.BoPhan>.ConvertObj(ref convert, context.BoPhans.Find(ma));
                return convert;
            }
            catch { }
            return null;
        }
//Ca Lam
        public int demCaLam()
        {
            try
            {
                return context.CaLams.Count();
            } catch { }
            return 0;
        }
        public IEnumerable<Object.CaLam> layDanhSachCaLam()
        {
            try
            {
                
                if (context != null)
                    return Convert<Object.CaLam, Data.CaLam>.ConvertObjs(context.CaLams);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
        public List<IEnumerable<Object.CaLam>> layDanhSachCaLam(Object.CaLam findBy = null, int page = 1, int pageSize = 10, string sortBy = "CL_Ma", bool sortTangDan = true)
        {
            try
            {
                IEnumerable<Object.CaLam> results =
                    (findBy != null ? timKiemNhieuCaLam(findBy)
                    : Convert<Object.CaLam, Data.CaLam>.ConvertObjs(from CaLam in context.CaLams select CaLam));
                if (results == null)
                    return new List<IEnumerable<Object.CaLam>>()
                    {
                        new List<Object.CaLam>(),
                        new List<Object.CaLam>()
                    };
                results = ObjectHelper.OrderByDynamic<Object.CaLam>(results, sortBy, sortTangDan ? "asc" : "desc");
                int p = results.Count();
                if (p == 0)
                    return new List<IEnumerable<Object.CaLam>>()
                    {
                        new List<Object.CaLam>(),
                        new List<Object.CaLam>()
                    };
                else
                    pageSize = pageSize < p ? pageSize : p;
                p = (p % pageSize) == 0 ? (p / pageSize) : (p / pageSize + 1);
                page = page > p ? p : page <= 0 ? 1 : page;
                return new List<IEnumerable<Object.CaLam>>()
                {
                    results,
                    results.Count() > pageSize ? results.ToPagedList(page, pageSize) : results
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
        public IEnumerable<Object.CaLam> timKiemNhieuCaLam(Object.CaLam find = null)
        {
            try
            {
                if (find != null)
                {
                    return Convert<Object.CaLam, Data.CaLam>.ConvertObjs(from CaLam in context.CaLams
                        where
                        (!string.IsNullOrEmpty(find.CL_Ma) ? CaLam.CL_Ma.StartsWith(find.CL_Ma) : CaLam != null) &&
                        (!string.IsNullOrEmpty(find.CL_TenCa) ? CaLam.CL_TenCa.StartsWith(find.CL_TenCa) : CaLam != null) &&
                        (find.CL_GioBatDau != null ? CaLam.CL_GioBatDau == find.CL_GioBatDau : CaLam != null) &&
                        (find.CL_PhutBatDau != null ? CaLam.CL_PhutBatDau == find.CL_PhutBatDau : CaLam != null) &&
                        (find.CL_GioKetThuc != null ? CaLam.CL_GioKetThuc == find.CL_GioKetThuc : CaLam != null) &&
                        (find.CL_PhutKetThuc != null ? CaLam.CL_PhutKetThuc == find.CL_PhutKetThuc : CaLam != null) 
                        select CaLam);
                }
                return Convert<Object.CaLam, Data.CaLam>.ConvertObjs(context.CaLams);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
        public Object.CaLam timKiemMotCaLam(string ma)
        {
            try
            {
                if (string.IsNullOrEmpty(ma))
                    return null;
                Object.CaLam convert = new CaLam();
                Convert<Object.CaLam, Data.CaLam>.ConvertObj(ref convert, context.CaLams.Find(ma));
                return convert;
            }
            catch { }
            return null;
        }
//Ngay nghi
        public int demNgayNghi()
        {
            try
            {
                return context.NgayNghis.Count();
            } catch { }
            return 0;
        }
        public IEnumerable<Object.NgayNghi> layDanhSachNgayNghi()
        {
            try
            {
                
                if (context != null)
                    return Convert<Object.NgayNghi, Data.NgayNghi>.ConvertObjs(context.NgayNghis);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
        public List<IEnumerable<Object.NgayNghi>> layDanhSachNgayNghi(Object.NgayNghi findBy = null, int page = 1, int pageSize = 10, string sortBy = "NN_Ma", bool sortTangDan = true)
        {
            try
            {
                IEnumerable<Object.NgayNghi> results =
                    (findBy != null ? timKiemNhieuNgayNghi(findBy)
                    : Convert<Object.NgayNghi, Data.NgayNghi>.ConvertObjs(from NgayNghi in context.NgayNghis select NgayNghi));
                if (results == null)
                    return new List<IEnumerable<Object.NgayNghi>>()
                    {
                        new List<Object.NgayNghi>(),
                        new List<Object.NgayNghi>()
                    };
                results = ObjectHelper.OrderByDynamic<Object.NgayNghi>(results, sortBy, sortTangDan ? "asc" : "desc");
                int p = results.Count();
                if (p == 0)
                    return new List<IEnumerable<Object.NgayNghi>>()
                    {
                        new List<Object.NgayNghi>(),
                        new List<Object.NgayNghi>()
                    };
                else
                    pageSize = pageSize < p ? pageSize : p;
                p = (p % pageSize) == 0 ? (p / pageSize) : (p / pageSize + 1);
                page = page > p ? p : page <= 0 ? 1 : page;
                return new List<IEnumerable<Object.NgayNghi>>()
                {
                    results,
                    results.Count() > pageSize ? results.ToPagedList(page, pageSize) : results
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
        public IEnumerable<Object.NgayNghi> timKiemNhieuNgayNghi(Object.NgayNghi find = null)
        {
            try
            {
                if (find != null)
                {
                    return Convert<Object.NgayNghi, Data.NgayNghi>.ConvertObjs(from NgayNghi in context.NgayNghis
                        where
                        (find.NN_Ma != null ? NgayNghi.NN_Ma == find.NN_Ma : NgayNghi != null) &&
                        (!string.IsNullOrEmpty(find.NN_GhiChu) ? NgayNghi.NN_GhiChu.StartsWith(find.NN_GhiChu) : NgayNghi != null) &&
                        (find.NN_Ngay != null ? NgayNghi.NN_Ngay == find.NN_Ngay : NgayNghi != null) 
                        select NgayNghi);
                }
                return Convert<Object.NgayNghi, Data.NgayNghi>.ConvertObjs(context.NgayNghis);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
        public Object.NgayNghi timKiemMotNgayNghi(long ma)
        {
            try
            {
                if (ma < 0)
                    return null;
                
                Object.NgayNghi convert = new NgayNghi();
                Convert<Object.NgayNghi, Data.NgayNghi>.ConvertObj(ref convert, context.NgayNghis.Find(ma));
                return convert;
            }
            catch { }
            return null;
        }
//Cham cong
        public int demChamCong()
        {
            try
            {
                return context.ChamCongs.Count();
            } catch { }
            return 0;
        }
        public IEnumerable<Object.ChamCong> layDanhSachChamCong()
        {
            try
            {
                
                if (context != null)
                    return Convert<Object.ChamCong, Data.ChamCong>.ConvertObjs(context.ChamCongs);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
        public List<IEnumerable<Object.ChamCong>> layDanhSachChamCong(Object.ChamCong findBy = null, int page = 1, int pageSize = 10, string sortBy = "CC_Ma", bool sortTangDan = true)
        {
            try
            {
                IEnumerable<Object.ChamCong> results =
                    (findBy != null ? timKiemNhieuChamCong(findBy)
                    : Convert<Object.ChamCong, Data.ChamCong>.ConvertObjs(from ChamCong in context.ChamCongs select ChamCong));
                if (results == null)
                    return new List<IEnumerable<Object.ChamCong>>()
                    {
                        new List<Object.ChamCong>(),
                        new List<Object.ChamCong>()
                    };
                results = ObjectHelper.OrderByDynamic<Object.ChamCong>(results, sortBy, sortTangDan ? "asc" : "desc");
                int p = results.Count();
                if (p == 0)
                    return new List<IEnumerable<Object.ChamCong>>()
                    {
                        new List<Object.ChamCong>(),
                        new List<Object.ChamCong>()
                    };
                else
                    pageSize = pageSize < p ? pageSize : p;
                p = (p % pageSize) == 0 ? (p / pageSize) : (p / pageSize + 1);
                page = page > p ? p : page <= 0 ? 1 : page;
                return new List<IEnumerable<Object.ChamCong>>()
                {
                    results,
                    results.Count() > pageSize ? results.ToPagedList(page, pageSize) : results
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
        public IEnumerable<Object.ChamCong> timKiemNhieuChamCong(Object.ChamCong find = null)
        {
            try
            {
                if (find != null)
                {
                    return Convert<Object.ChamCong, Data.ChamCong>.ConvertObjs(from ChamCong in context.ChamCongs
                        where
                        (find.CC_Ma != null ? ChamCong.CC_Ma == find.CC_Ma : ChamCong != null) &&
                        (find.CC_ThoiGianDen != null ? ChamCong.CC_ThoiGianDen == find.CC_ThoiGianDen : ChamCong != null) &&
                        (find.CC_ThoiGianVe != null ? ChamCong.CC_ThoiGianVe == find.CC_ThoiGianVe : ChamCong != null) &&
                        (find.CC_Ngay != null ? ChamCong.CC_Ngay == find.CC_Ngay : ChamCong != null) &&
                        (!string.IsNullOrEmpty(find.NS_Ma) ? ChamCong.NS_Ma.StartsWith(find.NS_Ma) : ChamCong != null)
                        select ChamCong);
                }
                return Convert<Object.ChamCong, Data.ChamCong>.ConvertObjs(context.ChamCongs);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
        public Object.ChamCong timKiemMotChamCong(long ma)
        {
            try
            {
                if (ma < 0)
                    return null;
                
                Object.ChamCong convert = new ChamCong();
                Convert<Object.ChamCong, Data.ChamCong>.ConvertObj(ref convert, context.ChamCongs.Find(ma));
                return convert;
            }
            catch { }
            return null;
        }
    }
}