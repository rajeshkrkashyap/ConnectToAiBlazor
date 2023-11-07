//using iTextSharp.text.pdf;
//using iTextSharp.text.pdf.parser;
using Tesseract;

namespace Services
{
    public class PdfToJsonlService
    {
        public string PdfToJsonL(string fileName)
        {
            var path = System.IO.Path.Combine(@"C:\", "files/");

            string directoryPath = path + "uploads/"; //ConfigurationManager.AppSettings["InputDirectoryPath"];
            string outputDirectoryPath = path + "JSONL_Files/"; //ConfigurationManager.AppSettings["OutputDirectoryPath"];
            try
            {
                string[] files = Directory.GetFiles(directoryPath, fileName);

                // Process each file path
                foreach (string filePath in files)
                {
                    // Do something with the file path, e.g., read the file content, etc.
                    // Example: Console.WriteLine(filePath);
                    FileInfo fileInfo = new FileInfo(filePath);

                    string pdfFilePath = filePath; //Replace with the path to your PDF file
                    var outputJsonLfineName = fileInfo.Name.Replace(fileInfo.Extension, ".jsonl");
                    string jsonlFilePath = outputDirectoryPath + outputJsonLfineName; //Replace with the desired path for the Excel output file

                    var extractedContent = ExtractPdfContent(pdfFilePath);

                    string[] lines = extractedContent.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);

                    //Prepare the JSONL content
                    string jsonlContent = "";
                    foreach (string line in lines)
                    {
                        jsonlContent += $"{{\"prompt\": \"{line}\", \"completion\": \"\"}}{Environment.NewLine}";
                    }

                    // Save the JSONL content to the file
                    File.WriteAllText(jsonlFilePath, jsonlContent);
                    Console.WriteLine("Content extracted and saved to JSONL file.");

                    return outputJsonLfineName;
                }


            }
            catch (Exception ex)
            {
                // Handle any exceptions that might occur during the process
                Console.WriteLine("An error occurred: " + ex.Message);
            }
            return null;
        }
        private static string ExtractPdfContent(string pdfFilePath)
        {
            string extractedText = "";

            //using (var pdfReader = new PdfReader(pdfFilePath))
            //{
            //    for (int page = 1; page <= pdfReader.NumberOfPages; page++)
            //    {
            //        var strategy = new SimpleTextExtractionStrategy();
            //        var currentText = PdfTextExtractor.GetTextFromPage(pdfReader, page, strategy);
            //        extractedText += currentText;
            //    }
            //}

            return extractedText;
        }
        private static string ExtractImageContent(string imagePath)
        {
            using (var engine = new TesseractEngine(@"tessdata", "eng", EngineMode.Default))
            {
                using (var image = Pix.LoadFromFile(imagePath))
                {
                    using (var page = engine.Process(image))
                    {
                        return page.GetText();
                    }
                }
            }
        }
    }
}
