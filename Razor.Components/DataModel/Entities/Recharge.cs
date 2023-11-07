namespace Razor.Components.DataModel.Entities
{
    public class Recharge
    {
        public string Id { get; set; } = Guid.NewGuid().ToString(); 
        public string? AppUserId { get; set; }
        public string? ServiceId { get; set; }
        public string? Currency { get; set; }
        public string? RechargeAmount { get; set; }
        public string? Tokens { get; set; }
        public string? Razorpay_payment_id { get; set; }
        public string? Razorpay_order_id { get; set; }
        public string? Razorpay_signature { get; set; }
        public string? OrderId { get; set; }
        public string? SubscriptionType { get; set; }
        public DateTime? SubscriptionStartDate { get; set; } 
        public DateTime? Created { get; set; } = DateTime.UtcNow;
        public DateTime? Updated { get; set; }
        public bool IsActive { get; set; }
        public virtual AppUser? AppUser { get; set; }
        public virtual Service? Service { get; set; }
    }
}
