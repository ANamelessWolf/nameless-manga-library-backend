using Nameless.MangaBI;
using Nameless.MangaBI.Services.Interfaces;
using Nameless.Manga.Models;
using Nameless.MangaBI.Repositories.Interfaces;
using Nameless.WebApi.Repositories;

namespace Nameless.MangaBI.Repositories.Implements
{
    public class AuthorRepository :
        GenericRepository<Author>, IAuthorRepository, IContextAccesible
    {
        private AuthorRoleRepository _authorRoleRepo;

        public MangaBIContext Context => (MangaBIContext)this._context;

        public AuthorRepository(MangaBIContext context, AuthorRoleRepository auRole) : base(context)
        {
            this.DataIsSelected = SelectAutorRole;
            this._authorRoleRepo = auRole;
            
        }

        private async void SelectAutorRole(Author author)
        {
            author.Role = await this._authorRoleRepo.GetById(author.RoleId);
        }
    }
}
