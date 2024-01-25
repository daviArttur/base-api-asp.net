using Testes.src.app.usecases;
using Microsoft.AspNetCore.Mvc;
using Testes.src.app.contracts.usecases;


namespace Testes.src.infra.controllers
{
    [ApiController]
    [Route("api/tools")]
    public class FindToolsController(IFindToolsUseCase _useCase) : ControllerBase
    {

        private readonly IFindToolsUseCase _useCase = _useCase;

        [HttpGet]
        public async Task<ActionResult> Handle()
        {
            var data = await this._useCase.Perform();
            return StatusCode(200, new { data });
        }
    }
}
