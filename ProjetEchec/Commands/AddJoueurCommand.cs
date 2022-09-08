using ProjetEchecDAL.Enum;

namespace ProjetEchec.Commands
{
    public class AddJoueurCommand
    {
        public string Pseudo { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty; 
        public string Password { get; set; } = string.Empty;
        public DateTime Birthday { get; set; }
        public Genre Genre { get; set; }
        public int? Elo { get; set; }
    }
}
