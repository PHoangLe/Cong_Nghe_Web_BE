using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.Enum;

namespace BusinessObjects.DTO.FoodOrderDTO
{
    public record FoodOrderAddDTO
    {
        public int FoodId { get; set; }
        public int Quantity { get; set; }
        public string? Note { get; set; }
        public int Price { get; set; }
    }
    
    public record FoodOrderShortDto
    {
        public int FoodOrderId { get; set; }
        public string FoodName { get; set; } 
        public int FoodId { get; set; }
        public int ServingId { get; set; }
        public int Quantity { get; set; }
        public string Status { get; set; } = null!;
        public string? Note { get; set; }
        public DateTime? CreatedAt { get; set; }
    }

    public record FoodOrderShortWithPriceDto
    {
        public int FoodOrderId { get; set; }
        public string FoodName { get; set; }
        public int FoodId { get; set; }
        public int ServingId { get; set; }
        public int Quantity { get; set; }
        public string Status { get; set; } = null!;
        public string? Note { get; set; }
        public int Price { get; set; }
        public string? Image { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
