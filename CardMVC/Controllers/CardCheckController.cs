using CardWebAPI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace CardMVC.Controllers
{
    public class CardCheckController : Controller
    {
        // GET: CardCheck
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetRequestResponseByUrl(string url)
        {
            using (var client = new WebClient()) //WebClient  
            {
                client.Headers.Add("Content-Type:application/json"); //Content-Type  
                client.Headers.Add("Accept:application/json");

                var result = client.DownloadString(url); //URI  

                var response = JsonConvert.DeserializeObject<RequestResponse>(result);

                return Json(response, JsonRequestBehavior.AllowGet);
            }

            //return Json(ProvinceCityList, JsonRequestBehavior.AllowGet);
        }

        // GET: Home
        public ActionResult Logs()
        {
            ConsumeEventSync con = new ConsumeEventSync();
            string url = "http://localhost:58114/api/CardValidation/GetTransactionLog";

            using (var client = new WebClient()) //WebClient  
            {
                client.Headers.Add("Content-Type:application/json"); //Content-Type  
                client.Headers.Add("Accept:application/json");

                var result = client.DownloadString(url); //URI  

                var response = JsonConvert.DeserializeObject<List<LogTransaction>>(result);

                return View(response);
            }
        }
    }
}