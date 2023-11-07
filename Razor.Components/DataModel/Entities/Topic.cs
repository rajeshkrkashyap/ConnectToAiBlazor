namespace Razor.Components.DataModel.Entities
{
    public class Topic
    {
        public string? Id { get; set; } = Guid.NewGuid().ToString();
        public string? ParentId { get; set; }
        public string? AppUserId { get; set; }
        public string? SubjectId { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public string? Url { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public DateTime Updated { get; set; }

        public virtual AppUser? AppUser { get; set; }
        public virtual Subject? Subject { get; set; }
        
    }
}
