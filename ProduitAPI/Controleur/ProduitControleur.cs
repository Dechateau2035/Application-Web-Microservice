using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProduitAPI.Dto;
using ProduitAPI.Models;
using ProduitAPI.Repositories;

namespace ProduitAPI.Controleur
{
    [Route("api/produits")]
    [ApiController]
    public class ProduitControleur : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IProduitRepository produitRepository;
        private readonly ICategorieRepository categorieRepository;

        public ProduitControleur(IProduitRepository produitRepository, ICategorieRepository categorieRepository, IMapper mapper)
        {
            this.produitRepository = produitRepository;
            this.categorieRepository = categorieRepository;
            this.mapper = mapper;
        }

        //Renvoie tous les produits
        [HttpGet]
        public async Task<IActionResult> GetProduits()
        {
            try
            {
                var produits = await produitRepository.GetProduits();
                //Mapper la liste de pproduit en ProduitDetailsDto
                var data = mapper.Map<IEnumerable<ProduitDetailsDto>>(produits);
                return Ok(data);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erreur lors de la recupération des produits! Cause : {e.Message}");
            }
        }

        //Renvoie un produit dont l'Id correspond à celui passé en paramètre
        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetProduit(int id)
        {
            try
            {
                var result = await produitRepository.GetProduit(id);
                if (result == null) return NotFound();

                var dto = mapper.Map<ProduitDetailsDto>(result);
                return Ok(dto);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erreur lors de la recupération du produit! Cause : {e.Message}");
            }
        }

        //Ajout d'un produit
        [HttpPost]
        public async Task<ActionResult<Produit>> CreateProduit([FromForm] ProduitDto dto)
        {
            try
            {
                if (dto == null) return BadRequest("Aucun Object passe dans la requete!!");
                else
                {
                    var p = await produitRepository.GetProduitByName(dto.name);
                    if (p != null && p.Description == dto.description)
                    {
                        ModelState.AddModelError("Produit", "Ce produit dejà existant");
                        return BadRequest(ModelState);
                    }

                    var categorie = await categorieRepository.GetCategorie(dto.CategorieId);
                    if (categorie == null) return BadRequest($"Id Catéorie {dto.CategorieId} invalide!!");

                    var produit = mapper.Map<Produit>(dto);

                    await produitRepository.AddProduit(produit);
                    return Ok(produit);
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erreur lors de la creation du produit! Cause : {e.Message}");
            }
        }

        [HttpGet("categorie/{CategorieId:int}")]
        public async Task<ActionResult<IEnumerable<Produit>>> FilterByCategorie(int CategorieId)
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

        //Mise à jour des informations d'un produit à partir de son Id
        [HttpPut("{id:int}")]
        public async Task<ActionResult<Produit>> UpdateProduit(int id, [FromForm] ProduitDto dto)
        {
            try
            {
                var produit = await produitRepository.GetProduit(id);
                if (produit == null)
                    return BadRequest($"Produit introuvable avec ID {id}!!");

                var categorie = await categorieRepository.GetCategorie(dto.CategorieId);

                produit.Name = dto.name;
                produit.Description = dto.description;
                produit.Price = dto.price;
                produit.CategorieId = dto.CategorieId;
                produit.quantite = dto.quantite;

                produitRepository.UpdateProduit(produit);

                return Ok(produit);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erreur lors de la mise à jour de la donnée!! Cause : {e.Message}");
            }
        }

        //Supression d'un produit à partir de son Id
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Produit>> DeleteProduit(int id)
        {
            try
            {
                var produitToDelete = await produitRepository.GetProduit(id);
                if (produitToDelete == null)
                    return NotFound($"Le produit avec l'Id {id} est introuvable");

                return await produitRepository.DeleteProduit(id);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erreur lors de la supression de la donnée!! Cause : {e.Message}");
            }
        }

        // Recherche sur les produits à partir d'un mot clé
        [HttpGet("{name}")]
        public async Task<ActionResult<IEnumerable<Produit>>> Search(string name)
        {
            try
            {
                var result = await produitRepository.Search(name);
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
