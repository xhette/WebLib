namespace WebLib.DataLayer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("_temp_Reader")]
    public partial class C_temp_Reader
    {
        public int? Id { get; set; }

        [StringLength(50)]
        public string Surname { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(50)]
        public string Patronymic { get; set; }

        public DateTime? BirthDate { get; set; }

        [StringLength(4)]
        public string PassSeria { get; set; }

        [StringLength(6)]
        public string PassNumber { get; set; }

        [StringLength(150)]
        public string Address { get; set; }

        [StringLength(22)]
        public string Phone { get; set; }

        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UserId { get; set; }

        [StringLength(50)]
        public string Operation { get; set; }

        [Key]
        [Column(Order = 1, TypeName = "timestamp")]
        [MaxLength(8)]
        [Timestamp]
        public byte[] DateTime { get; set; }

        public virtual Users Users { get; set; }
    }
}
