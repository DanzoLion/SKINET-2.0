using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.Dtos
{
    public class CustomerBasketDto
    {
        [Required]
        public string Id { get; set; }                                   // Angular as the customer will generate a unique Id 
        public List<BasketItemDto> Items {get; set; }
    }
}