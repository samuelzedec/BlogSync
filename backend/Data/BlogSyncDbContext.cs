using backend.Data.Mapping;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Data;

public class BlogSyncDbContext : DbContext
{
    public DbSet<Post> Posts { get; set; } = null!;
    public DbSet<Comment> Comments { get; set; } = null!;
    public DbSet<Tag> Tags { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            "Server=localhost,1433;Database=BlogSync;User ID=sa;Password=1q2w3e4r@#$;TrustServerCertificate=True"
        );
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new PostMap());
        modelBuilder.ApplyConfiguration(new CommentMap());
        modelBuilder.ApplyConfiguration(new TagMap());
    }
}