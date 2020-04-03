namespace WebLib.DataLayer.Base
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Supplies
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Supplies()
        {
            OrderLists = new HashSet<OrderLists>();
        }

        public int Id { get; set; }

        public DateTime? Date { get; set; }

        public double? Summ { get; set; }

        public int Shop { get; set; }

        public virtual Shops Shops { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderLists> OrderLists { get; set; }
    }
}
