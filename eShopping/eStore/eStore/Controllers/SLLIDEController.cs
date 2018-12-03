using BusinessLogic.Helpers;
using DataAccess;
using eStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using System.Data.Entity;
using BusinessLogic;
using System.Security.Cryptography;


namespace eStore.Controllers
{
    public class SLLIDEController : BaseController
    {
        eStoreDB db = new eStoreDB();
        public SLLIDEController()
        {
            ViewBag.AlertModel = new Alert() { AlertStyle = "", Dismissable = true, Message = "" };
        }
        // GET: SLLIDE
        public async  Task<ActionResult> Index()
        {
            var user = await GetUser();
            List<SLLIDE> lista = await db.SLLIDEs.OrderByDescending(p => p.ID).ToListAsync(); 
            return View(lista);
           
        } 
        // GET: SLLIDE/Create
        public async Task<ActionResult> Create()
        {
            var user = await GetUser();
            SLLIDE model = new SLLIDE();
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UploadFile()
        {
            MessageJS returnmodel = new MessageJS();

            if (Request.Files.Count > 0 && Request.Files[0] != null)
            {
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    var httpPostedFile = Request.Files[i];

                    string[] strSplit = httpPostedFile.FileName.Split('.');

                    string filetype = strSplit[strSplit.Length - 1].ToLower();

                    if (httpPostedFile.FileName != "" && (filetype == "jpg" || filetype == "jpeg" || filetype == "png"))
                    {
                        string srvPath = Server.MapPath("../Files/img");
                        string path = "/Files/img/" + httpPostedFile.FileName;

                        string savingpath = "";
                        try
                        {
                            using (MD5 md5Hash = MD5.Create())
                            {
                                string hash = HashMedia.GetMd5Hash(md5Hash, httpPostedFile.FileName + "" + DateTime.Now.ToString());

                                savingpath = srvPath + "/" + hash + "." + filetype;
                                httpPostedFile.SaveAs(savingpath);
                                savingpath = "/Files/img/" + hash + "." + filetype;

                                MEDIAT upload = new MEDIAT();
                                upload.Created = DateTime.Now;
                                upload.Emri = Guid.NewGuid();
                                Session["Emri"] = upload.Emri;
                                upload.Extensioni = (int)Enums.FileType.image;
                                upload.Pershkrimi = httpPostedFile.FileName;
                                //upload.Tipi = (int)FileType.image;
                                upload.Shtegu = savingpath;
                                Session["UploadedPicture"] = upload;
                                returnmodel.Status = true;
                                returnmodel.Message = "Imazhi u ngarkua me sukses!";
                            }
                        }
                        catch (Exception ex)
                        {
                            returnmodel.Status = false;
                            returnmodel.Message = "Ka ndodhur nje gabim!";
                            //throw (ex);
                            //return false;
                        }
                    }
                    else
                    {
                        returnmodel.Status = false;
                        returnmodel.Message = "File nuk eshte ne formatin e duhur!";
                    }
                }
            }
            return Json(returnmodel, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LoadFiles()
        {
            List<MEDIAT> uploadlist = new List<MEDIAT>();

            if (Session["UploadedPicture"] != null)
            {
                var item = (MEDIAT)Session["UploadedPicture"];

                var uploaditem = new MEDIAT();

                uploaditem.ID = item.ID;
                uploaditem.Created = item.Created;
                uploaditem.Emri = item.Emri;
                uploaditem.Extensioni = (int)(Enums.FileType)item.Extensioni;
                uploaditem.Shtegu = item.Shtegu;
                uploadlist.Add(uploaditem);
            }

            return View(uploadlist);
        }


        // POST: SLLIDE/Create
        [HttpPost]
        public async Task<ActionResult> Create(SLLIDE modeli)
        {
            if (ModelState.IsValid)
            {
                var user = await GetUser();
                try
                {
                    SLLIDE model = new SLLIDE();
                    model.ID = modeli.ID;
                    if (Session["UploadedPicture"] != null)
                    {
                        var item = (MEDIAT)Session["UploadedPicture"];
                        item.ID = modeli.ID;
                        item.Createdby = user.ID;
                        item.Created = DateTime.Now;
                        db.MEDIATs.Add(item);
                        await db.SaveChangesAsync();
                        model.MediaID = item.ID;
                    }
                    model.Linku = modeli.Linku;
                    model.OrderNr = modeli.OrderNr;
                    model.SllidePershkrimi = modeli.SllidePershkrimi;
                    model.SllideDataInsertimit = DateTime.Now;
                    model.SllideTitulli = modeli.SllideTitulli; 
                    db.SLLIDEs.Add(model);
                    await db.SaveChangesAsync();
                    Session["UploadedPicture"] = null;
                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();
                }

            }
            return View(modeli);
        }


        public async Task<ActionResult> DeleteSllide(int id)
        {
            SLLIDE slide = await db.SLLIDEs.FindAsync(id);
            DeleteSlideViewModel model = new DeleteSlideViewModel();
            model.id = slide.ID;
            return View(model);
        }
        [HttpPost]
 
        public async Task<ActionResult> DeleteChosenSlide_POST(DeleteSlideViewModel model)
        {
            MessageJS returnmodel = new MessageJS();
            var SlideID = await db.SLLIDEs.FindAsync(model.id);
            try
            {
                db.SLLIDEs.Remove(SlideID);
                await db.SaveChangesAsync();
                returnmodel.Status = true;
                returnmodel.Message = "Sllide u fshi nga kontrata";
                returnmodel.ID = model.id;
                return Json(new { result = "Redirect", url = Url.Action("Index", "SLIDE") });
            }
            catch (Exception)
            {
                returnmodel.Status = false;
                returnmodel.Message = "Ka ndodhur nje gabim";
            }
            return Json(new { result = "Redirect", url = Url.Action("Index", "SLIDE") });
        }

        // GET: SLLIDE/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            var user = await GetUser();

            SLLIDE modeli = await db.SLLIDEs.FindAsync(id);
            return View(modeli);
        }

        // POST: SLLIDE/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(SLLIDE model)
        {
            var user = await GetUser();
            if (ModelState.IsValid)
            {
                try
                {
                    SLLIDE model1 = await db.SLLIDEs.FindAsync(model.ID);
                    if (Session["UploadedPicture"] != null)
                    {
                        var item = (MEDIAT)Session["UploadedPicture"];
                        item.ID = model.ID;
                        item.Createdby = user.ID;
                        item.Created = DateTime.Now;
                        db.MEDIATs.Add(item);
                        await db.SaveChangesAsync();
                        model1.MediaID = item.ID;
                    }
                    model1.ID = model.ID;
                    model1.SllidePershkrimi = model.SllidePershkrimi; 
                    model1.SllideTitulli = model.SllideTitulli;
                    model1.SllideDataInsertimit = DateTime.Now;
                    model1.OrderNr = model.OrderNr;
                   
                    db.Entry(model1).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    Success("Slide është modifikuar me sukses!", true);
                    Session["UploadedPicture"] = null;
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

        // GET: SLLIDE/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SLLIDE/Delete/5
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
