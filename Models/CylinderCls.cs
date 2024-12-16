using Microsoft.AspNetCore.Mvc.Rendering;

namespace MVC_CORE_Project.Models
{
    public class Cyl_Type
    {
        public int cId { get; set; }
        public string? cType { get; set; }
    }

    public class CylinderCls
    {
        public int cId { get; set; }
        public string? cType { get; set; }

        public int CylinderId { get; set; }
        public string? CylinderType { get; set; }
        public int TotalStock { get; set; }
        public int Filled { get; set; }
        public int Empty { get; set; }
        public double Amount { get; set; }
    }
}
