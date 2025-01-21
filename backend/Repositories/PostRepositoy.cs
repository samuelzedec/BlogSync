using backend.Data;
using backend.DTOs;
using backend.DTOs.TagResponse;
using backend.Exceptions;
using backend.Extensions;
using backend.Models;
using backend.Repositories.@interface;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories;

public class PostRepository(BlogSyncDbContext context) : IRepository<Post>
{
    private BlogSyncDbContext Context { get; set; } = context;

    public async Task<List<Post>> GetAllAsync()
    {
        return await Context
            .Posts
            .AsNoTracking()
            .ToListAsync();
    }
    
    public async Task<PostsByAuthorResponse> GetPostsByAuthorAsync(string authorName)
    {
        var posts = await Context
            .Posts
            .AsNoTracking()
            .Where(x => x.AuthorName == authorName)
            .ToListAsync();

        return new PostsByAuthorResponse
        {
            Posts = posts,
            Quantity = posts.Count
        };
    }

    public async Task<List<Tag>> AddTagsAsync(List<TagCreateResponse> tagsModel)
    {
        var relatedTags = await Context.Tags
            .Where(x => tagsModel
                .Select(y => y.Slug.GetSlug())
                .Contains(x.Slug))
            .ToListAsync();
        
        // All() ele verifica então se cada um dos elementos atende a condição
        relatedTags.AddRange(tagsModel
            .Where(x => relatedTags.All(y => y.Slug != x.Slug.GetSlug()))
            .Select(x => new Tag
            {
                Name = x.Name,
                Slug = x.Slug.GetSlug(),
                CreatedAt = DateTime.Now,
                ModifiedAt = null
            }));

        return relatedTags;
    }
    
    public async Task<Post> GetAsync(int postId)
    {
        var post =  await Context
            .Posts
            .Include(x => x.Comments)
            .Include(x => x.Tags)
            .AsNoTracking()
            .AsSplitQuery()
            .FirstOrDefaultAsync(x => x.Id == postId);

        return post ?? throw new NotFoundException("Post não encontrado");
    }

    public async Task<Post> CreateAsync(Post model)
    {
        await Context.Posts.AddAsync(model);
        await Context.SaveChangesAsync();
        return model;
    }

    public async Task<Post> UpdateAsync(int postId, Post model)
    {
        var post = await Context
            .Posts
            .Include(x => x.Tags)
            .FirstOrDefaultAsync(x => x.Id == postId);

        if (post is null)
            throw new NotFoundException("Post não encontrado!");
        
        post.CopyModelFrom(model);

        Context.Update(post);
        await Context.SaveChangesAsync();
        return post;
    }

    public async Task<Post> DeleteAsync(int postId)
    {
        var post = await Context
            .Posts
            .Include(x => x.Tags)
            .FirstOrDefaultAsync(x => x.Id == postId);
        
        if (post is null)
            throw new NotFoundException("Post não encontrado!");
        
        Context.Posts.Remove(post);
        await Context.SaveChangesAsync();
        return post;
    }
}