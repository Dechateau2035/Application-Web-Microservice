using System.ComponentModel.DataAnnotations;

namespace MicroServiceClientProduit.Models
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

        [Required]
        public byte[]? ImageData { get; set; }  // Blob image

        public int CategorieId { get; set; }

        public Categorie Categorie { get; set; }

    }
}
