using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Services.Implementation
{
    public class AuthenticationService(UserManager<User> _userManager, IMapper _mapper,
        IAttachmentService attachment, IOptions<JWT> jwt) : IAuthenticationService
    {
        public async Task<UserDto> Login(UserLoginDto user)
        {
            var currentuser = await _userManager.FindByEmailAsync(user.Email);
            if (currentuser is null)
                throw new UnauthenticationException("Password or Emial is Wrong");

            if (!await _userManager.CheckPasswordAsync(currentuser, user.Password))
                throw new UnauthenticationException("Password or Emial is Wrong");

            var originUser = await _userManager.FindByEmailAsync(user.Email);
            if (originUser is null)
                throw new Exception("");

            string token = await CreateToken(originUser);
            return new(currentuser.UserName ?? "", user.Email, token);
        }

        public async Task<UserDto> Register(UserRegisterDto user)
        {
            if (user.Password != user.ConfirmedPassword)
                throw new AccountExceptions("Password not equal Confirmed Password");

            var originUser = _mapper.Map<User>(user);
            var result = await _userManager.CreateAsync(originUser, user.Password);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new AccountExceptions($"Failed to register {user.UserName} : {errors}");
            }
            if (user.Image is not null)
            {
                var path = attachment.Upload(user.Image, "ProfileImages");
                originUser.PictureUrl = path;
            }
            await _userManager.AddToRoleAsync(originUser, "User");
            string token = await CreateToken(originUser);
            return new(user.UserName, user.Email, token);
        }

        private async Task<string> CreateToken(User user)
        {
            var JwtOptions = jwt.Value;
            var Claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Name,user.UserName??""),
                new Claim(ClaimTypes.Email,user.Email??""),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            };

            var roles = await _userManager.GetRolesAsync(user);
            Claims.AddRange(roles.Select(e => new Claim(ClaimTypes.Role, e)));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtOptions.SecretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: JwtOptions.Issuer,
                audience: JwtOptions.Audience,
                claims: Claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(JwtOptions.ExpirationInMinutes),
                signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}