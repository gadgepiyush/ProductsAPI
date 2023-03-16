using AutoMapper;

namespace Products.Profiles
{
    public class SellerProfile : Profile
    {
        public SellerProfile() 
        {
            CreateMap<Models.Domain.Seller, Models.DTO.Seller>().ReverseMap();
        }
    }
}
