using BusinessObjects.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.DTO
{
    public record CreateDiningTableDto
    {
        [Required]
        public Guid CreatedBy { get; set; }

        [Required]
        public string TableNumber { get; set; }
        
        [Required] 
        public int Capacity { get; set; }
    }

    public record UpdateDiningTableDto
    {
        [Required]
        public Guid CreatedBy { get; set; }

        [Required]
        public string TableNumber { get; set; }

        [Required]
        public int Capacity { get; set; }
        [Required]
        public string Status { get; set; }
    }

    public record ResultDiningTableDto
    {
        public int TableId { get; set; }
        public Guid CreatedBy { get; set; }
        public string Status { get; set; }
        public string TableNumber { get; set; }
        public int Capacity { get; set; }
    }

    public record DiningTableShortDto
    {
        public int TableId { get; set; }
        public string Status { get; set; } = null!;
        public string TableNumber { get; set; } = null!;
        public int Capacity { get; set; }
    }
}
