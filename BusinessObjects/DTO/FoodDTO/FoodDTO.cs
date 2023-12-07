using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.DTO
{
    public record CreateFoodDto
    {
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public int Price { get; set; }
        public string? Recipe { get; set; }
        public string? Image { get; set; }
        [Required]
        public string Status { get; set; } = null!;
    }


    public record ResultFoodDto
    {
        public int FoodId { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; } = null!;
        public int Price { get; set; }
        public string? Recipe { get; set; }
        public string? Image { get; set; }
        public string Status { get; set; } = null!;
    }
}
