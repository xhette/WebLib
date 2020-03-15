namespace WebLib.DataLayer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("History.Supplies")]
    public partial class SuppliesHistory
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public DateTime? HistoryDate { get; set; }

        public decimal? HistorySumm { get; set; }

        public int? HistoryShop { get; set; }

        public DateTime? CurrentDate { get; set; }

        public decimal? CurrentSumm { get; set; }

        public int? CurrentShop { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(20)]
        public string Operation { get; set; }

        [Key]
        [Column(Order = 2)]
        public DateTime OperationDate { get; set; }
    }
}
