namespace Razor.Components.DataModel.Entities
{
    public class Instruction
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string? ParentId { get; set; }
        public string? SubjectId { get; set; }
        public int OrderId { get; set; }
        public string? Title { get; set; }
        public string? InstructionData { get; set; }
        public bool IsUserCanControl { get; set; } = false;
        public int Type { get; set; } = (int)InstructionType.Text; // 0 for Text, 1 for Image, 2 for Audio, 3 for Video
        public bool IsActive { get; set; } = true;
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public DateTime Updated { get; set; }
        public virtual Instruction? Parent { get; set; }
        public virtual Subject? Subject { get; set; }
        public List<Instruction> Children { get; set; }
    }

    public enum InstructionType
    {
        Text = 0,
        Image = 1,
        Audio = 3,
        Video = 4
    }
}
