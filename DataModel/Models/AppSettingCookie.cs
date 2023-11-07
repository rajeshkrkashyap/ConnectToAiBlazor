using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.Models
{
    public class AppSettingCookie
    {
        public string? Id { get; set; }
        public string? AppUserId { get; set; }
        public string? SubjectId { get; set; }
        public string? Language { get; set; }
        public string? InputType { get; set; }
        public string? IsQueryActive { get; set; }
        public string? Subject { get; set; }
        public DateTime? SubscriptionEndDate { get; set; }
        public string? ChildInstructionId { get; set; }
        public string? UserInstructions { get; set; }

    }
}
