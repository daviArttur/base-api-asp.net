namespace Testes.src.app.contracts.usecases
{
  public struct SignInUserOutput
  {
    public string AccessToken { get; set; }
  }
   public interface ISignInUserUseCase
  {
    public Task<SignInUserOutput> Perform(string email, string rawPassword);
  }
}