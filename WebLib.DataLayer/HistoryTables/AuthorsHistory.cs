namespace WebLib.DataLayer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("History.Authors")]
    public partial class AuthorsHistory
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [StringLength(50)]
        public string HistorySurname { get; set; }

        [StringLength(50)]
        public string HistoryName { get; set; }

        [StringLength(50)]
        public string HistoryPatronymic { get; set; }

        [StringLength(50)]
        public string CurrentSurname { get; set; }

        [StringLength(50)]
        public string CurrentName { get; set; }

        [StringLength(50)]
        public string CurrentPatronymic { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(20)]
        public string Operation { get; set; }

        [Key]
        [Column(Order = 2)]
        public DateTime OperationDate { get; set; }
    }
}
