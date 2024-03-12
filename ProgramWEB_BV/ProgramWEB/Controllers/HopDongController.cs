using Newtonsoft.Json;
using ProgramWEB.Define;
using ProgramWEB.Models.Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProgramWEB.Controllers
{
    public class HopDongController : BaseController
    {
        public string getAll(HopDong findBy = null, int page = 1, int pageSize = 10, string sortBy = "HD_Ma", bool sortTangDan = true)
        {
            try
            {
                QuanLy user = (QuanLy)Session[DefineSession.userSession];
                if (user == null || !user.quyenQuanLy)
                    return "";
                List<IEnumerable<HopDong>> results = user.layDanhSachHopDong(findBy, page, pageSize, sortBy, sortTangDan);
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
        public string add(HopDong hopDong)
        {
            try
            {
                QuanLy user = (QuanLy)Session[DefineSession.userSession];
                if (user == null)
                    return JsonConvert.SerializeObject(new { error = DefineError.canDangNhap });
                if (!user.quyenQuanLy)
                    return JsonConvert.SerializeObject(new { error = DefineError.khongCoQuyen });
                string error = ((QuanLy)user).themHopDong(hopDong);
                if (!string.IsNullOrEmpty(error))
                    return JsonConvert.SerializeObject(new { error = error });
                return JsonConvert.SerializeObject(new
                {
                    success = "Thêm thành công."
                });
            }
            catch { }
            return JsonConvert.SerializeObject(new
            {
                error = DefineError.loiHeThong
            });
        }
        public string delete(long[] mas)
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
                    string error = ((QuanLy)user).xoaHopDong(mas[0]);
                    if (!string.IsNullOrEmpty(error))
                        return JsonConvert.SerializeObject(new { error = error });
                    return JsonConvert.SerializeObject(new
                    {
                        success = "Xóa thành công"
                    });
                }
                if (mas != null && mas.Length > 1)
                {
                    string[] message = user.xoaNhieuHopDong(mas);
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
            }
            catch { }
            return JsonConvert.SerializeObject(new
            {
                error = DefineError.loiHeThong
            });
        }
    }
}