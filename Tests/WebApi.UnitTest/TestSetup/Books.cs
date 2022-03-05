using System;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.UnitTest.TestSetup
{
    public static class Books
    {
        public static void AddBooks(this BookStoreDbContext context)
        {
            context.Books.AddRange(

                     new Book { Title = "Nutuk", GenreId = 3, PageCount = 450, PublishDate = new DateTime(1937, 05, 13) },

                     new Book { Title = "Şimdinin Gücü", GenreId = 1, PageCount = 350, PublishDate = new DateTime(2018, 12, 01) },

                     new Book { Title = "Dune", GenreId = 2, PageCount = 520, PublishDate = new DateTime(1989, 02, 15) }
            );

        }
    }
}