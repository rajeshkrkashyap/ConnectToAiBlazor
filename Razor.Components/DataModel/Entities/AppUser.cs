using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Razor.Components.DataModel.Entities
{
    public class AppUser : IdentityUser
    {
        [MaxLength(50)]
        public string? Name { get; set; } = null!;
        public string? Gender { get; set; }
        public string? Address { get; set; }
        public string? RefreshToken { get; set; }
        public string? UserAvatar { get; set; }
        public long BalanceToken { get; set;}
        public DateTime? SubscriptionEndDate { get; set; }
    }
}
