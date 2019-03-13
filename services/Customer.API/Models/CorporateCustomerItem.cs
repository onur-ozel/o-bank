namespace Customer.API.Models {
    public class CorporateCustomerItem : CustomerItem {
        public string Name { get; set; }
        public decimal TaxId { get; set; }
    }
}