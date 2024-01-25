using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using Testes.src.domain.dto;


namespace Testes.src.infra.models
{
    [ExcludeFromCodeCoverage]
    [Table("tools")]
    public class ToolModel : BaseModel
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        [Column("title")]
        public string Title { get; set; } = "";

        [Required]
        [Column("link")]
        public string Link { get; set; } = "";

        [Required]
        [Column("description")]
        public string Description { get; set; } = "";

        public ICollection<TagModel> Tags { get; set; } = new List<TagModel>();

        [Required]
        public int UserId { get; set; }

        public UserModel User { get; set; } = null!;

        public ToolModel() { }

        public ToolModel(CreateToolDto dto)
        {
            this.Link = dto.Link;
            this.Description = dto.Description;
            this.Title = dto.Title;
            this.Id = dto.Id;
            this.UserId = dto.UserId;
        }
    }
}