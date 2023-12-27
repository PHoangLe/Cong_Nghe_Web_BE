using BusinessObjects.DataModels;
using BusinessObjects.DTO.AuthDTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.DTO.AccountDTO
{
    public record CreateAccountDto
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; } = null!;
        [Required(ErrorMessage = "Password is required")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters")]
        [MaxLength(20, ErrorMessage = "Password cannot exceed 20 characters")]
        public string Password { get; set; } = null!;

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; } = null!;
        public bool? Gender { get; set; }
        public string? PhoneNumber { get; set; }
        public int[]? PermissionIds { get; set; }
    }

    public record UpdateAccountDto
    {
        public string? Password { get; set; } = null!;
        public string? Name { get; set; } = null!;
        public bool? Gender { get; set; }
        public string? Avatar { get; set; }
        public string? PhoneNumber { get; set; }
        public DateOnly? DateOfBirth { get; set; }
        public bool? IsBlock { get; set; }
        public int[]? PermissionIds { get; set; }
    }

    public record ResultAccountDTO
    {
        public Guid AccountId { get; set; }
        public string Role { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Name { get; set; } = null!;
        public bool? Gender { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Avatar { get; set; }
        public DateOnly? DateOfBirth { get; set; }
        public bool? IsBlock { get; set; }
        public List<PermissonDto> Permissions { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    public record AccountSuccinctDto 
    {
        public Guid AccountId { get; set; }
        public string Role { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public bool IsBlock { get; set; }
        public string? Avatar { get; set; }
    }

    public record AccountShortDto
    {
        public Guid AccountId { get; set; }
        public string Email { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public string? Avatar { get; set; }
    }

    public record BlockAccountDto
    {
        [Required(ErrorMessage = "isBlock is required")]
        public Boolean isBlock { get; set; }
    }

    public record UpdatePasswordDto
    {
        [Required(ErrorMessage = "password is required")]
        public string? password { get; set; }

        [Required(ErrorMessage = "new password is required")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters")]
        [MaxLength(20, ErrorMessage = "Password cannot exceed 20 characters")]
        public string? newPassword { get; set; }
    }
}
