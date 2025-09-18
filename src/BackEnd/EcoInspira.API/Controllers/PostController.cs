using EcoInspira.API.Attributes;
using EcoInspira.Application.UseCases.Post.Register;
using EcoInspira.Communication.Requests;
using EcoInspira.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace EcoInspira.API.Controllers
{
    [AuthenticatedUser]
    public class PostController : EcoInspiraBaseController
    {
            [HttpPost]
            [ProducesResponseType(typeof(ResponsePostJson), StatusCodes.Status201Created)]
            [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
            public async Task<IActionResult> Register(
                [FromServices] IRegisterPostUserCase useCase, 
                [FromBody] RequestPostJson request)
            {
                var response = await useCase.Execute(request);

                return Created(string.Empty, response);
        }
    }
}
