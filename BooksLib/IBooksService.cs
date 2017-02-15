using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BooksLib
{
    public interface IBooksService
    {
        Task<IEnumerable<Book>> GetBooksAsync();
    }
}
