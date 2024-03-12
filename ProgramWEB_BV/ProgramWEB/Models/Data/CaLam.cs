namespace ProgramWEB.Models.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CaLam")]
    public partial class CaLam
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CaLam()
        {
            DangKyCaLams = new HashSet<DangKyCaLam>();
        }

        [Key]
        [StringLength(10)]
        public string CL_Ma { get; set; }

        [Required]
        [StringLength(30)]
        public string CL_TenCa { get; set; }

        public short CL_GioBatDau { get; set; }

        public short CL_PhutBatDau { get; set; }

        public short CL_GioKetThuc { get; set; }

        public short CL_PhutKetThuc { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DangKyCaLam> DangKyCaLams { get; set; }
    }
}
