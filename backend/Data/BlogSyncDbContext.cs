using backend.Data.Mapping;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Data;

public class BlogSyncDbContext : DbContext
{
    public DbSet<Post> Posts { get; set; } = null!;
    public DbSet<Comment> Comments { get; set; } = null!;
    public DbSet<Tag> Tags { get; set; } = null!;
    
    /* ===============================================================================
     * Esse construtor é necessário para que o ASP.NET insira a connection string
     * =============================================================================== */
    public BlogSyncDbContext(DbContextOptions<BlogSyncDbContext> options)
        : base(options) { }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new PostMap());
        modelBuilder.ApplyConfiguration(new CommentMap());
        modelBuilder.ApplyConfiguration(new TagMap());
    }
}