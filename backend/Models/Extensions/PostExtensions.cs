using backend.Models;
namespace backend.Extensions;

public static class PostExtensions
{
    public static void CopyFrom(this Post target, Post source)
    {
        target.Title = source.Title;
        target.Content = source.Content;
        target.AuthorName = source.AuthorName;
        target.Slug = source.Slug;
        target.Tags = source.Tags;
    }
}