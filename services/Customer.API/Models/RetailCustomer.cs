using System;
using System.Collections.Generic;

namespace Customer.API.Models {
    public partial class RetailCustomer : ModelBase {

        public decimal? CustomerNumber { get; set; }
        public string Status { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Nationality { get; set; }
        public decimal? NationalId { get; set; }
        public string Gender { get; set; }
        public string BirthPlace { get; set; }
        public DateTime? BirthDate { get; set; }
        public string CompanyName { get; set; }
        public string Department { get; set; }
        public string JobTitle { get; set; }
    }
}