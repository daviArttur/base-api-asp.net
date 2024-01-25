using Testes.src.app.contracts;

namespace Testes.src.app.interfaces
{
  public class JwtSecretKeyNotFound : AppException
  {
    public JwtSecretKeyNotFound()
    {
      this.message = "Um ero interno aconteceu, tente novamente mais tarde";
      this.statusCode = 500;
    }
  }
}

