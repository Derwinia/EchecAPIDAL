using Microsoft.AspNetCore.Mvc;
using ProjetEchec.Commands;
using ProjetEchec.DTO;
using ProjetEchec.Services;
using ProjetEchecDAL.Entities;

namespace ProjetEchec.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly TokenService _tokenService;
        private readonly JoueurService _joueurService;

        public LoginController(TokenService tokenService, JoueurService joueurService)
        {
            _tokenService = tokenService;
            _joueurService = joueurService;
        }

        [HttpPost]
        public IActionResult Login([FromBody] LoginCommand cmd)
        {
            JoueurDTO? u = _joueurService.Login(cmd.Pseudo, cmd.Password);
            if (u is null)
            {
                return BadRequest("Pseudo ou mot de passe incorrect");
            }
            if (u.Password != cmd.Password) // verifier password haché
            {
                return BadRequest("Pseudo ou mot de passe incorrect");
            }

            return Ok(
                new
                { 
                    token = _tokenService.CreateToken(u)
                }
            );
        }
    }
}
