using Microsoft.EntityFrameworkCore;
namespace backend.Data;

public class BlogSyncDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        
    }
}