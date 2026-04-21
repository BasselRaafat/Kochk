using AutoMapper;
using Kochk.API.Dtos;
using Kochk.Application.Features.Carts.DTOs;
using Kochk.Application.Features.Products.DTOs;
using Kochk.Application.Features.User.DTOs;
using Kochk.Domain.Entities;
using Kochk.Domain.Entities.OrderAggregate;

namespace Kochk.API.Helpers;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Product, ProductDto>()
            .ForMember(D => D.Brand, O => O.MapFrom(S => S.Brand.Name))
            .ForMember(D => D.Category, O => O.MapFrom(S => S.Name))
            .ForMember(D => D.PictureUrl, O => O.MapFrom<ProductPictureResolver>());

        CreateMap<Cart, CartDto>().ReverseMap();

        CreateMap<UserAddress, OrderAddressDto>().ReverseMap();

        CreateMap<Order, OrderToReturnDto>()
            .ForMember(D => D.DeliveryMethod, opts => opts.MapFrom(S => S.DeliveryMethod.Name))
            .ForMember(O => O.DeliveryMethodCost, opts => opts.MapFrom(S => S.DeliveryMethod.Cost));

        CreateMap<OrderItem, OrderItemDto>()
            .ForMember(D => D.Name, opts => opts.MapFrom(S => S.Product.Name))
            .ForMember(D => D.ProductId, opts => opts.MapFrom(S => S.Product.Id))
            .ForMember(D => D.PictureUrl, opts => opts.MapFrom(S => S.Product.DefaultPictureUrl))
            .ForMember(D => D.PictureUrl, opts => opts.MapFrom<ProductItemPictureUrlResolver>());
        CreateMap<UserAddress, UserAddressUpdateRequest>().ReverseMap();
    }
}
