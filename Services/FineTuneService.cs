namespace Services
{
    public class FineTuneService
    {
        public async Task<HttpResponseMessage?> Training(string fileName)
        {

            using (var httpClient = new HttpClient())
            {
                try
                {
                    var filePath = Path.Combine(@"C:\files\", "JSONL_Files", fileName);
                    //var filePath = @"C:\files\Chapter One ELECTRIC CHARGES AND FIELDS.jsonl";//Path.Combine(@"./", "JSONL_Files", fileName);

                    var requestUrl = "https://api.openai.com/v1/files";

                    httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "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");

                    using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                    {
                        using (var form = new MultipartFormDataContent())
                        {
                            form.Add(new StreamContent(fileStream), "file", Path.GetFileName(filePath));
                            if (!string.IsNullOrEmpty("fine-tune"))
                            {
                                form.Add(new StringContent("fine-tune"), "purpose");
                            }

                            var response = await httpClient.PostAsync(requestUrl, form);
                            response.EnsureSuccessStatusCode();
                            var responseContent = await response.Content.ReadAsStringAsync();
                        }
                    }

                }
                catch (Exception ex)
                {
                }
            }
            return null;
        }
    }
}
