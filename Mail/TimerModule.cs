using System;
using System.Linq;
using System.Threading;
using System.Web;
using UNP.Models;

namespace UNP.Mail
{
    public class TimerModule : IHttpModule
    {
        UserContext db = new UserContext();

        private EmailService emailService = new EmailService();
        private static Timer timer;
        readonly long interval = (long)8.64e+7;
        static object synclock = new object();
        static bool sent = false;

        public void Init(HttpApplication app)
        {
            timer = new Timer(new TimerCallback(ConstantSendingEmail), null, 0, interval);
        }

        private void ConstantSendingEmail(object obj)
        {
            lock (synclock)
            {
                DateTime dd = DateTime.Now;
                if (dd.Hour == 7 && dd.Minute == 0 && sent == false)
                {
                    int totalUsers = db.Users.Count();
                    int skip = 0;
                    do
                    {
                        var model = db.Users.Skip(skip);

                        foreach (var user in model)
                        {
                            emailService.SendMessage(user.UNP, user.Email);
                        }

                        totalUsers -= 100;
                        skip += 100;
                        if (totalUsers > 0)
                            Thread.Sleep(300000);
                    }
                    while (totalUsers > 0);
                }
                else if (dd.Hour != 7 && dd.Minute != 0)
                {
                    sent = false;
                }
            }
        }
        public void Dispose()
        { }
    }
}