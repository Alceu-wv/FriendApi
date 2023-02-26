using amigos_dev.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace amigos_dev.Infrastructure
{
    public class FDBContext : DbContext
    {
        public FDBContext(DbContextOptions options) : base(options) { }

        public DbSet<Friend> Friend { get; set; }
    }
}