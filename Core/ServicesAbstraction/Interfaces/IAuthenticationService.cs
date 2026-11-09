using Share.Dto.UsersDto;

namespace ServicesAbstraction.Interfaces
{
    public interface IAuthenticationService
    {
        Task<UserTokenDto> Login(UserLoginDto user);

        Task<UserTokenDto> Register(UserRegisterDto user);

        Task<UserTokenDto> LoginWithRefreshToken(string refreshToken);
    }
}