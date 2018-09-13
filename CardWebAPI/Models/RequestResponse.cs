using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CardWebAPI.Models
{
    public class RequestResponse
    {
        private string result;
        private string cardType;
        private string expiryDate;
        private string cardNumber;

        public string Result
        {
            get { return result; }
            set { result = value; }
        }

        public string CardType
        {
            get { return cardType; }
            set { cardType = value; }
        }

        public string ExpiryDate
        {
            get { return expiryDate; }
            set { expiryDate = value; }
        }

        public string CardNumber
        {
            get { return cardNumber; }
            set { cardNumber = value; }
        }
    }
}