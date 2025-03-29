using Application.DTOs.Category;
using Application.DTOs.Product;
using Application.DTOs.Cart;
using Application.DTOs.Order;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Category Mappings
        CreateMap<Category, CategoryDto>().ReverseMap();
        CreateMap<CreateCategoryDto, Category>();
        CreateMap<UpdateCategoryDto, Category>();

        // Product Mappings
        CreateMap<Product, ProductDto>()
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));
        CreateMap<CreateProductDto, Product>();
        CreateMap<UpdateProductDto, Product>();

        // Cart Mappings
        CreateMap<CartItem, CartItemDto>()
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name));
        CreateMap<Cart, CartDto>();
        CreateMap<AddToCartDto, CartItem>();

        // Order Mappings
        CreateMap<OrderItem, OrderItemDto>();
        CreateMap<Order, OrderDto>();
        CreateMap<CreateOrderDto, Order>();
        CreateMap<UpdateOrderStatusDto, Order>();
    }
}
