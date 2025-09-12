using EcoInspira.Application.UseCases.Login.DoLogin;
using EcoInspira.Communication.Requests;
using EcoInspira.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace EcoInspira.API.Controllers
{

    [Route("[controller]")]
    [ApiController]
    public class LoginController : EcoInspiraBaseController
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponseRegisteredUserJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromServices] IDoLoginUseCase useCase, [FromBody] RequestLoginJson request)
        {
            var response = await useCase.Execute(request);

            return Ok(response);
        }
    }
}