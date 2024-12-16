using System.ComponentModel.DataAnnotations;

namespace MVC_CORE_Project.Models
{
    public class CustomerCls
    {
        public int custId { get; set; }
        public string? custConsNo { get; set; }
        public string? custName { get; set; }
        public string? custAddr { get; set; }
        public string? custPhone { get; set; }
        public string? custEmail { get; set; }
        public string? custPhoto { get; set; }
        public string? custStatus { get; set; }
        public string? custUsrnm { get; set; }
        public string? custPwd { get; set; }

        [Compare("custPwd" ,ErrorMessage ="Password mismatch")]
        public string? custCnfPwd { get; set; }
        public string? custLogType { get; set; }

    }
}
