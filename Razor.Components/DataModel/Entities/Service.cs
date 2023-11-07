using System.ComponentModel.DataAnnotations.Schema;

namespace Razor.Components.DataModel.Entities    
{
    public class Service
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string? Name { get; set; }
        public string? Description { get; set; }

        [Column(TypeName = "decimal(18, 4)")]
        public decimal Cost { get; set; }
        public DateTime? Created { get; set; } = DateTime.UtcNow;
        public DateTime? Updated { get; set; }
        public bool IsActive { get; set; }
    }
}
