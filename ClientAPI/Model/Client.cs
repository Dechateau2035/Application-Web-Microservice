using System.ComponentModel.DataAnnotations;

namespace ClientAPI.Model
{
    public class Client
    {
        public int ClientId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required][EmailAddress]
        public string? EmailAddress { get; set; }

        [Required]
        public string Telephone { get; set; }
    }
}
