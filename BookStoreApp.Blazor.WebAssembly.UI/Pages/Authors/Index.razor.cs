using BookStoreApp.Blazor.WebAssembly.UI.Models;
using BookStoreApp.Blazor.WebAssembly.UI.Services;
using BookStoreApp.Blazor.WebAssembly.UI.Services.Base;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BookStoreApp.Blazor.WebAssembly.UI.Pages.Authors;

public partial class Index
{
    public List<AuthorGetAllDto>? Authors;
    public int TotalSize { get; set; }
    private Response<AuthorGetAllDtoVirtualizeResponse> _response = new() { Success = true };

    [Inject] private IAuthorService _authorService { get; set; } =  null!;
    [Inject] private IJSRuntime _js { get; set; } = null!;

    protected override async Task OnInitializedAsync()
    {
        var queryParams = new QueryParameters ();
        _response = await _authorService.GetAll(queryParams);

        if (_response.Success)
        {
            Authors = _response.Data.Items!.ToList();
        }
    }

    private async Task LoadAuthors(QueryParameters queryParams)
    {
        var vResult = await _authorService.GetAll(queryParams);
        Authors = vResult.Data.Items!.ToList();
        TotalSize = vResult.Data.TotalSize ?? 0;
    }

}
