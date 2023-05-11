using ChessApi.Helpers;
using BLL.Interfaces;
using Domain.DTO.JWT;
using Domain.DTO.Player;
using Domain.Forms.Player;
using Domain.Mappers;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Bll.Interfaces;
using Domain.DTO.Tournament;

namespace APIUserDevOps.Controllers
{
    [Route("api/[controller]/")]
    [ApiController]
    [Authorize]
    public class AuthController : ControllerBase
    {

        private readonly IPlayerService _playerService;
        private readonly JwtHelper _jwtHelper;

        public AuthController(IPlayerService playerService, JwtHelper jwtHelper)

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
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<PlayerDTO>))]
        [AllowAnonymous] // desactive authorize
        public ActionResult<IEnumerable<PlayerDTO>> GetAll()
        {
            return Ok(_playerService.GetAll().ToPlayerDTOList());
        }

        [HttpGet("{id:int}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PlayerDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<PlayerDTO> GetById([FromRoute] int id)
        {
            PlayerModel? model = _playerService.GetById(id);

            if (model is null)
            {
                return NotFound();
            }

            return Ok(model.ToPlayerDTO());
        }

        [HttpPost("register")]
        [AllowAnonymous]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PlayerDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<PlayerDTO> Create([FromBody] CreatePlayerForm createForm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            PlayerDTO? player = _playerService.Create(createForm.ToPlayerModel())?.ToPlayerDTO();

            if (player == null) return BadRequest();

            return Created($"/api/Auth/{player.PlayerId}", player);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Delete([FromRoute] int id)
        {
            if (_playerService.Delete(id))
            {
                return NoContent();
            }

            return BadRequest();
        }

        [HttpPatch]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult ChangePassword([FromRoute] ChangePasswordForm changePwdForm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            
            if (PlayerId == null)
            {
                return  Forbid();
            }
            
          

            if (_playerService.ChangePassword((int)PlayerId, changePwdForm.Password))
            {
                return NoContent();
            }

            return BadRequest();
        }

        [HttpPost("login")]
        [AllowAnonymous]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Login([FromBody] LoginPlayerForm loginForm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            int? userId = _playerService.Login(loginForm.Email, loginForm.Password);

            if (userId is null)
            {
                return Problem(
                    detail: "Credential invalide",
                    statusCode: 400
                );
            }
            PlayerModel? playerModel = _playerService.GetById((int)PlayerId);
            if (playerModel is null)
            {
                return Problem(statusCode: StatusCodes.Status500InternalServerError);
            }
            string token = _jwtHelper.CreateToken(playerModel);

            // TODO Change this to use JWT ;)
            return Ok(new JwtDTO() { Token = token });
        }

    }
}
