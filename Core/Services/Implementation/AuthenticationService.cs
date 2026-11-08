using AutoMapper;
using Domain.Exceptions;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using ServicesAbstraction.Interfaces;
using Share.Dto.UsersDto;

namespace Services.Implementation
{
    public class AuthenticationService(UserManager<User> _userManager, IMapper _mapper,
        IAttachmentService attachment) : IAuthenticationService
    {
        public async Task<UserDto> Login(UserLoginDto user)
        {
            var currentuser = await _userManager.FindByEmailAsync(user.Email);
            if (currentuser is null)
                throw new UnauthenticationException("Password or Emial is Wrong");

            if (!await _userManager.CheckPasswordAsync(currentuser, user.Password))
                throw new UnauthenticationException("Password or Emial is Wrong");
            var token = string.Empty;
            return new(currentuser.UserName ?? "", user.Email, token);
        }

        public async Task<UserDto> Register(UserRegisterDto user)
        {
            if (user.Password != user.ConfirmedPassword)
                throw new AccountExceptions("Password not equal Confirmed Password");
            var userModel = _mapper.Map<User>(user);
            var result = await _userManager.CreateAsync(userModel, user.Password);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new AccountExceptions($"Failed to register {user.UserName} : {errors}");
            }
            if (user.Image is not null)
            {
                var path = attachment.Upload(user.Image, "ProfileImages");
                userModel.PictureUrl = path;
            }
            await _userManager.AddToRoleAsync(userModel, "User");
            var token = string.Empty;
            return new(user.UserName, user.Email, "");
        }
    }
}