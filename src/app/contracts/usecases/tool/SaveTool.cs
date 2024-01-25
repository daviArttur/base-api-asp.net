using Testes.src.domain.dto;

namespace Testes.src.app.contracts.usecases
{
  public interface ISaveToolUseCase
  {
    public Task Perform(CreateToolDto dto);
  }
}

