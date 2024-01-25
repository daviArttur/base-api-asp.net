using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Testes.src.app.contracts.usecases;
using Testes.src.app.interfaces;
using Testes.src.app.usecases;
using Testes.src.domain.dto;
using Testes.src.infra.dto;


namespace Testes.src.infra.controllers
{
    [Authorize]
    [ApiController]
    [Route("api/tools")]
    public class SaveToolController(ISaveToolUseCase _useCase, IAuthService _authService) : ControllerBase
    {
        private readonly IAuthService _authService = _authService;
        private readonly ISaveToolUseCase _useCase = _useCase;

        [HttpPost]
        public async Task<ActionResult> Handle(SaveToolDtoInfra dto)
        {
            await this._useCase.Perform(new CreateToolDto{
                Description = dto.description,
                Link = dto.link,
                Id = 0,
                Title = dto.title,
                Tags = dto.tags,
                UserId = this._authService.RecoveryUserIdFromHttpContext(HttpContext),
            });
            return StatusCode(201);
        }
    }
}
