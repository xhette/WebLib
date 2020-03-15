namespace WebLib.DataLayer.Base
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class AbonentLists
    {
        [StringLength(20)]
        public string ReaderCard { get; set; }

        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Reader { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Library { get; set; }

        public int AbonentStatus { get; set; }

        public virtual Libraries Libraries { get; set; }

        public virtual Readers Readers { get; set; }
    }
}
