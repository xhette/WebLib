namespace WebLib.DataLayer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("History.Readers")]
    public partial class ReadersHistory
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

        public DateTime? HistoryBirthDate { get; set; }

        [StringLength(4)]
        public string HistoryPassSeria { get; set; }

        [StringLength(6)]
        public string HistoryPassNumber { get; set; }

        [StringLength(150)]
        public string HistoryAddress { get; set; }

        [StringLength(22)]
        public string HistoryPhone { get; set; }

        public int? HistoryUserId { get; set; }

        [StringLength(50)]
        public string CurrentSurname { get; set; }

        [StringLength(50)]
        public string CurrentName { get; set; }

        [StringLength(50)]
        public string CurrentPatronymic { get; set; }

        public DateTime? CurrentBirthDate { get; set; }

        [StringLength(4)]
        public string CurrentPassSeria { get; set; }

        [StringLength(6)]
        public string CurrentPassNumber { get; set; }

        [StringLength(150)]
        public string CurrentAddress { get; set; }

        [StringLength(22)]
        public string CurrentPhone { get; set; }

        public int? CurrentUserId { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(20)]
        public string Operation { get; set; }

        [Key]
        [Column(Order = 2)]
        public DateTime OperationDate { get; set; }
    }
}
