using REQUEST.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace REQUEST.Controllers
{
    public class HomeController : Controller
    {
        private IT_ASSET_MANAGEMENTEntities db = new IT_ASSET_MANAGEMENTEntities();
        public ActionResult login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult login(tbl_user tbl_User)
        {
            if (ModelState.IsValid)
            {
                using (IT_ASSET_MANAGEMENTEntities db = new IT_ASSET_MANAGEMENTEntities())
                {
                    var obj = db.tbl_user.Where(User => User.USER_NO.Equals(tbl_User.USER_NO) && User.USER_PASSWORD.Equals(tbl_User.USER_PASSWORD)).FirstOrDefault();

                    if (obj != null)
                    {

                        Session["USER_ID"] = obj.USER_ID.ToString();
                        Session["USER_NO"] = obj.USER_NO.ToString();
                        Session["USER_NAME"] = obj.USER_NAME.ToString();
                        Session["NAME_ENG"] = obj.NAME_ENG.ToString();
                        Session["PROSITION"] = obj.PROSITION.ToString();
                        Session["DEPARTMENT"] = obj.DEPARTMENT.ToString();
                        Session["USER_EMAIL"] = obj.USER_EMAIL.ToString();
                        Session["USER_EXTENSION"] = obj.USER_EXTENSION.ToString();
                        Session["USER_ROLE"] = obj.USER_ROLE.ToString();




                        return RedirectToAction("Index", "tbl_Req_AF");

                    }

                    else
                    {
                        ViewBag.Message = " Username or Password is not correct";

                    }

                        }

                


            }
            return View(tbl_User);
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("login");

        }
       
        
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(tbl_user tbl_User)
        {
            if (ModelState.IsValid)
            {
                db.tbl_user.Add(tbl_User);
                db.SaveChanges();
                return RedirectToAction("login");
            }
            return View(tbl_User);
        }
    }
}