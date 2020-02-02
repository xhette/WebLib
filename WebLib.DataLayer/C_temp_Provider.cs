namespace WebLib.DataLayer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("_temp_Provider")]
    public partial class C_temp_Provider
    {
        public int? Id { get; set; }

        [StringLength(50)]
        public string Surname { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(50)]
        public string Patronymic { get; set; }

        [StringLength(150)]
        public string Address { get; set; }

        public int? UserId { get; set; }

        [StringLength(50)]
        public string Operation { get; set; }

        [Key]
        [Column(TypeName = "timestamp")]
        [MaxLength(8)]
        [Timestamp]
        public byte[] DateTime { get; set; }

        public virtual Users Users { get; set; }
    }
}
