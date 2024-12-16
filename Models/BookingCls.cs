namespace MVC_CORE_Project.Models
{
    public class Booking_Cyl_Type
    {
        public int cId { get; set; }
        public string? cType { get; set; }
    }

    public class Booking_Mode
    {
        public int bId { get; set; }
        public string? bMode { get; set; }
    }

    public class Booking_Status
    {
        public int bkId { get; set; }
        public string? bkStatus { get; set; }
    }
    public class BookingCls : CustomerCls
    {
        public int cId { get; set; }
        public string? cType { get; set; }

        public int bId { get; set; }
        public string? bMode { get; set; }
        public int bkId { get; set; }
        public string? bkStatus { get; set; }

        public int CylinderId { get; set; }
        public string? CylinderType { get; set; }
        public int ConsumerNo { get; set; }
        public int CustId { get; set; }
        public int StaffId { get; set; }
        public string? BookingDate { get; set; }
        public string? BookingStatus { get; set; }
        public string? BookingMode{ get; set; }


    }
}
