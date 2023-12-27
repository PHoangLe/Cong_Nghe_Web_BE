using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.Enum;

namespace BusinessObjects.DTO.ReservationDTO
{
    public record CreateReservationDto
    {
        public Guid CreatedBy { get; set; }
        
        public string CustomerName { get; set; } = null!;

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd HH:mm:ss.fff}")]
        public DateTime ReservationTime { get; set; }

        [Required(ErrorMessage = "Phone of customer is reqired.")]

        public string CustomerPhone { get; set; } = null!;

        public int? NumberOfCustomer { get; set; }
        
        public string? Note { get; set; }
        
        public string? Status { get; set; }
    }

    public record UpdateReservationDto
    {
        public string? CustomerName { get; set; } = null!;

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd HH:mm:ss.fff}")]
        public DateTime? ReservationTime { get; set; }

        public string? CustomerPhone { get; set; } = null!;

        public int? NumberOfCustomer { get; set; }

        public string? Note { get; set; }

        public string? Status { get; set; }
    }
}


