namespace ProjetEchec.Commands
{
    public class AddTournoiCommand
    {
        public string Nom { get; set; }
        public string? Lieu { get; set; }
        public int MinJoueur { get; set; }
        public int MaxJoueur { get; set; }
        public int MinElo { get; set; }
        public int MaxElo { get; set; }
        public string[] Categories { get; set; }
        public bool FemmeOnly { get; set; }
        public DateTime InscriptionLimit { get; set; }
    }
}
