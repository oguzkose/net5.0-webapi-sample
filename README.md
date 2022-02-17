# Projenin oluşturulması

- Proje klasöründe .NET 5 WebApi projesi oluşturmak için;

```bash
    dotnet new webapi -f net5.0 -n WebApi
```

- Solution dosyası oluşturulur;

```bash
    dotnet new sln -n WebApi
```

- WebApi projesinin Solution'a eklenebilmesi için;

```bash
    dotnet sln add WebApi
```

- WebApi projesine yerleşip EntityFrameworkCore'u dahil etmek için

```bash
    dotnet add package Microsoft.EntityFrameworkCore --version 5.0.13
```

- WebApi projesine yerleşip EntityFrameworkCore.InMemory'i dahil etmek için

```bash
    dotnet add package Microsoft.EntityFrameworkCore.InMemory --version 5.0.13
```

# DbContext ve Entity

- BookStoreDbContext

```csharp
    public class BookStoreDbContext : DbContext
    {
        public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options) : base(options)
        {

        }
        public DbSet<Book> Books { get; set; }
    }
```

- Book Entity

```csharp
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int GenreId { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }
    }
```

# DataGenerator 
* Data Generator Class'ında ki Initalize() metodu ile Default'ta InMemoryDb'de varolması istenilen verilerin eklenmesi.
```csharp
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
                        GenreId = 1,
                        PageCount = 450,
                        PublishDate = new DateTime(1937, 05, 13)
                    },
                    new Book
                    {
                        Title = "Şimdinin Gücü",
                        GenreId = 2,
                        PageCount = 350,
                        PublishDate = new DateTime(2018, 12, 01)

                    },
                    new Book
                    {
                        Title = "Dune",
                        GenreId = 3,
                        PageCount = 520,
                        PublishDate = new DateTime(1989, 02, 15)
                    }
                );
                context.SaveChanges();
            }

        }
    }
```
# InMemory Configurations
* Program.cs
```csharp
    public static void Main(string[] args)
    {
        //InMemory Configurations
        var host = CreateHostBuilder(args).Build();
        using (var scope = host.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            DataGenerator.Initialize(services);
        }
        host.Run();
    }
```
# Dependency Injections
* Startup.cs dosyasında DbContext bağımlılığının belirtilmesi
```csharp
    public void ConfigureServices(IServiceCollection services)
    {
        //DbContext - Dependency Injection
        services.AddDbContext<BookStoreDbContext>(options =>
            options.UseInMemoryDatabase(databaseName: "BookStoreDB"));
    }
```
# Controller'in oluşturulması ve Route Configuration

```csharp
    [ApiController]
    [Route("api/[controller]s")]
    public class BookController : ControllerBase
    {
        //InMemoryDb'ye ulaşabilmek için constructor ile alınan context
        private readonly BookStoreDbContext _context;
        public BookController(BookStoreDbContext context)
        {
            _context = context;
        }
        //bu alanda endpoint'ler gelecek.
    }
```

# Endpoints

- [HttpGet] Attribute ile Data listesini getiren Endpoint

```csharp
    [HttpGet]
    public List<Book> GetBooks()
    {
        var bookList = _context.Books.OrderBy(x => x.Id).ToList();
        return bookList;
    }
```

- [HttpGet] Attribute ile , FromRoute'dan gelen Id'ye göre Data getiren Endpoint

```csharp
    [HttpGet("{id}")]
    public Book GetById(int id)
    {
        var book = _context.Books.Where(x => x.Id == id).SingleOrDefault();
        return book;
    }
```

- [HttpPost] Attribute ile FromBody'den gelen Data'yı ekleyen Endpoint

```csharp
    [HttpPost]
    public IActionResult AddBook([FromBody] Book newBook)
    {
        var book = _context.Books.SingleOrDefault(x => x.Title == newBook.Title);
        if (book is not null)
            return BadRequest();
        _context.Books.Add(newBook);
        _context.SaveChanges();
        return Ok();
    }
```

- [HttpPut] Attribute ile FromBody'den gelen Data'yı güncelleyen Endpoint

```csharp
    [HttpPut("{id}")]
    public IActionResult UpdateBook(int id, [FromBody] Book updatedBook)
    {
        var book = _context.Books.SingleOrDefault(x => x.Id == id);
        if (book is null)
            return BadRequest();

        book.Title = updatedBook.Title != default ? updatedBook.Title : book.Title;
        book.GenreId = updatedBook.GenreId != default ? updatedBook.GenreId : book.GenreId;
        book.PageCount = updatedBook.PageCount != default ? updatedBook.PageCount : book.PageCount;
        book.PublishDate = updatedBook.PublishDate != default ? updatedBook.PublishDate : book.PublishDate;

        _context.SaveChanges();

        return Ok();
    }
```

- [HttpDelete] Attribute ile FromRoute'dan gelen Id'ye göre Data'yı silen Endpoint

```csharp
    [HttpDelete("{id}")]
    public IActionResult DeleteBook(int id)
    {
        var book = _context.Books.SingleOrDefault(x => x.Id == id);
        if (book is null)
            return BadRequest();
        _context.Books.Remove(book);
        _context.SaveChanges();
        
        return Ok();
    }
```
