using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Produit> Produits { get; set; } = null!;
        public DbSet<Categorie> Categories { get; set; } = null!;
    }

    
}
