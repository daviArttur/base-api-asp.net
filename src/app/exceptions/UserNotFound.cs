using Testes.src.app.contracts;

namespace Testes.src.app.interfaces
{
  public class UserNotFoundException : AppException
  {
    public UserNotFoundException()
    {
      this.message = "Usuário ou senha inválidos";
      this.statusCode = 404;
    }
  }
}