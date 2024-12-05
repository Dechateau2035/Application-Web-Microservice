namespace ProduitAPI.Dto
{
    public class ProduitDetailsDto
    {
        public int Id { get; set; }
        public string name { get; set; }
        public string? description { get; set; }
        public decimal price { get; set; }
        public int quantite { get; set; }
        public string CategorieName { get; set; }
    }
}
