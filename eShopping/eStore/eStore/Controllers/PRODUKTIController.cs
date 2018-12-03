using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLogic.Helpers;
using DataAccess;
using eStore.Models;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Net;
using BusinessLogic;

namespace eStore.Controllers
{
    public class PRODUKTIController : BaseController
    {
        eStoreDB db = new eStoreDB();

        public PRODUKTIController()
        {
            ViewBag.AlertModel = new Alert() { AlertStyle = "", Dismissable = true, Message = "" };
        }
        // GET: PRODUKTI
        public async Task<ActionResult> Index()
        {
            var user = await GetUser();
            List<PRODUKTI> produktet = await db.PRODUKTIs.Where(q=>q.CreatedBy==user.ID).OrderByDescending(q => q.ID).ToListAsync();
            return View(produktet);
        }

        // GET: PRODUKTI/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PRODUKTI/Create
        public async Task<ActionResult> Create()
        {
            var user = await GetUser();
            var artikullidraft = await db.PRODUKTIs.Where(q => q.CreatedBy == user.ID && q.Statusi == (int)Enums.ProduktiStatus.Draft).FirstOrDefaultAsync();
            PRODUKTI produkti = new PRODUKTI();

            if (artikullidraft != null)
            {
                produkti.ID = artikullidraft.ID;
                produkti.Pershkrimi = artikullidraft.Pershkrimi;
                produkti.Emertimi = artikullidraft.Emertimi;
                produkti.Sasia = artikullidraft.Sasia;
                produkti.Kodi = artikullidraft.Kodi;
                produkti.CmimiBaze = artikullidraft.CmimiBaze;
                produkti.ProdhuesiID = artikullidraft.ProdhuesiID;
                produkti.TVSH = artikullidraft.TVSH;
                produkti.Zbritja = artikullidraft.Zbritja;
                ViewBag.ProdhuesiID = await LoadProdhuesit(artikullidraft.ProdhuesiID);
            }
            else
            {
                int nr;
                var numri = await db.NUMRATs.Where(q =>q.Tipi == (int)Enums.NumratRendorLloji.Produkt).FirstOrDefaultAsync();
                if (numri == null)
                {
                    nr = 1;
                }
                else
                {
                    nr = numri.Numri + 1;
                }

                produkti.ID = 0;
                produkti.Kodi = nr + "/" + DateTime.Now.Year;
                ViewBag.ProdhuesiID = await LoadProdhuesit(null);
            }

            return View(produkti);
        }

