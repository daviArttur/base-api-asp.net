using Testes.src.domain.dto;

namespace Testes.src.domain.entities
{
  public class Tool(CreateToolDto dto)
  {
    public int Id { get; } = dto.Id;
    public int UserId { get; } = dto.UserId;
    public string Title { get; } = dto.Title;
    public string Link { get; } = dto.Link;
    public string Description { get; } = dto.Description;
    public List<string> Tags { get; } = dto.Tags;
  }
}