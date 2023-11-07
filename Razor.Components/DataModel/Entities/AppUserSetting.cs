namespace Razor.Components.DataModel.Entities
{
    public class AppUserSetting
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string? AppUserId { get; set; }
        public string? SubjectId { get; set; }
        public string? Language { get; set; } 
        public string? InputType { get; set; } = "text";
        public string? TotalToken { get; set; }
        public string? TokenSpend { get; set; }
        public string? TodaySpend { get; set; }
        public string? Variable1 { get; set; }
        public string? Variable2 { get; set; }
        public string? Variable3 { get; set; }
        public string? Variable4 { get; set; }
        public string? Variable5 { get; set; }
        public bool IsQueryActive { get; set; } = false;
        public bool IsActive { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public virtual AppUser? AppUser { get; set; }
        public virtual Subject? Subject { get; set; }

    }
}
