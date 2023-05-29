using Nameless.MangaBI;
using Nameless.MangaBI.Services.Interfaces;
using Nameless.Manga.Models;
using Nameless.MangaBI.Repositories.Interfaces;
using Nameless.WebApi.Repositories;

namespace Nameless.MangaBI.Repositories.Implements
{
    public class AuthorRoleRepository :
        GenericRepository<AuthorRoleCatalogue>, IAuthorRoleRepository, IContextAccesible
    {
        public MangaBIContext Context => (MangaBIContext)this._context;

        public AuthorRoleRepository(MangaBIContext context) : base(context)
        {

        }

    }
}
