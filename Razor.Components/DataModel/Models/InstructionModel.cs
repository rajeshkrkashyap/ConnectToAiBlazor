namespace Razor.Components.DataModel.Models
{
    public class InstructionModel
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string? ParentId { get; set; }
        public string? SubjectId { get; set; }
        public int OrderId { get; set; }
        public string? Title { get; set; }
        public string? InstructionData { get; set; }
        public bool IsUserCanControl { get; set; } = false;
        public int Type { get; set; } = 0; // 0 for Text, 1 for Image, 2 for Audio, 3 for Video
        public bool IsActive { get; set; } = true;
    }
}
