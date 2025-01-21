using System.ComponentModel.DataAnnotations;
using backend.DTOs.TagResponse;

namespace backend.DTOs.PostResponse;

public class PostCreateResponse
{
    [Required(ErrorMessage = "Título é obrigatório")]
    [StringLength(255, MinimumLength = 3, ErrorMessage = "O Título tem que ter entre 3 a 255 caracteres")]
    public string Title { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "É preciso ter um corpo")]
    [StringLength(Int32.MaxValue, MinimumLength = 3, ErrorMessage = "O corpo tem que ter no minímo 3 caracteres")]
    public string Content { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Nome do autor é obrigatório")]
    [StringLength(255, MinimumLength = 3, ErrorMessage = "O nome do autor tem que ter entre 3 a 255 caracteres")]
    public string AuthorName { get; set; } = string.Empty;

    [Required] 
    public List<TagCreateResponse> Tags { get; set; } = new();
}