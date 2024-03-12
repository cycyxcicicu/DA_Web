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
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            try
            {
                User user = (User)Session[DefineSession.userSession];
                if (user == null)
                    return RedirectToAction("NotFound", "Error");
                List<Dictionary<string, object>> listManagement = new List<Dictionary<string, object>>();
                List<Dictionary<string, object>> listItem = new List<Dictionary<string, object>>();
                Dictionary<string, object> item1 = new Dictionary<string, object>();
                Dictionary<string, object> item = new Dictionary<string, object>();

                //Thong tin ca nhan
                item.Add("define", DefinePage.user_Profile);
                item.Add("icon", "fa-solid fa-address-card");
                listItem.Add(item);

                item = new Dictionary<string, object>();
                item.Add("define", DefinePage.user);
                item.Add("icon", "fa-solid fa-user");
                listItem.Add(item);

                item1.Add("name", "Người dùng");
                item1.Add("data", listItem);
                listManagement.Add(item1);
                item1 = new Dictionary<string, object>();
                listItem = new List<Dictionary<string, object>>();

                //Cac chuc nang
                if (user.quyenQuanLy)
                {
                    item = new Dictionary<string, object>();
                    item.Add("define", DefinePage.management);
                    item.Add("icon", "fa-solid fa-bars-progress");
                    listItem.Add(item);
                }

                item = new Dictionary<string, object>();
                item.Add("define", DefinePage.chamCong);
                item.Add("icon", "fa-regular fa-calendar-check");
                listItem.Add(item);

                item1.Add("name", "Chức năng");
                item1.Add("data", listItem);
                listManagement.Add(item1);
                item1 = new Dictionary<string, object>();
                listItem = new List<Dictionary<string, object>>();

                //Thông tin công ty
                item = new Dictionary<string, object>();
                item.Add("define", DefinePage.home_XemNgayNghi);
                item.Add("icon", "fa-solid fa-calendar-xmark");
                listItem.Add(item);

                item = new Dictionary<string, object>();
                item.Add("define", DefinePage.home_XemCaLam);
                item.Add("icon", "fa-solid fa-layer-group");
                listItem.Add(item);

                item1.Add("name", "Thông tin công ty");
                item1.Add("data", listItem);
                listManagement.Add(item1);

                ViewBag.data = JsonConvert.SerializeObject(new
                {
                    data = listManagement
                });
            } catch { }
            return View();
        }

        public ActionResult XemNgayNghi(int year = 0, int month = 0)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Models.Object.User user = (User)Session[DefineSession.userSession];
                    if (user == null)
                        return Index();
                    if (year == 0 || month == 0) {
                        year = DateTime.Now.Year;
                        month = DateTime.Now.Month;
                    }
                    IEnumerable<NgayNghi> ngayNghis = user.getNgayNghis(year, month);
                    ViewBag.jsonString = JsonConvert.SerializeObject(new
                    {
                        month = month,
                        year = year,
                        data = ngayNghis
                    });
                } catch { }
            }
            return View();
        }

        public ActionResult XemCaLam()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Models.Object.User user = (User)Session[DefineSession.userSession];
                    if (user != null)
                    {
                        ViewBag.data = user.getCaLams();
                        return View();
                    }
                }
                catch { }
            }
            return Index();
        }
    }
}