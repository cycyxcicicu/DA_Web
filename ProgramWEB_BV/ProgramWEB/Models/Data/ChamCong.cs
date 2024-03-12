namespace ProgramWEB.Models.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ChamCong")]
    public partial class ChamCong
    {
        [Key]
        public long CC_Ma { get; set; }

        public DateTime CC_ThoiGianDen { get; set; }

        public DateTime? CC_ThoiGianVe { get; set; }

        [Column(TypeName = "date")]
        public DateTime CC_Ngay { get; set; }

        [Required]
        [StringLength(10)]
        public string NS_Ma { get; set; }

        public virtual NhanSu NhanSu { get; set; }
    }
}
