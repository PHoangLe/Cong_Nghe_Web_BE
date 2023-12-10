using BusinessObjects.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.DTO.AccountDTO
{
    public record CreateAccountDTO
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Role { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool? IsBlock { get; set; }
        public bool? Gender { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Avatar { get; set; }
        public List<int> PermissionsIds { get; set; } = null!;
    }

    public record ResultAccountDTO
    {
        public Guid AccountId { get; set; }
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Role { get; set; }
        public DateOnly? DateOfBirth { get; set; }
        public bool? IsBlock { get; set; }
        public bool? Gender { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Avatar { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
