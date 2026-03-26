using Microsoft.EntityFrameworkCore;

namespace Gestion_des_membres_et_activités_d_un_club.Models
{
    public class ClubContext : DbContext
    {
        // Le constructeur obligatoire pour ASP.NET Core
        public ClubContext(DbContextOptions<ClubContext> options) : base(options)
        {
        }

        // Vos 3 tables dans la base de données
        public DbSet<Membre> Membres { get; set; }

        public DbSet<Activite> Activites { get; set; }
        public DbSet<Inscription> Inscriptions { get; set; }
    }
}