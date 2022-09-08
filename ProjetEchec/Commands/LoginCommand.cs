using System.ComponentModel.DataAnnotations;

namespace ProjetEchec.Commands
{
    public class LoginCommand
    {
        [Required]
        public string Pseudo { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
