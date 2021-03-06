namespace WebLib.DataLayer.Base
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Librarians
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [StringLength(50)]
        public string Surname { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(50)]
        public string Patronymic { get; set; }

        [StringLength(150)]
        public string Address { get; set; }

        [StringLength(22)]
        public string Phone { get; set; }

        public int Library { get; set; }

        public int? UserId { get; set; }

        public virtual Libraries Libraries { get; set; }

        public virtual Users Users { get; set; }
    }
}
