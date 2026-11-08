using Microsoft.AspNetCore.Http;
using Share.Enums;
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
        public string LastName { get; init; } = string.Empty;
        [Required]
        public IFormFile Image { get; init; } = default!;
        [Required]
        public Gender Gender { get; init; } = default!;
    }
}