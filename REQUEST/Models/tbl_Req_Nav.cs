//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace REQUEST.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbl_Req_Nav
    {
        public string Req_NAV_CODE { get; set; }
        public string USER_NO { get; set; }
        public string Allow_STATUS { get; set; }
        public string SECTION_ID { get; set; }
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
