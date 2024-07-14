﻿using System.ComponentModel.DataAnnotations;

namespace Cinema.DataAccess.Models;

public class Genre
{
    [Key]
    public  int Id { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;

    public ICollection<Movie> Movies { get; set; } = [];
}