using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace REQUEST.Models
{
    public class SharDrive
    {
        public string Req_SD_CODE { get; set; }

        [Required(ErrorMessage = "* กรุณากรอกสิทธิการใช้งาน")]
        [Display(Name = "Allow_STATUS: ")]
        public string Allow_STATUS { get; set; }

       
        public string ID_Authen { get; set; }

        [Required(ErrorMessage = "* กรุณากรอกรหัสพนักงาน")]
        [Display(Name = "USER_NO: ")]
        public string USER_NO { get; set; }
        public Nullable<System.DateTime> SD_DATE { get; set; }

        [Required(ErrorMessage = "* กรุณากรอกชื่อDrive")]
        [Display(Name = "SD_Drive: ")]
        public string SD_Drive { get; set; }

       
        public string SD_Folder { get; set; }
        public string SD_Approved_Owner { get; set; }
        public Nullable<System.DateTime> SD_Approved_Owner_Date { get; set; }
        public string SD_Note { get; set; }
        public string SD_REQUESTER { get; set; }
        public string SD_APPROVER { get; set; }
        public Nullable<System.DateTime> Date_APPROVER { get; set; }
        public string Req_OPEN_BY { get; set; }
        public Nullable<System.DateTime> Req_OPEN_DATE { get; set; }
        public Nullable<System.DateTime> Req_ASSIGN_TO_DATE { get; set; }
        public string Req_ASSIGN_TO { get; set; }
        public string Req_CLOSE { get; set; }
        public Nullable<System.DateTime> Req_CLOSE_DATE { get; set; }
        public string REQ_STATUS { get; set; }
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