namespace MicroServiceClientProduit.Models.Repositories
{
    public interface ICategorieRepository
    {
        Task<IEnumerable<Categorie>> GetCategories();
        Task<Categorie> GetCategorie(int id);
        Task<Categorie> AddCategorie(Categorie categorie);
        Task<Categorie> GetCategorieByName(string name);
        Task<Categorie> UpdateCategorie(Categorie categorie);
        Task<Categorie> DeleteCategorie(int id);
    }
}
