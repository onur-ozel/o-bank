using System;

namespace Customer.API.Models {
    public class RetailCustomerItem : CustomerItem {
        public decimal NationalId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Nationality { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public char Gender { get; set; }
      
    }
}