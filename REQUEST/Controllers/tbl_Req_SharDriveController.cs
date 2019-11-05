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
    public class tbl_Req_SharDriveController : Controller
    {
        private IT_ASSET_MANAGEMENTEntities db = new IT_ASSET_MANAGEMENTEntities();
        string connectionString = "Data Source = SPARE08\\SQLEXPRESS;Initial Catalog=IT_ASSET_MANAGEMENT;Integrated Security=True";

        // GET: tbl_Req_SharDrive
        public ActionResult Index(string search)
        {
            return View(db.View_Req_SD.OrderByDescending(s =>s.Req_SD_CODE).Where(s => s.Req_SD_CODE.Contains(search) || s.USER_NO.Contains(search) || search == null).ToList());
        }


        // GET: tbl_Req_SharDrive/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            View_Req_SD view_Req_SD = db.View_Req_SD.Find(id);
            if (view_Req_SD == null)
            {
                return HttpNotFound();
            }
            return View(view_Req_SD);
        }

        // GET: tbl_Req_SharDrive/Create
        public ActionResult Create()
        {
           
            return View();
        }

        // POST: tbl_Req_SharDrive/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SharDrive tbl_Req_SharDrive)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("AutoSharDrive", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Allow_STATUS", tbl_Req_SharDrive.Allow_STATUS);
                    cmd.Parameters.AddWithValue("@ID_Authen ", tbl_Req_SharDrive.ID_Authen);
                    cmd.Parameters.AddWithValue("@USER_No", tbl_Req_SharDrive.USER_NO);
                    cmd.Parameters.AddWithValue("@SD_DATE", tbl_Req_SharDrive.SD_DATE); 
                    cmd.Parameters.AddWithValue("@SD_Drive", tbl_Req_SharDrive.SD_Drive);
                    cmd.Parameters.AddWithValue("@SD_Folder", tbl_Req_SharDrive.SD_Folder);
                    
                    cmd.Parameters.AddWithValue("@SD_Note", tbl_Req_SharDrive.SD_Note);
                    cmd.Parameters.AddWithValue("@SD_REQUESTER", tbl_Req_SharDrive.SD_REQUESTER);
                    cmd.Parameters.AddWithValue("@SD_APPROVER", tbl_Req_SharDrive.SD_APPROVER);
                    cmd.Parameters.AddWithValue("@Date_APPROVER", tbl_Req_SharDrive.Date_APPROVER);
                    cmd.Parameters.AddWithValue("@REQ_STATUS", tbl_Req_SharDrive.REQ_STATUS);



                    cmd.Parameters.Add("@Req_SD_CODE", SqlDbType.NVarChar, 20);
                    cmd.Parameters["@Req_SD_CODE"].Direction = ParameterDirection.Output;


                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    ViewBag.EmpCount = cmd.Parameters["@Req_SD_CODE"].Value.ToString();


                    var email = Session["USER_EMAIL"].ToString();
                    MailMessage mm = new MailMessage();
                    mm.To.Add("testuser@pranda.co.th");
                    mm.From = new MailAddress(email);
                    mm.Subject = "แบบฟอร์มการขอสิทธิและยกเลิกรหัสผู้ใช้เพื่อเข้าสู่ Shar Drive ";

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
           



            return View(tbl_Req_SharDrive);
        }

            private string GetFormattedMessageHTML()
            {

            var link = "สามารถกดอนุมัติได้จาก <a href=https://localhost:44384/Home/Login >คลิกที่นี่" + "</a>";
            return "<b> เรียนผู้จัดการฝ่าย  </b>" + "</br>" +
                "การขอสิทธิและยกเลิกรหัสผู้ใช้เพื่อเข้าสู่ Shar Drive : " + Session["USER_NAME"].ToString() + "<br />" +
                 "รหัสพนักงาน : " + Session["USER_NO"].ToString() + "<br />" +
                 "ชื่อพนักงาน : " + Session["USER_NAME"].ToString() + "<br />" +
                 "เลขที่เอกสาร : " + ViewBag.EmpCount + "<br />" +
                 link + "<br />" +
                 "สามารถติดต่อสอบถาม ติดต่อเบอร์" + " " + Session["USER_EXTENSION"].ToString() + "<br/>" +
                 "<p>" + "จึงเรียนมาเพื่อทราบ" + "</p>";

        }

            // GET: tbl_Req_SharDrive/Edit/5
            public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Req_SharDrive tbl_Req_SharDrive = db.tbl_Req_SharDrive.Find(id);
            if (tbl_Req_SharDrive == null)
            {
                return HttpNotFound();
            }
            ViewData["Allow_STATUS"] = new SelectList(db.tbl_req_allow_status, "Allow_STATUS", "Allow_DESCRIPTION", tbl_Req_SharDrive.Allow_STATUS);
            ViewData["ID_Authen"] = new SelectList(db.tbl_Req_Authen, "ID_Authen", "Authen_DESCRIPTION", tbl_Req_SharDrive.ID_Authen);

            return View(tbl_Req_SharDrive);
        }

        // POST: tbl_Req_SharDrive/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(tbl_Req_SharDrive tbl_Req_SharDrive)
        {
            if (ModelState.IsValid)
            {
                var email = Session["USER_EMAIL"].ToString();
                MailMessage mm = new MailMessage();
                mm.To.Add("itservice@pranda.co.th");
                mm.From = new MailAddress(email);
                mm.Subject = "การขอสิทธิและยกเลิกรหัสผู้ใช้เพื่อเข้าสู่ Shar Drive";

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
                db.Entry(tbl_Req_SharDrive).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Trace");
            }
            return View(tbl_Req_SharDrive);
        }

        private string GetFormattedMessageIT()
        {
            return "<b> เรียน IT SUPPORT </b>" + "</br>" +
                    "การขอสิทธิและยกเลิกรหัสผู้ใช้เพื่อเข้าสู่ Shar Drive : " + Session["USER_NAME"].ToString() + "<br />" +
                     "รหัสพนักงาน : " + Session["USER_NO"].ToString() + "<br />" +

                     "เลขที่เอกสาร : " + ViewBag.EmpCount + "<br />" +
                     "ได้รับการอนุมัติจากผุ้จัดการฝ่ายเรียบร้อยแล้ว" + "<br/>" +
                     "สามารถติดต่อสอบถาม ติดต่อเบอร์" + " " + Session["USER_EXTENSION"].ToString() + "<br/>" +
                     "<p>" + "จึงเรียนมาเพื่อทราบ" + "</p>";
        }

        // GET: tbl_Req_SharDrive/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Req_SharDrive tbl_Req_SharDrive = db.tbl_Req_SharDrive.Find(id);
            if (tbl_Req_SharDrive == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Req_SharDrive);
        }

        // POST: tbl_Req_SharDrive/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]


        public ActionResult DeleteConfirmed(string id)
        {
            tbl_Req_SharDrive tbl_Req_SharDrive = db.tbl_Req_SharDrive.Find(id);
            db.tbl_Req_SharDrive.Remove(tbl_Req_SharDrive);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Trace(string search)

        {
            return View(db.View_Req_SD_Trace.OrderByDescending(s => s.Req_SD_CODE).Where(s => s.Req_SD_CODE.Contains(search) || s.USER_NO.Contains(search) || search == null).ToList());

        }
        public ActionResult Details_Trace(string id)

        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            View_Req_SD_Trace req_SD_Trace = db.View_Req_SD_Trace.Find(id);
            if (req_SD_Trace == null)
            {
                return HttpNotFound();
            }
            return View(req_SD_Trace);


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


            View("~/Views/tbl_Req_SharDrive/Report.cshtml", db.View_Req_SD_Trace.ToList()).ExecuteResult(this.ControllerContext);
            Response.Flush();
            Response.End();
            return View();

        }
        public ActionResult Details_Print(string id)

        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            View_Req_SD_Trace req_SD_Trace = db.View_Req_SD_Trace.Find(id);
            if (req_SD_Trace == null)
            {
                return HttpNotFound();
            }
            return View(req_SD_Trace);


        }

        public ActionResult PrintPartialViewToPdf(string id)

        {


            using (IT_ASSET_MANAGEMENTEntities db = new IT_ASSET_MANAGEMENTEntities())
            {
                View_Req_SD_Trace customer = db.View_Req_SD_Trace.FirstOrDefault(c => c.Req_SD_CODE == id);

                var report = new PartialViewAsPdf("~/Views/tbl_Req_SharDrive/Details_Print.cshtml", customer);

                return report;


            }
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
