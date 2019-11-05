﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace REQUEST.Models
{
    public class AF
    {

   
   public string Req_AF_CODE { get; set; }

     [Required(ErrorMessage = "* กรุณากรอกสิทธิการใช้งาน")]
     [Display(Name = "Allow_STATUS: ")]
        public string Allow_STATUS { get; set; }

    [Required(ErrorMessage = "* กรุณากรอกรหัสพนักงาน")]
    [Display(Name = "USER_NO: ")]
    public string USER_NO { get; set; }
    public Nullable<System.DateTime> AF_DATE { get; set; }
        [Required(ErrorMessage = "* กรุณากรอกSite")]
        [Display(Name = "AF_Site: ")]
        public string AF_Site { get; set; }

        [Required(ErrorMessage = "* กรุณากรอกFolder")]
        [Display(Name = "AF_Site: ")]
        public string AF_Folder { get; set; }
    public string AF_Note { get; set; }
    public string AF_REQUESTER { get; set; }
    public string AF_APPROVER { get; set; }
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
    public string AF_STATUS_ID { get; set; }
    public string APPROVER_OWNER { get; set; }
    public Nullable<System.DateTime> APPROVER_OWNER_DATE { get; set; }
}
}