﻿namespace BookStoreApp.Blazor.Server.UI.Models;

public class QueryParameters
{
    public int StartIndex { get; set; }
    public int PageSize { get; set; } = 15;
}
