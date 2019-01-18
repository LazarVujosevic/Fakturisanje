namespace Fakturisanje.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model11")
        {
        }

        public virtual DbSet<Fakture> Faktures { get; set; }
        public virtual DbSet<Saradnik> Saradniks { get; set; }
        public virtual DbSet<Stavke> Stavkes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
