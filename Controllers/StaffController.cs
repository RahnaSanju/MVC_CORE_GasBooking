using Microsoft.AspNetCore.Mvc;
using MVC_CORE_Project.Models;

namespace MVC_CORE_Project.Controllers
{
    public class StaffController : Controller
    {
        StaffDB dbObj=new StaffDB();
        public IActionResult Staff_Insert_Load()
        {
            return View();
        }

        public IActionResult Staff_Insert_Click(StaffCls objCls)
        {
            try
            {
                int stat = dbObj.CheckUsernameExistsDB(objCls);
                if (stat == 0)
                {
                    string resp = dbObj.InsertStaffDB(objCls);
                    TempData["msg"] = resp;
                }
                else
                {
                    TempData["msg"] = "Username already exist";
                }
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
            }

            return View("Staff_Insert_Load", objCls);
        }
    }
}
