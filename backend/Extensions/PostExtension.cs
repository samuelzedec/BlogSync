using backend.DTOs;
using backend.DTOs.PostResponse;
using backend.DTOs.TagResponse;
using backend.Models;

namespace backend.Extensions;

public static class PostExtension
{
    public static void CopyModelFrom(this Post target, Post source)
    {
        target.Title = source.Title;
        target.Content = source.Content;
        target.AuthorName = source.AuthorName;
        target.Slug = source.Slug;
        target.Tags = source.Tags;
        target.ModifiedAt = DateTime.UtcNow;
    }

    public static void CopyResponseFrom(this PostDetailsResponse target, Post source)
    {
        target.Id = source.Id;
        target.Title = source.Title;
        target.Content = source.Content;
        target.AuthorName = source.AuthorName;

        target.Comments = source.Comments?.Select(x => new CommentDetailsResponse
        {
            Id = x.Id,
            Content = x.Content,
            AuthorName = x.AuthorName,
            Date = x.ModifiedAt != DateTime.MinValue ? x.ModifiedAt : x.CreatedAt,
            Modified = x.ModifiedAt != DateTime.MinValue
        }).ToList();

        target.Tags = source.Tags.Select(x => new TagDetailsResponse
        {
            Id = x.Id,
            Slug = x.Slug,
            Date = x.ModifiedAt ?? x.CreatedAt,
            Modified = x.ModifiedAt != null
        }).ToList();

        target.Date = source.ModifiedAt ?? source.CreatedAt;
        target.Modified = source.ModifiedAt != null;
    }

    public static List<PostDetailsResponse> CopyAllResponseFrom(this List<Post> posts)
    {
        return posts.Select(source => new PostDetailsResponse
        {
            Id = source.Id,
            Title = source.Title,
            Content = source.Content,
            AuthorName = source.AuthorName,
            Comments = source.Comments?.Select(x => new CommentDetailsResponse
            {
                Id = x.Id,
                Content = x.Content,
                AuthorName = x.AuthorName,
                Date = x.ModifiedAt ?? x.CreatedAt,
                Modified = x.ModifiedAt != null
            }).ToList(),

            Tags = source.Tags.Select(x => new TagDetailsResponse
            {
                Id = x.Id,
                Slug = x.Slug,
                Date = x.ModifiedAt ?? x.CreatedAt,
                Modified = x.ModifiedAt != null
            }).ToList(),

            Date = source.ModifiedAt ?? source.CreatedAt,
            Modified = source.ModifiedAt != null
        }).ToList();
    }
}