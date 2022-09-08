namespace ProjetEchecDAL.Entities
{
    public class Categorie
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public ICollection<Tournoi> Tournois { get; set; }
    }
}
