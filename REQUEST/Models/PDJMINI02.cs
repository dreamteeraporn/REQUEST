using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace REQUEST.Models
{
    public class PDJMINI02
    {
        public string Req_PDJ02_CODE { get; set; }
        public string REQ_STATUS { get; set; }

        [Required(ErrorMessage = "* กรุณากรอกสิทธิการใช้งาน")]
        [Display(Name = "Allow_STATUS: ")]
        public string Allow_STATUS { get; set; }

        [Required(ErrorMessage = "* กรุณากรอกรหัสพนักงาน")]
        [Display(Name = "USER_NO: ")]
        public string USER_NO { get; set; }

       
        public string Sample_Pdj02 { get; set; }
        public string Proto_Pdj02 { get; set; }
        public string PrototypeInfrom { get; set; }
        public string CHM_Pdj02 { get; set; }
        public string PDJ02_REQUESTER { get; set; }
        public Nullable<System.DateTime> PDJ02_REQUESTER_DATE { get; set; }
        public string PDJ02_APPROVER { get; set; }
        public Nullable<System.DateTime> PDJ02_APPROVER_DATE { get; set; }
        public string APPROVER_MANAGER { get; set; }
        public Nullable<System.DateTime> DATE_APPROVER_MANAGER { get; set; }
        public string Req_OPEN_BY { get; set; }
        public Nullable<System.DateTime> Req_OPEN_DATE { get; set; }
        public Nullable<System.DateTime> Req_ASSIGN_TO_DATE { get; set; }
        public string Req_ASSIGN_TO { get; set; }
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