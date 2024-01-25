
namespace Testes.src.app.interfaces
{
  public interface IAuthService
  {
    public string GenerateToken(int userId);
    public void DecodeToken(string token);
    public int RecoveryUserIdFromHttpContext(HttpContext httpContext);
  }
}