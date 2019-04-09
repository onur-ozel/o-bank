using System;
using System.Collections.Generic;

namespace Customer.API.Models {
    public partial class Error : ModelBase {
        public string Type { get; set; }
        public string Code { get; set; }
        public string Level { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
        public string Help { get; set; }
    }

    public enum Level {
        Error,
        Fatal,
        Warning,
        Info,
        Debug
    }
}