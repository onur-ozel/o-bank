using System;
using System.Collections.Generic;

namespace Customer.API.Models
{
    public partial class ModelBase
    {
        public string Id { get; set; }
        public bool State { get; set; }
        public string LastModifiedUser { get; set; }
        public DateTime LastModifiedDate { get; set; }
    }
}
