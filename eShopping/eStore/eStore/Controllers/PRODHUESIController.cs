using DataAccess;
using eStore.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;

namespace eStore.Controllers
{
    public class PRODHUESIController : BaseController
    {
        eStoreDB db = new eStoreDB();
        // GET: PRODHUESI
        public async Task<ActionResult> Index()
        {
            var user = await GetUser();
            List<PRODHUESI> lista = await db.PRODHUESIs.OrderByDescending(p => p.ID).ToListAsync();
           
            return View(lista);
        }
         
        // GET: PRODHUESI/Create
        public async Task<ActionResult> Create()
        {

            var user = await GetUser();
            PRODHUESI model = new PRODHUESI();
 
            return View(model);
        }

        // POST: PRODHUESI/Create
        [HttpPost]
        public async Task<ActionResult> Create(PRODHUESI modeli)
        {

            if (ModelState.IsValid)
            {
                var user = await GetUser();
                try
                {
                    PRODHUESI model = new PRODHUESI();
                    model.ID = modeli.ID;
                    model.Prodhuesi1 = modeli.Prodhuesi1;
                    model.Shteti = modeli.Shteti;
                    db.PRODHUESIs.Add(model);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();
                }

            }
            return View(modeli);
        }

        // GET: PRODHUESI/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var user = await GetUser(); 
            PRODHUESI modeli = await db.PRODHUESIs.FindAsync(id); 
            return View(modeli);
        }

        // POST: PRODHUESI/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(PRODHUESI model)
        {
            var user = await GetUser();
            if (ModelState.IsValid)
            {
                try
                {
                    PRODHUESI model1 = await db.PRODHUESIs.FindAsync(model.ID);
                    model1.Prodhuesi1 = model.Prodhuesi1;
                    model1.Shteti = model.Shteti; 
                    db.Entry(model1).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    Success("Prodhuesi është modifikuar me sukses!", true);
                    return RedirectToAction("Index");
                }
                catch
                {
                    Danger("Ka ndodhur një gabim!", true);
                    return View("Index");
                }
            }
            return View(model);
        }

        // GET: PRODHUESI/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PRODHUESI/Delete/5
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
