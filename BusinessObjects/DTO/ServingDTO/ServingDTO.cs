using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.DTO.ServingDTO
{
    public record CreateServingDTO
    {
        public Guid CreatedBy { get; set; }
        public int NumberOfCutomer { get; set; }
        public DateTime TimeIn { get; set; }

        [Required(ErrorMessage = "DiningTableIds is required!")]
        public List<int> DiningTableIds { get; set; }
    }
}
