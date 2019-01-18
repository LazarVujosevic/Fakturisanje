namespace Fakturisanje.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Saradnik")]
    public partial class Saradnik
    {
        public int Id { get; set; }

        [StringLength(10)]
        public string Kor_ime { get; set; }

        [StringLength(20)]
        public string Sifra { get; set; }
    }
}
