using API.Dtos;
using AutoMapper;
using Core.Entities;
using Microsoft.Extensions.Configuration;

namespace API.Helpers {
    public class ProductUrlResolver : IValueResolver<Product, ProductToReturnDto, string> {
        private readonly IConfiguration _config;
        public ProductUrlResolver (IConfiguration config) {                                                                                         // object created, brings in config from IConfiguration
            _config = config;
        }

        public string Resolve (Product source, ProductToReturnDto destination, string destMember, ResolutionContext context) {      // interface created from ProudctUrlResolver
            if(!string.IsNullOrEmpty(source.PictureUrl))
            {
                return _config["ApiUrl"] + source.PictureUrl;                           // we can then return our _config
            }

            return null;                                                                                    // we are being overly cautious .. this cannot be null
        }
    }
}