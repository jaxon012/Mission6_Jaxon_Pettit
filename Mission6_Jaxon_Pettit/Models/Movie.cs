using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mission6_Jaxon_Pettit.Models;

public class Movie
{
    [Key]
    public int MovieId { get; set; }

    // Foreign Key
    [Required(ErrorMessage = "Category is required.")]
    public int CategoryId { get; set; }

    [ForeignKey("CategoryId")]
    public Category? Category { get; set; }

    [Required(ErrorMessage = "Title is required.")]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "Year is required.")]
    [Range(1888, 2100, ErrorMessage = "Year must be 1888 or later.")]
    public int Year { get; set; }

    public string? Director { get; set; }

    public string? Rating { get; set; }

    [Required(ErrorMessage = "Edited is required.")]
    public bool Edited { get; set; }

    public string? LentTo { get; set; }

    [Required(ErrorMessage = "CopiedToPlex is required.")]
    public bool CopiedToPlex { get; set; }

    [StringLength(25, ErrorMessage = "Notes must be 25 characters or less.")]
    public string? Notes { get; set; }
}