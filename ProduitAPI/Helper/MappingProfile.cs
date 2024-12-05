using AutoMapper;
using ProduitAPI.Dto;
using ProduitAPI.Models;

namespace ProduitAPI.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Produit, ProduitDetailsDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ProduitId))
                .ForMember(dest => dest.CategorieName, opt => opt.MapFrom(src => src.Categorie.Name));

            CreateMap<ProduitDto, Produit>()
                .ForMember(dest => dest.ProduitId, opt => opt.Ignore())
                .ForMember(dest => dest.Categorie, opt => opt.Ignore());

            CreateMap<Produit, ProduitDto>();
        }
    }
}
