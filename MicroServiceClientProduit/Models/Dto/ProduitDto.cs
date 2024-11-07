namespace MicroServiceClientProduit.Models.Dto
{
    public class ProduitDto
    {
        public string name { get; set; }
        public string? description { get; set; }
        public decimal price { get; set; }
        public int quantite { get; set; }
        public IFormFile? ImageData { get; set; }
        public int CategorieId { get; set; }
    }
}
