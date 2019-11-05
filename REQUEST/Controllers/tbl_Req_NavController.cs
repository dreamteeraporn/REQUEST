using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using REQUEST.Models;
using Rotativa;

namespace REQUEST.Controllers
{
    public class tbl_Req_NavController : Controller
    {
        private IT_ASSET_MANAGEMENTEntities db = new IT_ASSET_MANAGEMENTEntities();
        string connectionString = "Data Source = SPARE08\\SQLEXPRESS;Initial Catalog=IT_ASSET_MANAGEMENT;Integrated Security=True";


        // GET: tbl_Req_Nav
        public ActionResult Index(string search)
        {
            return View(db.View_Req_NAV.OrderByDescending(s => s.Req_NAV_CODE).Where(s => s.Req_NAV_CODE.Contains(search) || s.USER_NO.Contains(search) || search == null).ToList());
        }

        // GET: tbl_Req_Nav/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            View_Req_NAV view_Req_Nav = db.View_Req_NAV.Find(id);
            if (view_Req_Nav == null)
            {
                return HttpNotFound();
            }
            return View(view_Req_Nav);
        }

        // GET: tbl_Req_Nav/Create
        public ActionResult Create()
        {
           
            return View();
        }

        // POST: tbl_Req_Nav/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(NAV tbl_Req_Nav)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("AutoNAV", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                  
                    cmd.Parameters.AddWithValue("@USER_NO ", tbl_Req_Nav.USER_NO);
                    cmd.Parameters.AddWithValue("@Allow_STATUS", tbl_Req_Nav.Allow_STATUS);
                    cmd.Parameters.AddWithValue("@SECTION_ID", tbl_Req_Nav.SECTION_ID);
                    cmd.Parameters.AddWithValue("@DATE_NAV", tbl_Req_Nav.DATE_NAV);
                    cmd.Parameters.AddWithValue("@NAV_Detail", tbl_Req_Nav.NAV_Detail);
                    cmd.Parameters.AddWithValue("@NAV_DOC", tbl_Req_Nav.NAV_DOC);
                    cmd.Parameters.AddWithValue("@NAV_APPROVER", tbl_Req_Nav.NAV_APPROVER);
                    cmd.Parameters.AddWithValue("@Date_APPROVER", tbl_Req_Nav.Date_APPROVER);
                    cmd.Parameters.AddWithValue("@REQ_STATUS", tbl_Req_Nav.REQ_STATUS);
                  


                    cmd.Parameters.Add("@Req_NAV_CODE", SqlDbType.NVarChar, 20);
                    cmd.Parameters["@Req_NAV_CODE"].Direction = ParameterDirection.Output;


                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();


                    ViewBag.EmpCount = cmd.Parameters["@Req_NAV_CODE"].Value.ToString();


                    var email = Session["USER_EMAIL"].ToString();
                    MailMessage mm = new MailMessage();
                    mm.To.Add("testuser@pranda.co.th");
                    mm.From = new MailAddress(email);
                    mm.Subject = "แบบฟอร์มการขอสิทธิและแก้ไขเพิ่มเติมการใช้ระบบ NAV";

                    mm.IsBodyHtml = true;
                    mm.Body = GetFormattedMessageHTML();

                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "mail01.pranda.co.th";
                    smtp.Port = 25;
                    smtp.EnableSsl = false;
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = new System.Net.NetworkCredential();

                    System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate (object s,
                        System.Security.Cryptography.X509Certificates.X509Certificate certificate,
                        System.Security.Cryptography.X509Certificates.X509Chain chain,
                        System.Net.Security.SslPolicyErrors sslPolicyErrors)
                    {
                        return true;
                    };

