namespace Razor.Components.DataModel.Entities
{
    public class TokenSpentHistory
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string UserId { get; set; }
        public int TokenUsed { get; set; }
        public DateTime Created { get; set; }
    }
}
