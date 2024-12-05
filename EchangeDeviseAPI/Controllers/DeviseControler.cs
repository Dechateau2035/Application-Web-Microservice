using EchangeDeviseAPI.Model;
using EchangeDeviseAPI.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EchangeDeviseAPI.Controllers
{
    [Route("api/devise")]
    [ApiController]
    public class DeviseControler : ControllerBase
    {
        private readonly DeviseService deviseService;
        public DeviseControler(DeviseService deviseService)
        {
            this.deviseService = deviseService;
        }

        [HttpGet("taux/{deviseBase}")]
        public async Task<IActionResult> GetTauxEchange(string deviseBase)
        {
            try
            {
                var rates = await deviseService.GetTauxEchange(deviseBase);
                return rates != null ? Ok(rates) : NotFound(new { Message = "Aucune donnée trouvée." });
            }
            catch (Exception ex) {
                return StatusCode(500, new { Message = "Une erreur est survenue.", Details = ex.Message });
            }
        }

        [HttpGet("convert/{deviseBase}/{deviseConvert}")]
        public async Task<IActionResult> GetTauxEchangeConvert(string deviseBase,string deviseConvert)
        {
            try
            {
                var rates = await deviseService.GetTauxEchangeConvert(deviseBase,deviseConvert);
                return rates != null ? Ok(rates) : NotFound(new { Message = "Aucune donnée trouvée." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Une erreur est survenue.", Details = ex.Message });
            }
        }
    }
}
