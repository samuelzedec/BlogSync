using backend.DTOs.TagResponse;

namespace backend.DTOs.PostResponse;

public class PostDetailsResponse
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string AuthorName { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public bool Modified { get; set; }
    public List<CommentDetailsResponse>? Comments { get; set; }
    public List<TagDetailsResponse> Tags { get; set; } = new();
}