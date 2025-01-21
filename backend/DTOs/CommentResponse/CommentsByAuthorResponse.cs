using backend.Models;

namespace backend.DTOs.CommentResponse;

public class CommentsByAuthorResponse
{
    public required List<Comment> Comments { get; set; } = new();
    public required int Quantity { get; set; } = default;
}