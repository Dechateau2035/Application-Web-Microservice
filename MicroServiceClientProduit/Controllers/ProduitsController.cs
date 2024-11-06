using MicroServiceClientProduit.Models.Repositories;
using MicroServiceClientProduit.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MicroServiceClientProduit.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProduitsController : ControllerBase
    {
        private readonly IProduitRepository produitRepository;
        public ProduitsController(IProduitRepository produitRepository)
        {
            this.produitRepository=produitRepository;
        }

        [HttpGet("{categorie}")]
        public async Task<ActionResult<IEnumerable<Client>>> FilterByCategorie(int CategorieId)
        {
            try
            {
                var result = await produitRepository.GetProduitsByCategorieId(CategorieId);
                if (result.Any())
                    return Ok(result);

                return NotFound();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erreur de recupération des données de la BD!! Cause : {e.Message}");
            }
        }
    }
}
