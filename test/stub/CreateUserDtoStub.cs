using Testes.src.domain.dto;

namespace Testes.test.stub
{

  public class CreateUserDtoStub
  {
    public static CreateUserDto GetData(bool generateRandomId = false)
    {
      if(generateRandomId)
      {
        int randomId = new Random().Next(1, 9999);
        return new CreateUserDto(randomId, "Title of Tool", "https://");
      }

      return new CreateUserDto{Email = "test@mail.com", Password = "pass123"};
    }
  }
}