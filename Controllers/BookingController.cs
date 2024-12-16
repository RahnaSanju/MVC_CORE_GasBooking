using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVC_CORE_Project.Models;
using System.Reflection.Metadata.Ecma335;

namespace MVC_CORE_Project.Controllers
{
    public class BookingController : Controller
    {
        BookingDB dbObj = new BookingDB();
        public IActionResult Customer_Booking_PageLoad()
        {
            //dropdownlist
            List<Booking_Cyl_Type> cylTypeList = new List<Booking_Cyl_Type>
            {
                new Booking_Cyl_Type{cId=1,cType="Commercial"},
                new Booking_Cyl_Type{ cId=2,cType="Domestic"},
            };
            ViewBag.selCylType = new SelectList(cylTypeList, "cId", "cType");

            BookingCls objCls = new BookingCls();
            return View(objCls);
        }

        public IActionResult Customer_Check_Booking_Status()
        {
            int regid = Convert.ToInt32(TempData["regid"]);
            int status = dbObj.GetCustomerBookingStatus(regid);
            if (status == 1)
            {
                TempData["regid"] = regid;
                TempData["msg"] = "Booking is already active";
                return View("CustomerHome");
            }
            return View("Customer_Booking_PageLoad");

        }
        public IActionResult Customer_Booking_Insert_Click(BookingCls objCls, IFormCollection form)
        {
            List<Booking_Cyl_Type> cylTypeList = new List<Booking_Cyl_Type>
            {
                new Booking_Cyl_Type{cId=1,cType="Commercial"},
                new Booking_Cyl_Type{ cId=2,cType="Domestic"}
            };
            ViewBag.selCylType = new SelectList(cylTypeList, "cId", "cType");

            if (Convert.ToInt32(TempData["regid"]) != 0)
            {
                if (dbObj.GetCustomerBookingStatus(Convert.ToInt32(TempData["regid"])) == 1)
                {
                    TempData["msg"] = "Booking is already active";
                }
                else
                {
                    int selectedId = Convert.ToInt32(form["ddlBookingCylType"]);
                    Booking_Cyl_Type selectedItem = cylTypeList.FirstOrDefault(c => c.cId == selectedId);
                    objCls.cId = selectedItem.cId; //set
                    objCls.cType = selectedItem.cType; //set
                    //objCls.CylinderType = selectedItem.cType;

                    objCls.CylinderId = selectedItem.cId;
                    objCls.CustId = Convert.ToInt32(TempData["regid"]);
                    objCls.StaffId = 1;
                    objCls.BookingDate = System.DateTime.Today.Date.ToShortDateString();
                    objCls.BookingStatus = "Booked";
                    objCls.BookingMode = "online";

                    string resp = dbObj.InsertBookingDB(objCls);
                    TempData["regid"] = objCls.CustId;
                    TempData["msg"] = resp;

                }
            }

            return View("Customer_Booking_PageLoad", objCls);

        }

        public IActionResult Staff_Booking_PageLoad()
        {
            //dropdownlist
            List<Booking_Cyl_Type> cylTypeList = new List<Booking_Cyl_Type>
            {
                new Booking_Cyl_Type{cId=1,cType="Commercial"},
                new Booking_Cyl_Type{ cId=2,cType="Domestic"},
            };
            ViewBag.selCylType = new SelectList(cylTypeList, "cId", "cType");

            BookingCls objCls = new BookingCls();
            return View(objCls);
        }

        public IActionResult Staff_Cancel_Booking_PageLoad()
        {
            return View();
        }

        public IActionResult Staff_Cancel_Booking_Click(BookingCls objCls, IFormCollection form)
        {

            TempData["consno"] = objCls.ConsumerNo;
            objCls.CustId = getCustomerId(objCls.ConsumerNo);
            if (objCls.CustId != 0)
            {
                if (dbObj.GetCustomerBookingStatus(objCls.CustId) == 1)
                {
                    TempData["msg"] = dbObj.CancelBookingDB(objCls.CustId);
                    return View ("Staff_Cancel_Booking_PageLoad");
                }
                else
                {
                    TempData["msg"] = "No active booking";
                }
            }

            return View("Staff_Cancel_Booking_PageLoad", objCls);

        }

