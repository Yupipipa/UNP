using MailKit.Net.Smtp;
using MimeKit;
using UNP.Controllers;

namespace UNP.Mail
{
    public class EmailService
    {
        public void SendEmail(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(MailboxAddress.Parse("aliash99@mail.ru"));
            emailMessage.To.Add(MailboxAddress.Parse(email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            using (var client = new SmtpClient())
            {
                client.ConnectAsync("smtp.mail.ru", 465, true);
                client.AuthenticateAsync("aliash99@mail.ru", "xYFbLc2ZTWi5BbT7XUJC");
                client.SendAsync(emailMessage);

                client.DisconnectAsync(true);
            }
        }

        public void SendMessage(string unp, string email)
        {
            HomeController home = new HomeController();

            string registredDB = home.CheckLocal(unp) ? "зарегистрирован" : "не зарегистрирован";
            string registredPortal = home.CheckPortal(unp) ? "зарегистрирован" : "не зарегистрирован";

            var info = $"Ваш УНП {registredDB} в локальной базе данных и {registredPortal} в Государственном реестре платильщиков";

            SendEmail(email, "Информирование о наличии Вашего УНП в базах данных", info);
        }
    }
}