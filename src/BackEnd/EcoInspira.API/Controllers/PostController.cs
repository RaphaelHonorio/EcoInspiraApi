using EcoInspira.API.Attributes;
using EcoInspira.Application.UseCases.Post.Filter;
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

        [HttpPost("filter")]
        [ProducesResponseType(typeof(ResponseListPostsJson), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Filter(
            [FromServices] IFilterPostUseCase useCase, 
            [FromBody] RequestFilterPostJson request)
        {
            var response = await useCase.Execute(request);

            if (response.Post.Any())
                return Ok(response);

           return NoContent();
        }
    }
}
