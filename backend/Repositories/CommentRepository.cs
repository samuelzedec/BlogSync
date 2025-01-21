using backend.Data;
using backend.DTOs.CommentResponse;
using backend.Exceptions;
using backend.Extensions;
using backend.Models;
using backend.Repositories.@interface;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories;

public class CommentRepository(BlogSyncDbContext context) : IRepository<Comment>
{
    private BlogSyncDbContext Context { get; set; } = context;

    public async Task<List<Comment>> GetAllAsync()
    {
        return await Context
            .Comments
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<CommentsByAuthorResponse> GetCommentsByAuthorAsync(string authorName)
    {
        var comments = await Context
            .Comments
            .Include(x => x.Post)
            .AsNoTracking()
            .Where(x => x.AuthorName == authorName)
            .ToListAsync();

        return new CommentsByAuthorResponse
        {
            Comments = comments,
            Quantity = comments.Count
        };
    }

    public async Task<Comment> GetAsync(int commentId)
    {
        var comment = await Context
            .Comments
            .Include(x => x.Post)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == commentId);
        
        return comment ?? throw new NotFoundException("Comentário não encontrada");
    }

    public async Task<Comment> CreateAsync(Comment model)
    {
        await Context.Comments.AddAsync(model);
        await Context.SaveChangesAsync();

        return model;
    }

    public async Task<Comment> UpdateAsync(int tagId, Comment model)
    {
        var comment = await Context
            .Comments
            .FirstOrDefaultAsync(x => x.Id == tagId);

        if (comment is null)
            throw new NotFoundException("Comentário não encontrada");
        
        comment.CopyModelFrom(model);
        await Context.SaveChangesAsync();
        return comment;
    }

    public async Task<Comment> DeleteAsync(int tagId)
    {
        var comment = await Context
            .Comments
            .FirstOrDefaultAsync(x => x.Id == tagId);

        if (comment is null)
            throw new NotFoundException("Comentário não encontrada");
        
        Context.Comments.Remove(comment);
        await Context.SaveChangesAsync();
        return comment;
    }
}