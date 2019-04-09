using System;
using System.Collections.Generic;

namespace Customer.API.Models {
    public partial class CustomerPhone : ModelBase {
        public decimal? CustomerNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string PhoneNumberType { get; set; }
    }
}