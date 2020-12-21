using Microsoft.AspNet.Identity;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using SendGrid;

namespace REST.Services
{
    public class EmailService : IIdentityMessageService
    {


        public async Task SendAsync(IdentityMessage message)
        {
            await configSendGridasync(message);
        }

        // Use NuGet to install SendGrid (Basic C# client lib) 
        private async Task configSendGridasync(IdentityMessage message)
        {
            var myMessage = new SendGridMessage();

            myMessage.AddTo(message.Destination);
            myMessage.From = new EmailAddress(ConfigurationManager.AppSettings["emailFrom"], "PickUp");
            myMessage.Subject = message.Subject;
            myMessage.PlainTextContent = message.Body;
            myMessage.HtmlContent = message.Body;

            // Create a Web transport for sending email.

            var transportWeb = new SendGridClient(ConfigurationManager.AppSettings["apiKey"]);

            // Send the email.
            if (transportWeb != null)
            {
                await transportWeb.SendEmailAsync(myMessage);
            }
            else
            {
                //Trace.TraceError("Failed to create Web transport.");
                await Task.FromResult(0);
            }
        }
    }
}