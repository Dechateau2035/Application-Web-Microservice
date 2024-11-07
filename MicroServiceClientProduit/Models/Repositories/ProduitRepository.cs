
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace MicroServiceClientProduit.Models.Repositories
{
    public class ProduitRepository : IProduitRepository
    {
        private readonly AppDbContext appDbContext;
        private readonly ICategorieRepository _categorieRepository;
        public ProduitRepository(AppDbContext appDbContext, ICategorieRepository _categorieRepository)
        {
            this.appDbContext = appDbContext;
            this._categorieRepository = _categorieRepository;
        }

        public async Task<Produit> AddProduit(Produit produit)
        {
            // Vérifie si la catégorie associée existe
            var categorieExists = await appDbContext.Categories.AnyAsync(c => c.CategorieId == produit.CategorieId);
            if (!categorieExists)
            {
                throw new ArgumentException("La catégorie spécifiée n'existe pas.");
            }
            var result = await appDbContext.Produits.AddAsync(produit);
            await appDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Produit> DeleteProduit(int id)
        {
            var result = await appDbContext.Produits.FirstOrDefaultAsync(p =>p.ProduitId == id);

            if (result != null)
            {
                appDbContext.Produits.Remove(result);
                await appDbContext.SaveChangesAsync();
            }
            return result;
        }

        public async Task<Produit> GetProduit(int id)
        {
            return await appDbContext.Produits.FirstOrDefaultAsync(p => p.ProduitId == id);
        }

        public async Task<Produit> GetProduitByName(string name)
        {
            return await appDbContext.Produits.FirstOrDefaultAsync(p => p.Name == name);
        }

        public async Task<IEnumerable<Produit>> GetProduits()
        {
            return await appDbContext.Produits.ToListAsync();
        }

        public async Task<IEnumerable<Produit>> GetProduitsByCategorieId(int categorieId)
        {
            //Verifier si la categorie existe
            var categorie = await _categorieRepository.GetCategorie(categorieId);
            if (categorie != null)
            {
                //Recpère tous les produits
                var produits = await this.GetProduits();

                //Filtre les produits par l'Id de la categorie
                return produits.Where(p => p.CategorieId == categorieId);
            }
            return Enumerable.Empty<Produit>();
        }

        public async Task<IEnumerable<Produit>> Search(string name)
        {
            IQueryable<Produit> query = appDbContext.Produits;
            if (!string.IsNullOrEmpty(name))
                query = query.Where(p => p.Name.Contains(name));

            return await query.ToListAsync();
        }

        public async Task<Produit> UpdateProduit(Produit produit)
        {
            var result = await appDbContext.Produits.FirstOrDefaultAsync(p => p.ProduitId == produit.ProduitId);
            if(result != null)
            {
                result.Name = produit.Name;
                result.Description = produit.Description;
                result.Price = produit.Price;
                result.CategorieId = produit.CategorieId;

                await appDbContext.SaveChangesAsync();
            }
            return result;
        }
    }
}
