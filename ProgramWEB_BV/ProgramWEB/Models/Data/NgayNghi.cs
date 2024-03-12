namespace ProgramWEB.Models.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NgayNghi")]
    public partial class NgayNghi
    {
        [Key]
        public long NN_Ma { get; set; }

        [Column(TypeName = "date")]
        public DateTime NN_Ngay { get; set; }

        [StringLength(50)]
        public string NN_GhiChu { get; set; }
    }
}
