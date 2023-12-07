using System.ComponentModel.DataAnnotations;

namespace BusinessObjects.DTO.CategoryDTO
{
    public record CreateCategoryDto
    {
        [Required]
        public string CategoryName { get; set; } = null!;
    }

    public record ResultCategoryDto
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        public string Status { get; set; } = null!;
    }
}
