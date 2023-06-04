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
    public class MangaRepository :
        GenericRepository<MangaModel>, IMangaRepository, IContextAccesible
    {
        private AuthorRepository _authorRepo;
        private DemographicRepository _demoGraphicRepo;

        public MangaBIContext Context => (MangaBIContext)this._context;

        public MangaRepository(MangaBIContext context, AuthorRepository authorRepo, DemographicRepository demoGraphicRepo) 
            : base(context)
        {
            this.DataIsSelected = SelectMangaDependency;
            this._authorRepo = authorRepo;
            this._demoGraphicRepo = demoGraphicRepo;
        }

        private async void SelectMangaDependency(MangaModel manga)
        {
            manga.Demographic = await this._demoGraphicRepo.GetById(manga.DemographicId);
            manga.Writer = await this._authorRepo.GetById(manga.WriterId);
            if(manga.WriterId != manga.IllustratorId)
                manga.Illustrator = await this._authorRepo.GetById(manga.WriterId);
            manga.Illustrator = manga.Writer;
        }

    }
}
