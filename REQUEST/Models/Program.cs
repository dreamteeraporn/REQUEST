using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace REQUEST.Models
{
    public class Program
    {
        public string Req_PG_CODE { get; set; }
        public string USER_NO { get; set; }

        [Required(ErrorMessage = "* กรุณากรอกรหัสพนักงาน")]
        [Display(Name = "USER_NO: ")]
        public Nullable<System.DateTime> PG_DATE { get; set; }

        [Required(ErrorMessage = "* กรุณากรอกงาน")]
        [Display(Name = "PG_Work: ")]
        public string PG_Work { get; set; }

        [Required(ErrorMessage = "* กรุณากรอกปัญหา")]
        [Display(Name = "PG_Problem: ")]
        public string PG_Problem { get; set; }

        [Required(ErrorMessage = "* กรุณากรอกความต้องการ")]
        [Display(Name = "PG_Requirement: ")]
        public string PG_Requirement { get; set; }

        [Required(ErrorMessage = "* กรุณากรอกความถี่ในการใช้งาน")]
        [Display(Name = "PG_Frequency: ")]
        public string PG_Frequency { get; set; }

        [Required(ErrorMessage = "* กรุณากรอกต้องการใช้วันที่")]
        [Display(Name = "PG_Date_start: ")]
        public Nullable<System.DateTime> PG_Date_start { get; set; }
        public string PG_APPROVER { get; set; }
        public Nullable<System.DateTime> PG_APPROVER_DATE { get; set; }
        public string PG_APPROVER_IT { get; set; }
        public Nullable<System.DateTime> Date_APPROVER_IT { get; set; }
        public string Req_OPEN_BY { get; set; }
        public Nullable<System.DateTime> Req_OPEN_DATE { get; set; }
        public Nullable<System.DateTime> Req_ASSIGN_TO_DATE { get; set; }
        public string Req_ASSIGN_TO { get; set; }
        public string REQ_STATUS { get; set; }
        public string Req_CLOSE { get; set; }
        public Nullable<System.DateTime> Req_CLOSE_DATE { get; set; }
        public string Req_BILL_NO { get; set; }
        public Nullable<System.DateTime> Req_BILL_DATE { get; set; }
        public Nullable<float> Req_COST { get; set; }
        public string USER_CREATE { get; set; }
        public string USER_UPDATE { get; set; }
        public Nullable<System.DateTime> CREATE_DATE { get; set; }
        public Nullable<System.DateTime> UPDATE_DATE { get; set; }
        public Nullable<System.DateTime> Req_SUBMIT_DATE { get; set; }
    }
}