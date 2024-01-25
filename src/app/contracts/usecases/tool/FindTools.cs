using Testes.src.domain.entities;

namespace Testes.src.app.contracts.usecases
{
  public interface IFindToolsUseCase
  {
    public Task<List<Tool>> Perform();
  }
}