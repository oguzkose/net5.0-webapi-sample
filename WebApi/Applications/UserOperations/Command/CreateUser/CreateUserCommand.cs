using System;
using System.Linq;
using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Applications.UserOperations.Command.CreateUser
{
    public class CreateUserCommand
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;
        public CreateUserCommand(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public CreateUserModel Model { get; set; }
        public void Handle()
        {
            var user = _context.Users.SingleOrDefault(x => x.Email == Model.Email);
            if (user is not null)
                throw new InvalidOperationException("Kullanıcı zaten var");

            user = _mapper.Map<User>(Model);

            _context.Users.Add(user);
            _context.SaveChanges();
        }
        public class CreateUserModel
        {
            public string Name { get; set; }
            public string Surname { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
        }
    }

}