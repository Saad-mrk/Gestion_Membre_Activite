using Microsoft.EntityFrameworkCore;

namespace Gestion_des_membres_et_activités_d_un_club.Models
{
    public class ClubContext : DbContext
    {
        // Le constructeur obligatoire pour ASP.NET Core
        public ClubContext(DbContextOptions<ClubContext> options) : base(options)
        {
        }

    
        public DbSet<Membre> Membres { get; set; }

        public DbSet<Activite> Activites { get; set; }
        public DbSet<Inscription> Inscriptions { get; set; }

        public override int SaveChanges()
        {
            NormalizeDateTimesToUtc();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            NormalizeDateTimesToUtc();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void NormalizeDateTimesToUtc()
        {
            foreach (var entry in ChangeTracker.Entries().Where(e =>
                         e.State is EntityState.Added or EntityState.Modified))
            {
                foreach (var property in entry.Properties)
                {
                    if (property.Metadata.ClrType == typeof(DateTime) && property.CurrentValue is DateTime dateTime)
                    {
                        property.CurrentValue = NormalizeDateTime(dateTime);
                    }
                    else if (property.Metadata.ClrType == typeof(DateTime?) &&
                             property.CurrentValue is DateTime nullableDateTime)
                    {
                        property.CurrentValue = NormalizeDateTime(nullableDateTime);
                    }
                }
            }
        }

        private static DateTime NormalizeDateTime(DateTime value) =>
            value.Kind switch
            {
                DateTimeKind.Utc => value,
                DateTimeKind.Local => value.ToUniversalTime(),
                _ => DateTime.SpecifyKind(value, DateTimeKind.Utc)
            };
    }
}
