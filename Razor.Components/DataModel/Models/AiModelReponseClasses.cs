namespace Razor.Components.DataModel.Models
{
    #region Reponse Class structure
    public class ChatCompletion
    {
        public string? id { get; set; }
        public string? @object { get; set; }
        public long created { get; set; }
        public string? model { get; set; }
        public ChatChoice[]? choices { get; set; }
        public Usage? usage { get; set; }
    }
    public class ChatChoice
    {
        public int index { get; set; }
        public Chatdata? message { get; set; }
        public Chatdata? delta { get; set; }
        public string? finish_reason { get; set; }
    }
    public class Chatdata
    {
        public string? role { get; set; }
        public string? content { get; set; }
    }
    public class Usage
    {
        public int prompt_tokens { get; set; }
        public int completion_tokens { get; set; }
        public int total_tokens { get; set; }
    }
    #endregion
}
