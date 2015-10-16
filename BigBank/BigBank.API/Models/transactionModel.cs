using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BigBank.API.Models
{
    public class transactionModel
    {
        public int TransactionId { get; set; }
        public int AccountId { get; set; }
        public DateTime TransactionDate { get; set; }
        public decimal Amount { get; set; }
        public string transactionUrl
        {
            //Ask Cameron why this is here and what it does exactly...
            get
            {
                return "api/transactions" + TransactionId;
            }
        }
    }
}