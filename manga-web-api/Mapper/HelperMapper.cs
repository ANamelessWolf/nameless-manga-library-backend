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
            CreateMap<LanguageCatalogue, CatalogueDto>()
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
            CreateMap<DemographicCatalogue, CatalogueDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
            #endregion

            #region Mapper para Publisher
            CreateMap<PublisherCatalogue, CatalogueDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
            #endregion

            #region Mapper para Author role
            CreateMap<AuthorRoleCatalogue, CatalogueDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
            #endregion

            #region Mapper para Author
            CreateMap<Author, AuthorDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => src.RoleId))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.Name));
            CreateMap<NewAuthor, Author>()
               .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
               .ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => src.RoleId));
            CreateMap<AuthorDto, Author>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => src.RoleId));
            #endregion

            #region Mapper para Manga
            CreateMap<MangaModel, MangaDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.NameEn, opt => opt.MapFrom(src => src.NameEn))
                .ForMember(dest => dest.NameJp, opt => opt.MapFrom(src => src.NameJp))
                .ForMember(dest => dest.Volumes, opt => opt.MapFrom(src => src.Volumes))
                .ForMember(dest => dest.Chapters, opt => opt.MapFrom(src => src.Chapters))
                .ForMember(dest => dest.IsCompleted, opt => opt.MapFrom(src => src.IsCompleted))
                .ForMember(dest => dest.DemographicId, opt => opt.MapFrom(src => src.DemographicId))
                .ForMember(dest => dest.Illustrator, opt => opt.MapFrom(src => src.Illustrator))
                .ForMember(dest => dest.Writer, opt => opt.MapFrom(src => src.Writer))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
            CreateMap<MangaDto, MangaModel>()
                      .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.NameEn, opt => opt.MapFrom(src => src.NameEn))
                .ForMember(dest => dest.NameJp, opt => opt.MapFrom(src => src.NameJp))
                .ForMember(dest => dest.Volumes, opt => opt.MapFrom(src => src.Volumes))
                .ForMember(dest => dest.Chapters, opt => opt.MapFrom(src => src.Chapters))
                .ForMember(dest => dest.IsCompleted, opt => opt.MapFrom(src => src.IsCompleted))
                .ForMember(dest => dest.DemographicId, opt => opt.MapFrom(src => src.DemographicId))
                .ForMember(dest => dest.Illustrator, opt => opt.MapFrom(src => src.Illustrator))
                .ForMember(dest => dest.Writer, opt => opt.MapFrom(src => src.Writer))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
            #endregion

        }
    }
}
