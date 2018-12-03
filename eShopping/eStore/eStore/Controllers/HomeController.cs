using eStore.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace eStore.Controllers
{
   [Authorize]
    public class HomeController : BaseController
    {
        eStoreDB db = new eStoreDB();

      
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public async Task<ActionResult> GetUserInfo()
        {
            UserInfoModel model = new UserInfoModel();

            var user = await GetUser();
            model.Emri = user.Emri + " " + user.Mbiemri;
            model.ID = user.ID;

            return View(model);
        }

        public async Task<ActionResult> LoadNotifications()
        {
            List<NotificationsModel> list = new List<NotificationsModel>();

            var user = await GetUser();

            var notifications = await db.NJOFTIMETs.Where(q => q.ToUserID == user.ID).OrderByDescending(q => q.Data).Take(10).ToListAsync();
            foreach (var item in notifications)
            {
                list.Add(new NotificationsModel()
                {
                    ID = item.ID,
                    From = item.FromU,
                    Date = item.Data.ToString("dd/MM/yyyy HH:mm"),
                    Message = item.Message
                });
            }

            return View(list);
        }
    }
}