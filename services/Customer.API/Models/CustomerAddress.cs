﻿using System;
using System.Collections.Generic;

namespace Customer.API.Models
{
    public partial class CustomerAddress:ModelBase
    {
        public decimal? CustomerNumber { get; set; }
        public string CountryName { get; set; }
        public string ProvienceName { get; set; }
        public string DistrictName { get; set; }
        public string AddressLine { get; set; }
        public string PostalCode { get; set; }
        public string AddressType { get; set; }
    }
}
