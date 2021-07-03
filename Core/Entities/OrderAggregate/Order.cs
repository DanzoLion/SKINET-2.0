using System.Collections.Generic;
using System;
namespace Core.Entities.OrderAggregate
{

    public class Order : BaseEntity
    {
        public Order()
        {
        }

        public Order(IReadOnlyList<OrderItem> orderItems, string buyerEmail, Address shipToAddress, DeliveryMethod deliveryMethod,  decimal subtotal)
        {
            BuyerEmail = buyerEmail;
//            OrderDate = orderDate;                                                              // is already being set inside our class
            ShipToAddress = shipToAddress;
            DeliveryMethod = deliveryMethod;
            OrderItems = orderItems;
            Subtotal = subtotal;
//           Status = status;                                                                           // is already being set inside our class
//            PaymentIntentId = paymentIntentId;                                        // not required
        }

        public string BuyerEmail {get;set;}
        public DateTimeOffset OrderDate  {get;set;} = DateTimeOffset.Now;
        public Address ShipToAddress {get;set;}
        public DeliveryMethod DeliveryMethod {get;set;}
        public IReadOnlyList<OrderItem> OrderItems {get;set;}
        public decimal Subtotal {get;set;}
        public OrderStatus Status {get;set;} = OrderStatus.Pending;
        public string PaymentIntentId {get;set;}

        public decimal GetTotal()
        {
            return Subtotal + DeliveryMethod.Price;
        }
    }
}