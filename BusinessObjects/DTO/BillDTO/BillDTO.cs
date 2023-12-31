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
    public record BillDetailDto
    {
        public int FoodId { get; set; }
        public string Name { get; set; }
        public string image { get; set; }
        public int quantity { get; set; }
        public int price { get; set; }
        public string status { get; set; }
        public DateTime updateAt { get; set; }

        public BillDetailDto(int foodId, string name, string image, int quantity, int price, string status, DateTime updateAt)
        {
            FoodId = foodId;
            Name = name;
            this.image = image;
            this.quantity = quantity;
            this.price = price;
            this.status = status;
            this.updateAt = updateAt;
        }
    }
}
