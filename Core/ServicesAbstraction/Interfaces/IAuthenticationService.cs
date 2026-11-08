using Share.Dto.UsersDto;

namespace ServicesAbstraction.Interfaces
{
    public interface IAuthenticationService
    {
        Task<UserDto> Login(UserLoginDto user);

        Task<UserDto> Register(UserRegisterDto user);
    }
}