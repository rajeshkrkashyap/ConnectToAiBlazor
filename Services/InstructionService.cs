using DataModel;
using DataModel.Entities;
using DataModel.Models;
using DataModel.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class InstructionService : BaseService
    {
        public InstructionService(AppSettings appSettings) : base(appSettings)
        {
        }

        public async Task<Dictionary<string, string>> ParentChildrenAsync(string subjectId)
        {
            var returnResponse = new Dictionary<string, string>();
            using (var client = new HttpClient())
            {
                var url = $"{AppSettings.ApiBaseUrl}{ApiUrl.InstructionParentWithChildren}/?subjectId=" + subjectId;
                //var content = new StringContent(subjectId, System.Text.Encoding.UTF8, "application/json");
                //var serializedStr = JsonConvert.SerializeObject("{'subjectId':'" + subjectId + "'}");

                var response = await client.PostAsync(url, null);

                if (response.IsSuccessStatusCode)
                {
                    string contentStr = await response.Content.ReadAsStringAsync();
                    returnResponse = JsonConvert.DeserializeObject<Dictionary<string, string>>(contentStr);
                }
            }
            return returnResponse;
        }
        public async Task<Instruction> ParentNodeAsync(string subjectId)
        {
            var returnResponse = new Instruction();
            using (var client = new HttpClient())
            {
                var url = $"{AppSettings.ApiBaseUrl}{ApiUrl.InstructionParentNode}/?subjectId=" + subjectId;
                //var content = new StringContent(subjectId, System.Text.Encoding.UTF8, "application/json");
                //var serializedStr = JsonConvert.SerializeObject("{'subjectId':'" + subjectId + "'}");

                var response = await client.PostAsync(url, null);

                if (response.IsSuccessStatusCode)
                {
                    string contentStr = await response.Content.ReadAsStringAsync();
                    returnResponse = JsonConvert.DeserializeObject<Instruction>(contentStr);
                }
            }
            return returnResponse;
        }
        public async Task<List<Instruction>> ListAsync()
        {
            var returnResponse = new List<Instruction>();
            using (var client = new HttpClient())
            {
                var url = $"{AppSettings.ApiBaseUrl}{ApiUrl.InstructionList}";

                var serializedStr = JsonConvert.SerializeObject("");
                try
                {

                    var response = await client.PostAsync(url, new StringContent(serializedStr, Encoding.UTF8, "application/json"));

                    if (response.IsSuccessStatusCode)
                    {
                        string contentStr = await response.Content.ReadAsStringAsync();
                        returnResponse = JsonConvert.DeserializeObject<List<Instruction>>(contentStr);
                    }
                }
                catch (Exception ex)
                {

                    throw;
                }

            }
            return returnResponse;
        }
        public async Task<Instruction> GetById(string id)
        {
            var returnResponse = new Instruction();
            using (var client = new HttpClient())
            {
                try
                {
                    var url = $"{AppSettings.ApiBaseUrl}{ApiUrl.InstructionGetById}/?id=" + id;
                    var response = await client.PostAsync(url, null);

                    if (response.IsSuccessStatusCode)
                    {
                        string contentStr = await response.Content.ReadAsStringAsync();
                        returnResponse = JsonConvert.DeserializeObject<Instruction>(contentStr);
                    }
                }
                catch (Exception ex)
                {

                    throw;
                }

            }
            return returnResponse;
        }
        public async Task<Instruction> GetByTitle(string title)
        {
            var returnResponse = new Instruction();
            using (var client = new HttpClient())
            {
                var url = $"{AppSettings.ApiBaseUrl}{ApiUrl.InstructionGetByTitle}/?title=" + title;
                var response = await client.PostAsync(url, null);

                if (response.IsSuccessStatusCode)
                {
                    string contentStr = await response.Content.ReadAsStringAsync();
                    returnResponse = JsonConvert.DeserializeObject<Instruction>(contentStr);
                }
            }
            return returnResponse;
        }

        public async Task<Instruction> GetBySubjectIdAndTitle(string title, string subjectId)
        {
            var returnResponse = new Instruction();
            using (var client = new HttpClient())
            {
                var url = $"{AppSettings.ApiBaseUrl}{ApiUrl.InstructionGetBySubjectIdAndTitle}/?subjectId=" + subjectId + "&title=" + title;
                var response = await client.PostAsync(url, null);

                if (response.IsSuccessStatusCode)
                {
                    string contentStr = await response.Content.ReadAsStringAsync();
                    returnResponse = JsonConvert.DeserializeObject<Instruction>(contentStr);
                }
            }
            return returnResponse;
        }
        public async Task<Instruction> CreateAsync(InstructionModel instruction)
        {
            var returnResponse = new Instruction();
            using (var client = new HttpClient())
            {
                var url = $"{AppSettings.ApiBaseUrl}{ApiUrl.InstructionCreate}";

                var serializedStr = JsonConvert.SerializeObject(instruction);

                var response = await client.PostAsync(url, new StringContent(serializedStr, Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    string contentStr = await response.Content.ReadAsStringAsync();
                    returnResponse = JsonConvert.DeserializeObject<Instruction>(contentStr);
                }
            }
            return returnResponse;
        }

        public async Task<bool> UpdateAsync(Instruction instruction)
        {
            var returnResponse = false;
            using (var client = new HttpClient())
            {
                InstructionModel instructionModel = new InstructionModel
                {
                    Id = instruction.Id,
                    Title = instruction.Title,
                    InstructionData = instruction.InstructionData,
                    IsUserCanControl = false,  //Convert.ToBoolean(isUserCanControl),
                    SubjectId = instruction.SubjectId,
                    ParentId = instruction.ParentId,
                    IsActive = true,
                    OrderId = 0,
                    Type = 0,
                };

                var url = $"{AppSettings.ApiBaseUrl}{ApiUrl.InstructionUpdate}";
                var serializedStr = JsonConvert.SerializeObject(instructionModel);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.PostAsync(url, new StringContent(serializedStr, Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    string contentStr = await response.Content.ReadAsStringAsync();
                    returnResponse = JsonConvert.DeserializeObject<bool>(contentStr);
                }
            }
            return returnResponse;
        }
        public async Task<Instruction> UpdateAsync(string subjectId, string parentId, string instructionId, string title, string instructionData, string isUserCanControl = "false")
        {
            var returnResponse = new Instruction();
            using (var client = new HttpClient())
            {
                InstructionModel instruction = new InstructionModel
                {
                    Id = instructionId,
                    Title = title,
                    InstructionData = instructionData,
                    IsUserCanControl = Convert.ToBoolean(isUserCanControl),
                    SubjectId = subjectId,
                    ParentId = parentId,
                    IsActive = true,
                    OrderId = 0,
                    Type = 0,
                };

                var url = $"{AppSettings.ApiBaseUrl}{ApiUrl.InstructionUpdate}";
                var serializedStr = JsonConvert.SerializeObject(instruction);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.PostAsync(url, new StringContent(serializedStr, Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    string contentStr = await response.Content.ReadAsStringAsync();
                    returnResponse = JsonConvert.DeserializeObject<Instruction>(contentStr);
                }
            }
            return returnResponse;
        }
        public async Task<bool> Delete(string id)
        {
            var returnResponse = false;
            using (var client = new HttpClient())
            {
                var url = $"{AppSettings.ApiBaseUrl}{ApiUrl.InstructionDelete}/?id=" + id;

                var response = await client.PostAsync(url, null);

                if (response.IsSuccessStatusCode)
                {
                    string contentStr = await response.Content.ReadAsStringAsync();
                    returnResponse = JsonConvert.DeserializeObject<bool>(contentStr);
                }
            }
            return returnResponse;
        }

        public string GenerateInstructionTree(Instruction InstructionTree)
        {
            if (InstructionTree != null)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("<ol class=\"dd-list\">");
                sb.AppendLine($"<li class=\"dd-item\" data-id=\"{InstructionTree.Id}\">");
                sb.AppendLine($"<div class='dd-handle' id='Ins-" + InstructionTree.Id + "' onclick=selectInstruction('Ins-" + InstructionTree.Id + "')>" + InstructionTree.Title + " <a href='/Admin/Instruction/Create' class='pull-right'>Add <i class='fa fa-plus'></i></a> </div>");
                if (InstructionTree.Children != null)
                {
                    sb.AppendLine("<ol class=\"dd-list\">");
                    GenerateHtml(sb, InstructionTree.Children);
                    sb.AppendLine("</ol>");
                }
                sb.AppendLine("</li>");
                sb.AppendLine("</ol>");
                return sb.ToString();
            }
            else
            {
                return "";
            }
        }

        private static void GenerateHtml(StringBuilder sb, List<Instruction> instrcutionTree)
        {
            foreach (var item in instrcutionTree)
            {
                sb.AppendLine($"<li class=\"dd-item\" data-id=\"{item.Id}\">");
                sb.AppendLine($"<div class='dd-handle' id='Ins-" + item.Id + "'  onclick=selectInstruction('Ins-" + item.Id + "')>" + item.Title + "<a href='/Admin/Instruction/Create' class='pull-right'>Add <i class='fa fa-plus'></i></a> </div>");

                if (item.Children != null && item.Children.Count > 0)
                {
                    sb.AppendLine("<ol class=\"dd-list\">");
                    GenerateHtml(sb, item.Children);
                    sb.AppendLine("</ol>");
                }
                sb.AppendLine("</li>");
            }
        }

    }
}
