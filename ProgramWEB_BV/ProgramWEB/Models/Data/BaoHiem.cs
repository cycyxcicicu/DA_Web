namespace ProgramWEB.Models.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BaoHiem")]
    public partial class BaoHiem
    {
        [Key]
        public long BH_Ma { get; set; }

        [Required]
        [StringLength(10)]
        public string BH_SoBaoHiem { get; set; }

        [Column(TypeName = "date")]
        public DateTime BH_NgayCap { get; set; }

        [Column(TypeName = "date")]
        public DateTime BH_NgayHetHan { get; set; }

        [Required]
        [StringLength(50)]
        public string BH_NoiCap { get; set; }

        [StringLength(50)]
        public string BH_NoiKhamBenh { get; set; }

        [Required]
        [StringLength(10)]
        public string NS_Ma { get; set; }

        public virtual NhanSu NhanSu { get; set; }
    }
}
