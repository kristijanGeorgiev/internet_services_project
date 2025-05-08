using AutoMapper;
using ComputerStore.Application.DTOs;
using ComputerStore.Domain.Entities;

namespace ComputerStore.API.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Map Product <-> ProductDto
            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.Categories.Select(c => c.Name)))
                .ReverseMap()
                .ForMember(dest => dest.Categories, opt => opt.Ignore());
            // Map Product <-> StockDto
            CreateMap<Product, StockDto>()
                .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.Categories.Select(c => c.Name)))
                .ReverseMap()
                .ForMember(dest => dest.Categories, opt => opt.Ignore());

           
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Product, ProductDto>().ReverseMap();

           
            CreateMap<string, Category>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src));

            CreateMap<Category, string>()
                .ConvertUsing(src => src.Name);

            CreateMap<string, Product>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src));

            CreateMap<Product, string>()
                .ConvertUsing(src => src.Name);
        }
    }
}
