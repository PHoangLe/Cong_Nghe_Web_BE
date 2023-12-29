using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.DTO.FeedBackDTO
{
    public record CreateFeedBackDto
    {
        [Required]
        public int ServingId { get; set; }
        [Required]
        public int Rating { get; set; }
        [Required]
        public string? Message { get; set; }
    }

    public record ResultFeedBackDto
    {
        [Required]
        public int ServingId { get; set; }
        [Required]
        public int Rating { get; set; }
        [Required]
        public string? Message { get; set; }
    }
}
