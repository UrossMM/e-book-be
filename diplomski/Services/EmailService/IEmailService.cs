namespace diplomski.Services.EmailService
{
    public interface IEmailService
    {
        void SendEMail(string from, string recipients, string subject, string body, string documentName, Byte[] res);
    }
}
