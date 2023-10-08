using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IsTakipSistemiMVCı.Controllers
{
    public class errorController : Controller
    {
        // GET: error
        public ActionResult Index()
        {
             Exception model = TempData["error"] as Exception;
             
             return View(model);  
        }
    }
}