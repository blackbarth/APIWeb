using Microsoft.AspNetCore.Mvc;

namespace APIWEB.Controllers
{
    //[ApiVersion("1.0", Deprecated = true)] // mostra no header eh absoleto
    [ApiVersion("1.0")]
    //[ApiVersion("2.0")]
    //[Route("api/{v:apiVersion}/teste")] // envia esta absoleta no header qdo nao usado versao no header
    [Route("api/teste")] // envia esta absoleta no header 
    [ApiController]
    public class TesteV1Controller : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Content("<html><body><h2>TesteV1Controller - V 1.0 </<h2></body></html>");
        }


        //[HttpGet, MapToApiVersion("2.0")]
        //public IActionResult GetVersao2()
        //{
        //    return Content("<html><body><h2>TesteV1Controller - V 2.0 </<h2></body></html>");
        //}

    }
}