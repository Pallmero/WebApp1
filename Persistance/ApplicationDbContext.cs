using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Persistance
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
    }


}
