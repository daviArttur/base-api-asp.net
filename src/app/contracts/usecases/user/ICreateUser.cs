namespace Testes.src.app.contracts.usecases
{
  public interface ICreateUserUseCase
  {
    public Task Perform(string email, string password);
  }
}