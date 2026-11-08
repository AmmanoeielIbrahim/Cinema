using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Share.Dto.UsersDto
{
    public record UserLoginDto
    {
        [Required, MaxLength(150), EmailAddress]
        public string Email { get; init; } = string.Empty;
        [Required, PasswordPropertyText]
        public string Password { get; init; } = string.Empty;
    }
}