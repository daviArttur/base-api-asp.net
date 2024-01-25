using Testes.src.domain.entities;

namespace Testes.src.app.contracts.usecases
{
  public interface IFindToolsByTagUseCase
  {
    public Task<List<Tool>> Perform(string tag);
  }
}