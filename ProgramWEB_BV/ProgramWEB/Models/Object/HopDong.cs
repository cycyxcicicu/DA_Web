using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProgramWEB.Models.Object
{
    public class HopDong
    {
        public long? HD_Ma { get; set; }
        public DateTime? HD_NgayBatDau { get; set; }
        public DateTime? HD_NgayKetThuc { get; set; }
        public string HD_HinhThucLamViec { get; set; }
        public double? HD_Luong { get; set; }
        public string HD_DonViTinhuong { get; set; }
        public string HD_CongViec { get; set; }
        public string NS_Ma { get; set; }
        public HopDong()
        {
            this.HD_Ma = null;
            this.HD_NgayBatDau = null;
            this.HD_NgayKetThuc = null;
            this.HD_HinhThucLamViec = string.Empty;
            this.HD_Luong = null;
            this.HD_DonViTinhuong = string.Empty;
            this.HD_CongViec = string.Empty;
            this.NS_Ma = string.Empty;
        }
    }
}