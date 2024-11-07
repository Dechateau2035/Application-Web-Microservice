using AutoMapper;
using MicroServiceClientProduit.Models;
using MicroServiceClientProduit.Models.Dto;

namespace MicroServiceClientProduit.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Produit, ProduitDetailsDto>().ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ProduitId));
            CreateMap<ProduitDto, Produit>().ForMember(src => src.ImageData, opt => opt.Ignore());
        }
    }
}
