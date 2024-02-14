using Bulky.Models;
using Microsoft.EntityFrameworkCore;

namespace Bulky.DataAccess.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Category> Categories { get; set; }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category() { Id = 1, CategoryName = "Action", DisplayOrder = 2 },
                new Category() {Id = 2, CategoryName = "Thriller", DisplayOrder = 1 },
                new Category() { Id = 3, CategoryName = "Romance", DisplayOrder = 3 }
                );

            modelBuilder.Entity<Product>().HasData(
                new Product()
                {
                    Id = 1,
                    Title = "The Alchemist",
                    Auther = "Poul Cohelo",
                    Description = "Best seller book of 2023",
                    ISBN = "1234",
                    ListPrice = 350,
                    Price = 320,
                    Price50 = 300,
                    Price100 = 250,
                    CategoryId = 1,
                    ImageUrl = ""
                },
                 new Product()
                 {
                     Id = 2,
                     Title = "Power of focus",
                     Auther = "Alexander",
                     Description = "Best seller book of 2022",
                     ISBN = "4321",
                     ListPrice = 250,
                     Price = 220,
                     Price50 = 200,
                     Price100 = 150,
                     CategoryId = 2,
                     ImageUrl = ""
                 },
                  new Product()
                  {
                     Id = 3,
                      Title = "Rich Dad Poor Dad",
                      Auther = "Robert Kiyosaki",
                      Description = "Best seller book of 2021",
                      ISBN = "1243",
                      ListPrice = 450,
                      Price = 420,
                      Price50 = 400,
                      Price100 = 350,
                      CategoryId = 3,
                      ImageUrl = ""
                  },
                   new Product()
                   {
                       Id = 4,
                       Title = "Hyper Focus",
                       Auther = "Glenn Maxwell",
                       Description = "Best seller book of 2020",
                       ISBN = "1124",
                       ListPrice = 550,
                       Price = 520,
                       Price50 = 500,
                       Price100 = 450,
                       CategoryId = 2,
                       ImageUrl = ""
                   },
                    new Product()
                    {
                        Id = 5,
                        Title = "Deep Work",
                        Auther = "Carl Neoport",
                        Description = "Best seller book of 2019",
                        ISBN = "2222",
                        ListPrice = 650,
                        Price = 620,
                        Price50 = 600,
                        Price100 = 550,
                        CategoryId = 3,
                        ImageUrl = ""
                    }

                );
        }
    }
}
