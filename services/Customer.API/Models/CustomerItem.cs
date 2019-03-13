using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Customer.API.Models {
    public abstract class CustomerItem {
        public string Id { get; set; }

        public long No { get; set; }
    }
}