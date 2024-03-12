namespace ProgramWEB.Models.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NhanSu")]
    public partial class NhanSu
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NhanSu()
        {
            BaoHiems = new HashSet<BaoHiem>();
            BoPhans = new HashSet<BoPhan>();
            ChamCongs = new HashSet<ChamCong>();
            DangKyCaLams = new HashSet<DangKyCaLam>();
            DangKyNghiLams = new HashSet<DangKyNghiLam>();
            HopDongs = new HashSet<HopDong>();
            KhenThuongKyLuats = new HashSet<KhenThuongKyLuat>();
            LichSuLamViecs = new HashSet<LichSuLamViec>();
            PhongBans = new HashSet<PhongBan>();
            TaiKhoans = new HashSet<TaiKhoan>();
        }

        [Key]
        [StringLength(10)]
        public string NS_Ma { get; set; }

        [Required]
        [StringLength(50)]
        public string NS_HoVaTen { get; set; }

        public bool NS_GioiTinh { get; set; }

        [Column(TypeName = "date")]
        public DateTime NS_NgaySinh { get; set; }

        [Required]
        [StringLength(10)]
        public string NS_SoDienThoai { get; set; }

        [Required]
        [StringLength(100)]
        public string NS_Email { get; set; }

        [Column(TypeName = "ntext")]
        [Required]
        public string NS_DiaChi { get; set; }

        [Required]
        [StringLength(12)]
        public string NS_SoCCCD { get; set; }

        [StringLength(10)]
        public string NS_SoTaiKhoanNganHang { get; set; }

        [StringLength(50)]
        public string NS_TenChuTaiKhoan { get; set; }

        [StringLength(20)]
        public string NS_HocVan { get; set; }

        [Column(TypeName = "date")]
        public DateTime NS_NgayVao { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BaoHiem> BaoHiems { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BoPhan> BoPhans { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChamCong> ChamCongs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DangKyCaLam> DangKyCaLams { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DangKyNghiLam> DangKyNghiLams { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HopDong> HopDongs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<KhenThuongKyLuat> KhenThuongKyLuats { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LichSuLamViec> LichSuLamViecs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PhongBan> PhongBans { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TaiKhoan> TaiKhoans { get; set; }
    }
}
