namespace WebLib.DataLayer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("_temp_Book")]
    public partial class C_temp_Book
    {
        public int? Id { get; set; }

        [StringLength(50)]
        public string Title { get; set; }

        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AuthorId { get; set; }

        public int? DepartmentId { get; set; }

        [StringLength(50)]
        public string Operation { get; set; }

        [Key]
        [Column(Order = 1, TypeName = "timestamp")]
        [MaxLength(8)]
        [Timestamp]
        public byte[] DateTime { get; set; }

        public virtual Author Author { get; set; }

        public virtual Department Department { get; set; }
    }
}
