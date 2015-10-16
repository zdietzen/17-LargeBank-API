using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BigBank.API.Models
{
    public class accountModel
    {
        public int AccountId { get; set; }
        public int CustomerId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int AccountNumber { get; set; }
        public int Balance { get; set; }
        public string accountsUrl
        {
            //Ask Cameron why this is here and what it does exactly...
            get
            {  
                return "api/accounts/"+ AccountId;
            }
        }
    }

}