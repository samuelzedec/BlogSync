using backend.Models;

namespace backend.DTOs;

public class PostsByAuthorResponse
{
    public required List<Post> Posts { get; set; } = new();
    public required int Quantity { get; set; } = default;
}