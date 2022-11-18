namespace BookStoreApp.Api.Controllers;

public class VirtualizeResponse<T>
{
    public List<T> Items { get; set; } = new();

    public int TotalSize { get; set; }
}
