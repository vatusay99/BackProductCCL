using ApiInventarioCCL.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiInventarioCCL.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
            
        }

        public DbSet<Producto> Productos { get; set; }
        public DbSet<MoveProduct> MoveProducts { get; set; } 
    }
}
