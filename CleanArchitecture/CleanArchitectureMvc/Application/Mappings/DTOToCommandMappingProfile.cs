using Application.CQRS.Products.Commands;
using Application.DTOs;
using AutoMapper;

namespace Application.Mappings
{
    public class DTOToCommandMappingProfile : Profile
    {
        public DTOToCommandMappingProfile()
        {
            CreateMap<ProductCreateCommand, ProductDTO>().ReverseMap();
            CreateMap<ProductUpdateCommand, ProductDTO>().ReverseMap();
        }
    }
}
