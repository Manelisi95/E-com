using API.Core.Entity;
using API.DTO;
using AutoMapper;
using Microsoft.Extensions.Configuration;

namespace API.Helper
{
    public class ProducUrlReslover : IValueResolver<Product, ProductsToReturn, string>
    {
        private readonly IConfiguration _config;
        public ProducUrlReslover(IConfiguration config)
        {
                _config = config;
        }

        public string Resolve(Product source, ProductsToReturn destination, string destMember, ResolutionContext context)
        {
           if (!string.IsNullOrEmpty(source.PictureUrl))
           {
               return _config["APIurl"] + source.PictureUrl;
           }
           return null;
        }
    }
}