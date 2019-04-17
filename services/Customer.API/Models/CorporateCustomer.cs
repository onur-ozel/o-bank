﻿using System;
using System.Collections.Generic;

namespace Customer.API.Models {
    public partial class CorporateCustomer : ModelBase {
        public decimal? CustomerNumber { get; set; }
        public string Status { get; set; }
        public string Email { get; set; }
        public decimal? TaxId { get; set; }
        public string Name { get; set; }
        public string Industry { get; set; }
        public string Sector { get; set; }
    }
}