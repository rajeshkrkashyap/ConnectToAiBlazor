namespace Razor.Components.DataModel.Entities
{
    public class Project
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string? AppUserId { get; set; }
        public string? Name { get; set; }
        public string? URL { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public virtual AppUser? AppUser { get; set; }
    }
}
