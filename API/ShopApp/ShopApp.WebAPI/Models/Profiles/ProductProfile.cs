using AutoMapper;
using ShopApp.Core.Domain;
using ShopApp.WebAPI.Models.DTOs;

namespace ShopApp.WebAPI.Models.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            this.CreateMap<Product, ProductDTO>().ReverseMap();
        }
    }
}
