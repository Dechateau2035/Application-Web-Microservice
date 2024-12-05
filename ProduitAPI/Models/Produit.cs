using System.ComponentModel.DataAnnotations;

namespace ProduitAPI.Models
{
    public class Produit
    {
        public int ProduitId { get; set; }
        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int quantite { get; set; }
        public int CategorieId { get; set; }
        public Categorie Categorie { get; set; }
    }
}
