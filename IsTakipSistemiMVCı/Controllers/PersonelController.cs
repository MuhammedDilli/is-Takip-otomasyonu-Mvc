using IsTakipSistemiMVCı.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IsTakipSistemiMVCı.Controllers
{
    public class PersonelController : Controller
    {
        IsTakipDBEntities0 entity = new IsTakipDBEntities0();
        // GET: Personel
        public ActionResult Index()
        {
            var personeller=(from p in entity.Personeller where p.personelYetkiTurId !=3 select p).ToList();

            return View(personeller);
        }
    }
}