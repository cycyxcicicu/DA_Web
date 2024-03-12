using ProgramWEB.Models.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProgramWEB.Models.Object
{
    public class PhongBan
    {
        public string PB_Ma { get; set; }
        public string PB_Ten { get; set; }
        public string PB_VaiTro { get; set; }
        public string NS_Ma { get; set; }
        public PhongBan()
        {
            this.PB_Ma = string.Empty;
            this.PB_Ten = string.Empty;
            this.PB_VaiTro = string.Empty;
            this.NS_Ma = string.Empty;
        }
    }
}