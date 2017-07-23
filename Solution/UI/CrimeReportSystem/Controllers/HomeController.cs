using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CrimeReportSystem.Models;

namespace CrimeReportSystem.Controllers
{
    public class HomeController : TCRControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetEmployees()
        {
            List<EmployeeModel> list = new List<EmployeeModel>()
            {
                new EmployeeModel()
                {
                    FirstName = "Kefilwe",
                    Surname = "Mkhwanazi"

                },
                new EmployeeModel()
                {
                    FirstName = "Andy",
                    Surname = "Bernard"
                }
                
            };
            return SerializeToAngular(list);

        }

        [HttpPost]
        public ActionResult SaveEmployee(EmployeeModel model)
        {
            return new HttpStatusCodeResult(200, "Success");
        }

      
    }
}