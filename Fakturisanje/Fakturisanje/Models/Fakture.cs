namespace Fakturisanje.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Fakture")]
    public partial class Fakture
    {
        public int Id { get; set; }

        [Column(TypeName = "date")]
        public DateTime Datum_Fakture { get; set; }

        [Required]
        [StringLength(10)]
        public string Br_Dokumenta { get; set; }

        public double Ukupno { get; set; }
    }
}
