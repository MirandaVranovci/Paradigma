using BusinessLogic.Helpers;
using eStore.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Threading.Tasks;
using DataAccess;
using System.Data.Entity;
using System.Security.Cryptography;
using BusinessLogic;
using System.Linq;

namespace eStore.Controllers
{
    public class MEDIAController : BaseController
    {
        eStoreDB db = new eStoreDB();

        public MEDIAController()
        {
            ViewBag.AlertModel = new Alert() { AlertStyle = "", Dismissable = true, Message = "" };
        }

        // GET: MEDIA
        public async Task<ActionResult> Index()
        {

            var user = await GetUser();
            List<MEDIAT> model = await db.MEDIATs.OrderByDescending(q => q.ID).Take(100).ToListAsync();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<bool> UploadFileAsync(MEDIAT model)
        {
            Session["UploadedPicture"] = null;
            var media = await db.MEDIATs.FindAsync(model.ID);
            // Get the uploaded image from the Files collection
            string savingpath = "";
            try
            {
                if (Request.Files.Count > 0 && Request.Files[0] != null)
                {
                    var httpPostedFile = Request.Files["UploadedImage"];
                    if (httpPostedFile.FileName != "")
                    {
                        string srvPath = Server.MapPath("../Files/img");
                        string path = "/Files/img/" + httpPostedFile.FileName;

                        using (MD5 md5Hash = MD5.Create())
                        {
                            string hash = HashMedia.GetMd5Hash(md5Hash, httpPostedFile.FileName + "" + DateTime.Now.ToString());

                            string[] strSplit = httpPostedFile.FileName.Split('.');

                            string filetype = strSplit[strSplit.Length - 1];
                            if (httpPostedFile.FileName != "" && (filetype == "jpg" || filetype == "jpeg" || filetype == "png"))
                            {
                                savingpath = srvPath + "/" + hash + "." + filetype;
                                httpPostedFile.SaveAs(savingpath);
                                savingpath = "/files/img/" + hash + "." + filetype;

                                MEDIAT upload = new MEDIAT();

                                upload.Created = DateTime.Now;
                                upload.Createdby = 1;
                                upload.Pershkrimi = httpPostedFile.FileName;
                                if (model.Pershkrimi!=null)
                                {
                                    upload.Pershkrimi = model.Pershkrimi;
                                }
                                upload.Shtegu = savingpath;
                                upload.Emri = Guid.NewGuid();
                                upload.Extensioni = (int)Enums.FileType.image;
                                db.MEDIATs.Add(upload);
                                await db.SaveChangesAsync();
                                Session["UploadedPicture"] = upload;
                            }
                            }
                        
                    }
                }

                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
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

        // GET: MEDIA/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MEDIA/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MEDIA/Create
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

        // GET: MEDIA/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MEDIA/Edit/5
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

        // GET: MEDIA/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MEDIA/Delete/5
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
