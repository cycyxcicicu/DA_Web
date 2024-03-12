namespace ProgramWEB.Models.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TaiKhoan")]
    public partial class TaiKhoan
    {
        [Key]
        [StringLength(30)]
        public string TK_TenDangNhap { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string TK_MatKhau { get; set; }

        public bool TK_QuyenAdmin { get; set; }

        public bool TK_QuyenQuanLy { get; set; }

        public bool TK_BiKhoa { get; set; }

        public DateTime? TK_ThoiGianMoKhoa { get; set; }

        [StringLength(6)]
        public string TK_MaXacThuc { get; set; }

        public DateTime? TK_ThoiGianTaoMa { get; set; }

        [Column(TypeName = "text")]
        public string TK_AnhDaiDien { get; set; }

        [Required]
        [StringLength(10)]
        public string NS_Ma { get; set; }

        public virtual NhanSu NhanSu { get; set; }
    }
}
