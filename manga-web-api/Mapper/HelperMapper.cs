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
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.NameEn, opt => opt.MapFrom(src => src.NameEn))
                .ForMember(dest => dest.NameJp, opt => opt.MapFrom(src => src.NameJp))
                .ForMember(dest => dest.Volumes, opt => opt.MapFrom(src => src.Volumes))
                .ForMember(dest => dest.Chapters, opt => opt.MapFrom(src => src.Chapters))
                .ForMember(dest => dest.IsCompleted, opt => opt.MapFrom(src => src.IsCompleted))
                .ForMember(dest => dest.DemographicId, opt => opt.MapFrom(src => src.DemographicId))
                .ForMember(dest => dest.Demographic, opt => opt.MapFrom(src => src.Demographic.Name))
                .ForMember(dest => dest.IllustratorId, opt => opt.MapFrom(src => src.IllustratorId))
                .ForMember(dest => dest.Illustrator, opt => opt.MapFrom(src => src.Illustrator.Name))
                .ForMember(dest => dest.WriterId, opt => opt.MapFrom(src => src.WriterId))
                .ForMember(dest => dest.Writer, opt => opt.MapFrom(src => src.Writer.Name));
            CreateMap<NewManga, MangaModel>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.NameEn, opt => opt.MapFrom(src => src.NameEn))
                .ForMember(dest => dest.NameJp, opt => opt.MapFrom(src => src.NameJp))
                .ForMember(dest => dest.Volumes, opt => opt.MapFrom(src => src.Volumes))
                .ForMember(dest => dest.Chapters, opt => opt.MapFrom(src => src.Chapters))
                .ForMember(dest => dest.IsCompleted, opt => opt.MapFrom(src => src.IsCompleted))
                .ForMember(dest => dest.DemographicId, opt => opt.MapFrom(src => src.DemographicId))
                .ForMember(dest => dest.IllustratorId, opt => opt.MapFrom(src => src.IllustratorId))
                .ForMember(dest => dest.WriterId, opt => opt.MapFrom(src => src.WriterId));
            CreateMap<MangaDto, MangaModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.NameEn, opt => opt.MapFrom(src => src.NameEn))
                .ForMember(dest => dest.NameJp, opt => opt.MapFrom(src => src.NameJp))
                .ForMember(dest => dest.Volumes, opt => opt.MapFrom(src => src.Volumes))
                .ForMember(dest => dest.Chapters, opt => opt.MapFrom(src => src.Chapters))
                .ForMember(dest => dest.IsCompleted, opt => opt.MapFrom(src => src.IsCompleted))
                .ForMember(dest => dest.DemographicId, opt => opt.MapFrom(src => src.DemographicId))
                .ForMember(dest => dest.IllustratorId, opt => opt.MapFrom(src => src.IllustratorId))
                .ForMember(dest => dest.WriterId, opt => opt.MapFrom(src => src.WriterId));
            #endregion

            #region Mapper para Manga Library
            CreateMap<MangaLibrary, MangaLibraryDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.MangaId, opt => opt.MapFrom(src => src.MangaId))
                .ForMember(dest => dest.Manga, opt => opt.MapFrom(src => src.Manga.Name))
                .ForMember(dest => dest.Volume, opt => opt.MapFrom(src => src.Volume))
                .ForMember(dest => dest.PublisherId, opt => opt.MapFrom(src => src.PublisherId))
                .ForMember(dest => dest.Publisher, opt => opt.MapFrom(src => src.Publisher.Name))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.CurrencyId, opt => opt.MapFrom(src => src.CurrencyId))
                .ForMember(dest => dest.Currency, opt => opt.MapFrom(src => src.Currency.Name))
                .ForMember(dest => dest.LanguageId, opt => opt.MapFrom(src => src.LanguageId))
                .ForMember(dest => dest.Language, opt => opt.MapFrom(src => src.Language.Name));
            CreateMap<NewMangaVolume, MangaLibrary>()
                .ForMember(dest => dest.MangaId, opt => opt.MapFrom(src => src.MangaId))
                .ForMember(dest => dest.Volume, opt => opt.MapFrom(src => src.Volume))
                .ForMember(dest => dest.PublisherId, opt => opt.MapFrom(src => src.PublisherId))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.CurrencyId, opt => opt.MapFrom(src => src.CurrencyId))
                .ForMember(dest => dest.LanguageId, opt => opt.MapFrom(src => src.LanguageId));
            CreateMap<MangaLibraryDto, MangaLibrary>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.MangaId, opt => opt.MapFrom(src => src.MangaId))
                .ForMember(dest => dest.Volume, opt => opt.MapFrom(src => src.Volume))
                .ForMember(dest => dest.PublisherId, opt => opt.MapFrom(src => src.PublisherId))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.CurrencyId, opt => opt.MapFrom(src => src.CurrencyId))
                .ForMember(dest => dest.LanguageId, opt => opt.MapFrom(src => src.LanguageId));
            #endregion

        }
    }
}
