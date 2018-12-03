using eStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using DataAccess;
using System.Data.Entity;

namespace eStore.Controllers
{
    public class KATEGORIAController : BaseController
    {
        eStoreDB db = new eStoreDB();
         
        // GET: KATEGORIA
        public async Task<ActionResult> Index()
        {
            var user = await GetUser();
            List<KATEGORIA> lista = await db.KATEGORIAs.OrderByDescending(p => p.ID).ToListAsync(); 
            ViewBag.PrindiID = await LoadKategoria(null);
            return View(lista);
        } 

        // GET: KATEGORIA/Create
        public async Task<ActionResult> Create()
        {
            var user = await GetUser();
            KATEGORIA model = new KATEGORIA();
             ViewBag.KategoriaPrindID = await LoadKategoria(null);
            return View(model);
           
        }

        // POST: KATEGORIA/Create
        [HttpPost]
        public async Task<ActionResult> Create(KATEGORIA modeli)
        {
            if (ModelState.IsValid)
            {
                var user = await GetUser();
                try
                {
                    KATEGORIA model = new KATEGORIA();
                    model.ID = modeli.ID;
                    model.Pershkrimi = modeli.Pershkrimi;
                    model.Emertimi = modeli.Emertimi;
                    model.KategoriaPrindID = modeli.KategoriaPrindID; 
                    model.Aktiv = modeli.Aktiv;
                    db.KATEGORIAs.Add(model);
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

        // GET: KATEGORIA/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var user = await GetUser();
        
            KATEGORIA modeli = await db.KATEGORIAs.FindAsync(id);
            
            
            ViewBag.PrindiID = await LoadKategoria(modeli.KategoriaPrindID);
            return View(modeli);
            
        }

        // POST: KATEGORIA/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(KATEGORIA model)
        {
            var user = await GetUser();
            if (ModelState.IsValid)
            {
                try
                {
                    KATEGORIA model1 = await db.KATEGORIAs.FindAsync(model.ID);
                    model1.Emertimi = model.Emertimi;
                    model1.Pershkrimi = model.Pershkrimi;
                    
                    model1.KategoriaPrindID = model.KategoriaPrindID;
                    model1.Aktiv =model.Aktiv;
                   
                    db.Entry(model1).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    Success("Kategoria është modifikuar me sukses!", true);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    Danger("Ka ndodhur një gabim!", true);
                    return View("Index");
                }
            }
            return View(model);
        }

        // GET: KATEGORIA/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: KATEGORIA/Delete/5
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
