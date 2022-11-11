using System.Net;
using System.Net.Mail;

namespace PBL5BE.API.Services
{
    public class SendMail
    {
        public static async Task<bool> SendVerificationMail(string _to, string _code)
        {

            String _from = "duongtruongvu21@gmail.com";
            string _disName = "Vũ Simp Lỏd";
            String _subject = "<AutoMail> Hi, xin lỗi nếu bị spam ❤️!";
            String _body =
                "<div style=\"margin: 20px;\">" +
                    "<a style = \"background-color: #a173e8; padding: 20px;" +
                    " color: white; text-decoration:none; border-radius:10px; " +
                    " font-family:sans-serif\" href = \"https://www.facebook.com/\" > " +
                    "Xác thực tài khoản </ a ></ div >";

            try
            {
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com", 587);
                SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
                MailMessage email = new MailMessage();

                // START
                email.From = new MailAddress(_from, _disName);
                email.To.Add(_to);
                email.Subject = _subject;
                email.IsBodyHtml = true;
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