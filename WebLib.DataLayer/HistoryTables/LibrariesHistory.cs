namespace WebLib.DataLayer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("History.Libraries")]
    public partial class LibrariesHistory
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [StringLength(50)]
        public string HistoryName { get; set; }

        [StringLength(150)]
        public string HistoryAddress { get; set; }

        [StringLength(22)]
        public string HistoryPhone { get; set; }

        public int? HistoryCity { get; set; }

        [StringLength(50)]
        public string CurrentName { get; set; }

        [StringLength(150)]
        public string CurrentAddress { get; set; }

        [StringLength(22)]
        public string CurrentPhone { get; set; }

        public int? CurrentCity { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(20)]
        public string Operation { get; set; }

        [Key]
        [Column(Order = 2)]
        public DateTime OperationDate { get; set; }
    }
}
