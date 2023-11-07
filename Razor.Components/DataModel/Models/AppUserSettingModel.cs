using System.ComponentModel.DataAnnotations;

namespace Razor.Components.DataModel.Models
{
    public class AppUserSettingModel
    {
        public string? Id { get; set; } = Guid.NewGuid().ToString();
        [Required]
        public string? SubjectId { get; set; }
        [Required]
        public string? Language { get; set; }
        [Required]
        public string? InputType { get; set; }
        public bool IsQueryActive { get; set; }
    }

}
