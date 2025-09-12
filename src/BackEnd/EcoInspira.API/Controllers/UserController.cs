using EcoInspira.Application.UseCases.User.Register;
using EcoInspira.Communication.Requests;
using EcoInspira.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace EcoInspira.API.Controllers
{


    public class UserController : EcoInspiraBaseController
    {
        [HttpPost]
        [ProducesResponseType(typeof (ResponseRegisteredUserJson), StatusCodes.Status201Created )]
        public async Task<IActionResult> Register (
            [FromServices]IRegisterUserUseCase useCase,
            [FromBody]RequestRegisterUserJson request)
        {
            var result = await useCase.Execute(request);

            return Created(string.Empty,result);
        }
    }
}