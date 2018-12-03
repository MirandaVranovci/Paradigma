using BusinessLogic.Helpers;
using eStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DataAccess;
using System.Data.Entity;
using BusinessLogic;

namespace eStore.Controllers
{
    public class SYSMENUController : BaseController
    {
        public SYSMENUController()
        {
            ViewBag.AlertModel = new Alert() { AlertStyle = "", Dismissable = true, Message = "" };
        }
        eStoreDB db = new eStoreDB();
        // GET: SYSMENU
        public async Task<ActionResult> Index()
        {
            var user = await GetUser();
            SysMenuViewModel model = new SysMenuViewModel();
            List<SYSMENU_PARENT> lista = await db.SYSMENU_PARENT.OrderByDescending(q => q.ID).ToListAsync();
            List<SYSMENU> listam = await db.SYSMENUs.OrderByDescending(q => q.ID).ToListAsync();

            model.sys = listam;
            return View(model);
        }

        // GET: SYSMENU/Create
        public async Task<ActionResult> CreateAsync()
        {
            var user = await GetUser();
            SysMenuViewModel model = new SysMenuViewModel();
            var allRoles = (new ApplicationDbContext()).Roles.OrderBy(q => q.Name).ToList().Select(q => new SelectListItem { Value = q.Id, Text = q.Name }).ToList();
            ViewBag.RoleID = allRoles;
            ViewBag.RoliIDP = allRoles;
            ViewBag.ParentID = await LoadSysMenuParent(null);
            return View(model);
        }
        [HttpPost]
        public async Task<ActionResult> RuajParentin([Bind(Include = "ID,PershkrimiP,HtmlKlasaP,RoliIDP,RenditjaP")] SysMenuViewModel modeli)
        {
            if (ModelState.IsValid)
            {
                var user = await GetUser();
                try
                {
                    SYSMENU_PARENT model = new SYSMENU_PARENT();
                    model.ID = modeli.IDP;
                    model.Pershkrimi = modeli.PershkrimiP;
                    model.Renditja = modeli.RenditjaP;
                    model.RoleID = modeli.RoliIDP;
                    model.HtmlClass = modeli.HtmlKlasaP;
                    db.SYSMENU_PARENT.Add(model);
                    await db.SaveChangesAsync();
                    return Json(new { result = "Redirect", url = Url.Action("CreateAsync", "SYSMENU") });
                }
                catch (Exception ex)
                {
                    return Json("Gabim " + ex.Message, JsonRequestBehavior.DenyGet);
                }
            }

            return Json(new { result = "Redirect", url = Url.Action("CreateAsync", "SYSMENU") });
        }

        public ActionResult LoadParent(FormCollection collection)
        {
            string content = "";

            content = "<option value=\"\">Zgjedh</option>";

            var sysparent = (from b in db.SYSMENU_PARENT
                             select new
                             {
                                 id = b.ID,
                                 name = b.Pershkrimi
                             });
            foreach (var item in sysparent)
            {
                content += "<option value = \"" + item.id + "\">" + item.name + "</option>";
            }


            return Content(content);
        }

        [HttpPost]
        public async Task<ActionResult> RuajMenu([Bind(Include = "ID,ParentID,Pershkrimi,RoleID,HtmlClass,RoleName,Renditja,ActionName,Controller")] SysMenuViewModel modeli)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    var user = await GetUser();
                    try
                    {
                        SYSMENU model = new SYSMENU();
                        model.ID = modeli.ID;
                        model.ParentID = modeli.ParentID;
                        model.Pershkrimi = modeli.Pershkrimi;
                        model.RoleID = modeli.RoleID;
                        model.HtmlClass = modeli.HtmlClass;
                        model.RoleName = "Administrator";
                        model.Renditja = modeli.Renditja;
                        model.OpParamName = null;
                        model.OpParamValue = null;
                        model.styleid = null;
                        model.ActionName = modeli.ActionName;
                        model.Controller = modeli.Controller;

                        db.SYSMENUs.Add(model);
                        await db.SaveChangesAsync();
                        return Json(Url.Action("Index", "SYSMENU"));
                    }
                    catch (Exception ex)
                    {
                        ((System.Data.Entity.Validation.DbEntityValidationException)ex).EntityValidationErrors.ToString();
                        return Json("Gabim " + ex.Message, JsonRequestBehavior.DenyGet);
                    }

                }
                else
                {
                    return Json(Url.Action("Index", "SYSMENU"));
                }

            }
            catch
            {
                return View();
            }

        }

        // GET: SYSMENU/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var user = await GetUser();
            EditMenuViewModel model = new EditMenuViewModel();
            SYSMENU sysmenu = await db.SYSMENUs.FindAsync(id);
            SYSMENU_PARENT sysmenuparent = await db.SYSMENU_PARENT.FindAsync(sysmenu.ParentID);
            model.parenti = sysmenuparent;
            model.sysmenu = sysmenu;
            var allRoles = (new ApplicationDbContext()).Roles.OrderBy(q => q.Name).ToList();
            ViewBag.RoleID = new SelectList(allRoles, "Id", "Name", selectedValue: sysmenuparent.RoleID);
            ViewBag.RolesIDM = new SelectList(allRoles, "Id", "Name", selectedValue: sysmenu.RoleID);
            ViewBag.ParentID = await LoadSysMenuParent(sysmenu.ParentID);
            return View(model);
        }

        // POST: SYSMENU/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(EditMenuViewModel model)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    // TODO: Add update logic here
                    var sysm = model.sysmenu;
                    var sysp = model.parenti;

                    SYSMENU sm = await db.SYSMENUs.FindAsync(model.ID);
                    SYSMENU_PARENT parenti = await db.SYSMENU_PARENT.FindAsync(sm.ParentID);  //useri_i
                    string roliVjeterID = sm.RoleID.ToString();

                    //modifikimi tek sysmenu
                    sm.Pershkrimi = sysm.Pershkrimi;
                    sm.HtmlClass = sysm.HtmlClass;
                    if(sysm.ParentID!=0)
                    {
                        sm.ParentID = sysm.ParentID;
                    } 
                    sm.ActionName = sysm.ActionName;
                    sm.Controller = sysm.Controller;
                    sm.RoleID = sysm.RoleID;
                    sm.Renditja = sysm.Renditja;
                    db.Entry(sm).State = EntityState.Modified;

                    //modifikimi tek sysmmenu parenti
                     parenti.Pershkrimi= sysp.Pershkrimi;
                    parenti.HtmlClass= sysp.HtmlClass ;
                    parenti.RoleID= sysp.RoleID ;
                    parenti.Renditja =sysp.Renditja;
                    db.Entry(parenti).State = EntityState.Modified;

                    await db.SaveChangesAsync();
                    Success("Përdoruesi është modifikuar me sukses!", true);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                     Danger("Ka ndodhur një gabim!", true);
                }
            }

            return View(model);
        }

        // GET: SYSMENU/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SYSMENU/Delete/5
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
