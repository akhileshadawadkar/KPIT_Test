using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Test.Models;

namespace Test.Controllers
{
    public class HomeController : Controller
    {
        LinkHubDBEntities db = new LinkHubDBEntities();
        public ActionResult Index(FormCollection frmcollection)
        {
            var SearchBy = frmcollection.Get("SearchBy");
            var SearchValue = frmcollection.Get("Search");
            List<tbl_Employees> lstEmployees = new List<tbl_Employees>();
            if (SearchBy == "Salary")
            {
                try
                {
                    if (SearchValue != "")
                    {
                        int salary = Convert.ToInt32(SearchValue);
                        lstEmployees = db.tbl_Employees.Where(x => x.Salary == salary || SearchValue == null).ToList();
                        return View(lstEmployees);
                    }
                    else
                        lstEmployees = db.tbl_Employees.ToList();
                }
                catch (FormatException ex)
                {
                    TempData["Msg"] = ex.Message.ToString();
                }
                catch(Exception ex)
                {
                    TempData["Msg"] = ex.Message.ToString();
                }

            }
            if (SearchBy == "Age")
            {
                try
                {
                    if (SearchValue != "")
                    {
                        int age = Convert.ToInt32(SearchValue);
                        lstEmployees = db.tbl_Employees.Where(x => x.Age == age || SearchValue == null).ToList();
                        return View(lstEmployees);
                    }
                    else
                        lstEmployees = db.tbl_Employees.ToList();
                }
                catch (FormatException ex)
                {
                    TempData["Msg"] = ex.Message.ToString();
                }
                catch (Exception ex)
                {
                    TempData["Msg"] = ex.Message.ToString();
                }
            }
            if (SearchBy == "Location")
            {
                try
                {
                    if (SearchValue != "")
                    {
                        string location = SearchValue;
                        lstEmployees = db.tbl_Employees.Where(x => x.Location.Contains(SearchValue) || SearchValue == null).ToList();
                        return View(lstEmployees);
                    }
                    else
                        lstEmployees = db.tbl_Employees.ToList();
                    
                }
                catch (FormatException ex)
                {
                    TempData["Msg"] = ex.Message.ToString();
                }
                catch (Exception ex)
                {
                    TempData["Msg"] = ex.Message.ToString();
                }
            }
            return View(db.tbl_Employees.ToList());
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                tbl_Employees emp = db.tbl_Employees.Where(e => e.ID == id).FirstOrDefault();
                if (emp != null)
                {
                    db.tbl_Employees.Remove(emp);
                    db.SaveChanges();
                    return Content("Deleted Successfully!");
                }
                return Content("Error!");
            }
            catch(Exception ex)
            {
                TempData["Msg"] = ex.Message.ToString();
            }
            return Content("");
        }
    }
}