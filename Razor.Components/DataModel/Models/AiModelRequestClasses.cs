namespace Razor.Components.DataModel.Models
{
    #region Request Class structure
    public class Message
    {
        public string? role { get; set; }
        public string? content { get; set; }
    }

    public class ClientPromptInput
    {
        public string? InputType { get; set; }
        public string? UserId { get; set; }
        public string? Prompt { get; set; }
        public bool IsStreamOutPut { get; set; } = false;
        public string? Subject { get; set; }
        public string? ChildInstructionId { get; set; }
    }
    #endregion
}
