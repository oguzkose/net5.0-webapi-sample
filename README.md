# Projenin oluşturulması
* Proje klasöründe .NET 5 WebApi projesi oluşturmak için; 
```bash
    dotnet new webapi -f net5.0 -n WebApi
```
* Solution dosyası oluşturulur;
```bash
    dotnet new sln -n WebApi
```
* WebApi projesinin Solution'a eklenebilmesi için;
```bash
    dotnet sln add WebApi   
``` 

# Controller'in oluşturulması , Route Configuration ve Static bir Data listesinin yazılması
```csharp
    [ApiController]
    [Route("api/[controller]s")]
    public class BookController : ControllerBase
    {
        public static List<Book> BookList = new List<Book>()
        {
            new Book{
                Id=1,
                Title="Nutuk",
                GenreId=1, //History
                PageCount=450,
                PublishDate= new DateTime(1937,05,13)
            },
            new Book{
                Id=2,
                Title="Şimdinin Gücü",
                GenreId=2, //philosophy
                PageCount=350,
                PublishDate= new DateTime(2018,12,01)

            },
            new Book{
                Id=3,
                Title="Dune",
                GenreId=3, //Fantastic
                PageCount=520,
                PublishDate= new DateTime(1989,02,15)
            }
        };
    }
```

# Endpoints
* [HttpGet] Attribute ile Data listesini getiren Endpoint
```csharp
    [HttpGet]
    public List<Book> GetBooks()
    {
        var bookList = BookList.OrderBy(x => x.Id).ToList();
        return bookList;
    }
```
* [HttpGet] Attribute ile , FromRoute'dan gelen Id'ye göre Data getiren Endpoint
```csharp
    [HttpGet("{id}")]
    public Book GetById(int id)
    {
        var book = BookList.Where(x => x.Id == id).SingleOrDefault();
        return book;
    }
```
* [HttpPost] Attribute ile FromBody'den gelen Data'yı ekleyen Endpoint
```csharp
    [HttpPost]
    public IActionResult AddBook([FromBody] Book newBook)
    {
        var book = BookList.SingleOrDefault(x => x.Title == newBook.Title);
        if (book is not null)
            return BadRequest();

        BookList.Add(newBook);
        return Ok();
    }
```
* [HttpPut] Attribute ile FromBody'den gelen Data'yı güncelleyen Endpoint
```csharp
    [HttpPut("{id}")]
    public IActionResult UpdateBook(int id, [FromBody] Book updatedBook)
    {
        var book = BookList.SingleOrDefault(x => x.Id == id);
        if (book is null)
            return BadRequest();

        // Güncellenmesi istenen field eğer eski field ile aynı ise güncelleme gerçekleşmesin!!!    
        book.Title = updatedBook.Title != default ? updatedBook.Title : book.Title;
        book.GenreId = updatedBook.GenreId != default ? updatedBook.GenreId : book.GenreId;
        book.PageCount = updatedBook.PageCount != default ? updatedBook.PageCount : book.PageCount;
        book.PublishDate = updatedBook.PublishDate != default ? updatedBook.PublishDate : book.PublishDate;
        return Ok();
    }
```
* [HttpDelete] Attribute ile FromRoute'dan gelen Id'ye göre Data'yı silen Endpoint
```csharp
    [HttpDelete("{id}")]
    public IActionResult DeleteBook(int id)
    {
        var book = BookList.SingleOrDefault(x => x.Id == id);
        if (book is null)
            return BadRequest();
        BookList.Remove(book);
        return Ok();
    }
```