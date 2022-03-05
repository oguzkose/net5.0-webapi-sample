using System;
using System.Linq;
using WebApi.DBOperations;

namespace WebApi.Applications.AuthorOperations.Command.UpdateAuthor
{

    public class UpdateAuthorCommand
    {
        private readonly IBookStoreDbContext _context;
        public UpdateAuthorCommand(IBookStoreDbContext context)
        {
            _context = context;
        }
        public UpdateAuthorModel Model { get; set; }
        public int AuthorId { get; set; }
        public void Handle()
        {
            var author = _context.Authors.SingleOrDefault(x => x.Id == AuthorId);
            if (author is null)
                throw new InvalidOperationException("Güncellenecek yazar bulunamadı");

            author.Name = Model.Name != default ? Model.Name : author.Name;
            author.Surname = Model.Surname != default ? Model.Surname : author.Surname;
            author.DateOfBirth = Model.DateOfBirth != default ? Model.DateOfBirth : author.DateOfBirth;

            _context.SaveChanges();
        }

    }
    public class UpdateAuthorModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}