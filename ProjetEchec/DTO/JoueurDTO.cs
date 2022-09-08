using ProjetEchecDAL.Enum;

namespace ProjetEchec.DTO
{
    public class JoueurDTO
    {
        public Guid Id { get; set; }
        public string? Pseudo { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public DateTime Birthday { get; set; }
        public Genre Genre { get; set; } 
        public int Elo { get; set; }
        public Droit Droit { get; set; }
    }
}