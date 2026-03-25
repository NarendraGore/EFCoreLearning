using Microsoft.EntityFrameworkCore;
namespace EFCoreLearning.Models
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext>option):base(option) 
        { 
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Currency>().HasData(
               new Currency() {Id = 1, Title = "INR", Description="Indian INR" },
               new Currency() {Id = 2, Title = "Dollar", Description="Dollar " },
               new Currency() {Id = 3, Title = "Euro", Description= "Euro" },
               new Currency() {Id = 4, Title = "Dinar", Description="Dinar" }
                );

            modelBuilder.Entity<Language>().HasData(
                new Language() { Id=1, Title ="Marathi", Description="Marathi" },
                new Language() { Id=2, Title ="Hindi", Description="Hindi" },
                new Language() { Id=3, Title ="English", Description="English" },
                new Language() { Id=4, Title ="Punjabi", Description="Punjabi" }
                );
        }

        public DbSet<Book> Books { get; set; }

        public DbSet<Language> Languages { get; set; }

        public DbSet<Currency> currencies { get; set; }

        public DbSet<BookPrice> BookPrices { get; set; }

        public DbSet<Author> Authors { get; set; }

        
    }
    
}
