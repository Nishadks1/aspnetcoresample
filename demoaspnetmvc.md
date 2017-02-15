# Demo ASP.NET MVC

## Tools

1. dotnet new mvc --auth None --framework netcoreapp1.1
2. dotnet restore
3. dotnet build
4. dotnet run

## Execute

1. open with Visual Studio, run
2. show DI, logging, configuration
3. show controller
4. reference BooksLib library

## Tag Helper

1. controller methods for tag helper

```csharp
        public IActionResult TagHelpers() =>
            View(new Book { Title = "enter a title" });

        [HttpPost]
        public string UploadBook(Book b) =>
            $"book {b.Title} from {b.Publisher} uploaded";
```

2. view for tag helpers

```html
@model Book

<form asp-controller="Home" asp-action="UploadBook" method="post">
    Title:<input type="text" asp-for="Title" />
    <br />
    Publisher:<input type="text" asp-for="Publisher" />
    <br />
    <button type="submit">Register</button>
    <br />
</form>
```

## View Component

1. Create View Component

```csharp
    [ViewComponent(Name = "BooksList")]
    public class BooksViewComponent : ViewComponent
    {
        private readonly IBooksService _booksService;

        public BooksViewComponent(IBooksService booksService) =>
            _booksService = booksService;

        // InvokeAsync
        public async Task<IViewComponentResult> InvokeAsync(string publisher)
        {
            var books = await _booksService.GetBooksAsync();
            return View(books.Where(b => b.Publisher == publisher));
        }
    }
```

2. IBooksService

```csharp
    public interface IBooksService
    {
        Task<IEnumerable<Book>> GetBooksAsync();
    }
```

3. View

```html
@model IEnumerable<Book>

<h2>Books View Component</h2>

<ul>
    @foreach (var book in Model)
    {
        <li>@book.Title</li>
    }
</ul>
```

4. controller

```csharp
        public IActionResult Books1() =>
            ViewComponent("BooksList", "Wrox Press");

        public IActionResult Books2() =>
            View();

        public IActionResult Books3() =>
            View();
```

5. viewimports

```html
@using ASPNETMVCSample
@using BooksLib
@addTagHelper *, Microsoft.AspNetCore.Mvc.TagHelpers
@addTagHelper *, BooksLib
```

6. view like 1.0

```html
<h2>Books 2</h2>

@await Component.InvokeAsync("BooksList", new { publisher = "Wrox Press" })
```

7. view with tag helper

```html
<h2>Books 3</h2>

<vc:books-list publisher="Wrox Press" />

<hr />
<vc:books-list publisher="AWL" />
```
