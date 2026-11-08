using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Share.Dto.UsersDto
{
    public record UserRegisterDto
    {
        [Required]
        public DateOnly BirthDate { get; init; }
        [Required, PasswordPropertyText]
        public string Password { get; init; } = string.Empty;
        [Required, PasswordPropertyText]
        public string ConfirmedPassword { get; init; } = string.Empty;
        [Required, EmailAddress, MaxLength(150)]
        public string Email { get; init; } = string.Empty;
        [Required, Phone, MaxLength(20)]
        public string PhoneNumber { get; init; } = string.Empty;
        [Required, MaxLength(50)]
        public string FirstName { get; init; } = string.Empty;
        [Required, MaxLength(50)]
        public string UserName { get; init; } = string.Empty;
        [Required]
        public string LastName { get; init; } = string.Empty;
        public IFormFile? Image { get; init; } = default!;
        [Required, MaxLength(30)]
        public string Gender { get; init; } = default!;
    }
}