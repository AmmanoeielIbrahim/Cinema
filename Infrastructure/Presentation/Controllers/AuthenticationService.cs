using Microsoft.AspNetCore.Mvc;
using ServicesAbstraction.Interfaces;
using Share.Dto.UsersDto;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationService(IAuthenticationService _authenticationService) : ControllerBase
    {
        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register(UserRegisterDto user)
            => Ok(await _authenticationService.Register(user));

        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login(UserLoginDto user)
            => Ok(await _authenticationService.Login(user));
    }
}