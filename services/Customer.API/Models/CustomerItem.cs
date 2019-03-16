using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Customer.API.Models {
    public abstract class CustomerItem {
        public string Id { get; set; }

        public decimal? CustomerNo { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedUser { get; set; }
    }
}