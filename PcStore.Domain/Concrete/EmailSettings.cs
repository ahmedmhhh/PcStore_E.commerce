using PcStore.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PcStore.Domain.Entities;
using System.Net.Mail;
using System.Net;

namespace PcStore.Domain.Concrete
{
    public class EmailSettings
    {
        public string MailToAddress = "Your Email";
        public string MailFromAddress = "Your Email";
        public bool UseSsl = true;
        public string Username = "Your Email";
        public string Password = "Your Pass";
        public string ServerName = "smtp.gmail.com";
        public int ServerPost = 587;
        public bool WriteAsFile = false;
        public string FileLocation = @"E:\asp_email";
    }
    public class EmailOrderProcessor : IOrderProcessor
    {
        private EmailSettings emailSettings;
        public EmailOrderProcessor(EmailSettings settings)
        {
            emailSettings = settings;
        }
        public void ProcessorOrder(Cart cart, ShippingDetails shippingDetails)
        {
            using (var smtpClient = new SmtpClient())
            {
                smtpClient.EnableSsl = emailSettings.UseSsl;
                smtpClient.Host = emailSettings.ServerName;
                smtpClient.Port = emailSettings.ServerPost;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(emailSettings.Username,emailSettings.Password);
                if (emailSettings.WriteAsFile)
                {
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                    smtpClient.PickupDirectoryLocation = emailSettings.FileLocation;
                    smtpClient.EnableSsl = false;
                }
                StringBuilder body = new StringBuilder()
                    .AppendLine("A new order has been submitted")
                    .AppendLine("------------------------------")
                    .AppendLine("Products.");

                foreach (var item in cart.lines)
                {
                    var subtotal = item.products.Price * item.Quantity;
                    body.AppendFormat("{0} x {1} (subtotal: {2:c})", item.Quantity, item.products.Name, subtotal);
                }
                body.AppendFormat("Total order value: {0:c}", cart.ComputeTotalValue())
                    .AppendLine("\n------------------------------------------")
                    .AppendLine("Ship to:")
                    .AppendLine(shippingDetails.Name)
                    .AppendLine(shippingDetails.Line1)
                    .AppendLine(shippingDetails.Line2)
                    .AppendLine(shippingDetails.State)
                    .AppendLine(shippingDetails.City)
                    .AppendLine(shippingDetails.Country)
                    .AppendLine("------------------------")
                    .AppendFormat("Gift wrap: {0}", shippingDetails.GiftWrap ? "Yes" : "No");

                MailMessage mailMessage = new MailMessage(
                    emailSettings.MailFromAddress,
                    emailSettings.MailToAddress,
                    "New Order submitted",
                    body.ToString()
                    );
                if (emailSettings.WriteAsFile)
                    mailMessage.BodyEncoding = Encoding.ASCII;
                try {
                    smtpClient.Send(mailMessage);
                }
                catch(Exception ex)
                {

                }
            }
        }
    }
}
