using Microsoft.AspNetCore.Mvc;
using MVC_CORE_Project.Models;

namespace MVC_CORE_Project.Controllers
{
    public class CustomerController : Controller
    {

        private readonly IWebHostEnvironment _hostingEnvironment;
        public CustomerController(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        CustomerDB dbObj=new CustomerDB();
        public IActionResult Cust_Insert_Load()
        {
            return View();
        }

        public IActionResult Cust_Insert_Click(CustomerCls objCls,IFormFile prfPhoto)
        {
            try
            {
                if (prfPhoto != null && prfPhoto.Length > 0)
                {
                    string uploadFolder = Path.Combine(_hostingEnvironment.WebRootPath, "Photos");
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(prfPhoto.FileName);
                    string filePath = Path.Combine(uploadFolder, uniqueFileName);
                    using (var stream = System.IO.File.Create(filePath))
                    {
                        prfPhoto.CopyTo(stream);
                    }
                    objCls.custPhoto = "/Photos/" + uniqueFileName;


                    int stat = dbObj.CheckUsernameExistsDB(objCls);
                    if (stat == 0)
                    {
                        string resp = dbObj.InsertCustomerDB(objCls);
                        TempData["msg"] = resp;
                        TempData["regid"] = objCls.custId;
                        TempData["logtype"]= objCls.custLogType;
                        RedirectToAction("CustomerHome", "Login");
                    }
                    else
                    {
                        TempData["msg"] = "Username already exist";
                    }

                }

            }
            catch (Exception ex)
            {
                TempData["msg"] =ex.Message;
            }
           
            return View("Cust_Insert_Load",objCls);
        }

    }
}
