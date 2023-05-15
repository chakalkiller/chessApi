using Bll.Interfaces;
using BLL.Services;
using ChessApi.Helpers;
using Domain.DTO.Tournament;
using Domain.Forms.Player;
using Domain.Forms.TournamentForm;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Domain.Mappers;
using BLL.Interfaces;
using Domain.DTO.Player;
using Domain.Models;

namespace chessApi.Controllers
{
    [Route("api/[controller]/")]
    [ApiController]
    public class TournamentController : ControllerBase
    {

        private readonly ITournamentService _tournamentService;

        public TournamentController(ITournamentService tournamentService)

        {
            _tournamentService = tournamentService;
        }
        private int? PlayerId
        {
            get
            {
                string? tokenId = User?.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
                return (tokenId is null) ? null : int.Parse(tokenId);
            }
        }

        [HttpPost("register")]
        [AllowAnonymous]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TournamentDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
       public ActionResult<TournamentDTO> Create([FromBody] CreateTournamentForm createForm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            TournamentDTO? tournament = _tournamentService.Create(createForm.ToTournamentModel((int)PlayerId), (int)PlayerId)?.ToTournamentDTO();

            if (tournament == null) return BadRequest();

            return Created($"/api/Tornament/{tournament.TournamentId}", tournament);
        }
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<TournamentDTO>))]
        [AllowAnonymous] // desactive authorize
        public ActionResult<IEnumerable<TournamentDTO>> GetAll()
        {
            return Ok(_tournamentService.GetAll().ToTournamentDTOList());
        }

        [HttpGet("{id:int}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TournamentDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<TournamentDTO> GetById([FromRoute] int id)
        {
            TournamentModel? model = _tournamentService.GetById(id);

            if (model is null)
            {
                return NotFound();
            }

            return Ok(model.ToTournamentDTO());
        }
        [HttpPatch("{TournamentId:int}/open")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult OpenRegister([FromRoute] int TournamentId, [FromBody] TournamentStatusForm tournamentStatus)
        {
            TournamentModel? tournament = _tournamentService.GetById(TournamentId);
            if (!ModelState.IsValid || tournament is null )
                return BadRequest();
            if (tournamentStatus.ChangeStatus != true)
                return new ForbidResult();
            if (_tournamentService.OpenRegister(TournamentId, (int)PlayerId))
                return NoContent();
            return BadRequest();
        }
        /*
        [HttpPatch("{id:int}/cancel")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CancelTournament([FromRoute] int playerId, [FromBody] CreateTournamentForm tournamentStatus)
        {
            TournamentModel? tournament = _tournamentService.GetById(playerId);
            if (!ModelState.IsValid || tournament is null || (tournament.TournamentState != "Announced" && tournament.TournamentState != "Registering"))
                return BadRequest();
            if (tournamentStatus.Changer_Le_Statut_Oui_Ou_Non.ToLower() != "oui")
                return new ForbidResult();
            if (_tournamentService.CancelTournament(playerId, (int)PlayerId))
                return NoContent();
            return BadRequest();
        }
        */

    }
}
