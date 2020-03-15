namespace WebLib.DataLayer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("History.Books")]
    public partial class BooksHistory
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [StringLength(50)]
        public string HistoryTitle { get; set; }

        public int? HistoryAuthor { get; set; }

        public int? HistoryDepartment { get; set; }

        [StringLength(50)]
        public string CurrentTitle { get; set; }

        public int? CurrentAuthor { get; set; }

        public int? CurrentDepartment { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(20)]
        public string Operation { get; set; }

        [Key]
        [Column(Order = 2)]
        public DateTime OperationDate { get; set; }
    }
}
