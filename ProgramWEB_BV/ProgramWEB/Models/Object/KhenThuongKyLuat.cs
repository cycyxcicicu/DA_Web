using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProgramWEB.Models.Object
{
    public class KhenThuongKyLuat
    {
        public long? KTKL_Ma { get; set; }
        public string KTKL_MoTa { get; set; }
        public DateTime? KTKL_ThoiGian { get; set; }
        public string KTKL_HinhThuc { get; set; }
        public double? KTKL_SoTien { get; set; }
        public string NS_Ma { get; set; }
        public KhenThuongKyLuat()
        {
            this.KTKL_Ma = null;
            this.KTKL_MoTa = string.Empty;
            this.KTKL_ThoiGian = null;
            this.KTKL_HinhThuc = string.Empty;
            this.KTKL_SoTien = null;
            this.NS_Ma = string.Empty;
        }
    }
}