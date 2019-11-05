using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace REQUEST.Models
{
    public class NAV
    {
        public string Req_NAV_CODE { get; set; }

        [Required(ErrorMessage = "* กรุณากรอกรหัสพนักงาน")]
        [Display(Name = "USER_NO: ")]
        public string USER_NO { get; set; }

        [Required(ErrorMessage = "* กรุณากรอกสิทธิการใช้งาน")]
        [Display(Name = "Allow_STATUS: ")]
        public string Allow_STATUS { get; set; }

        [Required(ErrorMessage = "* กรุณากรอกฝ่าย/ส่วน")]
        [Display(Name = " SECTION_ID: ")]
        public string SECTION_ID { get; set; }

        [Required(ErrorMessage = "* กรุณากรอกรายละเอียดและสิ่งที่ต้องการ")]
        [Display(Name = "NAV_Detail: ")]
        public string NAV_Detail { get; set; }
        public string NAV_DOC { get; set; }
        public string NAV_APPROVER { get; set; }
        public Nullable<System.DateTime> Date_APPROVER { get; set; }
        public string APPROVER_Owner_System { get; set; }
        public Nullable<System.DateTime> APPROVER_Owner_System_Date { get; set; }
        public string Req_OPEN_BY { get; set; }
        public Nullable<System.DateTime> Req_OPEN_DATE { get; set; }
        public string Req_ASSIGN_TO { get; set; }
        public Nullable<System.DateTime> Req_ASSIGN_TO_DATE { get; set; }
        public string Req_CLOSE { get; set; }
        public Nullable<System.DateTime> Req_CLOSE_DATE { get; set; }
        public string REQ_STATUS { get; set; }
        public string Req_BILL_NO { get; set; }
        public Nullable<System.DateTime> Req_BILL_DATE { get; set; }
        public Nullable<float> Req_COST { get; set; }
        public string USER_CREATE { get; set; }
        public string USER_UPDATE { get; set; }
        public Nullable<System.DateTime> UPDATE_DATE { get; set; }
        public Nullable<System.DateTime> CREATE_DATE { get; set; }
        public Nullable<System.DateTime> Req_SUBMIT_DATE { get; set; }
        public Nullable<System.DateTime> DATE_NAV { get; set; }
    }
}