using ProjetEchecDAL.Entities;
using ProjetEchecDAL.Enum;

namespace ProjetEchec.DTO
{
    public class TournoiDTO
    {
        public Guid Id { get; set; }
        public string Nom { get; set; } = string.Empty;
        public string Lieu { get; set; } = string.Empty;
        public int? NbJoueurActuel { get; set; }
        public int NbJoueurMin { get; set; }
        public int NbJoueurMax { get; set; }
        public int EloMin { get; set; }
        public int EloMax { get; set; }
        public double DateLimiteInscription { get; set; }
        public Statut Statut { get; set; }
        public string[] Categories { get; set; }
        public bool femmeOnly { get; set; }
        public int Ronde { get; set; }
        public IEnumerable<JoueurDTO>? Joueurs { get; set; }

    }
}
