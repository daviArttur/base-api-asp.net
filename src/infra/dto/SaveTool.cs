using System.ComponentModel.DataAnnotations;

namespace Testes.src.infra.dto
{
  public class EachTagNotEmptyAttribute : ValidationAttribute
  {
    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
      { 
          if (value is not List<string> tags) return new ValidationResult(ErrorMessage);
          foreach (var tag in tags)
          {
              if (string.IsNullOrWhiteSpace(tag))
              {
                  return new ValidationResult(ErrorMessage);
              }
          }
          return ValidationResult.Success!;
      }
  }

  public class SaveToolDtoInfra
  {
    [Required(ErrorMessage = "O campo Link é obrigatório.")]
    [MinLength(6, ErrorMessage = "O campo Link é obrigatório.")]
    [StringLength(100, ErrorMessage = "O campo Link deve ter no máximo 100 caracteres.")]
    public string link { get; set; } = null!;
    [Required(ErrorMessage = "O campo Description é obrigatório.")]
    [MinLength(10, ErrorMessage = "O campo Description é obrigatório.")]
    [StringLength(500, ErrorMessage = "O campo Description deve ter no máximo 500 caracteres.")]
    public string description { get; set; } = null!;
    [Required(ErrorMessage = "O campo Title é obrigatório.")]
    [MinLength(2, ErrorMessage = "O campo Title é obrigatório.")]
    [StringLength(50, ErrorMessage = "O campo Title deve ter no máximo 50 caracteres.")]
    public string title { get; set; } = null!;
    [Required(ErrorMessage = "O campo Tags é obrigatório.")]
    //[MinLength(1, ErrorMessage = "A lista de tags deve conter no mínimo 5 elementos.")]
    [EachTagNotEmpty(ErrorMessage = "Cada tag não pode estar vazia.")]
    public List<string> tags { get; set; } = null!;
  }
}