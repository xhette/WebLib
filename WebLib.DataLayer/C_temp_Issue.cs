namespace WebLib.DataLayer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("_temp_Issue")]
    public partial class C_temp_Issue
    {
        public int? Id { get; set; }

        public DateTime? IssueDate { get; set; }

        public DateTime? ReturnDate { get; set; }

        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int BookId { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ReaderId { get; set; }

        [StringLength(50)]
        public string Operation { get; set; }

        [Key]
        [Column(Order = 2, TypeName = "timestamp")]
        [MaxLength(8)]
        [Timestamp]
        public byte[] DateTime { get; set; }

        public virtual Book Book { get; set; }

        public virtual Reader Reader { get; set; }
    }
}
