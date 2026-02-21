using System.ComponentModel.DataAnnotations;

namespace Mission6_Jaxon_Pettit.Models;

public class Category
{
    [Key]
    public int CategoryId { get; set; }

    public string CategoryName { get; set; } = string.Empty;

    public List<Movie> Movies { get; set; } = new();
}