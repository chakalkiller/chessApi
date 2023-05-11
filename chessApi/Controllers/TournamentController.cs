using Bll.Interfaces;
using BLL.Interfaces;
using BLL.Services;
using ChessApi.Helpers;
using Domain.DTO.Tournament;
using Domain.Forms.Player;
using Domain.Forms.TournamentForm;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Domain.Mappers;

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

            TournamentDTO? tournament = _tournamentService.Create(createForm.ToTournamentModel())?.ToTournamentDTO();

            if (tournament == null) return BadRequest();

            return Created($"/api/Tornament/{tournament.TournamentId}", tournament);
        }
    }
}
