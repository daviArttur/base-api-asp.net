

using NUnit.Framework;

namespace Testes.src.domain.objectValues
{
  public class TagTest
  {
    [Test]
    [Category("unit")]
    [Description("It should return tags of repository")]
    public void Test()
    {
      // Arrange
      Tag tag = new Tag{
        Id = 1,
        Value = "Test",
        ToolId = 10,
      };

      // Assert
      Assert.That(tag.Id, Is.EqualTo(1));
      Assert.That(tag.Value, Is.EqualTo("Test"));
      Assert.That(tag.ToolId, Is.EqualTo(10));
    }
  }
}