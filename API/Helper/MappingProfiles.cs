using API.Core.Entity;
using API.DTO;
using AutoMapper;

namespace API.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductsToReturn>()
            .ForMember(d => d.ProductBrand,o => o.MapFrom(s => s.ProductBrand.Name)).
            ForMember(d => d.ProductType,o => o.MapFrom(s => s.ProductType.Name)).
            ForMember(d => d.PictureUrl,o => o.MapFrom<ProducUrlReslover>());
        }
    }
}