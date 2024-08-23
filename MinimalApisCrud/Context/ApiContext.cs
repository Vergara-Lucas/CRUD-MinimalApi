using Microsoft.EntityFrameworkCore;
using MinimalApisCrud.Models;

namespace MinimalApisCrud.Context
{
    public class ApiContext : DbContext
    {
        public DbSet<Book> BookEntity { get; set; }
        public ApiContext(DbContextOptions<ApiContext> dbContextOptions) : base(dbContextOptions) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>(entity=> entity.Property(p=>p.Code).IsRequired());
        }
    }
}
