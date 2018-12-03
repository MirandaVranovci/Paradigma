using eStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using DataAccess;
namespace eStore.Controllers
{
    public class LOGSController : BaseController
    {
        eStoreDB db = new eStoreDB();
        // GET: LOGS
        public async Task<ActionResult> Index()
        {
            var user = await GetUser();
            return View(await db.LOGs.ToListAsync());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SearchUser(SearchModel model)
        {

            List<PERDORUESI> users = await db.PERDORUESIs.Where(q => (q.Emri.ToLower().Contains(model.value) || q.Mbiemri.ToLower().Contains(model.value))).ToListAsync();
            return View(users);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SelectedUser(SearchModel model)
        {
            var user = await db.PERDORUESIs.FindAsync(model.id);

            ViewBag.Logs = await db.LOGs.Where(q => q.Created_by == user.ID).OrderByDescending(q => q.Created_date).Take(300).ToListAsync();
            ViewBag.LogEntry = await db.LOG_ENTRY.Where(q => q.PerdoruesiID == user.ID).OrderByDescending(q => q.EntryDate).Take(300).ToListAsync();
            ViewBag.LogActivity = await db.LOG_USERACTIVITY.Where(q => q.PerdoruesiID == user.ID).OrderByDescending(q => q.EntryDate).Take(300).ToListAsync();
            return View(user);
        }

        // GET: LOGS/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: LOGS/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LOGS/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: LOGS/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: LOGS/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: LOGS/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: LOGS/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
