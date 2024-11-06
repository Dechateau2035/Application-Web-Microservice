using System.ComponentModel.DataAnnotations;

namespace MicroServiceClientProduit.Models
{
    public class Client
    {
        public int ClientId { get; set; }

        [Required]
        public string FirstName { get; set; }

        public string? LastName { get; set; }

        [Required][EmailAddress]
        public string? EmailAddress { get; set; }
    }
}
