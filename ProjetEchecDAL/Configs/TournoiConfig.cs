using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjetEchecDAL.Entities;

namespace ProjetEchecDAL.Configs 
{
   internal class TournoiConfig : IEntityTypeConfiguration<Tournoi>
    {
        public void Configure(EntityTypeBuilder<Tournoi> builder)
        {
            builder.HasIndex(p => p.Nom).IsUnique();
            builder.Property(p => p.Statut).HasConversion<string>();
        }
    }
}
