using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.Models
{
    public class LoginRegisterMobileViewModel
    {
        public string? CountryCode { get; set; }

        [Required]
        [StringLength(10)]
        public string? MobileNumber { get; set; }

        [Required]
        [StringLength(10, MinimumLength = 6)]
        public string? OTP { get; set; }

        [Required]
        [StringLength(50)]
        public string Role { get; set; } = "avatar";
    }
}
