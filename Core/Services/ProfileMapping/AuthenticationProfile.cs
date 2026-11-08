using AutoMapper;
using Domain.Models;
using Share.Dto.UsersDto;

namespace Services.ProfileMapping
{
    public class AuthenticationProfile : Profile
    {
        public AuthenticationProfile()
        {
            CreateMap<UserRegisterDto, User>();
        }
    }
}