        public IActionResult Staff_Booking_Insert_Click(BookingCls objCls, IFormCollection form)
        {
            List<Booking_Cyl_Type> cylTypeList = new List<Booking_Cyl_Type>
            {
                new Booking_Cyl_Type{cId=1,cType="Commercial"},
                new Booking_Cyl_Type{ cId=2,cType="Domestic"},
            };
            ViewBag.selCylType = new SelectList(cylTypeList, "cId", "cType");

            TempData["consno"] = objCls.ConsumerNo;
            objCls.CustId = getCustomerId(objCls.ConsumerNo);
            if(objCls.CustId != 0 )
            {
                if (dbObj.GetCustomerBookingStatus(objCls.CustId) == 1)
                {
                    TempData["msg"] = "Already Booked";
                }
                else
                {
                    int selectedId = Convert.ToInt32(form["ddlBookingCylType"]);
                    Booking_Cyl_Type selectedItem = cylTypeList.FirstOrDefault(c => c.cId == selectedId);
                    objCls.cId = selectedItem.cId; //set
                    objCls.cType = selectedItem.cType; //set
                    //objCls.CylinderType = selectedItem.cType;

                    objCls.CylinderId = selectedItem.cId;
                    objCls.StaffId = Convert.ToInt32(TempData["regid"]);
                    objCls.BookingDate = System.DateTime.Today.Date.ToShortDateString();
                    objCls.BookingStatus = "Booked";
                    objCls.BookingMode = "offline";

                    string resp = dbObj.InsertBookingDB(objCls);
                    TempData["msg"] = resp;

                }
            }
            
            return View("Staff_Booking_PageLoad", objCls);

        }


        public IActionResult getCustomerDetails_Click(BookingCls objCls)
        {

            dbObj.getCustomerDetails(Convert.ToInt32(objCls.ConsumerNo));
            return View("Staff_Booking_PageLoad", objCls);
        }


