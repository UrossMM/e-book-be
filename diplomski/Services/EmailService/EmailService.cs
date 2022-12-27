using System.Net.Mail;

namespace diplomski.Services.EmailService
{
    public class EmailService : IEmailService
    {
        public void SendEMail(string from, string recipients, string subject, string body, string documentName, Byte[] res)
        {
            MemoryStream docStream = new MemoryStream(res);
            //Creates the email message
            MailMessage emailMessage = new MailMessage(from, recipients);
            //Adds the subject for email
            emailMessage.Subject = subject;
            //Sets the HTML string as email body
            emailMessage.IsBodyHtml = true;
            emailMessage.Body = body;
            //Add the file attachment to this e-mail message.
            emailMessage.Attachments.Add(new Attachment(docStream, documentName));
            //Sends the email with prepared message
            using (SmtpClient client = new SmtpClient())
            {
                //Update your SMTP Server address here
                client.Host = "smtp.outlook.com";
                client.UseDefaultCredentials = false;
                //Update your email credentials here
                client.Credentials = new System.Net.NetworkCredential(from, "Urosurosuros");
                client.Port = 587;
                client.EnableSsl = true;
                client.Send(emailMessage);
            }
        }
    }
}
