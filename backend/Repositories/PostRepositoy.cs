using backend.Data;
using backend.Exceptions;
using backend.Extensions;
using backend.Interface;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories;

public class PostRepository : IRepository<Post>
{
    private BlogSyncDbContext Context { get; set; }

    public PostRepository(BlogSyncDbContext context)
        => Context = context;

    public async Task<List<Post>> ReadAll()
    {
        return await Context
            .Posts
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Post> Read(int postId)
    {
        return await Context
            .Posts
            .Include(x => x.Comments)
            .Include(x => x.Tags)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == postId);
    }

    public async Task<Post> Create(Post model)
    {
        await Context.Posts.AddAsync(model);
        await Context.SaveChangesAsync();

        return await Context
            .Posts
            .Include(x => x.Tags)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Slug == model.Slug);
    }

    public async Task<Post> Update(int postId, Post model)
    {
        var post = await Context
            .Posts
            .Include(x => x.Tags)
            .FirstOrDefaultAsync(x => x.Id == postId)
            ?? throw new NotFoundException("Post não encontrado");

        post.CopyFrom(model);
        post.ModifiedAt = DateTime.UtcNow;

        Context.Update(post);
        await Context.SaveChangesAsync();
        return post;
    }

    public async Task<Post> Delete(int postId)
    {
        var post = await Context
            .Posts
            .Include(x => x.Tags)
            .FirstOrDefaultAsync(x => x.Id == postId)
            ?? throw new NotFoundException("Post não encontrado");

        Context.Posts.Remove(post);
        await Context.SaveChangesAsync();
        return post;
    }
}