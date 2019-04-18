using System;
using System.Collections.Generic;
using System.Text;

namespace Customer.API.Models {
    public partial class CorporateCustomer : ModelBase {
        public decimal CustomerNumber { get; set; }
        public string Status { get; set; }
        public string Email { get; set; }
        public decimal TaxId { get; set; }
        public string Name { get; set; }
        public string Industry { get; set; }
        public string Sector { get; set; }

       

        public string ValidateAndPrepeareForInsert () {
            string validationResult = ValidateForInsert ();

            if (string.IsNullOrEmpty (validationResult)) {
                PrepeareForInsert ();
            }
            return validationResult;
        }

        private void PrepeareForInsert () {
            Id = Guid.NewGuid ().ToString ();
            State = true;
            //TODO set user
            LastModifiedUser = "System";
            LastModifiedDate = DateTime.Now;
        }

        private string ValidateForInsert () {
            StringBuilder validationErrors = new StringBuilder ();

            if (default (decimal) == CustomerNumber) {
                validationErrors.AppendLine ("customerNumber is required");
            }

            return validationErrors.ToString ();
        }
    }
}