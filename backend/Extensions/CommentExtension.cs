using backend.Models;

namespace backend.Extensions;

public static class CommentExtension
{
    public static void CopyModelFrom(this Comment target, Comment source)
    {
        target.Content = source.Content;
        target.AuthorName = source.AuthorName;
        target.ModifiedAt = DateTime.UtcNow;
    }
}