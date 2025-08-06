using EcoInspira.Application.UseCases.User.Register;
using EcoInspira.Communication.Requests;
using EcoInspira.Communication.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcoInspira.API.Controllers
{

    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof (ResponseRegisteredUserJson), StatusCodes.Status201Created )]
        public IActionResult Register (RequestRegisterUserJson request)
        {
            var useCase = new RegisterUserUseCase();

            var result = useCase.Execute(request);

            return Created(string.Empty,result);
        }
    }
}
