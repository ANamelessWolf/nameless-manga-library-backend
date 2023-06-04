using System.IdentityModel.Tokens.Jwt;
using Nameless.MangaWebApi.Installers;
using Nameless.MangaBI;
using Microsoft.EntityFrameworkCore;
using MySQL.Data.EntityFrameworkCore;
using Nameless.WebApi.Repositories;
using Nameless.MangaBI.Repositories.Implements;

namespace Nameless.MangaWebApi
{
    public class Startup
        {
            public Startup(IConfiguration configuration)
            {
                Configuration = configuration;
            }

            public IConfiguration Configuration { get; }

            public readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            /*
            services.AddAuthentication("Bearer").AddJwtBearer("Bearer", opt =>
            {
                opt.Authority = "https://localhost:44304/IdentityServerMX/";
                opt.RequireHttpsMetadata = false;                
                opt.Audience = "iMerchantMxAngular";
                opt.TokenValidationParameters = new TokenValidationParameters
                {                    
                    //RoleClaimType = "role"
                };
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("ApiScope", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    //policy.RequireClaim("IntegrationMxApi");
                });
            });
            */

            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                  builder =>
                                  {
                                      builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                                  });
            });

            services.InstallServicesInAssembly(Configuration);

            services.AddDbContext<MangaBIContext>(
                    options => options.UseMySQL(Configuration.GetConnectionString("connectionStrig")));

            // repositories
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped(typeof(DbContext), typeof(MangaBIContext));
            
            //Catalogues
            services.AddScoped(typeof(LanguageRepository), typeof(LanguageRepository));
            services.AddScoped(typeof(CurrencyRepository), typeof(CurrencyRepository));
            services.AddScoped(typeof(PublisherRepository), typeof(PublisherRepository));
            services.AddScoped(typeof(DemographicRepository), typeof(DemographicRepository));
            services.AddScoped(typeof(AuthorRoleRepository), typeof(AuthorRoleRepository));

            //Tables
            services.AddScoped(typeof(AuthorRepository), typeof(AuthorRepository));
            services.AddScoped(typeof(MangaRepository), typeof(MangaRepository));
            services.AddScoped(typeof(MangaLibraryRepository), typeof(MangaLibraryRepository));


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
            {
                if (env.IsDevelopment())
                {
                    app.UseDeveloperExceptionPage();
                }

                app.UseHttpsRedirection();

                app.UseRouting();


                app.UseAuthorization();

                app.UseCors(MyAllowSpecificOrigins);

                //          app.UseAuthentication();

                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers()
                  //  .RequireAuthorization("ApiScope")
                  ;
                });

                app.UseSwagger();
                app.UseSwaggerUI(config =>
                {
                    config.DocumentTitle = "Nameless - Manga Library Web API";
                    config.InjectJavascript("/swagger-ui/custom.js");
                    config.InjectStylesheet("/swagger-ui/custom.css");
                    config.SwaggerEndpoint("/swagger/v1/swagger.json", "NamelessMangaLibraryAPI");
                });

            }
        }
    }
