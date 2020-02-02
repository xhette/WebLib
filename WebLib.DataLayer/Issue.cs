namespace WebLib.DataLayer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Issue")]
    public partial class Issue
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public DateTime? IssueDate { get; set; }

        public DateTime? ReturnDate { get; set; }

        public int BookId { get; set; }

        public int ReaderId { get; set; }

        public virtual Book Book { get; set; }

        public virtual Reader Reader { get; set; }
    }
}
