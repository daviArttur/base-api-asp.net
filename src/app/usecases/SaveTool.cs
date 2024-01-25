using Testes.src.app.interfaces;
using Testes.src.domain.entities;
using Testes.src.domain.dto;
using Testes.src.app.contracts.usecases;

namespace Testes.src.app.usecases
{
    public class SaveToolUseCase(IToolRepository _toolRepository) : ISaveToolUseCase
    {
        private readonly IToolRepository _toolRepository = _toolRepository;

        public async Task Perform(CreateToolDto dto)
        {
            Tool tool = new Tool(dto);
            await this._toolRepository.Save(tool);
        }
    }
}
