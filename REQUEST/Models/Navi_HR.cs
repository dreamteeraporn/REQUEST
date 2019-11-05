using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace REQUEST.Models
{
    public class Navi_HR
    {
        public string Req_Navi_HR_CODE { get; set; }

        [Required(ErrorMessage = "* กรุณากรอกรหัสพนักงาน")]
        [Display(Name = "USER_NO: ")]
        public string USER_NO { get; set; }

        [Required(ErrorMessage = "* กรุณากรอกสิทธิการใช้งาน")]
        [Display(Name = "Allow_STATUS: ")]
        public string Allow_STATUS { get; set; }
        public string Navi_HR_User_Log_In { get; set; }

        [Required(ErrorMessage = "* กรุณากรอกวัตถุประสงค์")]
        [Display(Name = "Navi_HR_Detail: ")]
        public string Navi_HR_Detail { get; set; }
        public string Navi_HR_REQUESTER { get; set; }
        public Nullable<System.DateTime> Navi_HR_REQUESTER_DATE { get; set; }
        public string Navi_HR_APPROVER { get; set; }
        public Nullable<System.DateTime> Navi_HR_APPROVER_DATE { get; set; }
        public string Navi_HR_APPROVER_IT { get; set; }
        public Nullable<System.DateTime> Navi_HR_APPROVER_IT_DATE { get; set; }
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
    }
}