using CardWebAPI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CardMVC
{
    public class ConsumeEventSync
    {
        public RequestResponse GetRequestResponseByUrl(string url) //Get All Events Records  
        {
            using (var client = new WebClient()) //WebClient  
            {
                client.Headers.Add("Content-Type:application/json"); //Content-Type  
                client.Headers.Add("Accept:application/json");

                var result = client.DownloadString(url); //URI  

                var response = JsonConvert.DeserializeObject<RequestResponse>(result);

                return response;
            }
        }

        internal RequestResponse CheckCard(string cardNumber, string expiryDate)
        {
            string url = "http://localhost:58114/api/CardValidation/CheckCard/" + cardNumber + "," + expiryDate; //URI 
            return GetRequestResponseByUrl(url);
        }

    }
}
