using Microsoft.AspNetCore.Mvc;
using ServicesAbstraction.Interfaces;
using Share.Dto;
using Share.Dto.UsersDto;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController(IAuthenticationService _authenticationService) : ControllerBase
    {
        [HttpPost("Register")]
        public async Task<ActionResult<UserTokenDto>> Register([FromForm] UserRegisterDto user)
            => Ok(await _authenticationService.Register(user));

        [HttpPost("Login")]
        public async Task<ActionResult<UserTokenDto>> Login([FromBody] UserLoginDto user)
            => Ok(await _authenticationService.Login(user));

        [HttpPost("RefreshToken")]
        public async Task<ActionResult<UserTokenDto>> LoginWithRefreshToken([FromBody] RefreshTokenRequest refreshToken)
            => Ok(await _authenticationService.LoginWithRefreshToken(refreshToken.refreshToken));
    }
}