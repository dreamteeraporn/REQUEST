using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace REQUEST.Models
{
    public class user
    {
        public int USER_ID { get; set; }

        [Required(ErrorMessage = "* กรุณากรอกรหัสพนักงาน")]
        [Display(Name = "USER_NO: ")]
        public string USER_NO { get; set; }

        [Required(ErrorMessage = "* กรุณากรอกรหัสผ่าน")]
        [Display(Name = "USER_PASSWORD: ")]


        public string USER_PASSWORD { get; set; }

        [Required(ErrorMessage = "* กรุณากรอกชื่อไทย")]
        [Display(Name = "USER_NAME: ")]
        public string USER_NAME { get; set; }
        public Nullable<System.DateTime> USER_LOGIN_TIME { get; set; }
        public string USER_LOGIN_FLAG { get; set; }
        public string USER_STATUS { get; set; }

        [Required(ErrorMessage = "* กรุณากรอกอีเมล")]
        [Display(Name = "USER_EMAIL: ")]
        public string USER_EMAIL { get; set; }

        [Required(ErrorMessage = "* กรุณากรอกเบอร์ติดต่อ")]
        [Display(Name = "USER_EXTENSION: ")]
        public string USER_EXTENSION { get; set; }
        public string USER_ROLE { get; set; }

        [Required(ErrorMessage = "* กรุณากรอกชื่ออังกฤษ")]
        [Display(Name = "NAME_ENG: ")]
        public string NAME_ENG { get; set; }

        [Required(ErrorMessage = "* กรุณากรอกตำแหน่ง")]
        [Display(Name = "PROSITION: ")]
        public string PROSITION { get; set; }

        [Required(ErrorMessage = "* กรุณากรอกฝ่าย")]
        [Display(Name = "DEPARTMENT: ")]
        public string DEPARTMENT { get; set; }
    }
}