        // POST: PRODUKTI/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateAjax]
        public async Task<ActionResult> Create(PRODUKTI produkti)
        {
            if (ModelState.IsValid)
            {
                MessageJS returnmodel = new MessageJS();

                var user = await GetUser();
                try
                {
                    PRODUKTI model = new PRODUKTI();
                    if (produkti.ID != 0)
                        model = await db.PRODUKTIs.FindAsync(produkti.ID);

                    model.ID = produkti.ID;
                    model.Emertimi = produkti.Emertimi;
                    model.Pershkrimi = produkti.Pershkrimi;
                    model.CmimiBaze = produkti.CmimiBaze;
                    model.TVSH = produkti.TVSH;
                    model.Zbritja = produkti.Zbritja;
                    model.ProdhuesiID = produkti.ProdhuesiID;
                    model.Sasia = produkti.Sasia;
                    model.Statusi = (int)Enums.ProduktiStatus.Draft;

                    if (produkti.ID == 0)
                    {
                        model.Kodi = await GjeneroNumrinProduktit();
                        model.CreatedBy = user.ID;
                        model.Created = DateTime.Now;

                        db.PRODUKTIs.Add(model);
                        await db.SaveChangesAsync();
                    }
                    else
                    {
                        db.Entry(model).State = EntityState.Modified;
                        await db.SaveChangesAsync();
                    }

                    returnmodel.ID = model.ID;
                }
                catch (Exception ex)
                {
                    return Json("Gabim " + ex.Message, JsonRequestBehavior.DenyGet);
                }

                returnmodel.Status = true;
                returnmodel.Message = "Produkti u ruajt me sukses";
                return Json(returnmodel, JsonRequestBehavior.AllowGet);
            }
            return Json("Gabim", JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> LoadKategoria(SearchModel model)
        {
            var kategorite = db.PRODUKTI_IN_KATEGORIA.Where(q => q.ProduktiID == model.id).Select(q => q.KategoriaID);
            List<KATEGORIA> returnmodel = await db.KATEGORIAs.Where(q=> !kategorite.Contains(q.ID) && q.Aktiv==true).OrderByDescending(q => q.ID).ToListAsync();
            return View(returnmodel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddKategori(PRODUKTI_IN_KATEGORIA model)
        {
            MessageJS returnmodel = new MessageJS();
            var user = await GetUser();
            if (ModelState.IsValid)
            {
                try
                {


                    PRODUKTI_IN_KATEGORIA addmodel = new PRODUKTI_IN_KATEGORIA();
                    addmodel.ID = model.ID;
                    addmodel.ProduktiID = model.ProduktiID;
                    addmodel.KategoriaID = model.KategoriaID;
                    db.PRODUKTI_IN_KATEGORIA.Add(addmodel);
                    await db.SaveChangesAsync();

                    returnmodel.Status = true;
                    returnmodel.Message = "Produkti u shtua ne kategori!";
                    return Json(returnmodel, JsonRequestBehavior.AllowGet);

                }
                catch (Exception ex)
                {
                    returnmodel.Status = false;
                    returnmodel.Message = "Ka ndodhur një gabim";
                }
            }

            return Json(returnmodel, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> LoadAddedKategorite(PRODUKTI_IN_KATEGORIA model)
        {
            var added = await db.PRODUKTI_IN_KATEGORIA.Where(q => q.ProduktiID == model.ProduktiID).OrderByDescending(q => q.ID).ToListAsync();

            return View(added);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteChosenKategoria(PRODUKTI_IN_KATEGORIA model)
        {
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteChosenKategoria_POST(PRODUKTI_IN_KATEGORIA model)
        {
            MessageJS returnmodel = new MessageJS();
            var kategorianeprodukt = await db.PRODUKTI_IN_KATEGORIA.FindAsync(model.KategoriaID);
            try
            {
                db.PRODUKTI_IN_KATEGORIA.Remove(kategorianeprodukt);
                await db.SaveChangesAsync();
                returnmodel.Status = true;
                returnmodel.Message = "Kategoria u fshi nga produkti";
                returnmodel.ID = model.KategoriaID;
            }
            catch (Exception)
            {
                returnmodel.Status = false;
                returnmodel.Message = "Ka ndodhur nje gabim";
            }
            return Json(returnmodel, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> LoadMediat(SearchModel model)
        {
   

            var mediat = db.PRODUKTI_IN_MEDIA.Where(q => q.ProduktiID == model.id).Select(q => q.MediaID);
            List<MEDIAT> returnmodel = await db.MEDIATs.Where(q =>!mediat.Contains(q.ID)).OrderByDescending(q => q.ID).ToListAsync();
            return View(returnmodel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddMedia(PRODUKTI_IN_MEDIA model)
        {
            MessageJS returnmodel = new MessageJS();
            var user = await GetUser();
            if (ModelState.IsValid)
            {
                try
                {


                    PRODUKTI_IN_MEDIA addmodel = new PRODUKTI_IN_MEDIA();
                    addmodel.ID = model.ID;
                    addmodel.ProduktiID = model.ProduktiID;
                    addmodel.MediaID = model.MediaID;
                    db.PRODUKTI_IN_MEDIA.Add(addmodel);
                    await db.SaveChangesAsync();

                    returnmodel.Status = true;
                    returnmodel.Message = "Produktit iu shtua nje fotografi!";
                    return Json(returnmodel, JsonRequestBehavior.AllowGet);

                }
                catch (Exception ex)
                {
                    returnmodel.Status = false;
                    returnmodel.Message = "Ka ndodhur një gabim";
                }
            }

            return Json(returnmodel, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> LoadAddedMedia(PRODUKTI_IN_MEDIA model)
        {
            var added = await db.PRODUKTI_IN_MEDIA.Where(q => q.ProduktiID == model.ProduktiID).OrderByDescending(q => q.ID).ToListAsync();

            return View(added);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteChosenMedia(PRODUKTI_IN_MEDIA model)
        {
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteChosenMedia_POST(PRODUKTI_IN_MEDIA model)
        {
            MessageJS returnmodel = new MessageJS();
            var medianeprodukt = await db.PRODUKTI_IN_MEDIA.FindAsync(model.MediaID);
            try
            {
                db.PRODUKTI_IN_MEDIA.Remove(medianeprodukt);
                await db.SaveChangesAsync();
                returnmodel.Status = true;
                returnmodel.Message = "Media u fshi nga produkti";
                returnmodel.ID = model.MediaID;
            }
            catch (Exception)
            {
                returnmodel.Status = false;
                returnmodel.Message = "Ka ndodhur nje gabim";
            }
            return Json(returnmodel, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<bool> Perfundo([Bind(Include = "ID,Emertimi,Pershkrimi,Kodi,Sasia,CmimiBaze,TVSH,Zbritja,ProdhuesiID")] PRODUKTI model)
        {
            var produkti = await db.PRODUKTIs.FindAsync(model.ID);
            try
            {
                produkti.Statusi = (int)Enums.ProduktiStatus.Aktive;
                produkti.Perfunduar = true;
                db.Entry(produkti).State = EntityState.Modified;
                await db.SaveChangesAsync();
                
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }


        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PRODUKTI produkti = await db.PRODUKTIs.FindAsync(id);
            var user = await GetUser();
            if (produkti.CreatedBy != user.ID)
            {

                return RedirectToAction("Index");
            }
            if (produkti == null)
            {
                return HttpNotFound();
            }

            ViewBag.ProdhuesiID = await LoadProdhuesit(produkti.ProdhuesiID);
            return View(produkti);
        }

       

        // POST: PRODUKTI/Edit/5
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

        // GET: PRODUKTI/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PRODUKTI/Delete/5
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
