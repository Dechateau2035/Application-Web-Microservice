using MicroServiceClientProduit.Models;
using MicroServiceClientProduit.Models.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MicroServiceClientProduit.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategorieRepository categorieRepository;
        public CategoriesController(ICategorieRepository categorieRepository)
        {
            this.categorieRepository = categorieRepository;
        }

        //Renvoie toutes les catégories
        [HttpGet]
        public async Task<ActionResult> GetCategories()
        {
            try
            {
                return Ok(await categorieRepository.GetCategories());
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                $"Erreur lors de la recupération des clients! Cause : {e.Message}");
            }
        }

        //Renvoie une catégorie dont l'Id correspond à celui passé en paramètre
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Categorie>> GetCategorie(int id)
        {
            try
            {
                var result = await categorieRepository.GetCategorie(id);
                if (result == null) return NotFound();
                return result;
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erreur lors de la recupération de la categorie!! Cause : {e.Message}");
            }
        }

        //Ajout d'une catégorie
        [HttpPost]
        public async Task<ActionResult<Client>> CreateCategorie(Categorie categorie)
        {
            try
            {
                if (categorie == null) return BadRequest();
                else
                {
                    //Verifie si l'email existe dejà en Base de données
                    var c = await categorieRepository.GetCategorieByName(categorie.Name);
                    if (c != null)
                    {
                        ModelState.AddModelError("Email", "Ce mail est déjà utilisé par un client");
                        return BadRequest(ModelState);
                    }
                    else
                    {
                        var createdCategorie = await categorieRepository.AddCategorie(categorie);
                        return CreatedAtAction(nameof(GetCategorie), new { id = createdCategorie.CategorieId }, createdCategorie);
                    }
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erreur lors de la creation d'une nouvelle catégorie!! Cause : {e.Message}");
            }
        }

        //Mise à jour des informations sur une catégorie à partir de son Id
        [HttpPut("{id:int}")]
        public async Task<ActionResult<Categorie>> UpdateCategorie(int id, Categorie categorie)
        {
            try
            {
                if (id != categorie.CategorieId)
                    return BadRequest("L'Id du client ne correspond pas!!");

                var employeeToUpdate = await categorieRepository.GetCategorie(id);

                if (employeeToUpdate != null)
                    return NotFound($"Le client avec l'Id = {id} est introuvable");

                return await categorieRepository.UpdateCategorie(categorie);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erreur lors de la mise à jour de la donnée!! Cause : {e.Message}");
            }
        }

        //Supression d'un client à partir de son Id
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Categorie>> DeleteCategorie(int id)
        {
            try
            {
                var employeeToDelete = await categorieRepository.GetCategorie(id);
                if (employeeToDelete == null)
                    return NotFound($"La catégorie avec l'Id = {id} est introuvable");

                return await categorieRepository.DeleteCategorie(id);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erreur lors de la supression de la donnée!! Cause : {e.Message}");
            }
        }
    }
}
