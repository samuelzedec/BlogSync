using System.ComponentModel.DataAnnotations;

namespace backend.DTOs.TagResponse;

public class TagCreateResponse
{
    [Required]
    [StringLength(255, MinimumLength = 3, ErrorMessage = "Slug é necessário para tag")]
    public string Name { get; set; } = string.Empty;
    
    [Required]
    [StringLength(255, MinimumLength = 3, ErrorMessage = "Slug é necessário para tag")]
    public string Slug { get; set; } = string.Empty;
}