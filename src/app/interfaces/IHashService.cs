
namespace Testes.src.app.interfaces
{
  public interface IHashService
  {
    public string Hash(string rawPassword);
    
    public bool Compare(string rawPassword, string hashedPassword);
  } 
}