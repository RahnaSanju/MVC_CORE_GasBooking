using System.ComponentModel.DataAnnotations;

namespace MVC_CORE_Project.Models
{
    public class StaffCls
    {
        public int sId { get; set; }
        public string? sName { get; set; }
        public string? sPhone { get; set; }
        public string? sEmail { get; set; }
        public string? sStatus { get; set; }
        public string? sUsrnm { get; set; }
        public string? sPwd { get; set; }

        [Compare("sPwd", ErrorMessage = "Password mismatch")]
        public string? sCnfPwd { get; set; }
        public string? sLogType { get; set; }
    }
}
