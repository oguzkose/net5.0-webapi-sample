using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Entities;

namespace WebApi.DBOperations
{
    public class DataGenerator
    {
        //Başlangıçta InMemory Db'de veri olması için yazılan metot
        public static void Initialize(IServiceProvider serviceProvider)
        {
            //Context.Books eğer boş ise 3 adet Book ekler.
            using (var context = new BookStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
            {
                if (context.Books.Any())
                {
                    return;
                }

                context.Books.AddRange(
                    new Book
                    {
                        Title = "Nutuk",
                        GenreId = 3,
                        PageCount = 450,
                        PublishDate = new DateTime(1937, 05, 13)
                    },
                    new Book
                    {
                        Title = "Şimdinin Gücü",
                        GenreId = 1,
                        PageCount = 350,
                        PublishDate = new DateTime(2018, 12, 01)

                    },
                    new Book
                    {
                        Title = "Dune",
                        GenreId = 2,
                        PageCount = 520,
                        PublishDate = new DateTime(1989, 02, 15)
                    }
                );


                context.Genres.AddRange(
                    new Genre
                    {
                        Name = "Personal Growth",
                    },
                    new Genre
                    {
                        Name = "Science Fiction",
                    },
                    new Genre
                    {
                        Name = "History",
                    }
                );

                context.Authors.AddRange(
                    new Author
                    {
                        Name = "Mustafa Kemal",
                        Surname = "Atatürk",
                        DateOfBirth = new DateTime(1881, 05, 19)
                    },
                    new Author
                    {
                        Name = "Eckhart",
                        Surname = "Tolle",
                        DateOfBirth = new DateTime(1948, 12, 10)
                    },
                    new Author
                    {
                        Name = "Frank",
                        Surname = "Herbert",
                        DateOfBirth = new DateTime(1920, 10, 08)
                    }
                );
                
                context.SaveChanges();
            }

        }
    }
}