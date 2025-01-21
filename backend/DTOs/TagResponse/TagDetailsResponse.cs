namespace backend.DTOs.TagResponse;

public class TagDetailsResponse
{
    public int Id { get; set; }
    public string Slug { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public DateTime? Date { get; set; } = null;
    public bool Modified { get; set; }
}