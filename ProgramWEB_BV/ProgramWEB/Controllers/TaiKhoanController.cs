using Newtonsoft.Json;
using ProgramWEB.Define;
using ProgramWEB.Models.Object;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProgramWEB.Controllers
{
    public class TaiKhoanController : BaseController
    {
        public string getAll(TaiKhoan findBy = null, int page = 1, int pageSize = 10, string sortBy = "TK_TenDangNhap", bool sortTangDan = true)
        {
            try
            {
                QuanLy user = (QuanLy)Session[DefineSession.userSession];
                if (user == null || !user.quyenQuanLy)
                    return string.Empty;
                List<IEnumerable<TaiKhoan>> results = user.layDanhSachTaiKhoan(findBy, page, pageSize, sortBy, sortTangDan);
                if (results == null)
                    return string.Empty;
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
            return string.Empty;
        }
        public string khoaTaiKhoan(string username, int time = 0)
        {
            try
            {
                Admin user = (Admin)Session[DefineSession.userSession];
                if (user == null)
                    return JsonConvert.SerializeObject(new { error = DefineError.canDangNhap });
                if (!user.quyenAdmin)
                    return JsonConvert.SerializeObject(new { error = DefineError.khongCoQuyen });
                string error = user.khoaTaiKhoan(username, time);
                if (!string.IsNullOrEmpty(error))
                    return JsonConvert.SerializeObject (new { error = error });
                return JsonConvert.SerializeObject(new { success = "Khóa tài khoản thành công" });
            } catch { }
            return JsonConvert.SerializeObject(new
            {
                error = DefineError.loiHeThong
            }); 
        }
        public string capQuyen(string username, bool huy = false)
        {
            try
            {
                Admin user = (Admin)Session[DefineSession.userSession];
                if (user == null)
                    return JsonConvert.SerializeObject(new { error = DefineError.canDangNhap });
                if (!user.quyenAdmin)
                    return JsonConvert.SerializeObject(new { error = DefineError.khongCoQuyen });
                string error = user.capQuyenQuanLy(username, huy);  
                if (!string.IsNullOrEmpty(error))
                    return JsonConvert.SerializeObject(new { error = error });
                if (huy)
                    return JsonConvert.SerializeObject(new { success = "Hủy quyền quản lý cho tài khoản thành công" });
                return JsonConvert.SerializeObject(new { success = "Cấp quyền quản lý cho tài khoản thành công" });
            }
            catch { }
            return JsonConvert.SerializeObject(new
            {
                error = DefineError.loiHeThong
            });
        }
    }
}