using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Dtos;
using AutoMapper;
using ProductDb.DataClasses;

namespace Services.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductToReturnDto>()
                .ForMember(d => d.ProductBrand, o => o.MapFrom(s => s.ProductBrand.Name))
                .ForMember(d => d.ProductType, o => o.MapFrom(s => s.ProductType.Name))
                .ForMember(d => d.Category, o => o.MapFrom(s => s.Category.Name))
                .ForMember(d => d.PictureUrl, o => o.MapFrom<ProductUrlResolver>());
        
            CreateMap<Box, BoxToReturnDto>()
                .ForMember(d => d.Products, o => o.MapFrom(s => s.BoxProducts.Select(x => x.Product).ToList()))
                .ReverseMap();

            CreateMap<BoxToCreateDto, Box>()
                .ForMember(d => d.BoxProducts, o => o.MapFrom(s => s.ProductIds.Select(p => new BoxProduct { ProductId = p}).ToList()));

             CreateMap<BoxToUpdateDto, Box>()
                .ForMember(d => d.BoxProducts, o => o.MapFrom(s => s.ProductIds.Select(p => new BoxProduct { ProductId = p, BoxId = s.Id}).ToList()));
        }
    }
}
