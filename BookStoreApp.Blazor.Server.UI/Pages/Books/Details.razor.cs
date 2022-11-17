using BookStoreApp.Blazor.Server.UI.Services;
using BookStoreApp.Blazor.Server.UI.Services.Base;
using Microsoft.AspNetCore.Components;

namespace BookStoreApp.Blazor.Server.UI.Pages.Books;

public partial class Details
{
    [Inject] private IBookService _bookService { get; set; } = null!;
    [Inject] private NavigationManager _navigationManager { get; set; } = null!;

    [Parameter]
    public int Id { get; set; }

    private BookGetSingleDto _book = new();
    protected override async Task OnInitializedAsync()
    {
        var response = await _bookService.Get(Id);

        if (!response.Success)
        {
            return;
        }

        _book = response.Data;
    }

    private void GoToEdit()
    {
        _navigationManager.NavigateTo($"/books/update/{Id}");
    }
    private void BackToList()
    {
        _navigationManager.NavigateTo("/books");
    }

}
