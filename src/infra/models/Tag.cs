using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Testes.src.infra.models
{
    [ExcludeFromCodeCoverage]
    [Table("tags")]
    public class TagModel : BaseModel
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        public int ToolId { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        [Column("value")]
        public required string Value { get; set; }

        public ToolModel Tool { get; set; } = null!;
    }
}