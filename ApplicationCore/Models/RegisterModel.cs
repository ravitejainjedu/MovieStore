using System;
using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Models
{
    public class RegisterModel
    {
        [Required, StringLength(128)]
        public string FirstName { get; set; } = string.Empty;

        [Required, StringLength(128)]
        public string LastName { get; set; } = string.Empty;

        [DataType(DataType.Date)]
        [Display(Name = "Date of Birth")]
        public DateTime? DateOfBirth { get; set; }

        [Phone, StringLength(16)]
        public string? PhoneNumber { get; set; }

        [Required, EmailAddress, StringLength(256)]
        public string Email { get; set; } = string.Empty;

        [Required, DataType(DataType.Password), StringLength(100, MinimumLength = 6)]
        public string Password { get; set; } = string.Empty;

        [Required, DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare(nameof(Password), ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
