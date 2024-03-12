using Newtonsoft.Json;
using ProgramWEB.Models.Object;
using ProgramWEB.Define;
using System.Linq;
using Newtonsoft.Json.Converters;
using Microsoft.Ajax.Utilities;
using System.Collections.Generic;
using PagedList;
using System.Globalization;
using System.Collections;
using System;
using System.Threading;
using System.Configuration;
using System.Web.Mvc;

namespace ProgramWEB.Controllers
{
    public class NhanSuController : BaseController
    {
        public string getAll(NhanSu findBy = null, int page = 1, int pageSize = 10, string sortBy = "NS_Ma", bool sortTangDan = true)
        {
            try
            {
                QuanLy user = (QuanLy)Session[DefineSession.userSession];
                if (user == null || !user.quyenQuanLy)
                    return "";
                List<IEnumerable<NhanSu>> results = user.layDanhSachNhanSu(findBy, page, pageSize, sortBy, sortTangDan);
                if (results == null)
                    return "";
                return JsonConvert.SerializeObject(new
                {
                    countData = results[0].Count(),
                    data = results[1]
                }, Formatting.Indented,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                }
                );
            }
            catch { }
            return "";
        }
        public string add(NhanSu nhanSu)
        {
            try
            {
                QuanLy user = (QuanLy)Session[DefineSession.userSession];
                if (user == null)
                    return JsonConvert.SerializeObject(new { error = DefineError.canDangNhap });
                if (!user.quyenQuanLy)
                    return JsonConvert.SerializeObject(new { error = DefineError.khongCoQuyen });
                string error = ((QuanLy)user).themNhanSu(nhanSu);
                if (!string.IsNullOrEmpty(error))
                    return JsonConvert.SerializeObject(new { error = error });
                Thread createAccount = new Thread(() => {
                    try
                    {
                        string[] tach = nhanSu.NS_HoVaTen.Split(' ');
                        string name = tach[tach.Length - 1];
                        name = Libary.StringHelper.RemoveDiacritics(name);
                        string password = "";
                        Random ran = new Random();
                        for (int j = 0; j < 6; j++)
                        {
                            password += (char)ran.Next(48, 127);
                        }
                        Models.Data.QuanLyNhanSuContext context = new Models.Data.QuanLyNhanSuContext();
                        Models.Data.TaiKhoan taiKhoan = new Models.Data.TaiKhoan();
                        int i = -1;
                        while (true)
                        { 
                            taiKhoan.TK_TenDangNhap = (name + (nhanSu.NS_Ma.StartsWith("NS") ?
                                nhanSu.NS_Ma.Substring(2).Trim() : nhanSu.NS_Ma.Trim())).ToLower();
                            if (i >= 0)
                                taiKhoan.TK_TenDangNhap += i.ToString();
                            if (context.TaiKhoans.Find(taiKhoan.TK_TenDangNhap) == null)
                                break;
                            i++;
                        }
                        taiKhoan.TK_MatKhau = BCrypt.Net.BCrypt.HashPassword(password);
                        taiKhoan.NS_Ma = nhanSu.NS_Ma;
                        context.TaiKhoans.Add(taiKhoan);
                        int check = context.SaveChanges();
                        if (check == 0)
                            return;
                        sendEmail("~/Contents/forms/SendEmailCreateSuccessAccount.html", nhanSu.NS_Email, "Cấp tài khoản sử dụng hệ thống",
                            new List<string> { "{{email}}", "{{name}}", "{{username}}", "{{password}}", "{{companyName}}", "{{time}}" },
                            new List<string> { nhanSu.NS_Email, nhanSu.NS_HoVaTen, 
                                taiKhoan.TK_TenDangNhap, password, ConfigurationManager.AppSettings["FromEmailDisplayName"].ToString(), DateTime.Now.ToString()
                        });
                    }
                    catch { }
                });
                createAccount.Start();
                return JsonConvert.SerializeObject(new
                {
                    success = "Thêm thành công."
                });
            } catch { }
            return JsonConvert.SerializeObject(new
            {
                error = DefineError.loiHeThong
            }); 
        }
        public string edit(NhanSu nhanSu)
        {
            try
            {
                QuanLy user = (QuanLy)Session[DefineSession.userSession];
                if (user == null)
                    return JsonConvert.SerializeObject(new { error = DefineError.canDangNhap });
                if (!user.quyenQuanLy)
                    return JsonConvert.SerializeObject(new { error = DefineError.khongCoQuyen });
                string error = ((QuanLy)user).suaNhanSu(nhanSu);
                if (!string.IsNullOrEmpty(error))
                    return JsonConvert.SerializeObject(new { error = error });
                return JsonConvert.SerializeObject(new
                {
                    success = "Sửa thành công"
                });
            }
            catch { }
            return JsonConvert.SerializeObject(new
            {
                error = DefineError.loiHeThong
            });
        }
        public string delete(string[] mas)
        {
            try
            {
                QuanLy user = (QuanLy)Session[DefineSession.userSession];
                if (user == null)
                    return JsonConvert.SerializeObject(new { error = DefineError.canDangNhap });
                if (!user.quyenQuanLy)
                    return JsonConvert.SerializeObject(new { error = DefineError.khongCoQuyen });
                if (mas != null && mas.Length == 1)
                {
                    string error = ((QuanLy)user).xoaNhanSu(mas[0]);
                    if (!string.IsNullOrEmpty(error))
                        return JsonConvert.SerializeObject(new { error = error });
                    return JsonConvert.SerializeObject(new
                    {
                        success = "Xóa thành công"
                    });
                }
                if (mas != null && mas.Length > 1 )
                {
                    string[] message = user.xoaNhieuNhanSu(mas);
                    if (message.Length > 0)
                    {
                        string error = message[0];
                        string success = message.Length > 1 ? message[1] : string.Empty;
                        return JsonConvert.SerializeObject(new
                        {   
                            success = success,
                            error = error
                        });
                    }
                }
            } catch { }
            return JsonConvert.SerializeObject(new
            {
                error = DefineError.loiHeThong
            });
        }
        public ActionResult Profile(string ma)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    QuanLy user = (QuanLy)Session[DefineSession.userSession];
                    if (user == null || !user.quyenQuanLy)
                        return RedirectToAction("Index","Home");
                    NhanSu nhanSu = user.timKiemMotNhanSu(ma);
                    if (nhanSu == null)
                        return RedirectToAction("NotFound","Error");
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    data.Add("taiKhoan", user.timTaiKhoanBangMaNhanSu(nhanSu.NS_Ma));
                    data.Add("nhanSu", nhanSu);
                    data.Add("baoHiem", user.layDanhSachBaoHiem(new BaoHiem() { NS_Ma = nhanSu.NS_Ma }, 1, user.demBaoHiem(), DefineTable.baoHiem.thuocTinhs[2], true)[0]);
                    data.Add("lichSuLamViec", user.layDanhSachLichSuLamViec(new LichSuLamViec() { NS_Ma = nhanSu.NS_Ma }, 1, user.demLichSuLamViec(), DefineTable.lichSuLamViec.thuocTinhs[1], true)[0]);
                    data.Add("khenThuongKyLuat", user.layDanhSachKhenThuongKyLuat(new KhenThuongKyLuat() { NS_Ma = nhanSu.NS_Ma }, 1, user.demKhenThuongKyLuat(), DefineTable.khenThuongKyLuat.thuocTinhs[3], true)[0]);
                    ViewBag.data = data;
                }
                catch { }
            }
            return View();
        }
    }
}