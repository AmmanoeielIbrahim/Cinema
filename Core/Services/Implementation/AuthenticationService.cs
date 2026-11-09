using Domain.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace Services.Implementation
{
    public class AuthenticationService(UserManager<User> _userManager, IMapper _mapper,
        IAttachmentService attachment, IOptions<JWT> jwt, IRefreshToken _refreshToken) : IAuthenticationService
    {
        public async Task<UserTokenDto> Login(UserLoginDto user)
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

            string refreshTokenn = CreateRefreshToken();
            await _refreshToken.AddAsync(refreshTokenn, currentuser.Id);

            return new(token, refreshTokenn);
        }

        public async Task<UserTokenDto> Register(UserRegisterDto user)
        {
            if (user.Password != user.ConfirmedPassword)
                throw new AccountExceptions("Password not equal Confirmed Password");
            string? imagePath = null;
            if (user.Image is not null)
                imagePath = await attachment.UploadAsync(user.Image, "ProfileImages");

            var originUser = _mapper.Map<User>(user);
            var result = await _userManager.CreateAsync(originUser, user.Password);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new AccountExceptions($"Failed to register {user.UserName} : {errors}");
            }
            await _userManager.AddToRoleAsync(originUser, "User");

            if (!string.IsNullOrEmpty(imagePath))
                originUser.PictureUrl = imagePath;

            string token = await CreateToken(originUser);
            string refreshTokenn = CreateRefreshToken();
            await _refreshToken.AddAsync(refreshTokenn, originUser.Id);
            return new(token, refreshTokenn);
        }

        public async Task<UserTokenDto> LoginWithRefreshToken(string refreshToken)
        {
            var refresh = await _refreshToken.GetByTokenAsync(refreshToken);
            if (refresh is null || refresh.ExpiresOnUtc < DateTime.UtcNow)
                throw new ApplicationException("The refresh token has expired");

            string token = await CreateToken(refresh.User);
            await _refreshToken.UpdateAsync(refresh, CreateRefreshToken());
            return new UserTokenDto(token, refresh.Token);
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

        private string CreateRefreshToken()
            => Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
    }
}