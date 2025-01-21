namespace backend.DTOs;

public class CommentDetailsResponse
{
    public int Id { get; set; }
    public string Content { get; set; } = string.Empty;
    public string AuthorName { get; set; } = string.Empty;
    public DateTime? Date { get; set; }
    public bool Modified { get; set; }
}