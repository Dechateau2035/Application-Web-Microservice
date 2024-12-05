using System.ComponentModel.DataAnnotations;

namespace ProduitAPI.Models
{
    public class Categorie
    {
        public int CategorieId { get; set; }
        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }
    }
}
