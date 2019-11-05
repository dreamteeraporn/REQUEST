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
    public class tbl_Req_ProgramController : Controller
    {
        private IT_ASSET_MANAGEMENTEntities db = new IT_ASSET_MANAGEMENTEntities();
        string connectionString = "Data Source = SPARE08\\SQLEXPRESS;Initial Catalog=IT_ASSET_MANAGEMENT;Integrated Security=True";

        // GET: tbl_Req_Program
        public ActionResult Index(string search)
        {
            return View(db.View_Req_PG.OrderByDescending(s => s.Req_PG_CODE).Where(s => s.Req_PG_CODE.Contains(search) || s.USER_NO.Contains(search) || search == null).ToList());
        }

        // GET: tbl_Req_Program/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            View_Req_PG view_Req_PG = db.View_Req_PG.Find(id);
            if (view_Req_PG == null)
            {
                return HttpNotFound();
            }
            return View(view_Req_PG);
        }

        // GET: tbl_Req_Program/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: tbl_Req_Program/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Program tbl_Req_Program)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("AutoProgram", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@USER_No", tbl_Req_Program.USER_NO);
                    cmd.Parameters.AddWithValue("@PG_DATE", tbl_Req_Program.PG_DATE);
                    cmd.Parameters.AddWithValue("@PG_Work", tbl_Req_Program.PG_Work);
                    cmd.Parameters.AddWithValue("@PG_Problem", tbl_Req_Program.PG_Problem);
                    cmd.Parameters.AddWithValue("@PG_Requirement", tbl_Req_Program.PG_Requirement);
                    cmd.Parameters.AddWithValue("@PG_Frequency", tbl_Req_Program.PG_Frequency);
                    cmd.Parameters.AddWithValue("@PG_Date_start", tbl_Req_Program.PG_Date_start);
                    cmd.Parameters.AddWithValue("@REQ_STATUS ", tbl_Req_Program.REQ_STATUS);
                    cmd.Parameters.AddWithValue("@PG_APPROVER ", tbl_Req_Program.PG_APPROVER);
                    cmd.Parameters.AddWithValue("@PG_APPROVER_DATE ", tbl_Req_Program.PG_APPROVER_DATE);



                    cmd.Parameters.Add("@Req_PG_CODE", SqlDbType.NVarChar, 20);
                    cmd.Parameters["@Req_PG_CODE"].Direction = ParameterDirection.Output;


                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();


                    ViewBag.EmpCount = cmd.Parameters["@Req_PG_CODE"].Value.ToString();

                    var email = Session["USER_EMAIL"].ToString();
                    MailMessage mm = new MailMessage();
                    mm.To.Add("testuser@pranda.co.th");
                    mm.From = new MailAddress(email);
                    mm.Subject = "ใบขอรับบริการด้านโปรแกรมคอมพิวเตอร์";

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


            return View(tbl_Req_Program);
        }

        private string GetFormattedMessageHTML()
        {
            var link = "สามารถกดอนุมัติได้จาก <a href=https://localhost:44384/Home/Login >คลิกที่นี่" + "</a>";
            return "<b> เรียนผู้จัดการฝ่าย  </b>" + "</br>" +
                "ขอรับบริการด้านโปรแกรมคอมพิวเตอร์ : " + Session["USER_NAME"].ToString() + "<br />" +
                 "รหัสพนักงาน : " + Session["USER_NO"].ToString() + "<br />" +
                 "ชื่อพนักงาน : " + Session["USER_NAME"].ToString() + "<br />" +
                 "เลขที่เอกสาร : " + ViewBag.EmpCount + "<br />" +
                 link + "<br />" +
                 "สามารถติดต่อสอบถาม ติดต่อเบอร์" + " " + Session["USER_EXTENSION"].ToString() + "<br/>" +
                 "<p>" + "จึงเรียนมาเพื่อทราบ" + "</p>";
        }

        // GET: tbl_Req_Program/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Req_Program tbl_Req_Program = db.tbl_Req_Program.Find(id);
            if (tbl_Req_Program == null)
            {
                return HttpNotFound();
            }

            return View(tbl_Req_Program);
        }

        // POST: tbl_Req_Program/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(tbl_Req_Program tbl_Req_Program)
        {
            if (ModelState.IsValid)
            {
                var email = Session["USER_EMAIL"].ToString();
                MailMessage mm = new MailMessage();
                mm.To.Add("itservice@pranda.co.th");
                mm.From = new MailAddress(email);
                mm.Subject = "ขอรับบริการด้านโปรแกรมคอมพิวเตอร์";

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
                db.Entry(tbl_Req_Program).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Trace");
            }
            return View(tbl_Req_Program);
        }

        private string GetFormattedMessageIT()
        {
            return "<b> เรียน IT SUPPORT </b>" + "</br>" +
                   "ขอรับบริการด้านโปรแกรมคอมพิวเตอร์ : " + Session["USER_NAME"].ToString() + "<br />" +
                    "รหัสพนักงาน : " + Session["USER_NO"].ToString() + "<br />" +

                    "เลขที่เอกสาร : " + ViewBag.EmpCount + "<br />" +
                    "ได้รับการอนุมัติจากผุ้จัดการฝ่ายเรียบร้อยแล้ว" + "<br/>" +
                    "สามารถติดต่อสอบถาม ติดต่อเบอร์" + " " + Session["USER_EXTENSION"].ToString() + "<br/>" +
                    "<p>" + "จึงเรียนมาเพื่อทราบ" + "</p>";
        }

        // GET: tbl_Req_Program/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Req_Program tbl_Req_Program = db.tbl_Req_Program.Find(id);
            if (tbl_Req_Program == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Req_Program);
        }

        // POST: tbl_Req_Program/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            tbl_Req_Program tbl_Req_Program = db.tbl_Req_Program.Find(id);
            db.tbl_Req_Program.Remove(tbl_Req_Program);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Trace(string search)

        {
            return View(db.View_Req_PG_Trace.OrderByDescending(s => s.Req_PG_CODE).Where(s => s.Req_PG_CODE.Contains(search) || s.USER_NO.Contains(search) || search == null).ToList());

        }
        public ActionResult Details_Trace(string id)

        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            View_Req_PG_Trace req_PG_Trace = db.View_Req_PG_Trace.Find(id);
            if (req_PG_Trace == null)
            {
                return HttpNotFound();
            }
            return View(req_PG_Trace);


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


            View("~/Views/tbl_Req_Program/Report.cshtml", db.View_Req_PG_Trace.ToList()).ExecuteResult(this.ControllerContext);
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
            View_Req_PG_Trace req_PG_Trace = db.View_Req_PG_Trace.Find(id);
            if (req_PG_Trace == null)
            {
                return HttpNotFound();
            }
            return View(req_PG_Trace);


        }

        public ActionResult PrintPartialViewToPdf(string id)

        {


            using (IT_ASSET_MANAGEMENTEntities db = new IT_ASSET_MANAGEMENTEntities())
            {
               View_Req_PG_Trace customer = db.View_Req_PG_Trace.FirstOrDefault(c => c.Req_PG_CODE == id);

                var report = new PartialViewAsPdf("~/Views/tbl_Req_Program/Details_Print.cshtml", customer);

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
