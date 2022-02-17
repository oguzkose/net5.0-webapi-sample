using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

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
                        Id = 1,
                        Title = "Nutuk",
                        GenreId = 1, //History
                        PageCount = 450,
                        PublishDate = new DateTime(1937, 05, 13)
                    },
                    new Book
                    {
                        Id = 2,
                        Title = "Şimdinin Gücü",
                        GenreId = 2, //philosophy
                        PageCount = 350,
                        PublishDate = new DateTime(2018, 12, 01)

                    },
                    new Book
                    {
                        Id = 3,
                        Title = "Dune",
                        GenreId = 3, //Fantastic
                        PageCount = 520,
                        PublishDate = new DateTime(1989, 02, 15)
                    }
                );
                context.SaveChanges();
            }

        }
    }
}