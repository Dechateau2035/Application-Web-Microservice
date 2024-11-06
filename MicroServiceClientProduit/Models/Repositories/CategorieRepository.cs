
using Microsoft.EntityFrameworkCore;

namespace MicroServiceClientProduit.Models.Repositories
{
    public class CategorieRepository : ICategorieRepository
    {
        private readonly AppDbContext appDbContext;
        public CategorieRepository(AppDbContext appDbContext)
        {
            this.appDbContext=appDbContext;
        }

        public async Task<Categorie> AddCategorie(Categorie categorie)
        {
            var result = await appDbContext.Categories.AddAsync(categorie);
            await appDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Categorie> DeleteCategorie(int id)
        {
            var result = await appDbContext.Categories.FirstOrDefaultAsync(c => c.CategorieId == id);

            if (result != null)
            {
                //Recupère tous les produits de cette catégorie
                var produitsResult = appDbContext.Produits.Where(p => p.CategorieId == id);
                //Supprime les produits associés
                appDbContext.Produits.RemoveRange(produitsResult);
                //Supprime la categorie 
                appDbContext.Categories.Remove(result);
                await appDbContext.SaveChangesAsync();
            }
            return result;
        }

        public async Task<Categorie> GetCategorie(int id)
        {
            return await appDbContext.Categories.FirstOrDefaultAsync(c => c.CategorieId == id);
        }

        public async Task<Categorie> GetCategorieByName(string name)
        {
            return await appDbContext.Categories.FirstOrDefaultAsync(c => c.Name == name);
        }

        public async Task<IEnumerable<Categorie>> GetCategories()
        {
            return await appDbContext.Categories.ToListAsync();
        }

        public async Task<Categorie> UpdateCategorie(Categorie categorie)
        {
            var result = await appDbContext.Categories.FirstOrDefaultAsync(c => c.CategorieId == categorie.CategorieId);
            if (result != null)
            {
                result.Name = categorie.Name;
                result.Description = categorie.Description;
                await appDbContext.SaveChangesAsync();
            }
            return result;
        }
    }
}
