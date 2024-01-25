using Testes.src.app.contracts;

namespace Testes.src.app.interfaces
{
  public class UserEmailAlreadyUsedException : AppException
  {
    public UserEmailAlreadyUsedException()
    {
      this.message = "Já existe um usuário com esse email";
      this.statusCode = 422;
    }
  }
}