using AutoMapper;
using ComputerStore.Application.DTOs;
using ComputerStore.Domain.Entities;

namespace ComputerStore.API.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
         
            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.Categories.Select(c => c.Name)))
                .ReverseMap()
                .ForMember(dest => dest.Categories, opt => opt.MapFrom(src =>
                    src.Categories.Select(name => new Category { Name = name }).ToList()
                ));

           
            CreateMap<Product, StockDto>()
                .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.Categories.Select(c => c.Name)))
                .ReverseMap()
                .ForMember(dest => dest.Categories, opt => opt.MapFrom(src =>
                    src.Categories.Select(name => new Category { Name = name }).ToList()
                ));

            CreateMap<Category, CategoryDto>()
                .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Products.Select(p => p.Name)))
                .ReverseMap()
                .ForMember(dest => dest.Products, opt => opt.Ignore());

            
            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.Categories.Select(c => c.Name)))
                .ReverseMap()
                .ForMember(dest => dest.Categories, opt => opt.Ignore());
            
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
