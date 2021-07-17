using System.Collections.Generic;

namespace Core.Entities
{
    public class CustomerBasket
    {
        public CustomerBasket()                                         // empty construct for Redis so we don't have issues when creating a new instance of CustomerBasket, ie new instance without knowing Id
        {
        }

        public CustomerBasket(string id)
        {
            Id = id;
        }

        public string Id { get; set; }                                   // Angular as the customer will generate a unique Id 
        public List<BasketItem> Items {get; set; } = new List<BasketItem>();
        public int? DeliveryMethodId { get; set; }
        public string ClientSecret { get; set; }
        public string PaymentIntentId { get; set; }
        public decimal ShippingPrice { get; set; }
     }
}