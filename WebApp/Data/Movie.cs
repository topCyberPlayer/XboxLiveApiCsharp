using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Data;

public partial class Movie
{
    [Key]
    public int Id { get; set; }

    public string? Title { get; set; }

    public DateTime ReleaseDate { get; set; }

    public string? Genre { get; set; }

    public decimal Price { get; set; }
}
