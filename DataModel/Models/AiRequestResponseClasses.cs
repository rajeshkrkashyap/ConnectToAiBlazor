namespace DataModel.Models
{
    #region Request Class structure
    public class Message
    {
        public string role { get; set; }
        public string content { get; set; }
    }
    public class PromptInput
    {
        public string? model { get; set; } 
        public int temperature { get; set; } = 1;
        public bool stream { get; set; } = false;
        public int max_tokens { get; set; } = 3096; //this is the output/response max length and reserved 1000 token for input and instructions
        public int presence_penalty { get; set; } = 0;
        public int frequency_penalty { get; set; } = 0;
        public string user { get; set; } = "";
        public List<Message> messages { get; set; }
    }
    public class ClientPromptInput
    {
        public string? InputType { get; set; } = "text";
        public string? UserId { get; set; }
        public string? Prompt { get; set; }
        public bool IsStreamOutPut { get; set; } = false;
        public string? Subject { get; set; } = "English";
        public string? Language { get; set; } = "English";
        public string? ChildInstructionId { get; set; }
        public string? UserDefinedSystemInstructions { get; set; }
        public string? UserBioToUnderstandUser { get; set; }

    }
    #endregion

    #region Reponse Class structure
    public class ChatCompletion
    {
        public string id { get; set; }
        public string @object { get; set; }
        public long created { get; set; }
        public string model { get; set; }
        public ChatChoice[] choices { get; set; }
        public Usage usage { get; set; }
    }
    public class ChatChoice
    {
        public int index { get; set; }
        public Chatdata message { get; set; }
        public Chatdata delta { get; set; }
        public string finish_reason { get; set; }
    }
    public class Chatdata
    {
        public string role { get; set; }
        public string content { get; set; }
    }
    public class Usage
    {
        public int prompt_tokens { get; set; }
        public int completion_tokens { get; set; }
        public int total_tokens { get; set; }
    }
    #endregion

    #region Image Prompt Request
    public class ImagePrompt
    {
        public string prompt { get; set; }
        public int n { get; set; } = 1;
        public string size { get; set; } = "256x256";
    }
    #endregion

    #region Image Prompt Reponse
    public class ImageOutput
    {
        public int created { get; set; }
        public List<ImageURL> data { get; set; }

    }
    public class ImageURL
    {
        public string url { get; set; }
    }
    #endregion

    #region MathPix Input Request
    public class MathPixRequest
    {
        public MathPixRequest()
        {
            this.data_options = new DataOptions();
        }

        public string src { get; set; }
        public string[] formats { get; set; }
        public DataOptions data_options { get; set; }
    }
    public class DataOptions
    {
        public bool include_asciimath { get; set; }
    }
    #endregion

    #region MathPix Response output
    public class MathOCRResponse
    {
        public string? request_id { get; set; }
        public string? version { get; set; }
        public int image_width { get; set; }
        public int image_height { get; set; }
        public bool is_printed { get; set; }
        public bool is_handwritten { get; set; }
        public double auto_rotate_confidence { get; set; }
        public int auto_rotate_degrees { get; set; }
        public double confidence { get; set; }
        public double confidence_rate { get; set; }
        public string? text { get; set; }
        public List<MathOCRData> data { get; set; }
    }

    public class MathOCRData
    {
        public string? type { get; set; }
        public string? value { get; set; }
    }
    #endregion

    //Image data Response 
    public class ReadImageDataResponse
    {
        public bool success { get; set; }
        public string? blobPath { get; set; }
        public string? imageContent { get; set; }
    }
}
