using backend.Data;
using backend.Exceptions;
using backend.Extensions;
using backend.Models;
using backend.Repositories.@interface;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories;

public class TagRepository(BlogSyncDbContext context) : IRepository<Tag>
{
    private BlogSyncDbContext Context { get; set; } = context;

    public async Task<List<Tag>> GetAllAsync()
    {
        return await Context
            .Tags
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Tag> GetAsync(int tagId)
    {
        var tag =  await Context
            .Tags
            .Include(x => x.Posts)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == tagId);
        
        return tag ?? throw new NotFoundException("Tag não encontrada");
    }

    public async Task<Tag> CreateAsync(Tag model)
    {
        await Context.Tags.AddAsync(model);
        await Context.SaveChangesAsync();
        return model;
    }

    public async Task<Tag> UpdateAsync(int tagId, Tag model)
    {
        var tag = await Context
            .Tags
            .FirstOrDefaultAsync(x => x.Id == tagId);

        if (tag is null)
            throw new NotFoundException("Tag não encontrada");
        
        tag.CopyModelFrom(model);
        Context.Update(tag);
        await Context.SaveChangesAsync();
        return tag;
    }

    public async Task<Tag> DeleteAsync(int tagId)
    {
        var tag = await Context
            .Tags
            .FirstOrDefaultAsync(x => x.Id == tagId);
        
        if (tag is null)
            throw new NotFoundException("Tag não encontrada");
        
        Context.Tags.Remove(tag);
        await Context.SaveChangesAsync();
        return tag;
    }
}