using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.DTO.BillDTO
{
    public record BillCreateDto
    {
        [Required (ErrorMessage = "ServingId is required")]
        public int ServingId { get; set; }
        public Guid? CreatedBy { get; set; }
    }
    public record BillResultDto
    {
        public int ServingId { get; set; }
        public Guid CreatedBy { get; set; }
        public int TotalPrice { get; set; }
    }
}
