using BusinessObjects.DataModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.DTO.AuthDTO
{
    public record LoginDto
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters")]
        [MaxLength(20, ErrorMessage = "Password cannot exceed 20 characters")]
        public string Password { get; set; }
    }

    public record ResultLoginDto
    {
        public string Email { get; set; }
        public string Role { get; set; }
        public string Name { get; set; }
        public string? Avatar { get; set; }
        public string AccessToken { get; set; }
        public Guid AccountId { get; set; }
        public List<Permission> Permissions { get; set; } = null!;
    }

    public record GetAuthAccountDto
    {
        public Guid AccountId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string Name { get; set; }
        public string? Avatar { get; set; }
        public bool? IsBlock { get; set; }
    }
}
