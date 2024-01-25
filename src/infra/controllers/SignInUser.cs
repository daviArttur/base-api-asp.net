using Microsoft.AspNetCore.Mvc;
using Testes.src.app.contracts.usecases;
using Testes.src.infra.dto;

namespace Testes.src.infra.controllers
{

    [ApiController]
    [Route("api/users/signin")]
    public class SignInUserController(ISignInUserUseCase _useCase) : ControllerBase
    {
        private readonly ISignInUserUseCase _useCase = _useCase;

        [HttpPost]
        public async Task<ActionResult> Handle([FromBody] CreateUserDtoInfra dto)
        {   
            var data = await this._useCase.Perform(dto.Email, dto.Password);
            return StatusCode(200, new { data });
        }
    }
}
