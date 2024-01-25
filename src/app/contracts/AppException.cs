using System.Net;

namespace Testes.src.app.contracts
{
  public abstract class AppException : Exception
  {
    public int statusCode = 500;
    public string message = "";
  }
}