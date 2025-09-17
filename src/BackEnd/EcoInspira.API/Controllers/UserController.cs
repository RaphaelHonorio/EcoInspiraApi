using EcoInspira.API.Attributes;
using EcoInspira.Application.UseCases.User.ChangePassword;
using EcoInspira.Application.UseCases.User.Profile;
using EcoInspira.Application.UseCases.User.Register;
using EcoInspira.Application.UseCases.User.Update;
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

        [HttpGet]
        [ProducesResponseType(typeof(ResponseUserProfileJson), StatusCodes.Status200OK)]
        [AuthenticatedUser]
        public async Task<IActionResult> GetUserProfile([FromServices] IGetUserProfileUseCase useCase)
        {
            var result = await useCase.Execute();

            return Ok(result);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        [AuthenticatedUser]
        public async Task<IActionResult> Update(
            [FromServices] IUpdateUserUseCase useCase,
            [FromBody] RequestUpdateUserJson request)
        {
            await useCase.Execute(request);

            return NoContent();
        }


        [HttpPut("Change-password")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        [AuthenticatedUser]
        public async Task<IActionResult> ChangePassword(
            [FromServices] IChangePasswordUseCase useCase,
            [FromBody] RequestChangePasswordJson request
            )
        {
            await useCase.Execute(request);

            return NoContent();
        }
    }
}