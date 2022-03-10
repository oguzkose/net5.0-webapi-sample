using System;
using WebApi.DBOperations;
using WebApi.Entities;

namespace Tests.WebApi.UnitTest.TestSetup
{
    public static class Authors
    {
        public static void AddAuthors(this BookStoreDbContext context)
        {
            context.Authors.AddRange(
                new Author
                {
                    Name = "Mustafa Kemal", Surname = "Atatürk", DateOfBirth = new DateTime(1881, 05, 19)
                },
                new Author
                {
                     Name = "Eckhart", Surname = "Tolle", DateOfBirth = new DateTime(1948, 12, 10)
                },
                new Author
                { 
                    Name = "Frank", Surname = "Herbert", DateOfBirth = new DateTime(1920, 10, 08)
                }
            );
        }
    }
}