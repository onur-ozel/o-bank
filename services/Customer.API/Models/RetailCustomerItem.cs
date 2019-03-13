namespace Customer.API.Models {
    public class RetailCustomerItem : CustomerItem {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal NationalId { get; set; }
    }
}