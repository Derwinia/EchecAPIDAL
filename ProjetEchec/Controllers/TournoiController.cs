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
    public class TournoiController : ControllerBase
    {
        private readonly TournoiService _tournoiService;

        public TournoiController(TournoiService tournoiService)
        {
            _tournoiService = tournoiService;
        }

        [HttpGet]
        [Produces(typeof(IEnumerable<TournoiDTO>))]
        public IActionResult Get()
        {
            IEnumerable<TournoiDTO> result = _tournoiService.Get();
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Post([FromBody] AddTournoiCommand cmd)
        {
            try
            {
                _tournoiService.Add(cmd);
                return NoContent();
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("addjoueur")]
        public IActionResult PostJoueur([FromBody] AddJoueurForm form)
        {
            try
            {
                _tournoiService.AddJoueur(form.Tournoi, form.Joueur);
                return NoContent();
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] Guid id)
        {
            try
            {
                _tournoiService.Remove(id);
                return NoContent(); // 204
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}
