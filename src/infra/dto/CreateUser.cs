

using System.ComponentModel.DataAnnotations;

namespace Testes.src.infra.dto
{
  public class CreateUserDtoInfra
  {
    [Required(ErrorMessage = "O campo Link é obrigatório.")]
    //[MinLength(6, ErrorMessage = "O campo Link é obrigatório.")]
    //[StringLength(100, ErrorMessage = "O campo Link deve ter no máximo 100 caracteres.")]
    public string Email { get; set; } = "";

    [Required(ErrorMessage = "O campo Description é obrigatório.")]
    //[MinLength(10, ErrorMessage = "O campo Description é obrigatório.")]
    //[StringLength(500, ErrorMessage = "O campo Description deve ter no máximo 500 caracteres.")]
    public string Password { get; set; } = "";
  }
}