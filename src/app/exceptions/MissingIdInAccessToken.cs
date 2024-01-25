using Testes.src.app.contracts;

namespace Testes.src.app.interfaces
{
  public class IdMissingFromAccessTokenException : AppException
  {
    public IdMissingFromAccessTokenException()
    {
      this.message = "Token param invalid";
      this.statusCode = 403;
    }
  }
}

