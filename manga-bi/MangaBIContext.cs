using Microsoft.EntityFrameworkCore;
using MySQL.Data.EntityFrameworkCore;
using Nameless.Manga.Models;

namespace Nameless.MangaBI
{
    public class MangaBIContext : DbContext
    {
        private readonly string _connectionString = "";

        public MangaBIContext(DbContextOptions<MangaBIContext> options)
         : base(options)
        {

        }

        public MangaBIContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySQL(_connectionString,
                providerOptions => providerOptions.CommandTimeout(60)).EnableSensitiveDataLogging(true);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");
            //Catalogos
            this.CreateCatalogueModel<LanguageCatalogue>("cat_language", modelBuilder);
            this.CreateCatalogueModel<PublisherCatalogue>("cat_publisher", modelBuilder);
            this.CreateCatalogueModel<AuthorRoleCatalogue>("cat_author_role", modelBuilder);
            this.CreateCatalogueModel<DemographicCatalogue>("cat_demographic", modelBuilder);
            this.CreateCurrencyCatalogueModel(modelBuilder);
            //Tablas
            this.CreateAuthorModel(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }

        private void CreateAuthorModel(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Author>(entity =>
            {
                entity.ToTable("author");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name");

                entity.Property(e => e.RoleId)
                    .IsRequired()
                    .HasColumnName("role_id");


            });
        }

        private void CreateCurrencyCatalogueModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CurrencyCatalogue>(entity =>
            {
                entity.ToTable("cat_currency");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("Name");

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasColumnName("Value");

                entity.Property(e => e.Symbol)
                    .IsRequired()
                    .HasColumnName("Symbol");
            });
        }

        protected void CreateCatalogueModel<T>(string tableName, ModelBuilder modelBuilder) where T : Catalogue
        {
            modelBuilder.Entity<T>(entity =>
            {
                entity.ToTable(tableName);
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("Name");

            });
        }

    }

}