using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AftaScool.Models;

namespace AftaScool.Controllers
{
    public class HomeController : TCRControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetEmployees()
        {
            return null;

        }

      
    }
}