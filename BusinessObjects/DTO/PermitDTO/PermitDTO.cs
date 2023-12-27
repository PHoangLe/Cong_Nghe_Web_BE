using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.DTO.PermitDTO
{
    public record CreatePermitDto
    {
        [Required(ErrorMessage = "PermissionId is required")]
        public int PermissionId { get; set; }

        [Required(ErrorMessage = "AccountId is required")]
        public Guid AccountId { get; set; }
    }

    public record UpdatePermitAccountDto {
        [Required(ErrorMessage = "permissionIds is required")]
        public List<int>? permissionIds { get; set; }
    }
}
