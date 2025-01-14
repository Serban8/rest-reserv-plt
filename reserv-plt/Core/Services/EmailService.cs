using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Core.Services
{
    public class EmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendReservationEmailAsync(
            string recipientEmail,
            string recipientName,
            Guid reservationId)
        {
            // Setup SMTP settings
  
            var smtpServer = _config["EmailSettings:SmtpServer"];
            var smtpPort = int.Parse(_config["EmailSettings:SmtpPort"]);
            var enableSsl = bool.Parse(_config["EmailSettings:EnableSsl"]);
            var username = _config["EmailSettings:Username"];
            var password = _config["EmailSettings:Password"];
            var fromEmail = _config["EmailSettings:FromEmail"];

            var emailTemplate = _config["EmailSettings:ReservationEmailTemplate"];

       
            var confirmUrl = $"http://localhost:5111/api/Admin/confirm-reservation?reservationId={reservationId}";

           
            var body = string.Format(emailTemplate, recipientName, confirmUrl);

            try
            {
                using (var client = new SmtpClient(smtpServer, smtpPort)) 
                {
                    client.EnableSsl = true;
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential(username, password);

                    var mail = new MailMessage
                    {
                        From = new MailAddress(fromEmail),
                        Subject = "Your Reservation",
                        Body = emailTemplate,
                    };

                    mail.To.Add(recipientEmail);

                    await client.SendMailAsync(mail);
                }
            }
            catch (SmtpException smtpEx)
            {
                Console.WriteLine($"SMTP Error: {smtpEx.Message}");
                Console.WriteLine($"Status Code: {smtpEx.StatusCode}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"General Error: {ex.Message}");
            }
        }
    }
}