        public int getCustomerId(int ConsNo)
        {
            try
            {
                //@consNo= ConsNo;
                int custId = dbObj.GetCustomerID(ConsNo);
                if (custId == 0)
                {
                    TempData["msg"] = "Incorrect Consumer Number";
                }
                return custId;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

        public IActionResult Customer_View_PageLoad()
        {
            //dropdownlist
            List<Booking_Cyl_Type> cylTypeList = new List<Booking_Cyl_Type>
            {
                new Booking_Cyl_Type{cId=1,cType="Commercial"},
                new Booking_Cyl_Type{ cId=2,cType="Domestic"},
            };
            ViewBag.selCylType = new SelectList(cylTypeList, "cId", "cType");

            List<Booking_Mode> BookModeList = new List<Booking_Mode>
            {
                new Booking_Mode{bId=1,bMode="Online"},
                new Booking_Mode{bId=2,bMode="Offline"}
            };
            ViewBag.selBookMode = new SelectList(BookModeList, "bId", "bMode");

            List<Booking_Status> BookStatusList = new List<Booking_Status>
            {
                new Booking_Status{bkId=1,bkStatus="Booked" },
                new Booking_Status{bkId=2,bkStatus="Cancelled" },
                new Booking_Status{bkId=1,bkStatus="Delivered" }
            };
            ViewBag.selBookStatus = new SelectList(BookStatusList, "bkId", "bkStatus");

            string qry = "and 1=1";
            List<BookingCls> getlist = dbObj.getCustomerDetailsSearch(qry);
            return View(getlist);
            //BookingCls objCls = new BookingCls();
            //return View(objCls);
        }

        public IActionResult SearchCustomer_Click(BookingCls objCls, IFormCollection form)
        {
            string qry = "";

            List<Booking_Cyl_Type> cylTypeList = new List<Booking_Cyl_Type>
            {
                new Booking_Cyl_Type{cId=1,cType="Commercial"},
                new Booking_Cyl_Type{ cId=2,cType="Domestic"},
            };
            ViewBag.selCylType = new SelectList(cylTypeList, "cId", "cType");

            List<Booking_Mode> BookModeList = new List<Booking_Mode>
            {
                new Booking_Mode{bId=1,bMode="Online"},
                new Booking_Mode{bId=2,bMode="Offline"}
            };
            ViewBag.selBookMode = new SelectList(BookModeList, "bId", "bMode");

            List<Booking_Status> BookStatusList = new List<Booking_Status>
            {
                new Booking_Status{bkId=1,bkStatus="Booked" },
                new Booking_Status{bkId=2,bkStatus="Cancelled" },
                new Booking_Status{bkId=3,bkStatus="Delivered" }
            };
            ViewBag.selBookStatus = new SelectList(BookStatusList, "bkId", "bkStatus");

            //Cylinder Type
            var selectedValue = form["ddlCylinderType"];
            if (!string.IsNullOrEmpty(selectedValue))
            {
                int selCylId = Convert.ToInt32(form["ddlCylinderType"]);
                Booking_Cyl_Type selectedCylinderType = cylTypeList.FirstOrDefault(c => c.cId == selCylId);
                objCls.cId = selectedCylinderType.cId; //set
                objCls.cType = selectedCylinderType.cType; //set
            }


            //Booking_Mode
            selectedValue = form["ddlBookingMode"];
            if (!string.IsNullOrEmpty(selectedValue))
            {
                int selBookId = Convert.ToInt32(form["ddlBookingMode"]);
                Booking_Mode selectedBookingMode = BookModeList.FirstOrDefault(b => b.bId == selBookId);
                objCls.bId = selectedBookingMode.bId; //set
                objCls.bMode = selectedBookingMode.bMode; //set
            }



            //Booking_Status
            selectedValue = form["ddlBookingStatus"];
            if (!string.IsNullOrEmpty(selectedValue))
            {
                int selStatusId = Convert.ToInt32(form["ddlBookingStatus"]);
                Booking_Status selectedBookingStatus = BookStatusList.FirstOrDefault(b => b.bkId == selStatusId);
                objCls.bkId = selectedBookingStatus.bkId; //set
                objCls.bkStatus = selectedBookingStatus.bkStatus; //set
            }

            //FromDate to ToDate
            var fromDate = form["fromDate"];
            var endDate = form["endDate"];
            if (!string.IsNullOrEmpty(fromDate) && !string.IsNullOrEmpty(endDate))
            {
                DateTime parsedFromDate;
                DateTime parsedEndDate;

                if (DateTime.TryParse(fromDate, out parsedFromDate) && DateTime.TryParse(endDate, out parsedEndDate))
                {
                    qry += " and b.Bk_Date between '" + parsedFromDate + "' and '" + parsedEndDate + "'";
                }
                else
                {
                    throw new Exception("Invalid date format");
                }
            }

            if (objCls.cId>0)
            {
                switch(objCls.cId) //Cylinder Type
                {
                    case 1:
                        {
                            qry += "and ct.Cyl_Id=1";
                            break;
                        }
                    case 2:
                        {
                            qry += "and ct.Cyl_Id=2";
                            break;
                        }
                }
            }
            if (objCls.bId>0) //Booking Mode
            {
                switch (objCls.bId)
                {
                    case 1:
                        {
                            qry += "and b.Bk_Mode = 'online'";
                            break;
                        }
                    case 2:
                        {
                            qry += "and b.Bk_Mode = 'offline'";
                            break;
                        }
                }
            }

            if (objCls.bkId > 0) //Booking Status
            {
                switch (objCls.bkId)
                {
                    case 1:
                        {
                            qry += "and b.Bk_Status='Booked'";
                            break;
                        }
                    case 2:
                        {
                            qry += "and b.Bk_Status='Cancelled'";
                            break;
                        }
                    case 3:
                        {
                            qry += "and b.Bk_Status='Delivered'";
                            break;
                        }
                }
            }


            List<BookingCls> getlist = dbObj.getCustomerDetailsSearch(qry);


            return View("Customer_View_PageLoad", getlist);

        }

    }
}
