using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProduitAPI.Models;
using ProduitAPI.Repositories;

namespace ProduitAPI.Controleur
{
    [Route("api/categorie")]
    [ApiController]
    public class CategorieControler : ControllerBase
    {
        private readonly ICategorieRepository categorieRepository;
        public CategorieControler(ICategorieRepository categorieRepository)
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
                $"Erreur lors de la recupération des catégories! Cause : {e.Message}");
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
        public async Task<ActionResult<Categorie>> CreateCategorie(Categorie categorie)
        {
            try
            {
                if (categorie == null) return BadRequest();
                else
                {
                    //Verifie si une catégorie avec le meme nom existe dejà en Base de données
                    var c = await categorieRepository.GetCategorieByName(categorie.Name);
                    if (c != null)
                    {
                        ModelState.AddModelError("Categorie", "Ce nom est déjà utilisé par une catégorie");
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
                    return BadRequest("L'Id du catégorie ne correspond pas!!");

                var categorieToUpdate = await categorieRepository.GetCategorie(id);

                if (categorieToUpdate == null)
                    if (categorieToUpdate == null)
                        return NotFound($"La catégorie avec l'Id = {id} est introuvable");

                return await categorieRepository.UpdateCategorie(categorie);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erreur lors de la mise à jour de la donnée!! Cause : {e.Message}");
            }
        }

        //Supression d'une catégorie à partir de son Id
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Categorie>> DeleteCategorie(int id)
        {
            try
            {
                var categorieToDelete = await categorieRepository.GetCategorie(id);
                if (categorieToDelete == null)
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
