namespace DataModel.Utility
{
    public class AppSettings
    {
        public string? ApiBaseUrl { get; set; } = "https://connecttoapi.in/";
        public string? PromptModel_Text_Davinci_003 { get; set; } = "text-davinci-003";
        public string? PromptModel_GPT_3_5_Turbo { get; set; } = "gpt-3.5-turbo";
        public string? OutputFolderPath { get; set; } = string.Empty;
        public string? InputFolderPath { get; set; } = string.Empty;
        public string? ChatCompletionsApi { get; set; } = string.Empty;
        public string? FineTuneApi { get; set; } = string.Empty;
        public string? BlobConnectionString { get; set; } = string.Empty;
        public string? BlobFilesContainer { get; set; } = string.Empty;
        public string? BlobImagesContainer { get; set; } = string.Empty;
        public string? AppRootFolderpath { get; set; } = string.Empty;
    }
}
