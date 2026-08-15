using AutoMapper;
using Onion.APIs.DTOs;
using Onion.Core.Entities;

namespace Onion.APIs.Helper
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductToReturnDTO>()
                .ForMember(d => d.ProductType, o => o.MapFrom(S => S.ProductType.Name))
                .ForMember(d => d.ProductBrand, o => o.MapFrom(S => S.ProductBrand.Name))
                .ForMember(d => d.PictureUrl,o=>o.MapFrom<ProductPictureUrlResolver>());
        }
    }
}
