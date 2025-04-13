using AutoMapper;
using SalesApi.Application.Commands.Models;
using SalesApi.Domain.Entities;

namespace SalesApi.Application.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Domain to DTO mappings
        CreateMap<SaleItem, SaleItemModel>();
        CreateMap<Product, ProductModel>();
        CreateMap<Sale, SaleModel>()
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items))
            .ForMember(dest => dest.Cancelled, opt => opt.MapFrom(src => src.IsCancelled));

    }
}
