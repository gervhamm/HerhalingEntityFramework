using HerhalingEntityFramework.Entities;
using Microsoft.EntityFrameworkCore;

namespace HerhalingEntityFramework.Database
{
    public class HerhalingEntityFrameworkContext(DbContextOptions<HerhalingEntityFrameworkContext>options) : DbContext(options)
    {
        public DbSet<Blog> Blogs { get; set; } = null!;
        public DbSet<Comment> Comments { get; set; } = null!;

    }
}
