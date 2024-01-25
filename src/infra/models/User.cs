using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Testes.src.domain.dto;

namespace Testes.src.infra.models
{
  [Index(nameof(Email), IsUnique = true)]
  [ExcludeFromCodeCoverage]
  [Table("users")]
  public class UserModel : BaseModel
  {
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Required]
    [Column("email")]
    public string Email { get; set; } = "";

    [Required]
    [MinLength(6)]
    [Column("password")]
    public string Password { get; set; } = "";

    public ICollection<ToolModel> Tools { get; set; } = new List<ToolModel>();

    public UserModel() { }

    public UserModel(CreateUserDto dto)
    {
      this.Id = dto.Id;
      this.Email = dto.Email;
      this.Password = dto.Password;
      this.Tools = null!;
    }
  }
}