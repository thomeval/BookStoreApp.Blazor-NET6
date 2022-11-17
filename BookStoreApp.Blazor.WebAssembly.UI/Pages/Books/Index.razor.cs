using BookStoreApp.Blazor.WebAssembly.UI.Services;
using BookStoreApp.Blazor.WebAssembly.UI.Services.Base;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BookStoreApp.Blazor.WebAssembly.UI.Pages.Books;

public partial class Index
{
    private List<BookGetAllDto>? _books;
    private Response<List<BookGetAllDto>> _response = new() { Success = true };

    [Inject] private IBookService _bookService {get;set;} = null!;
    [Inject] private IJSRuntime _js {get;set; } = null!;

    protected override async Task OnInitializedAsync()
    {
        _response = await _bookService.GetAll();

        if (_response.Success)
        {
            _books = _response.Data;
        }
    }

    private async Task DeleteBook(int bookId)
    {
        if (_books == null)
        {
            return;
        }

        var book = _books.Single(e => bookId == e.Id);
        var confirm = await _js.InvokeAsync<bool>("confirm", $"Are you sure you want to delete {book.Title}?");

        if (!confirm)
        {
            return;
        }

        var response = await _bookService.Delete(bookId);

        if (response.Success)
        {
            await OnInitializedAsync();
        }
    }
}
