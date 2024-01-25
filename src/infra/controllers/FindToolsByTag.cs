using Microsoft.AspNetCore.Mvc;
using Testes.src.app.contracts.usecases;


namespace Testes.src.infra.controllers
{
    [ApiController]
    [Route("api/tools/tags/")]
    public class FindToolsByTagController(IFindToolsByTagUseCase _useCase) : ControllerBase
    {
        private readonly IFindToolsByTagUseCase _useCase = _useCase;

        [HttpGet("{tag}")]
        public async Task<ActionResult> Handle(string tag)
        {
            var data = await this._useCase.Perform(tag);
            return StatusCode(200, new { data });
        }
    }
}
