namespace Razor.Components.DataModel.Entities
{
    public class TopicMedia
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string? TopicId { get; set; }
        public string? LocalURLFileName { get; set; }
        public string? URL { get; set; }
        public string? MediaPrompt { get; set; }
        public string? Type { get; set; }
        public bool IsActive { get; set; }
        public virtual Topic? Topic { get; set; }
    }
}
