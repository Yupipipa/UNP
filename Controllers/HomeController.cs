using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using UNP.Mail;
using UNP.Models;

namespace UNP.Controllers
{
    public class HomeController : Controller
    {
        UserContext db = new UserContext();
        private EmailService emailService = new EmailService();

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CheckUnps(string email, List<string> unps)
        {
            List<User> results = new List<User>();

            foreach (string unp in unps)
            {
                bool localExists = CheckLocal(unp);
                bool portalExists = CheckPortal(unp);

                var newUser = new User
                {
                    UNP = unp,
                    Email = email,
                    LocalStatus = "Есть",
                    PortalStatus = portalExists ? "Есть" : "Нет"
                };

                results.Add(newUser);

                if (!localExists)
                {
                    db.Users.Add(newUser);
                    db.SaveChanges();
                }

                emailService.SendMessage(unp, email);
            }

            return Json(results);
        }

        public bool CheckLocal(string unp)
        {
            User user = db.Users.FirstOrDefault(x => x.UNP == unp);
            return user != null;
        }

        public bool CheckPortal(string unp)
        {
            var request = new GetRequest($"http://www.portal.nalog.gov.by/grp/getData?unp={unp}&charset=UTF-8&type=json");
            request.Run();

            return request.Response != null;
        }
    }
}