namespace Fakturisanje.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Stavke")]
    public partial class Stavke
    {
        [Key]
        public int Redni_broj { get; set; }

        public int Kolicina { get; set; }

        public int Cena { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int? Ukupno { get; set; }
    }
}
