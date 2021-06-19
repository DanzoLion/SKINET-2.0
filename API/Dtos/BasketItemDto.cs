using System.ComponentModel.DataAnnotations;

namespace API.Dtos
{
    public class BasketItemDto
    {
        [Required]
        public int Id { get; set; }
        
        [Required]
        public string ProductName { get; set; }
        
        [Required]
        [Range(0.1, double.MaxValue, ErrorMessage = "Price should be greater than zero")]
        public decimal Price { get; set; }
        
        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Quantity should be at least 1")]
        public int Quantity { get; set; }
        
        [Required]
        public string PictureUrl { get; set; }
        
        [Required]
        public string Brand { get; set; }
        
        [Required]
        public string Type { get; set; }
    }
}