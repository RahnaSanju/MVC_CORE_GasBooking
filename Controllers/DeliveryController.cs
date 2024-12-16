using Microsoft.AspNetCore.Mvc;
using MVC_CORE_Project.Models;

namespace MVC_CORE_Project.Controllers
{
    public class DeliveryController : Controller
    {
        DeliveryDB dbObj = new DeliveryDB();
        public IActionResult Delivery_PageLoad()
        {
            List<DeliveryCls> getlist = dbObj.GetDeliveryDetails();
            return View(getlist);
        }

        public IActionResult DeliveredClick(int id)
        {
            DeliveryCls data = new DeliveryCls();
            data=dbObj.getBookingDetails(id);

            dbObj.InsertDeliveryDB(id, DateTime.Now.Date, Convert.ToInt32(TempData["regid"]),data.cylId);
            List<DeliveryCls> getlist = dbObj.GetDeliveryDetails();

            return View("Delivery_PageLoad", getlist);
        }

    }
}
