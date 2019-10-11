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

namespace UrbanDevelopmentProj.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            string apiUrl = "http://localhost:8083/areaDetail/1";

            Uri address = new Uri(apiUrl);

            // Create the web request
            HttpWebRequest request = WebRequest.Create(address) as HttpWebRequest;

            // Set type to POST
            request.Method = "GET";
            request.ContentType = "application/json";
            // request.charset = "UTF - 8";

            //using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            //{
            //    // Get the response stream
            //    StreamReader reader = new StreamReader(response.GetResponseStream());

            //    // Console application output
            //    ViewData["Message"] = reader.ReadToEnd();
            //}

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
            SendMail();
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
}

