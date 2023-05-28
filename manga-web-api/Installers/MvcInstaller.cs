using Microsoft.OpenApi.Models;
using System.Reflection;

namespace Nameless.MangaWebApi.Installers
{
    public class MvcInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();
            services.AddSwaggerGen(config =>
            {
                config.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Nameless Manga Library",
                    Description = "<p><b>Nameless - Manga Library API</b></p><p>API encargada de manejar la integración de datos</p>",
                    Contact = new OpenApiContact
                    {
                        Name = "Nameless Manga Library",
                        Url = new Uri("https://github.com/anamelesswolf")
                    }
                });
                /*
                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                config.IncludeXmlComments(xmlPath);
                */
            });
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }
    }
}
