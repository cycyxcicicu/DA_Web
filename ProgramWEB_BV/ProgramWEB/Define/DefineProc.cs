using ProgramWEB.Define.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProgramWEB.Define
{
    public class DefineProc
    {
        public static Proc nhanSuGetAll { get; } = new Proc("proc_NhanSu_GetAll", 
            new string[] {"@ma", "@hoVaTen", "@gioiTinh", "@ngaySinh", 
                            "@soDienThoai", "@email", "@diaChi", "@cccd", 
                            "@soTaiKhoan", "@chuTaiKhoan", "@hocVan", "@ngayVao"});
        public DefineProc() { 
        }
    }
}