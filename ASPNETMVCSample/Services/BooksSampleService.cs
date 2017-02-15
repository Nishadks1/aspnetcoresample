using BooksLib;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ASPNETMVCSample.Services
{
    public class BooksSampleService : IBooksService
    {
        private readonly List<Book> _books = new List<Book>();
        public BooksSampleService() => InitBooks();

        private void InitBooks()
        {
            _books.AddRange(new Book[]
            {
                new Book { BookId = 1, Title = "Professional C# 6 and .NET Core 1.0", Publisher = "Wrox Press"},
                new Book { BookId = 2, Title = "Enterprise Services", Publisher = "AWL"},
                new Book { BookId = 3, Title = "Professional C# 5 and .NET 4.5.1", Publisher = "Wrox Press"}
            });
        }
        public Task<IEnumerable<Book>> GetBooksAsync() =>
            Task.FromResult<IEnumerable<Book>>(_books);
    }
}
