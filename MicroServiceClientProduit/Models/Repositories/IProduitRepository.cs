namespace MicroServiceClientProduit.Models.Repositories
{
    public interface IProduitRepository
    {
        Task<IEnumerable<Produit>> GetProduits();
        Task<Produit> GetProduit(int id);
        Task<Produit> GetProduitByName(string name);
        Task<Produit> AddProduit(Produit produit);
        Task<Produit> UpdateProduit(Produit produit);
        Task<Produit> DeleteProduit(int id);
        Task<IEnumerable<Produit>> GetProduitsByCategorieId(int categorieId);
        Task<IEnumerable<Produit>> Search(string name);
    }
}
