using System;
using System.Collections.Generic;

namespace Customer.API.Models
{
    public partial class CustomerPhone
    {
        public string Id { get; set; }
        public bool? State { get; set; }
        public string CreatedUser { get; set; }
        public DateTime? CreatedDate { get; set; }
        public decimal? CustomerNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string PhoneNumberType { get; set; }
    }
}
