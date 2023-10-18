using System;
using System.Collections.Generic;

namespace WebApp.ModelsDb;

public partial class Movie
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public DateTime ReleaseDate { get; set; }

    public string? Genre { get; set; }

    public decimal Price { get; set; }
}
