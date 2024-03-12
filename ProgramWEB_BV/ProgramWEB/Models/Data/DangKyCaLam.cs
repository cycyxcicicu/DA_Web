namespace ProgramWEB.Models.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DangKyCaLam")]
    public partial class DangKyCaLam
    {
        [Key]
        public long DKCL_Ma { get; set; }

        [Column(TypeName = "date")]
        public DateTime DKCL_Ngay { get; set; }

        public DateTime? DKCL_ThoiGianDangKy { get; set; }

        public bool DKCL_DaDuocDuyet { get; set; }

        [Required]
        [StringLength(10)]
        public string NS_Ma { get; set; }

        [Required]
        [StringLength(10)]
        public string CL_Ma { get; set; }

        public long? DDK_Ma { get; set; }

        public virtual CaLam CaLam { get; set; }

        public virtual NhanSu NhanSu { get; set; }

        public virtual DuyetDangKy DuyetDangKy { get; set; }
    }
}
