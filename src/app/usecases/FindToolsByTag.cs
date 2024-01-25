using Testes.src.app.contracts.usecases;
using Testes.src.app.interfaces;
using Testes.src.domain.entities;

namespace Testes.src.app.usecases
{
    public class FindToolsByTagUseCase(IToolRepository _repository) : IFindToolsByTagUseCase
    {
        private readonly IToolRepository _repository = _repository;

        public async Task<List<Tool>> Perform(string tag)
        {
            var Tool = await _repository.FindByTag(tag);
            return Tool;
        }
    }
}

