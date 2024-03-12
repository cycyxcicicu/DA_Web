using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace ProgramWEB.Models.Data
{
    public partial class QuanLyNhanSuContext : DbContext
    {
        public QuanLyNhanSuContext()
            : base("name=QuanLyNhanSuContext")
        {
        }

        public virtual DbSet<BaoHiem> BaoHiems { get; set; }
        public virtual DbSet<BoPhan> BoPhans { get; set; }
        public virtual DbSet<CaLam> CaLams { get; set; }
        public virtual DbSet<ChamCong> ChamCongs { get; set; }
        public virtual DbSet<DangKyCaLam> DangKyCaLams { get; set; }
        public virtual DbSet<DangKyNghiLam> DangKyNghiLams { get; set; }
        public virtual DbSet<DuyetDangKy> DuyetDangKies { get; set; }
        public virtual DbSet<HopDong> HopDongs { get; set; }
        public virtual DbSet<KhenThuongKyLuat> KhenThuongKyLuats { get; set; }
        public virtual DbSet<LichSuLamViec> LichSuLamViecs { get; set; }
        public virtual DbSet<NgayNghi> NgayNghis { get; set; }
        public virtual DbSet<NhanSu> NhanSus { get; set; }
        public virtual DbSet<PhongBan> PhongBans { get; set; }
        public virtual DbSet<TaiKhoan> TaiKhoans { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BaoHiem>()
                .Property(e => e.BH_SoBaoHiem)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<BaoHiem>()
                .Property(e => e.NS_Ma)
                .IsUnicode(false);

            modelBuilder.Entity<BoPhan>()
                .Property(e => e.BP_Ma)
                .IsUnicode(false);

            modelBuilder.Entity<BoPhan>()
                .Property(e => e.PB_Ma)
                .IsUnicode(false);

            modelBuilder.Entity<BoPhan>()
                .Property(e => e.NS_Ma)
                .IsUnicode(false);

            modelBuilder.Entity<CaLam>()
                .Property(e => e.CL_Ma)
                .IsUnicode(false);

            modelBuilder.Entity<ChamCong>()
                .Property(e => e.NS_Ma)
                .IsUnicode(false);

            modelBuilder.Entity<DangKyCaLam>()
                .Property(e => e.NS_Ma)
                .IsUnicode(false);

            modelBuilder.Entity<DangKyCaLam>()
                .Property(e => e.CL_Ma)
                .IsUnicode(false);

            modelBuilder.Entity<DangKyNghiLam>()
                .Property(e => e.NS_Ma)
                .IsUnicode(false);

            modelBuilder.Entity<DuyetDangKy>()
                .Property(e => e.NS_Ma)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<DuyetDangKy>()
                .HasMany(e => e.DangKyCaLams)
                .WithOptional(e => e.DuyetDangKy)
                .WillCascadeOnDelete();

            modelBuilder.Entity<DuyetDangKy>()
                .HasMany(e => e.DangKyNghiLams)
                .WithOptional(e => e.DuyetDangKy)
                .WillCascadeOnDelete();

            modelBuilder.Entity<HopDong>()
                .Property(e => e.NS_Ma)
                .IsUnicode(false);

            modelBuilder.Entity<KhenThuongKyLuat>()
                .Property(e => e.NS_Ma)
                .IsUnicode(false);

            modelBuilder.Entity<LichSuLamViec>()
                .Property(e => e.NS_Ma)
                .IsUnicode(false);

            modelBuilder.Entity<LichSuLamViec>()
                .Property(e => e.BP_Ma)
                .IsUnicode(false);

            modelBuilder.Entity<NhanSu>()
                .Property(e => e.NS_Ma)
                .IsUnicode(false);

            modelBuilder.Entity<NhanSu>()
                .Property(e => e.NS_SoDienThoai)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<NhanSu>()
                .Property(e => e.NS_Email)
                .IsUnicode(false);

            modelBuilder.Entity<NhanSu>()
                .Property(e => e.NS_SoCCCD)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<NhanSu>()
                .Property(e => e.NS_SoTaiKhoanNganHang)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<NhanSu>()
                .Property(e => e.NS_TenChuTaiKhoan)
                .IsUnicode(false);

            modelBuilder.Entity<PhongBan>()
                .Property(e => e.PB_Ma)
                .IsUnicode(false);

            modelBuilder.Entity<PhongBan>()
                .Property(e => e.NS_Ma)
                .IsUnicode(false);

            modelBuilder.Entity<TaiKhoan>()
                .Property(e => e.TK_TenDangNhap)
                .IsUnicode(false);

            modelBuilder.Entity<TaiKhoan>()
                .Property(e => e.TK_MatKhau)
                .IsUnicode(false);

            modelBuilder.Entity<TaiKhoan>()
                .Property(e => e.TK_MaXacThuc)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<TaiKhoan>()
                .Property(e => e.TK_AnhDaiDien)
                .IsUnicode(false);

            modelBuilder.Entity<TaiKhoan>()
                .Property(e => e.NS_Ma)
                .IsUnicode(false);
        }
    }
}
