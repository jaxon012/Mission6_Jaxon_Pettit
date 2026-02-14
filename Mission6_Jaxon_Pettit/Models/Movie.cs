using System.ComponentModel.DataAnnotations;

namespace Mission6_Jaxon_Pettit.Models;

public class Movie
{
    [Key]
    public int MovieId { get; set; }

    [Required(ErrorMessage = "Category is required.")]
    public string Category { get; set; } = string.Empty;

    [Required(ErrorMessage = "Title is required.")]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "Year is required.")]
    [Range(1888, 2100, ErrorMessage = "Year must be a valid year.")]
    public int Year { get; set; }

    [Required(ErrorMessage = "Director is required.")]
    public string Director { get; set; } = string.Empty;

    // Not required; must allow "None"
    public string? Rating { get; set; } = "None";

    [Required]
    public bool Edited { get; set; }

    // Not required
    public string? LentTo { get; set; }

    // Not required; max 25 chars
    [StringLength(25, ErrorMessage = "Notes must be 25 characters or less.")]
    public string? Notes { get; set; }
}