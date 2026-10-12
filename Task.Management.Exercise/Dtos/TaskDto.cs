using System.ComponentModel.DataAnnotations;
using TaskManagement.Exercise.Enums;

namespace TaskManagement.Exercise.Dtos
{
    public record TaskDto
    {
        public int? Id { get; set; }
        [Required]
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public Status Status { get; set; } = Status.Open; 
    }
}
