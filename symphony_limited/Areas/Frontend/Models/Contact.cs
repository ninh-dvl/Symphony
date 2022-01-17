using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace symphony_limited.Areas.Frontend.Models
{
    public class Contact
    {
        public string FullName {get;set;}
        public string Email {get;set;}
        public string Subject {get;set;}
        public string Message {get;set;}
        public string To {get;set;}
    }
}