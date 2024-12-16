using Microsoft.AspNetCore.Mvc;
using MVC_CORE_Project.Models;
using System.Diagnostics.CodeAnalysis;

namespace MVC_CORE_Project.Controllers
{
    public class LoginController : Controller
    {
        LoginDB dbObj = new LoginDB();
        BookingDB dbObjBooking = new BookingDB();
        public IActionResult Login_PageLoad()
        {
            return View();
        }

        public IActionResult Login_Click(LoginCls objCls)
        {
            try
            {
                int stat = dbObj.CheckUsrnmPwdinDB(objCls);
                if (stat == 1)
                {   
                    TempData["regid"] = dbObj.GetRegIDfromDB(objCls);

                    string logtype = dbObj.CheckLogTypeinDB(objCls);
                    TempData["logtype"] = logtype;
                    TempData["msg"] = null;
                    switch (logtype)
                    {
                        case "Admin":
                            {
                                return (RedirectToAction("AdminHome"));
                                //break;
                            }
                        case "Customer":
                            {
                                return (RedirectToAction("CustomerHome"));
                                //break;

                            }
                        case "Staff":
                            {
                                return (RedirectToAction("StaffHome"));
                                //break; 
                            }
                        default:
                            {
                                return (RedirectToAction("Login_PageLoad"));
                                //break;
                            }
                    }
                }
                else
                {
                    TempData["msg"] = "Invalid username and password";
                }
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
            }
            return View("Login_PageLoad", objCls);
        }

        public IActionResult AdminHome()
        {
            return View();
        }
        public IActionResult StaffHome()
        {
            return View();
        }
        public IActionResult CustomerHome()
        {
            return View();
        }

        public IActionResult Customer_Cancel_Booking_Click()
        {
            TempData["msg"]=dbObjBooking.CancelBookingDB(Convert.ToInt32(TempData["regid"]));
            return (RedirectToAction("CustomerHome"));
        }

        //public IActionResult Customer_Check_Booking_Status()
        //{

        //    int status = dbObj.GetCustomerBookingStatus(Convert.ToInt32(TempData["regid"]));
        //    if (status == 1)
        //    {
        //        TempData["msg"] = "Booking is already active";
        //    }
        //    return View("CustomerHome");

        //}
    }
}
