using EchangeDeviseAPI.Model;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text.Json;

namespace EchangeDeviseAPI.Service
{
    public class DeviseService
    {
        private readonly HttpClient httpClient;
        private readonly string apiKey;
        private readonly string urlBase;

        public DeviseService(HttpClient httpClient, IConfiguration configuration)
        {
            this.httpClient = httpClient;
            this.apiKey = configuration["CurrencyApi:ApiKey"]; // Clé API dans appsettings.json
            this.urlBase = configuration["CurrencyApi:ApiURL"]; // Url de base dans appsettings.json
        }

        public async Task<IActionResult> GetTauxEchange(string deviseBase)
        {
            var url = $"{urlBase}/{apiKey}/latest/{deviseBase}";
            Console.WriteLine($"URL de taux : {url}");

            try
            {
                // Effectuer l'appel HTTP
                var response = await httpClient.GetAsync(url);

                // Vérifier le succès de la requête
                response.EnsureSuccessStatusCode();

                // Lire et désérialiser la réponse JSON
                var jsonResponse = await response.Content.ReadAsStringAsync();
                return new JsonResult(JsonSerializer.Deserialize<object>(jsonResponse));
            }
            catch(HttpRequestException ex)
            {
                throw new Exception($"Erreur lors de l'appel à l'API des taux de change : {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Une erreur est survenue : {ex.Message}");
            }
        }

        public async Task<IActionResult> GetTauxEchangeConvert(string deviseBase, string deviseConvert)
        {
            var url = $"{urlBase}/{apiKey}/pair/{deviseBase}/{deviseConvert}";
            Console.WriteLine($"URL de convertion : {url}");

            try
            {
                // Effectuer l'appel HTTP
                var response = await httpClient.GetAsync(url);

                // Vérifier le succès de la requête
                response.EnsureSuccessStatusCode();

                // Lire et désérialiser la réponse JSON
                var jsonResponse = await response.Content.ReadAsStringAsync();
                return new JsonResult(JsonSerializer.Deserialize<object>(jsonResponse));
            }
            catch(HttpRequestException ex)
            {
                throw new Exception($"Erreur lors de l'appel à l'API pour la conversion des devises : {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Une erreur est survenue : {ex.Message}");
            }
        }
    }
}
