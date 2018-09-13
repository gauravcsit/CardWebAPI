using CardWebAPI.DAL;
using CardWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CardWebAPI.Controllers
{
    public class CardValidationController : ApiController
    {
        CardDBManager dbmanager = new CardDBManager();

        // POST api/CardValidation/CheckCard
        [AcceptVerbs("GET", "POST")]
        public RequestResponse CheckCard(string cardNumber, string expiryDate)
        {
            return dbmanager.CheckCardValid(cardNumber, expiryDate);
        }

        [AcceptVerbs("GET", "POST")]
        public List<LogTransaction> GetTransactionLog()
        {
            return dbmanager.GetTransactionLog();
        }

    }
}
