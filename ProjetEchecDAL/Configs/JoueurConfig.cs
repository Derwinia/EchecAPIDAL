using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjetEchecDAL.Entities;

namespace ProjetEchecDAL.Configs
{
    internal class JoueurConfig : IEntityTypeConfiguration<Joueur>
    {
        public void Configure(EntityTypeBuilder<Joueur> builder)
        {
            builder.HasIndex(p => p.Email).IsUnique();
            builder.HasIndex(p => p.Pseudo).IsUnique();
            builder.Property(p => p.Genre).HasConversion<string>();
            builder.Property(p => p.Droit).HasConversion<string>();
        }
    }
}
