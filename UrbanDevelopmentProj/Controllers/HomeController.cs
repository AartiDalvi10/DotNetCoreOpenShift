using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UrbanDevelopmentProj.Models;
using System.IO;
using SendGrid;
using SendGrid.Helpers.Mail;
using Newtonsoft.Json;
using System.Text;
using locationsFind.Models;

namespace UrbanDevelopmentProj.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            //string apiUrl = "http://localhost:8083/areaDetail/1";

            //Uri address = new Uri(apiUrl);

            //// Create the web request
            //HttpWebRequest request = WebRequest.Create(address) as HttpWebRequest;

            //// Set type to POST
            //request.Method = "GET";
            //request.ContentType = "application/json";
            //// request.charset = "UTF - 8";

            ////using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            ////{
            ////    // Get the response stream
            ////    StreamReader reader = new StreamReader(response.GetResponseStream());

            ////    // Console application output
            ////    ViewData["Message"] = reader.ReadToEnd();
            ////}

            //SendMail();


            ViewBag.BackgroudImage = "/Images/Background.jpg";

            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";
            
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public void SendMail()
        {
           // var isEmailSent = false;
            try
            {
                var email = new EmailDTO
                {
                    Subject = "Email Subject : Hello World from the SendGrid",
                    From = "azure_dfec7e7ba12b14ef8b09bb2dcb1e3d87@azure.com",
                    FromName = "Cloud Team",
                    To = "dalviaarti10@gmail.com",
                    ToName = "Aarti",
                    HtmlContent = "<strong>Hello, Email!</strong>",
                    PlainTextContent = "Hi....This is email body"
                };
                SendEmailAsync(email);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private async System.Threading.Tasks.Task<bool> SendEmailAsync(EmailDTO emailDetails)
        {
            try
            {

                var apiKey = "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";
                var client = new SendGridClient(apiKey);
                var msg = new SendGrid.Helpers.Mail.SendGridMessage()
                {
                    From = new EmailAddress(emailDetails.From, emailDetails.FromName),
                    Subject = emailDetails.Subject,
                    PlainTextContent = emailDetails.PlainTextContent,
                    HtmlContent = emailDetails.HtmlContent,

                };
                msg.AddTo(new EmailAddress(emailDetails.To, emailDetails.ToName));

                //var bytes = System.IO.File.ReadAllBytes("C:/Attachment.txt");
                //var file = Convert.ToBase64String(bytes);
                //msg.AddAttachment("file.txt", file);

                var response = await client.SendEmailAsync(msg);
                return true;

            }
            catch (Exception ex)
            {
                //logger.LogError(ex);  
                return false;
            }
        }

        [AcceptVerbs("Post")]
        public ActionResult getschoolincity(string json)
        {
            try
            {
                //var result = @"{'searchObjectType':'School','location':'Airoli','locationLat':'19.159014','locationLng':'72.9985686','searchObjectDetail':[{'name':'Nilkanth Patil Vidyalaya','address':'Plot no 58, 59, Shani Mandir Road, Sector 20C, Airoli','lat':'19.1624849','lng':'72.9905583'},{'name':'Manish Chandarana','address':'Mugalsan Road, Sector 20C, Sector-19, Airoli, Navi Mumbai','lat':'19.16071699999999','lng':'72.98941099999999'},{'name':'Little Scholars','address':'Shop No.1 , Plot No - C-80 , Ekuiraa Apartment, Shani Mandir Road, Sector 20, Airoli, Navi Mumbai','lat':'19.162749','lng':'72.990694'},{'name':'Funschool Play School & Nursery','address':'2, Sri Sadguru Vamanrao Pai Marg, Sector-19, Airoli, Navi Mumbai','lat':'19.160959','lng':'72.989847'},{'name':'TILLI TONG','address':'Ashapura Park, Shho no. 8, near Andhra Bank, CHS Road, Sector-19, Airoli, Navi Mumbai','lat':'19.1589599','lng':'72.9913294'},{'name':'Little Millennium - Preschool & Daycare, Airoli, Sector 19 - Navi Mumbai','address':'Plot No. 44, Laxman Plaza, Sector-19, Airoli, Navi Mumbai','lat':'19.1597117','lng':'72.9911058'},{'name':'Mindseed Preschool & Daycare','address':'Shop No. 9/10,11, Plot No. 36, Narmada Co-op Housing, Sector19, Airoli, Navi Mumbai','lat':'19.1605827','lng':'72.99057379999999'},{'name':'Aai The Day Care Center','address':'Shop No.2, Whispering Palm, Plot No.49, Sector-19, Airoli, Navi Mumbai','lat':'19.159455','lng':'72.991052'}],'suggestion':['Need More Schools ','Survey to check kids exempted from education','More nursery play group is required']}";
                PostParameters data =JsonConvert.DeserializeObject<PostParameters>(json);
                string city = data.city;
                string searchType = data.searchType;

                shoolDetails schoolData = new shoolDetails();
                string apiUrl = "http://localhost:8083/getLocationAndSuggestionDetail/?location=" + city + "&searchObject=" + searchType + "";
                Uri address = new Uri(apiUrl);

                // Create the web request
                HttpWebRequest request = WebRequest.Create(address) as HttpWebRequest;

                // Set type to POST
                request.Method = "GET";
                request.ContentType = "application/json";
                // request.charset = "UTF - 8";

                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    // Get the response stream
                    StreamReader reader = new StreamReader(response.GetResponseStream());

                    // Console application output
                    var result = reader.ReadToEnd();
                    schoolData = JsonConvert.DeserializeObject<shoolDetails>(result);
                }

                var lstSchooList = schoolData.searchObjectDetail;
                var lstsuggestions = schoolData.suggestion;

                StringBuilder citycordinates = new StringBuilder();
                citycordinates.Append(schoolData.locationLat);
                citycordinates.Append(',');
                citycordinates.Append(schoolData.locationLng);

                var jsclobj = JsonConvert.SerializeObject(lstSchooList);
                var jscitycordinates = JsonConvert.SerializeObject(citycordinates.ToString());
                var jssuggestion = JsonConvert.SerializeObject(lstsuggestions);

                var returnvalue = new
                {

                    citylonglat = jscitycordinates,
                    schooldetails = jsclobj,
                    suggestions= jssuggestion
                };

                return Json(returnvalue);
            }
            catch (Exception ex)
            {
                throw ex;
                //return Json(null);
            }
        }

       
    }
    public class EmailDTO
    {
        public string Subject { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string FromName { get; set; }
        public string ToName { get; set; }
        public string HtmlContent { get; set; }
        public string PlainTextContent { get; set; }

    }
    public class PostParameters
    {
        public string city { get; set; }
        public string searchType { get; set; }
    }
}

