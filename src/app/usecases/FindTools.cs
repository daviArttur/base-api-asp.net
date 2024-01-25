using Testes.src.app.contracts.usecases;
using Testes.src.app.interfaces;
using Testes.src.domain.entities;

namespace Testes.src.app.usecases
{
    public class FindToolsUseCase(IToolRepository _repository) : IFindToolsUseCase
    {
        private readonly IToolRepository _repository = _repository;

        public async Task<List<Tool>> Perform()
        {
            var Tool = await _repository.FindAll();
            return Tool;
        }
    }
}
