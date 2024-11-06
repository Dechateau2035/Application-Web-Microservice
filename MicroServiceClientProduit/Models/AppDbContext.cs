using Microsoft.EntityFrameworkCore;

namespace MicroServiceClientProduit.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Produit> Produits { get; set; }
        public DbSet<Categorie> Categories { get; set; }
    }
}
