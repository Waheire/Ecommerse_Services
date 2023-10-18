using MailKit.Net.Smtp;
using Microsoft.Azure.Amqp.Framing;
using MimeKit;
using TheJitu_Commerce_Email.Model.Dtos;

namespace TheJitu_Commerce_Email.Services
{
    public class EmailSendService
    {
        public async Task SendEmail(UserMessageDto res, string message) 
        {
            {
                MimeMessage message1 = new MimeMessage();
                message1.From.Add(new MailboxAddress("The Jitu E-Commerce ", "waheirearthurwanjohi@gmail.com"));

                // Set the recipient's email address
                message1.To.Add(new MailboxAddress(res.Name, res.Email));

                message1.Subject = "Welcome to TheJitu Shopping Site";

                var body = new TextPart("html")
                {
                    Text = message.ToString()
                };
                message1.Body = body;

                var client = new SmtpClient();

                client.Connect("smtp.gmail.com", 587, false);

                client.Authenticate("waheirearthurwanjohi@gmail.com", "maja mujo ynvy qtqq");

                await client.SendAsync(message1);

                await client.DisconnectAsync(true);
            }
        }
    }
}
