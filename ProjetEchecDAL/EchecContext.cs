using Microsoft.EntityFrameworkCore;
using ProjetEchecDAL.Configs;
using ProjetEchecDAL.Entities;

namespace ProjetEchecDAL
{
    public class EchecContext : DbContext
    {
        public DbSet<Joueur> Joueurs { get; set; }
        public DbSet<Tournoi> Tournois { get; set; }
        public DbSet<Categorie> Categories { get; set; }

        public EchecContext(DbContextOptions options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new JoueurConfig());
            builder.ApplyConfiguration(new TournoiConfig());
        }
    }
}
