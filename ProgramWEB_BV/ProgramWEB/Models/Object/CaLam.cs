using ProgramWEB.Models.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProgramWEB.Models.Object
{
    public class CaLam
    {
        public string CL_Ma { get; set; }
        public string CL_TenCa { get; set; }
        public short? CL_GioBatDau { get; set; }
        public short? CL_PhutBatDau { get; set; }
        public short? CL_GioKetThuc { get; set; }
        public short? CL_PhutKetThuc { get; set; }
        public CaLam()
        {
            this.CL_Ma = string.Empty;
            this.CL_TenCa = string.Empty;
            this.CL_GioBatDau = null;
            this.CL_GioKetThuc = null;
            this.CL_PhutBatDau = null;
            this.CL_PhutKetThuc = null;
        }
    }
}