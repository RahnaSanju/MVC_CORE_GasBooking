namespace MVC_CORE_Project.Models
{
    public class DeliveryCls:CustomerCls
    {
        public int delId { get; set; }
        public int bookId { get; set; }
        public string? BkDate { get; set; }
        public int staffId { get; set; }
        public string? delDate {get;set;}
        public int cylId { get; set; }
        public string? cylType { get; set; }
        public double? cylAmt { get; set; }

    }
}
