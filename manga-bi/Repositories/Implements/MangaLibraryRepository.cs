using Nameless.Manga.Models;
using Nameless.MangaBI.Repositories.Interfaces;
using Nameless.MangaBI.Services.Interfaces;
using Nameless.WebApi.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nameless.MangaBI.Repositories.Implements
{
    public class MangaLibraryRepository :
        GenericRepository<MangaLibrary>, IMangaLibraryRepository, IContextAccesible
    {
        private MangaRepository _mangaRepo;
        private PublisherRepository _pubRepository;
        private CurrencyRepository _currencyRepository;
        private LanguageRepository _languageRepository;

        public MangaBIContext Context => (MangaBIContext)this._context;

        public MangaLibraryRepository(MangaBIContext context, MangaRepository mangaRepo, PublisherRepository pubRepo, CurrencyRepository currRepository, LanguageRepository lanRepository)
            : base(context)
        {
            this.DataIsSelected = SelectMangaDependency;
            this._mangaRepo = mangaRepo;
            this._pubRepository = pubRepo;
            this._currencyRepository = currRepository;
            this._languageRepository = lanRepository;
        }

        private async void SelectMangaDependency(MangaLibrary mangaVolume)
        {
            mangaVolume.Manga = await this._mangaRepo.GetById(mangaVolume.MangaId);
            mangaVolume.Publisher = await this._pubRepository.GetById(mangaVolume.PublisherId);
            mangaVolume.Language = await this._languageRepository.GetById(mangaVolume.LanguageId);
            mangaVolume.Currency = await this._currencyRepository.GetById(mangaVolume.CurrencyId);
        }

    }
}
