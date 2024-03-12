using ProgramWEB.Define;
using ProgramWEB.Models.Data;
using ProgramWEB.Models.Object;
using ProgramWEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using BCrypt.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Web.UI;
using ProgramWEB.Libary;
using System.Threading;
using System.Configuration;
using System.EnterpriseServices.CompensatingResourceManager;

namespace ProgramWEB.Controllers
{
    public class UserController : Controller
    {
        public ActionResult Index()
        {
            if (ModelState.IsValid)
            {
                if (Session[DefineSession.userSession] == null)
                {
                    return Login();
                }
            }
            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (Session[DefineSession.userSession] != null)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    //Lay du lieu tu cookie
                    if (Request.Cookies[DefineCookie.cookieUsername] == null || Request.Cookies[DefineCookie.cookiePassword] == null)
                        return View();
                    var username = Request.Cookies[DefineCookie.cookieUsername].Value;
                    var password = Request.Cookies[DefineCookie.cookiePassword].Value;
                    if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                        return View();
                    QuanLyNhanSuContext context = new QuanLyNhanSuContext();
                    if (context == null)
                        return View();
                    Models.Data.TaiKhoan taiKhoan = context.TaiKhoans.Where(item => item.TK_TenDangNhap == username).FirstOrDefault();
                    if (taiKhoan == null || taiKhoan.TK_MatKhau != password || Models.Object.User.kiemTraBiKhoaVaMoKhoa(taiKhoan.TK_TenDangNhap))
                    {
                        //Xoa khoi cookie
                        removeFromCookie(new string[] { DefineCookie.cookieUsername, DefineCookie.cookiePassword });
                        return View();
                    }
                    //Them vao session
                    User userSession;
                    if (taiKhoan.TK_QuyenAdmin == true)
                    {
                        userSession = new Admin(taiKhoan.TK_TenDangNhap, taiKhoan.NS_Ma.Trim(),
                        taiKhoan.TK_QuyenAdmin, taiKhoan.TK_QuyenQuanLy, taiKhoan.TK_AnhDaiDien);
                    }
                    else if (taiKhoan.TK_QuyenQuanLy == true)
                    {
                        userSession = new QuanLy(taiKhoan.TK_TenDangNhap, taiKhoan.NS_Ma.Trim(),
                        taiKhoan.TK_QuyenAdmin, taiKhoan.TK_QuyenQuanLy, taiKhoan.TK_AnhDaiDien);
                    }
                    else
                    {
                        userSession = new User(taiKhoan.TK_TenDangNhap, taiKhoan.NS_Ma.Trim(),
                        taiKhoan.TK_QuyenAdmin, taiKhoan.TK_QuyenQuanLy, taiKhoan.TK_AnhDaiDien);
                    }
                    Session.Add(DefineSession.userSession, userSession);
                    string[] beforeURL = (string[])Session[DefineSession.beforeUrlSession];
                    if (beforeURL != null)
                    {
                        Session.Remove(DefineSession.beforeUrlSession);
                        return RedirectToAction(beforeURL[0], beforeURL[1]);
                    }
                    return RedirectToAction("Index", "Home");
                } catch { }
            }
            return View();
        }
        [HttpPost]
        public string ToLogin(string username, string password, bool keepLogin = false)
        {
            try
            {
                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) ||
                    username.Length > 30 || password.Length < 6)
                    return JsonConvert.SerializeObject(new
                    {
                        error = "Thông tin gửi lên không hợp lệ."
                    });
                QuanLyNhanSuContext context = new QuanLyNhanSuContext();
                if (context == null)
                    return JsonConvert.SerializeObject(new
                    {
                        error = DefineError.loiHeThong
                    });
                Models.Data.TaiKhoan taiKhoan = context.TaiKhoans.Find(username);
                string message = Models.Object.User.login(taiKhoan.TK_TenDangNhap, password);
                if (!string.IsNullOrEmpty(message))
                    return JsonConvert.SerializeObject(new
                    {
                        error = message
                    });
                User userSession;
                if (taiKhoan.TK_QuyenAdmin == true)
                {
                    userSession = new Admin(taiKhoan.TK_TenDangNhap, taiKhoan.NS_Ma.Trim(),
                    taiKhoan.TK_QuyenAdmin, taiKhoan.TK_QuyenQuanLy, taiKhoan.TK_AnhDaiDien);
                }
                else if (taiKhoan.TK_QuyenQuanLy == true)
                {
                    userSession = new QuanLy(taiKhoan.TK_TenDangNhap, taiKhoan.NS_Ma.Trim(),
                    taiKhoan.TK_QuyenAdmin, taiKhoan.TK_QuyenQuanLy, taiKhoan.TK_AnhDaiDien);
                }
                else
                {
                    userSession = new User(taiKhoan.TK_TenDangNhap, taiKhoan.NS_Ma.Trim(),
                    taiKhoan.TK_QuyenAdmin, taiKhoan.TK_QuyenQuanLy, taiKhoan.TK_AnhDaiDien);
                }
                Session.Add(DefineSession.userSession, userSession);
                if (keepLogin)
                {
                    DateTime timeSave = DateTime.Now.AddDays(30);
                    Response.AppendCookie(new HttpCookie(DefineCookie.cookieUsername, taiKhoan.TK_TenDangNhap) { Expires = timeSave });
                    Response.AppendCookie(new HttpCookie(DefineCookie.cookiePassword, taiKhoan.TK_MatKhau) { Expires = timeSave });
                }
                return JsonConvert.SerializeObject(new
                {
                    success = "Đăng nhập thành công",
                    url = "Home"
                });
            } catch { }
            return JsonConvert.SerializeObject(new {
                error = DefineError.loiHeThong
            });
        }
        public ActionResult ForgetPassword()
        {
            if (ModelState.IsValid)
            {
                if (Request.Cookies[DefineCookie.cookieUsername] != null || Request.Cookies[DefineCookie.cookiePassword] != null)
                    removeFromCookie(new string[] { DefineCookie.cookieUsername, DefineCookie.cookiePassword });
                if (Session[DefineSession.userSession] != null)
                    Session.Remove(DefineSession.userSession);
            }
            return View("ForgetPassword");
        }
        [HttpPost]
        public ActionResult ForgetPassword(string email)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(email))
                {
                    ViewBag.error = DefineError.loiDuLieuKhongHopLe;
                    return View();
                }
                string error = Models.Object.User.thayDoiMaXacThuc(email);
                if (!string.IsNullOrEmpty(error))
                {
                    ViewBag.error = error;
                    return View();
                }
                Thread sendMail = new Thread(() =>
                {
                    QuanLyNhanSuContext context = new QuanLyNhanSuContext();
                    Models.Data.NhanSu nhanSu = context.NhanSus.Where(item => item.NS_Email == email).FirstOrDefault();
                    if (nhanSu == null)
                        return;
                    Models.Data.TaiKhoan taiKhoan = context.TaiKhoans.Where(item => item.NS_Ma == nhanSu.NS_Ma).FirstOrDefault();
                    if (taiKhoan == null)
                        return;
                    sendEmail("~/Contents/forms/SendEmailForgetPassword.html", nhanSu.NS_Email, "Xác thực đặt lại mật khẩu",
                        new List<string> { "{{email}}", "{{name}}", "{{maXacThuc}}", "{{companyName}}", "{{time}}" },
                        new List<string> { nhanSu.NS_Email, nhanSu.NS_HoVaTen, taiKhoan.TK_MaXacThuc,
                            ConfigurationManager.AppSettings["FromEmailDisplayName"].ToString(), DateTime.Now.ToString()
                    });
                });
                sendMail.Start();
                return XacThucTaiKhoan(email);
            }
            return ForgetPassword();
        }
        [HttpPost]
        public ActionResult XacThucTaiKhoan(string email)
        {
            try
            {
                ViewBag.email = email;
                return View("XacThucTaiKhoan");
            } catch { }
            return ForgetPassword();
        }
        [HttpPost]
        public string GuiLaiMa(string email)
        {
            try
            {
                string error = Models.Object.User.thayDoiMaXacThuc(email);
                if (!string.IsNullOrEmpty(error))
                    return JsonConvert.SerializeObject(new {error = error});
                Thread sendMail = new Thread(() =>
                {
                    QuanLyNhanSuContext context = new QuanLyNhanSuContext();
                    Models.Data.NhanSu nhanSu = context.NhanSus.Where(item => item.NS_Email == email).FirstOrDefault();
                    if (nhanSu == null)
                        return;
                    Models.Data.TaiKhoan taiKhoan = context.TaiKhoans.Where(item => item.NS_Ma == nhanSu.NS_Ma).FirstOrDefault();
                    if (taiKhoan == null)
                        return;
                    sendEmail("~/Contents/forms/SendEmailForgetPassword.html", nhanSu.NS_Email, "Xác thực đặt lại mật khẩu",
                        new List<string> { "{{email}}", "{{name}}", "{{maXacThuc}}", "{{companyName}}", "{{time}}" },
                        new List<string> { nhanSu.NS_Email, nhanSu.NS_HoVaTen, taiKhoan.TK_MaXacThuc,
                            ConfigurationManager.AppSettings["FromEmailDisplayName"].ToString(), DateTime.Now.ToString()
                    });
                });
                sendMail.Start();
                return JsonConvert.SerializeObject(new
                {
                    success = true
                });
            } catch { }
            return JsonConvert.SerializeObject(new
            {
                error = DefineError.loiHeThong
            });
        }
        [HttpPost]
        public string DoiMatKhauMoi(string email, string maXacThuc, string newPassword)
        {
            try
            {
                if (string.IsNullOrEmpty(newPassword))
                    return JsonConvert.SerializeObject(new { error = DefineError.loiDuLieuKhongHopLe });
                string error = Models.Object.User.kiemTraMaXacThuc(email, maXacThuc);
                if (!string.IsNullOrEmpty(error))
                    return JsonConvert.SerializeObject(new { error = error });
                QuanLyNhanSuContext context = new QuanLyNhanSuContext();
                Models.Data.TaiKhoan taiKhoan = context.TaiKhoans.Where(item => item.NhanSu.NS_Email == email).FirstOrDefault();
                if (taiKhoan == null)
                    return JsonConvert.SerializeObject(new { error = DefineError.khongTonTai });
                taiKhoan.TK_MatKhau = BCrypt.Net.BCrypt.HashPassword(newPassword);
                int check = context.SaveChanges();
                if (check == 0)
                    return JsonConvert.SerializeObject(new { error = DefineError.loiHeThong });
                return JsonConvert.SerializeObject(new { success = "Đổi mật khẩu mới thành công" });
            }
            catch { }
            return JsonConvert.SerializeObject(new {error = DefineError.loiHeThong});
        }
        public ActionResult ChangePassword()
        {
            if (ModelState.IsValid)
            {
                if (Session[DefineSession.userSession] == null)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }
        public string ToChangePassword(string oldPassword, string newPassword)
        {
            try
            {
                User user = (User)Session[DefineSession.userSession];
                if (user == null)
                    return DefineError.canDangNhap;
                string error = user.doiMatKhau(oldPassword, newPassword);
                if (string.IsNullOrEmpty(error))
                    return JsonConvert.SerializeObject(new
                    {
                        success = "Đổi mật khẩu thành công"
                    });
                return JsonConvert.SerializeObject(new
                {
                    error = error
                });
            } catch { }
            return JsonConvert.SerializeObject(new { error = DefineError.loiHeThong });
        }
        public ActionResult Logout()
        {
            removeFromCookie(new string[] {DefineCookie.cookieUsername, DefineCookie.cookiePassword});
            Session.Remove(DefineSession.userSession);
            return RedirectToAction("Index","Home");
        }
        public void removeFromCookie(string[] cookiesName)
        {
            DateTime dateTime = DateTime.Now.AddDays(-1);
            for (int i = 0; i < cookiesName.Length; i++)
            {
                Response.AppendCookie(
                    new HttpCookie(cookiesName[i], null)
                    {
                        Expires = dateTime,
                    }
                );
            }
        }
        public ActionResult Profile()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    User user = (User)Session[DefineSession.userSession];
                    if (user == null)
                        return Login();
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    data.Add("taiKhoan", user.getTaiKhoan());
                    data.Add("nhanSu", user.getNhanSu());
                    data.Add("baoHiem", user.getBaoHiems());
                    data.Add("lichSuLamViec", user.getLichSuLamViecs());
                    data.Add("khenThuongKyLuat", user.getKhenThuongKyLuats());
                    ViewBag.data = data;
                } catch { }
            }
            return View();
        }
        [HttpPost]
        public ActionResult Profile(HttpPostedFileBase changeAvatar)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    User user = (User)Session[DefineSession.userSession];
                    if (user == null)
                        return Login();
                    if (changeAvatar == null || changeAvatar.ContentLength == 0)
                        return Profile();
                    var url = "/Images/avatar/";

                    var urlSV = Server.MapPath(url);
                    var date = DateTime.Now;
                    var fileNewName = user.maNhanSu.Trim() + "-" + date.Year + "-" + date.Month + "-" + date.Day + "-" +
                        date.Hour + "-" + date.Minute + "-" + date.Second + "-" + date.Millisecond + "-" + changeAvatar.FileName;
                    changeAvatar.SaveAs(urlSV + fileNewName);

                    string old = user.avatar;

                    if (!user.thayDoiAnhDaiDien(fileNewName))
                        return Profile();

                    user.avatar = fileNewName;

                    FileInfo file = new FileInfo(Server.MapPath(url + old));
                    if (file.Exists)
                    {
                        file.Delete();
                    }
                }
                catch { }
            }
            return Profile();
        }
        public bool sendEmail(string pathForm, string toEmail, string title, List<string> keys, List<string> values)
        {
            try
            {
                string content = System.IO.File.ReadAllText(Server.MapPath(pathForm));
                for (int i = 0; i < keys.Count; i++)
                {
                    content = content.Replace(keys[i], values[i]);
                }
                EmailHelper.SendEmail(toEmail, title, content);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }
    }
}