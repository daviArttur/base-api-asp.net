
namespace Testes.src.domain.dto
{
    public class CreateToolDto
    {
        public int Id;
        public string Title = "";
        public string Link = "";
        public string Description = "";
        public int UserId;
        public List<string> Tags = null!;
    }
}

