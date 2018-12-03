using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLogic.Helpers;
using eStore.Models;
using System.Threading.Tasks;
using DataAccess;
using System.Text;
using BusinessLogic;
using System.Data.Entity;
using System.Net;

namespace eStore.Controllers
{
    public class USERController : BaseController
    {
        eStoreDB db = new eStoreDB();

        public USERController()
        {
            ViewBag.AlertModel = new Alert() { AlertStyle = "", Dismissable = true, Message = "" };
        }
        // GET: USER
        public async Task<ActionResult> Index()
        {
            var user = await GetUser();
           List<PERDORUESI> model =await db.PERDORUESIs.ToListAsync();
            return View(model);
        }


        public async Task<ActionResult> PasswordReset(int id)
        {
            PERDORUESI user = await db.PERDORUESIs.FindAsync(id);
            ChangePasswordViewModel model = new ChangePasswordViewModel();
            model.ID = user.ID;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> PasswordReset_POST([Bind(Include = "ID,NewPassword,ConfirmPassword")]ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = await db.PERDORUESIs.FindAsync(model.ID);
                    byte[] oldPasswordBinary = user.Password;
                    ASCIIEncoding Password = new ASCIIEncoding();
                    string oldPasswordEncrypted = Password.GetString(oldPasswordBinary);
                    string oldPasswordDecrypted = Encrypt.Decrypt(oldPasswordEncrypted);

                    var result = await UserManager.ChangePasswordAsync(user.UserID, oldPasswordDecrypted, model.NewPassword);
                    if (result.Succeeded)
                    {
                        string password = model.NewPassword;
                        ASCIIEncoding BinaryPassword = new ASCIIEncoding();
                        string encrypted = Encrypt.Encryption(password);
                        byte[] passwordArray = BinaryPassword.GetBytes(encrypted);
                        user.Password = passwordArray;
                        db.Entry(user).State = EntityState.Modified;
                        await db.SaveChangesAsync();
                        Success("Fjalëkalimi është rivendosur me sukses.", true);
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception ex)
                {

                }
            }
            Information("Ka ndodhur një gabim, provoni përsëri", true);
            return RedirectToAction("Index");
        }

        public JsonResult CheckPassword(string NewPassword)
        {
            if (!(NewPassword.Any(char.IsUpper) && NewPassword.Any(char.IsDigit) && NewPassword.Any(char.IsLower) && (NewPassword.Any(char.IsSymbol) || NewPassword.Any(char.IsPunctuation))))
            {
                return Json("Fjalëkalimi duhet të përmbajë shkronja të mëdha, të vogla, simbole dhe numër.", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
        }
        // GET: USER/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: USER/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: USER/Create
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
        // GET: USER/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            var userlogged = await GetUser();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PERDORUESI user = await db.PERDORUESIs.FindAsync(id);
            EditViewModel model = new EditViewModel();
            model.user = user;
            var userauth = await UserManager.FindByIdAsync(user.UserID);
            model.UserName = userauth.UserName;
            model.ID = user.ID;

            var allRoles = (new ApplicationDbContext()).Roles.OrderBy(q => q.Name).ToList();
            ViewBag.Roles = new SelectList(allRoles, "Id", "Name", selectedValue: user.Roles);
            return View(model);
        }

        public async Task<JsonResult> CheckUserName(string UserName, int? ID)
        {
            var user = await UserManager.FindByNameAsync(UserName);
            var users = await db.PERDORUESIs.FindAsync(ID);
            if (UserName.Any(char.IsDigit))
            {
                return Json("Përdoruesi nuk duhet të përmbajë numra.", JsonRequestBehavior.AllowGet);
            }
            if (user != null && user.Id != users.UserID)
            {
                return Json("Përdoruesi ekziston.", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
        }

        // POST: USER/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditViewModel model)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    // TODO: Add update logic here
                    var user_model = model.user;
                    PERDORUESI useri = await db.PERDORUESIs.FindAsync(model.ID);
                    string roliVjeterID = useri.Roles.ToString();
                    var user = await UserManager.FindByIdAsync(useri.UserID);
                    if (user.UserName.ToLower().Trim() != model.UserName.ToLower().Trim())
                    {
                        user.UserName = model.UserName;
                        await UserManager.UpdateAsync(user);
                    }

                    //modifikimi tek perdoruesi
                    useri.Emri = user_model.Emri;
                    useri.Mbiemri = user_model.Mbiemri;
                    useri.Datelindja = user_model.Datelindja;
                    useri.Adresa = user_model.Adresa;
                    useri.Telefoni = user_model.Telefoni;
                    useri.Roles = user_model.Roles;

                    db.Entry(useri).State = EntityState.Modified;

                    await db.SaveChangesAsync();

                    if (useri.Roles.ToString() != model.user.Roles.ToString())
                    {
                        //fshirja e rolit te vjeter
                        var rolefound = (new ApplicationDbContext()).Roles.FirstOrDefault(q => q.Id == useri.ID.ToString());
                        await UserManager.RemoveFromRoleAsync(useri.UserID, rolefound.Name.ToString());

                        //vendosja e rolit te ri
                        string roliRiID = useri.Roles.ToString();
                        var newrolefound = (new ApplicationDbContext()).Roles.FirstOrDefault(q => q.Id == roliRiID);
                        await UserManager.AddToRoleAsync(useri.UserID, newrolefound.Name.ToString());

                        await db.SaveChangesAsync();
                    }

                    Success("Përdoruesi është modifikuar me sukses!", true);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    Danger("Ka ndodhur një gabim!", true);
                }
            }

            var userlogged = await GetUser();
            return View(model);
        }

        // GET: USER/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: USER/Delete/5
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
