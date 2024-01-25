using Microsoft.AspNetCore.Mvc;
using Testes.src.app.contracts.usecases;
using Testes.src.infra.dto;

namespace Testes.src.infra.controllers
{

    [ApiController]
    [Route("api/users")]
    public class CreateUserController(ICreateUserUseCase _useCase) : ControllerBase
    {
        private readonly ICreateUserUseCase _useCase = _useCase;

        [HttpPost]
        public async Task<ActionResult> Handle([FromBody] CreateUserDtoInfra dto)
        {
            await this._useCase.Perform(dto.Email, dto.Password);
            return StatusCode(201);
        }
    }
}
