namespace Customer.API.Models {
    public class CorporateCustomerItem : CustomerItem {
        public string Name { get; set; }
        public string Industry { get; set; }
        public string Sector { get; set; }
        public decimal TaxId { get; set; }
        public string WebSite { get; set; }
        public string Email { get; set; }
    }
}