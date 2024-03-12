namespace ProgramWEB.Models.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HopDong")]
    public partial class HopDong
    {
        [Key]
        public long HD_Ma { get; set; }

        [Column(TypeName = "date")]
        public DateTime HD_NgayBatDau { get; set; }

        [Column(TypeName = "date")]
        public DateTime? HD_NgayKetThuc { get; set; }

        [Required]
        [StringLength(30)]
        public string HD_HinhThucLamViec { get; set; }

        public double HD_Luong { get; set; }

        [Required]
        [StringLength(20)]
        public string HD_DonViTinhuong { get; set; }

        [Required]
        [StringLength(30)]
        public string HD_CongViec { get; set; }

        [Required]
        [StringLength(10)]
        public string NS_Ma { get; set; }

        public virtual NhanSu NhanSu { get; set; }
    }
}
