using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BigBank.API.Models
{
    public class customerModel
    {
        public int CustomerId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }

        public string accountsUrl
        {
            get
            {
                return "api/accounts/GetForcustomer" + CustomerId;
            }
        }
    }
}