﻿using System.ComponentModel.DataAnnotations;

namespace BookStoreApp.Api.Models.Book;

#nullable disable

public class BookCreateDto
{
    [Required]
    [StringLength(50)]
    public string Title { get; set; }

    [Required]
    [Range(1800, int.MaxValue)]
    public int Year { get; set; }

    [Required]
    public string Isbn { get; set; }

    [Required]
    [StringLength(250, MinimumLength = 10)]
    public string Summary { get; set; }
    public string ImageData { get; set; }

    public string OriginalImageName { get; set; }

    [Required]
    [Range(0, int.MaxValue)]
    public decimal Price { get; set; }

    public int? AuthorId { get; set; }

}
