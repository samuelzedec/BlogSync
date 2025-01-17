namespace backend.Models;

public class Tag
{
    public int Id { get; set; }
    public string Slug { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime ModifiedAt { get; set; }
    public List<Post>? Posts { get; set; }
}