using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjetEchec.Commands;
using ProjetEchec.DTO;
using ProjetEchec.Services;
using System.ComponentModel.DataAnnotations;

namespace ProjetEchec.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JoueurController : ControllerBase
    {
        private readonly JoueurService _joueurService;

        public JoueurController(JoueurService joueurService)
        {
            _joueurService = joueurService;
        }

        [HttpGet]
        [Produces(typeof(IEnumerable<JoueurDTO>))]
        public IActionResult Get()
        {
            IEnumerable<JoueurDTO> result = _joueurService.Get();
            return Ok(result);
        }

        [HttpGet("{id}")]
        [Produces(typeof(JoueurDTO))]
        public IActionResult GetById([FromRoute] Guid id)
        {
            JoueurDTO result = _joueurService.GetById(id);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Post([FromBody] AddJoueurCommand cmd)
        {
            try
            {
                _joueurService.Add(cmd);
                return NoContent();
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
