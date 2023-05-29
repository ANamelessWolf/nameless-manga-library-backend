using AutoMapper;
using Nameless.Manga.Models;
using Nameless.Manga.ModelsDto;

namespace Nameless.WebApi.Mapper
{
    public class HelperMapper : Profile
    {
        public HelperMapper()
        {
            #region Mapper para Language
            CreateMap<LanguageCatalogue, LanguageCatalogueDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
            #endregion

            #region Mapper para Currency
            CreateMap<CurrencyCatalogue, CurrencyCatalogueDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Value))
                .ForMember(dest => dest.Symbol, opt => opt.MapFrom(src => src.Symbol))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
            #endregion

            #region Mapper para Demographic
            CreateMap<DemographicCatalogue, DemographicCatalogueDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
            #endregion

            #region Mapper para Publisher
            CreateMap<PublisherCatalogue, PublisherCatalogueDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
            #endregion

            #region Mapper para Author role
            CreateMap<AuthorRoleCatalogue, AuthorRoleCatalogueDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
            #endregion

            #region Mapper para Author
            CreateMap<Author, AuthorDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role));
            CreateMap<AuthorDto, Author>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => src.RoleId));
            #endregion

        }
    }
}
