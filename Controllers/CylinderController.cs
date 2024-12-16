using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVC_CORE_Project.Models;
using System.Data.SqlClient;

namespace MVC_CORE_Project.Controllers
{
    public class CylinderController : Controller
    {
        CylinderDB dbObj=new CylinderDB();
        public IActionResult Cylinder_PageLoad()
        {
            //dropdownlist
            List<Cyl_Type> cylTypeList = new List<Cyl_Type>
            {
                new Cyl_Type{cId=1,cType="Commercial"},
                new Cyl_Type{ cId=2,cType="Domestic"},
            };
            ViewBag.selCylType = new SelectList(cylTypeList, "cId", "cType");

            CylinderCls objCls=new CylinderCls();
            return View(objCls);
        }

        public IActionResult Cylinder_Update_PageLoad()
        {
            //dropdownlist
            List<Cyl_Type> cylTypeList = new List<Cyl_Type>
            {
                new Cyl_Type{cId=1,cType="Commercial"},
                new Cyl_Type{ cId=2,cType="Domestic"},
            };
            ViewBag.selCylType = new SelectList(cylTypeList, "cId", "cType");

            CylinderCls objCls = new CylinderCls();

            objCls.Amount = dbObj.GetCylDetails(objCls.cId, out int totstck, out int fild, out int empt);
            objCls.TotalStock = totstck;
            objCls.Filled = fild;
            objCls.Empty = empt;

            return View(objCls);
        }

        public IActionResult Cylinder_Update_Click(CylinderCls objCls, IFormCollection form)
        {
            List<Cyl_Type> cylTypeList = new List<Cyl_Type>
            {
                new Cyl_Type{cId=1,cType="Commercial"},
                new Cyl_Type{ cId=2,cType="Domestic"},
            };
            ViewBag.selCylType = new SelectList(cylTypeList, "cId", "cType");

            int selectedId = Convert.ToInt32(form["ddlCylType"]);
            Cyl_Type selectedItem = cylTypeList.FirstOrDefault(c => c.cId == selectedId);
            objCls.cId = selectedItem.cId; //set
            objCls.cType = selectedItem.cType; //set
            objCls.CylinderType = selectedItem.cType;

            string resp = dbObj.UpdateCylinderDB(objCls);
            TempData["msg"] = resp;

            return View("Cylinder_Update_PageLoad", objCls);

        }

        public JsonResult GetCylinderDetails(int CylinderId)
        {
            var amt = dbObj.GetCylDetails(CylinderId, out int totstck, out int fild, out int empt);
            return Json(new { amt = amt, totstck= totstck, fild= fild, empt=empt });
        }

        public IActionResult Cylinder_Insert_Click(CylinderCls objCls,IFormCollection form)
        {
            List<Cyl_Type> cylTypeList = new List<Cyl_Type>
            {
                new Cyl_Type{cId=1,cType="Commercial"},
                new Cyl_Type{ cId=2,cType="Domestic"},
            };
            ViewBag.selCylType = new SelectList(cylTypeList, "cId", "cType");

            int selectedId = Convert.ToInt32(form["ddlCylType"]);
            Cyl_Type selectedItem = cylTypeList.FirstOrDefault(c => c.cId == selectedId);
            objCls.cId = selectedItem.cId; //set
            objCls.cType = selectedItem.cType; //set
            objCls.CylinderType = selectedItem.cType;


            string resp = dbObj.InsertCylinderDB(objCls);
            TempData["msg"]=resp;
            

            return View("Cylinder_PageLoad",objCls);


        }
    }
}
