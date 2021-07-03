using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Entities.Identity;
using Core.Entities.OrderAggregate;

namespace API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductToReturnDto>()
            .ForMember(d => d.ProductBrand, o => o.MapFrom(s => s.ProductBrand.Name))
            .ForMember(d => d.ProductType, o => o.MapFrom(s => s.ProductType.Name))
            .ForMember(d => d.PictureUrl, o => o.MapFrom<ProductUrlResolver>());

            CreateMap<Core.Entities.Identity.Address, AddressDto>().ReverseMap();     // used after creating AddressDto to avoid cycle confilct when returning UserAddress details // ReverseMap() acts as a reverse mapping function as an option
            CreateMap<CustomerBasketDto, CustomerBasket>();
            CreateMap<BasketItemDto, BasketItem>();
            CreateMap<AddressDto, Core.Entities.OrderAggregate.Address>();  // full path namespace used to identify .Address
            CreateMap<Order, OrderToReturnDto>()                               // map from Order -> OrderToReturnDto once we create necessary OrderDto classes
                .ForMember(d => d.DeliveryMethod, o => o.MapFrom(s => s.DeliveryMethod.ShortName))      // cleans up response from our order: "deliveryMethod": "Core.Entities.OrderAggregate.DeliveryMethod",
                .ForMember(d => d.ShippingPrice, o => o.MapFrom(s => s.DeliveryMethod.Price));

            CreateMap<OrderItem, OrderItemDto>()
             .ForMember(d => d.ProductId, o => o.MapFrom(s => s.ItemOrdered.ProductItemId))
             .ForMember(d => d.ProductName, o  => o.MapFrom(s => s.ItemOrdered.ProductName))
             .ForMember(d => d.PictureUrl, o => o.MapFrom(s => s.ItemOrdered.PrictureUrl))
             .ForMember(d => d.PictureUrl, o => o.MapFrom<OrderItemUrlResolver>());
            
        }
    }
}