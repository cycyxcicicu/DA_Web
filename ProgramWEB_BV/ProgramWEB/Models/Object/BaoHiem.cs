using System;
using System.ComponentModel.DataAnnotations;

namespace ProgramWEB.Models.Object
{
    public class BaoHiem
    {
        public long? BH_Ma { get; set; }
        public string BH_SoBaoHiem { get; set; }
        public DateTime? BH_NgayCap { get; set; }
        public DateTime? BH_NgayHetHan { get; set; }
        public string BH_NoiCap { get; set; }
        public string BH_NoiKhamBenh { get; set; }
        public string NS_Ma { get; set; }
        public BaoHiem()
        {
            this.BH_Ma = null;
            this.BH_SoBaoHiem = string.Empty;
            this.BH_NgayCap = null;
            this.BH_NgayHetHan = null;
            this.BH_NoiCap = string.Empty;
            this.BH_NoiKhamBenh = string.Empty;
            this.NS_Ma = string.Empty;
        }
    }
}
