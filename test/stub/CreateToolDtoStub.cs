using Testes.src.domain.dto;



namespace Testes.test.stub
{

  public class CreateToolDtoStub
  {
    public static CreateToolDto GetData(int UserId = 1, bool generateRandomId = false)
    {
      List<string> tags = ["tag1", "tag2"];
      string title = "Title of Tool";
      string link = "https://testlink.com";
      string description = "mock description";

      if (!generateRandomId) return new CreateToolDto{Title = title, Link = link, Description = description, Tags = [], UserId = UserId};
      
      int randomId = new Random().Next(1, 9999);
      return new CreateToolDto{
        Id = randomId,
        Description = description,
        Title = title,
        Link = link,
        Tags = tags,
        UserId = UserId,
      };
    }
  }
}