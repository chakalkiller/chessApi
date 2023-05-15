using BLL.Interfaces;
using BLL.Services;
using Domain.DTO.Player;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Domain.Mappers;
using ChessApi.Helpers;
using Domain.DTO.JWT;


namespace chessApi.Controllers

{
    [Route("api/[controller]/")]
    [ApiController]
    [Authorize]
    public class ProfileController : ControllerBase

    {
        private readonly IPlayerService _playerService;
        private readonly JwtHelper _jwtHelper;

        public ProfileController(IPlayerService playerService, JwtHelper jwtHelper)

        {
            _playerService = playerService;
            _jwtHelper = jwtHelper;

        }
        private int? PlayerId
        {
            get
            {
                string? tokenId = User?.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
                return (tokenId is null) ? null : int.Parse(tokenId);
            }
        }



        [HttpGet("{id:int}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PlayerDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public ActionResult<PlayerDTO> GetById([FromRoute] int id)
        {
            PlayerModel? model = _playerService.GetById(id);

            if (model is null)
            {
                return NotFound();
            }

            return Ok(model.ToPlayerDTO());
        }
    }
}
