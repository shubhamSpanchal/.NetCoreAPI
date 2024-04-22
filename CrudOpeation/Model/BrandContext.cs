using Microsoft.EntityFrameworkCore;

namespace CrudOpeation.Model
{
    public class BrandContext : DbContext
    {
        public BrandContext(DbContextOptions<BrandContext> options):base(options)
        {
            
        }
        public DbSet<Brands> Brands { get; set; }
    }
}
