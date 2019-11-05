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
    public class tbl_Req_PDJCOST01Controller : Controller
    {
        private IT_ASSET_MANAGEMENTEntities db = new IT_ASSET_MANAGEMENTEntities();
        string connectionString = "Data Source = SPARE08\\SQLEXPRESS;Initial Catalog=IT_ASSET_MANAGEMENT;Integrated Security=True";
        // GET: tbl_Req_PDJCOST01
        public ActionResult Index(string search)
        {
            return View(db.View_Req_PDJCOST01.OrderByDescending(s => s.Req_PDJ01_CODE).Where(s => s.Req_PDJ01_CODE.Contains(search) || s.USER_NO.Contains(search) || search == null).ToList());
        }

        // GET: tbl_Req_PDJCOST01/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            View_Req_PDJCOST01 view_Req_PDJCOST01 = db.View_Req_PDJCOST01.Find(id);
            if (view_Req_PDJCOST01 == null)
            {
                return HttpNotFound();
            }
            return View(view_Req_PDJCOST01);
        }

        // GET: tbl_Req_PDJCOST01/Create
        public ActionResult Create()
        {
            ViewData["Allow_STATUS"] = new SelectList(db.tbl_req_allow_status, "Allow_STATUS", "Allow_DESCRIPTION");
            
            return View();
        }

        // POST: tbl_Req_PDJCOST01/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PDJCOST01 tbl_Req_PDJCOST01)
        {
            if (ModelState.IsValid)
            {

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("AutoPDJ01", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@USER_NO", tbl_Req_PDJCOST01.USER_NO);
                    cmd.Parameters.AddWithValue("@REQ_STATUS" , tbl_Req_PDJCOST01.REQ_STATUS);
                    cmd.Parameters.AddWithValue("@Allow_STATUS", tbl_Req_PDJCOST01.Allow_STATUS);
                    cmd.Parameters.AddWithValue("@Color_Picture", tbl_Req_PDJCOST01.Color_Picture);
                    cmd.Parameters.AddWithValue("@Quotation", tbl_Req_PDJCOST01.Quotation);
                    cmd.Parameters.AddWithValue("@Spec_Sheet", tbl_Req_PDJCOST01.Spec_Sheet);
                    cmd.Parameters.AddWithValue("@Produce_Prices", tbl_Req_PDJCOST01.Produce_Prices);
                    cmd.Parameters.AddWithValue("@Product_Analysis", tbl_Req_PDJCOST01.Product_Analysis);
                    cmd.Parameters.AddWithValue("@Line_Drawing", tbl_Req_PDJCOST01.Line_Drawing);
                    cmd.Parameters.AddWithValue("@PDJ01_REQUESTER", tbl_Req_PDJCOST01.PDJ01_REQUESTER);
                    cmd.Parameters.AddWithValue("@PDJ01_REQUESTER_DATE", tbl_Req_PDJCOST01.PDJ01_REQUESTER_DATE);
                    cmd.Parameters.AddWithValue("@PDJ01_APPROVER", tbl_Req_PDJCOST01.PDJ01_APPROVER);
                    cmd.Parameters.AddWithValue("@PDJ01_APPROVER_DATE ", tbl_Req_PDJCOST01.PDJ01_APPROVER_DATE);
                    cmd.Parameters.AddWithValue("@APPROVER_MANAGER ", tbl_Req_PDJCOST01.APPROVER_MANAGER);
                    cmd.Parameters.AddWithValue("@DATE_APPROVER_MANAGER ", tbl_Req_PDJCOST01.DATE_APPROVER_MANAGER);

                    cmd.Parameters.Add("@Req_PDJ01_CODE", SqlDbType.NVarChar, 20);
                    cmd.Parameters["@Req_PDJ01_CODE"].Direction = ParameterDirection.Output;


                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();


                    ViewBag.EmpCount = cmd.Parameters["@Req_PDJ01_CODE"].Value.ToString();


                    var email = Session["USER_EMAIL"].ToString();
                    MailMessage mm = new MailMessage();
                    mm.To.Add("testuser@pranda.co.th");
                    mm.From = new MailAddress(email);
                    mm.Subject = "แบบฟอร์มการขอสิทธิ การใช้เครื่อง PDJCOST01";

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
            ViewData["Allow_STATUS"] = new SelectList(db.tbl_req_allow_status, "Allow_STATUS", "Allow_DESCRIPTION", tbl_Req_PDJCOST01.Allow_STATUS);
           


            return View(tbl_Req_PDJCOST01);
        }

        private string GetFormattedMessageHTML()
        {
            var link = "สามารถกดอนุมัติได้จาก <a href=https://localhost:44384/Home/Login >คลิกที่นี่" + "</a>";
            return "<b> เรียนผู้จัดการฝ่าย  </b>" + "</br>" +
                "การขอสิทธิ การใช้เครื่อง PDJCOST01 : " + Session["USER_NAME"].ToString() + "<br />" +
                 "รหัสพนักงาน : " + Session["USER_NO"].ToString() + "<br />" +
                 "ชื่อพนักงาน : " + Session["USER_NAME"].ToString() + "<br />" +
                 "เลขที่เอกสาร : " + ViewBag.EmpCount + "<br />" +
                 link + "<br />" +
                 "สามารถติดต่อสอบถาม ติดต่อเบอร์" + " " + Session["USER_EXTENSION"].ToString() + "<br/>" +
                 "<p>" + "จึงเรียนมาเพื่อทราบ" + "</p>";
        }

        // GET: tbl_Req_PDJCOST01/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Req_PDJCOST01 tbl_Req_PDJCOST01 = db.tbl_Req_PDJCOST01.Find(id);
            if (tbl_Req_PDJCOST01 == null)
            {
                return HttpNotFound();
            }
            ViewData["Allow_STATUS"] = new SelectList(db.tbl_req_allow_status, "Allow_STATUS", "Allow_DESCRIPTION", tbl_Req_PDJCOST01.Allow_STATUS);
           

            return View(tbl_Req_PDJCOST01);
        }

        // POST: tbl_Req_PDJCOST01/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( tbl_Req_PDJCOST01 tbl_Req_PDJCOST01)
        {
            if (ModelState.IsValid)
            {
                var email = Session["USER_EMAIL"].ToString();
                MailMessage mm = new MailMessage();
                mm.To.Add("itservice@pranda.co.th");
                mm.From = new MailAddress(email);
                mm.Subject = "การขอสิทธิ การใช้เครื่อง PDJCOST01";

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
                db.Entry(tbl_Req_PDJCOST01).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Trace");
            }
            return View(tbl_Req_PDJCOST01);
        }

        private string GetFormattedMessageIT()
        {
            return "<b> เรียน IT SUPPORT </b>" + "</br>" +
                 "การขอสิทธิ การใช้เครื่อง PDJCOST01 : " + Session["USER_NAME"].ToString() + "<br />" +
                  "รหัสพนักงาน : " + Session["USER_NO"].ToString() + "<br />" +

                  "เลขที่เอกสาร : " + ViewBag.EmpCount + "<br />" +
                  "ได้รับการอนุมัติจากผุ้จัดการฝ่ายเรียบร้อยแล้ว" + "<br/>" +
                  "สามารถติดต่อสอบถาม ติดต่อเบอร์" + " " + Session["USER_EXTENSION"].ToString() + "<br/>" +
                  "<p>" + "จึงเรียนมาเพื่อทราบ" + "</p>";
        }

        // GET: tbl_Req_PDJCOST01/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Req_PDJCOST01 tbl_Req_PDJCOST01 = db.tbl_Req_PDJCOST01.Find(id);
            if (tbl_Req_PDJCOST01 == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Req_PDJCOST01);
        }

        // POST: tbl_Req_PDJCOST01/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            tbl_Req_PDJCOST01 tbl_Req_PDJCOST01 = db.tbl_Req_PDJCOST01.Find(id);
            db.tbl_Req_PDJCOST01.Remove(tbl_Req_PDJCOST01);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Trace(string search)

        {
            return View(db.View_Req_PDJCOST01_Trace.OrderByDescending(s => s.Req_PDJ01_CODE).Where(s => s.Req_PDJ01_CODE.Contains(search) || s.USER_NO.Contains(search) || search == null).ToList());


        }
        public ActionResult Details_Trace(string id)

        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            View_Req_PDJCOST01_Trace req_PDJCOST01_Trace = db.View_Req_PDJCOST01_Trace.Find(id);
            if (req_PDJCOST01_Trace == null)
            {
                return HttpNotFound();
            }
            return View(req_PDJCOST01_Trace);


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


            View("~/Views/tbl_Req_PDJCOST01/Report.cshtml", db.View_Req_PDJCOST01_Trace.ToList()).ExecuteResult(this.ControllerContext);
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
            View_Req_PDJCOST01_Trace req_PDJCOST01_Trace = db.View_Req_PDJCOST01_Trace.Find(id);
            if (req_PDJCOST01_Trace == null)
            {
                return HttpNotFound();
            }
            return View(req_PDJCOST01_Trace);


        }
        public ActionResult PrintPartialViewToPdf(string id)

        {


            using (IT_ASSET_MANAGEMENTEntities db = new IT_ASSET_MANAGEMENTEntities())
            {
                View_Req_PDJCOST01_Trace customer = db.View_Req_PDJCOST01_Trace.FirstOrDefault(c => c.Req_PDJ01_CODE == id);

                var report = new PartialViewAsPdf("~/Views/tbl_Req_PDJCOST01/Details_Print.cshtml", customer);

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
