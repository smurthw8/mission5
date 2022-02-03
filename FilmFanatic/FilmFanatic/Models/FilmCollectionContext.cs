using System;
using FilmFanatic.Models;
using Microsoft.EntityFrameworkCore;

namespace FilmFanatic.Models
{
    public class FilmCollectionContext : DbContext
    {
        //this is the constructor
        public FilmCollectionContext(DbContextOptions<FilmCollectionContext> options ) : base (options)
        {
            //leave blank for now
        }

        //set up actual dataset (model) > pull set of data from database
        //set of instances of the FIlm Collection Dabase
        // varname {get; set;} will be the name of the table in the database
        public DbSet<CollectionResponse> Films { get; set; }

        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            mb.Entity<Category>().HasData(
                new Category {CategoryId = 1, CategoryName = "Family"},
                new Category {CategoryId = 2, CategoryName = "Drama"},
                new Category {CategoryId = 3, CategoryName = "Action" },
                new Category {CategoryId = 4, CategoryName = "Romance" },
                new Category {CategoryId = 5, CategoryName = "Comedy" }
                );

            //database seeded entries
            mb.Entity<CollectionResponse>().HasData(

                new CollectionResponse
                {
                    FilmId = 1,
                    Title = "Megamind",
                    Year = 2010,
                    CategoryId = 1,
                    Director = "Tom McGrath",
                    Rating = "PG"
                },
                new CollectionResponse
                {
                    FilmId = 2,
                    Title = "Spider-Man: Into the Spider-Verse",
                    Year = 2018,
                    CategoryId = 1,
                    Director = "Bob Persichetti",
                    Rating = "PG"
                },
                new CollectionResponse
                {
                    FilmId = 3,
                    Title = "Sherlock",
                    Year = 2018,
                    CategoryId = 2,
                    Director = "Mark Gatiss",
                    Rating = "TV-14"
                }

            );
        }
    }
}
