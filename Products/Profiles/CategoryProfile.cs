using AutoMapper;

namespace Products.Profiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile() 
        {
            CreateMap<Models.Domain.Category, Models.DTO.Category>().ReverseMap();
        }
    }
}
