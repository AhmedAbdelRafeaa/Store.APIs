using AutoMapper;
using Microsoft.Extensions.Configuration;
using Store.Core.Dtos.Products;
using Store.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Core.Mapping.Products
{
    public class ProductProfile : Profile
    {
        public ProductProfile(IConfiguration cofiguration)
        {
            CreateMap<Product, ProductDto>()
                .ForMember(D => D.BrandName, options => options.MapFrom(s => s.Brand.Name))
                .ForMember(D => D.TypeName, options => options.MapFrom(s => s.Type.Name))
                //.ForMember(D => D.PictureUrl, options => options.MapFrom(s => $"{cofiguration["BASEURL"]}{s.PictureUrl}"))
                .ForMember(D => D.PictureUrl, options => options.MapFrom(new PictureUrlResolver(cofiguration)))
                ;


            CreateMap<ProductBrand,TypeBrandDto>();
            CreateMap<ProductType,TypeBrandDto>();
        }
    }
}
