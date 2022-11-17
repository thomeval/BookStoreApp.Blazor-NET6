using BookStoreApp.Blazor.WebAssembly.UI.Services;
using BookStoreApp.Blazor.WebAssembly.UI.Services.Base;
using Microsoft.AspNetCore.Components;

namespace BookStoreApp.Blazor.WebAssembly.UI.Pages.Authors;

public partial class Details
{
    [Inject] private IAuthorService _authorService {get;set;} = null!;
    [Inject] private IBookService _bookService {get;set;} = null!;
    [Inject] private NavigationManager _navigationManager {get;set; } = null!;
    
    [Parameter]
    public int Id { get; set; }

    private AuthorDetailsDto _author = new();
    protected override async Task OnInitializedAsync()
    {
        var response = await _authorService.Get(Id);

        if (!response.Success)
        {
            return;
        }

        _author = response.Data!;
    }

    private void GoToEdit()
    {
        _navigationManager.NavigateTo($"/authors/update/{Id}");
    }
    private void BackToList()
    {
        _navigationManager.NavigateTo("/authors");
    }

    private void GoToBook(int bookId)
    {
        _navigationManager.NavigateTo($"books/details/{Id}");
    }

}
