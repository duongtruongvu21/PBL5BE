using System.Net;
using System.Net.Mail;

namespace PBL5BE.API.OtherModulesce
{
    public class SendMail
    {
        public static async Task<bool> SendVerificationMail(string _to, string _code) 
        {
                        
            String _from = "duongtruongvu21@gmail.com";
            string _disName = "Vũ Simp Lỏd";
            String _subject = "<AutoMail> Hi, xin lỗi nếu bị spam ❤️!";
            String _body = $"<Code>: {_code}";

            try
            {
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com", 587);
                SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
                MailMessage email = new MailMessage();

                // START
                email.From = new MailAddress(_from, _disName);
                email.To.Add(_to);
                email.Subject = _subject;
                email.Body = _body;

                //END
                SmtpServer.Timeout = 5000;
                SmtpServer.EnableSsl = true;
                SmtpServer.UseDefaultCredentials = false;
                SmtpServer.Credentials = 
                    new NetworkCredential(_from, "gntdvhnhsfsnuyfe");

                await SmtpServer.SendMailAsync(email);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}