
using ProjetEchecDAL.Enum;

namespace ProjetEchecDAL.Entities
{
    public class Tournoi
    {
        public Guid Id { get; set; }
        public string Nom { get; set; }
        public string? Lieu { get; set; }
        public int MinJoueur { get; set; }
        public int MaxJoueur { get; set; }
        public int MinElo { get; set; }
        public int MaxElo { get; set; }
        public virtual ICollection<Categorie> Categories { get; set; } = new List<Categorie>();
        public Statut Statut { get; set; }
        public int Ronde { get; set; }
        public bool FemmeOnly { get; set; }
        public DateTime InscriptionLimit { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public virtual ICollection<Joueur> Joueurs { get; set; } = new List<Joueur>();
    }
}
