using MicroServiceClientProduit.Models;
using MicroServiceClientProduit.Models.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace MicroServiceClientProduit.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly IClientRepository clientRepository;
        public ClientsController(IClientRepository clientRepository)
        {
            this.clientRepository = clientRepository;
        }

        //Renvoie tous les clients
        [HttpGet]
        public async Task<ActionResult> GetClients()
        {
            try
            {
                return Ok(await clientRepository.GetClients());
            }
            catch(Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erreur lors de la recupération des clients! Cause : {e.Message}");
            }
        }

        //Renvoie le client dont l'Id correspond à celui passé en paramètre
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Client>> GetClient(int id)
        {
            try
            {
                var result = await clientRepository.GetClient(id);
                if (result == null) return NotFound();
                return result;
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erreur lors de la recupération du client! Cause : {e.Message}");
            }
        }

        //Ajout d'un client
        [HttpPost]
        public async Task<ActionResult<Client>> CreateClient(Client client)
        {
            try
            {
                if(client == null) return BadRequest();
                else
                {
                    //Verifie si l'email existe dejà en Base de données
                    var cl = await clientRepository.GetClientByEmail(client.EmailAddress);
                    if(cl != null)
                    {
                        ModelState.AddModelError("Email", "Ce mail est déjà utilisé par un client");
                        return BadRequest(ModelState);
                    }
                    else
                    {
                        var createdClient = await clientRepository.AddClient(client);
                        return CreatedAtAction(nameof(GetClient), new { id = createdClient.ClientId }, createdClient);
                    }
                }
            }
            catch(Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erreur lors de la creation du nouveau client! Cause : {e.Message}");
            }
        }

        //Mise à jour des informations sur un client à partir de son Id
        [HttpPut("{id:int}")]
        public async Task<ActionResult<Client>> UpdateEmployee(int id, Client client)
        {
            try
            {
                if (id != client.ClientId)
                    return BadRequest("L'Id du client ne correspond pas!!");

                var employeeToUpdate = await clientRepository.GetClient(id);

                if (employeeToUpdate != null)
                    return NotFound($"Le client avec l'Id = {id} est introuvable");

                return await clientRepository.UpdateClient(client);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erreur lors de la mise à jour de la donnée!! Cause : {e.Message}");
            }
        }

        //Supression d'un client à partir de son Id
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Client>> DeleteEmployee(int id)
        {
            try
            {
                var employeeToDelete = await clientRepository.GetClient(id);
                if (employeeToDelete == null)
                    return NotFound($"Le client avec l'Id = {id} est introuvable");

                return await clientRepository.DeleteClient(id);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erreur lors de la supression de la donnée!! Cause : {e.Message}");
            }
        }

        // Recherche sur les clients à partir d'un mot clé
        [HttpGet("{search}")]
        public async Task<ActionResult<IEnumerable<Client>>> Search(string name)
        {
            try
            {
                var result = await clientRepository.Search(name);
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
