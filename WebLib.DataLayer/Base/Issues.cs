namespace WebLib.DataLayer.Base
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Issues
    {
        public int Id { get; set; }

        public DateTime? IssueDate { get; set; }

        public DateTime? ReturnDate { get; set; }

        public int Book { get; set; }

        public int Reader { get; set; }

        public virtual Books Books { get; set; }

        public virtual Readers Readers { get; set; }
    }
}
