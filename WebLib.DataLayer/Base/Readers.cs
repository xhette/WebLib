namespace WebLib.DataLayer.Base
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Readers
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Readers()
        {
            Issues = new HashSet<Issues>();
            AbonentLists = new HashSet<AbonentLists>();
        }

        public int Id { get; set; }

        [StringLength(50)]
        public string Surname { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(50)]
        public string Patronymic { get; set; }

        public DateTime? BirthDate { get; set; }

        [StringLength(4)]
        public string PassSeria { get; set; }

        [StringLength(6)]
        public string PassNumber { get; set; }

        [StringLength(150)]
        public string Address { get; set; }

        [StringLength(22)]
        public string Phone { get; set; }

        public int? UserId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Issues> Issues { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AbonentLists> AbonentLists { get; set; }

        public virtual Users Users { get; set; }
    }
}
