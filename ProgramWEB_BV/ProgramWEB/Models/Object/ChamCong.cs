using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProgramWEB.Models.Object
{
    public class ChamCong
    {
        public long? CC_Ma { get; set; }
        public DateTime? CC_ThoiGianDen { get; set; }
        public DateTime? CC_ThoiGianVe { get; set; }
        public DateTime? CC_Ngay { get; set; }
        public string NS_Ma { get; set; }
        public ChamCong()
        {
            this.CC_Ma = null;
            this.CC_ThoiGianDen = null;
            this.CC_ThoiGianVe = null;
            this.CC_Ngay = null;
            this.NS_Ma = string.Empty;
        }
    }
}