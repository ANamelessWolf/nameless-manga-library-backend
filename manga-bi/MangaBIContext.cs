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
            this.CreateLanguageCatalogueModel(modelBuilder);
            this.CreateCurrencyCatalogueModel(modelBuilder);
            base.OnModelCreating(modelBuilder);
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

        protected void CreateLanguageCatalogueModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LanguageCatalogue>(entity =>
            {
                entity.ToTable("cat_language");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("Name");

            });
        }

    }

}