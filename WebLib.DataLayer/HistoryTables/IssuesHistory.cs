namespace WebLib.DataLayer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("History.Issues")]
    public partial class IssuesHistory
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public DateTime? HistoryIssueDate { get; set; }

        public DateTime? HistoryReturnDate { get; set; }

        public int? HistoryBook { get; set; }

        public int? HistoryReader { get; set; }

        public DateTime? CurrentIssueDate { get; set; }

        public DateTime? CurrentReturnDate { get; set; }

        public int? CurrentBook { get; set; }

        public int? CurrentReader { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(20)]
        public string Operation { get; set; }

        [Key]
        [Column(Order = 2)]
        public DateTime OperationDate { get; set; }
    }
}
