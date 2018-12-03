using eStore.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using DataAccess;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using BusinessLogic.Helpers;

namespace eStore.Controllers
{
    public class BaseController : Controller
    {

        private ApplicationUserManager _userManager;
        private eStoreDB db = new eStoreDB();
        private eStoreEntities db1 = new eStoreEntities();

        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {
            var user = (GetUser)Session["User"];
            try
            {
                if (user != null)
                {
                    var loguseractivity = new LOG_USERACTIVITY()
                    {
                        EntryDate = DateTime.Now,
                        HttpMethod = Request.HttpMethod,
                        PerdoruesiID = user.ID,
                        Activity = Request.Path
                    };

                    db1.LOG_USERACTIVITY.Add(loguseractivity);
                    db1.SaveChanges();

                }
            }
            catch
            {
            }
            return base.BeginExecuteCore(callback, state);
        }


        public async Task<GetUser> GetUser()
        {
            PERDORUESI user = new PERDORUESI();
            eStore.Models.GetUser usertotal = new Models.GetUser();
            if (Session["User"] == null)
            {
                if (User.Identity.IsAuthenticated)
                {
                    try
                    {
                        var userfound = await UserManager.FindByEmailAsync(User.Identity.Name);
                        if (userfound == null)
                            userfound = await UserManager.FindByNameAsync(User.Identity.Name);

                        user = db.PERDORUESIs.Single(q => q.UserID == userfound.Id);

                        usertotal.ID = user.ID;
                        usertotal.UserID = user.UserID;
                        usertotal.Emri = user.Emri;
                        usertotal.Mbiemri = user.Mbiemri;
                        usertotal.Ditelindja = user.Datelindja;
                        usertotal.Adresa = user.Adresa;
                        usertotal.Telefoni = user.Telefoni;



                        Session["User"] = usertotal;
                    }
                    catch
                    {
                        usertotal = null;
                    }
                }
            }
            else
                usertotal = (GetUser)Session["User"];

            Session["Roli"] = usertotal.Roles;
            return usertotal;
        }



        public async Task<string> GjeneroNumrinProduktit()
        {
            var numri = await GetNumber(null, (int)Enums.NumratRendorLloji.Produkt);

            return numri + "/" + DateTime.Now.Year;
        }


        private async Task<int> GetNumber(int? depoid, int type)
        {
            GetUser user = await GetUser();
            var model = new NUMRAT();

            model = await db1.NUMRATs.Where(q => q.Tipi == type).FirstOrDefaultAsync();

            try
            {
                if (model == null)
                {
                    var numriaktual = 1;

                    model = new NUMRAT();
                    model.Numri = numriaktual;
                    model.Tipi = type;
                    db1.NUMRATs.Add(model);
                }
                else
                {
                    model.Numri = model.Numri + 1;
                    db1.Entry(model).State = EntityState.Modified;
                }

                await db1.SaveChangesAsync();
            }
            catch (Exception ex)
            {

            }

            return model.Numri;
        }



        public void Success(string message, bool dismissable = false)
        {
            AddAlert(AlertStyles.Success, message, dismissable);
        }

        public void Information(string message, bool dismissable = false)
        {
            AddAlert(AlertStyles.Information, message, dismissable);
        }

        public void Warning(string message, bool dismissable = false)
        {
            AddAlert(AlertStyles.Warning, message, dismissable);
        }

        public void Danger(string message, bool dismissable = false)
        {
            AddAlert(AlertStyles.Danger, message, dismissable);
        }

        private void AddAlert(string alertStyle, string message, bool dismissable)
        {
            var alerts = TempData.ContainsKey(Alert.TempDataKey)
                ? (List<Alert>)TempData[Alert.TempDataKey]
                : new List<Alert>();

            alerts.Add(new Alert
            {
                AlertStyle = alertStyle,
                Message = message,
                Dismissable = dismissable
            });

            TempData[Alert.TempDataKey] = alerts;
        }

        public async Task<SelectList> LoadSysMenuParent(int? selected)
        {
            var user = await GetUser();

            var allvalues = await db.SYSMENU_PARENT.ToListAsync();

            if (selected.HasValue)
                return new SelectList(allvalues, "ID", "Pershkrimi", selected.Value);
            else
                return new SelectList(allvalues, "ID", "Pershkrimi");
        }

        public async Task<SelectList> LoadKategoria(int? selected)
        {
            var user = await GetUser();

            var allvalues = await db.KATEGORIAs.ToListAsync();

            if (selected.HasValue)
                return new SelectList(allvalues, "ID", "Emertimi", selected.Value);
            else
                return new SelectList(allvalues, "ID", "Emertimi");
        }

        public async Task<SelectList> LoadProdhuesit(int? selected)
        {
            var user = await GetUser();

            var allvalues = await db.PRODHUESIs.ToListAsync();

            if (selected.HasValue)
                return new SelectList(allvalues, "ID", "Prodhuesi1", selected.Value);
            else
                return new SelectList(allvalues, "ID", "Prodhuesi1");
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
    }
}