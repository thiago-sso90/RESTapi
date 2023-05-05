using Microsoft.EntityFrameworkCore;
using NovoprojetoApi.Entidades;

namespace NovoprojetoApi.percistencia
{
    public class DevEventDbContext :DbContext
    {
        public DevEventDbContext(DbContextOptions<DevEventDbContext> option) : base(option)
        {
            //DevEvents = new List<DevEventSpeker>();
        }

        public DbSet<DevEvent> DevEvents { get; set; }

        public DbSet<DevEventSpeaker> DevEventsSpeaker { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<DevEvent>(e =>
            {
                e.HasKey(de => de.Id);

                e.Property(de => de.Titulo).IsRequired(false);

                e.Property(de => de.Descricao)
                .HasMaxLength(200)
                .HasColumnType("varchar(200)");

                e.Property(de => de.DataInicial)
                .HasColumnName("Data_Inicial");

                e.Property(de => de.DataFinal)
               .HasColumnName("Data_Final");

                e.HasMany(de => de.Palestrante)
                .WithOne()
                .HasForeignKey(s => s.DevEventId);
                

            });

            builder.Entity<DevEventSpeaker>(e =>
            {
                e.HasKey(de => de.Id);
            });
        }


    }
}
