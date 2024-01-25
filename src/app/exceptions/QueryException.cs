using Testes.src.app.contracts;

namespace Testes.src.app.interfaces
{
  public class QueryException : AppException
  {
    public QueryException()
    {
      this.message = "Um erro inesperado aconteceu";
      this.statusCode = 500;
    }
  }
}