                    smtp.Send(mm);
                    return RedirectToAction("Index");

                }
            }
            


            return View(tbl_Req_Nav);
        }

        private string GetFormattedMessageHTML()
        {
            var link = "สามารถกดอนุมัติได้จาก <a href=https://localhost:44384/Home/Login >คลิกที่นี่" + "</a>";
            return "<b> เรียนผู้จัดการฝ่าย  </b>" + "</br>" +
                "การขอสิทธิและแก้ไขเพิ่มเติมการใช้ระบบ NAV : " + Session["USER_NAME"].ToString() + "<br />" +
                 "รหัสพนักงาน : " + Session["USER_NO"].ToString() + "<br />" +
                 "ชื่อพนักงาน : " + Session["USER_NAME"].ToString() + "<br />" +
                 "เลขที่เอกสาร : " + ViewBag.EmpCount + "<br />" +
                 link + "<br />" +
                 "สามารถติดต่อสอบถาม ติดต่อเบอร์" + " " + Session["USER_EXTENSION"].ToString() + "<br/>" +
                 "<p>" + "จึงเรียนมาเพื่อทราบ" + "</p>";
        }


        // GET: tbl_Req_Nav/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Req_Nav tbl_Req_Nav = db.tbl_Req_Nav.Find(id);
            if (tbl_Req_Nav == null)
            {
                return HttpNotFound();
            }

            ViewData["Allow_STATUS"] = new SelectList(db.tbl_req_allow_status, "Allow_STATUS", "Allow_DESCRIPTION", tbl_Req_Nav.Allow_STATUS);
            ViewData["SECTION_ID"] = new SelectList(db.tbl_section, "SECTION_ID", "SECTION_DESC", tbl_Req_Nav.SECTION_ID);

            return View(tbl_Req_Nav);
        }

        // POST: tbl_Req_Nav/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( tbl_Req_Nav tbl_Req_Nav)
        {
            if (ModelState.IsValid)
            {
                var email = Session["USER_EMAIL"].ToString();
                MailMessage mm = new MailMessage();
                mm.To.Add("itservice@pranda.co.th");
                mm.From = new MailAddress(email);
                mm.Subject = "การขอสิทธิและแก้ไขเพิ่มเติมการใช้ระบบ NAV";

                mm.IsBodyHtml = true;
                mm.Body = GetFormattedMessageIT();

                SmtpClient smtp = new SmtpClient();
                smtp.Host = "mail01.pranda.co.th";
                smtp.Port = 25;
                smtp.EnableSsl = false;
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = new System.Net.NetworkCredential();

                System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate (object s,
                    System.Security.Cryptography.X509Certificates.X509Certificate certificate,
                    System.Security.Cryptography.X509Certificates.X509Chain chain,
                    System.Net.Security.SslPolicyErrors sslPolicyErrors)
                {
                    return true;
                };

                smtp.Send(mm);
                db.Entry(tbl_Req_Nav).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Trace");
            }
         


            return View(tbl_Req_Nav);
        }

        private string GetFormattedMessageIT()
        {
            return "<b> เรียน IT SUPPORT </b>" + "</br>" +
                  "การขอสิทธิและแก้ไขเพิ่มเติมการใช้ระบบ NAV : " + Session["USER_NAME"].ToString() + "<br />" +
                   "รหัสพนักงาน : " + Session["USER_NO"].ToString() + "<br />" +

                   "เลขที่เอกสาร : " + ViewBag.EmpCount + "<br />" +
                   "ได้รับการอนุมัติจากผุ้จัดการฝ่ายเรียบร้อยแล้ว" + "<br/>" +
                   "สามารถติดต่อสอบถาม ติดต่อเบอร์" + " " + Session["USER_EXTENSION"].ToString() + "<br/>" +
                   "<p>" + "จึงเรียนมาเพื่อทราบ" + "</p>";
        }

            // GET: tbl_Req_Nav/Delete/5
            public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Req_Nav tbl_Req_Nav = db.tbl_Req_Nav.Find(id);
            if (tbl_Req_Nav == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Req_Nav);
        }

        // POST: tbl_Req_Nav/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            tbl_Req_Nav tbl_Req_Nav = db.tbl_Req_Nav.Find(id);
            db.tbl_Req_Nav.Remove(tbl_Req_Nav);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Trace(string search)

        {
            return View(db.View_Req_NAV_Trace.OrderByDescending(s => s.Req_NAV_CODE).Where(s => s.Req_NAV_CODE.Contains(search) || s.USER_NO.Contains(search) || search == null).ToList());


        }

        public ActionResult Details_Trace(string id)

        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            View_Req_NAV_Trace req_NAV_Trace = db.View_Req_NAV_Trace.Find(id);
            if (req_NAV_Trace == null)
            {
                return HttpNotFound();
            }
            return View(req_NAV_Trace);


        }

        public ActionResult Details_Print(string id)

        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            View_Req_NAV_Trace req_NAV_Trace = db.View_Req_NAV_Trace.Find(id);
            if (req_NAV_Trace == null)
            {
                return HttpNotFound();
            }
            return View(req_NAV_Trace);


        }
        public ActionResult PrintPartialViewToPdf(string id)

        {


            using (IT_ASSET_MANAGEMENTEntities db = new IT_ASSET_MANAGEMENTEntities())
            {
                View_Req_NAV_Trace customer = db.View_Req_NAV_Trace.FirstOrDefault(c => c.Req_NAV_CODE == id);

                var report = new PartialViewAsPdf("~/Views/tbl_Req_NAV/Details_Print.cshtml", customer);

                return report;


            }
        }


        public ActionResult Report()

        {
            Response.ClearContent();
            Response.ContentType = "application/fore-download";
            Response.AddHeader("content-disposition", " attachment; Filename=" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls");
            Response.Write("<html xmlns:x=\"urn:schemas-microsoft-com:office:excel\">");
            Response.Write("<head>");
            Response.Write("<META http-equiv=\"content-Type\" content=\"text/html; charset=utf-8\">");

            Response.Write("<!--[if gte mso 9]><xml>");
            Response.Write("<x:ExcelWorkbook>");
            Response.Write("<x:ExcelWorksheets>");
            Response.Write("<x:ExcelWorksheet>");
            Response.Write("<x:Name>Report Data</x:Name>");
            Response.Write("<x:WorksheetOptions>");
            Response.Write("<x:Print>");
            Response.Write("<x:ValidprinterInfo/>");
            Response.Write("<x:Print>");
            Response.Write("<x:WorksheetOptions>");
            Response.Write("<x:ExcelWorksheet>");
            Response.Write("<x:ExcelWorksheets>");
            Response.Write("<x:ExcelWorkbook>");
            Response.Write("</xml>");
            Response.Write("<![endif] --> ");


            View("~/Views/tbl_Req_NAV/Report.cshtml", db.View_Req_NAV_Trace.ToList()).ExecuteResult(this.ControllerContext);
            Response.Flush();
            Response.End();
            return View();

        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
