using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IsTakipSistemiMVCı.Filters;
using IsTakipSistemiMVCı.Models;
namespace IsTakipSistemiMVCı.Controllers
{
    public class LoginController : Controller
    {
        IsTakipDBEntities0 entity = new IsTakipDBEntities0(); 
        // GET: Login
        public ActionResult Index()
        {
            ViewBag.mesaj = null;
            return View();
        }
        [HttpPost,ExcFilter]
        public ActionResult Index(string kullaniciAd, string parola)
        {
             var personel=(from p in entity.Personeller where p.personelKullaniciAd==kullaniciAd  && 
                           p.personelParola== parola select p).FirstOrDefault();


            if (personel!=null) 
            {
                var birim=(from b in entity.Birimler where b.birimId==personel.personelBirimId  select b).FirstOrDefault();
                Session["PersonelAdSoyad"] = personel.personelAdSoyad;
                Session["PersonelId"] = personel.personelId;
                Session["PersonelBirimId"] = personel.personelBirimId;
                Session["PersonelYetkiTurId"] = personel.personelYetkiTurId;

                if (birim == null)
                {
                    return RedirectToAction("Index", "SistemYoneticisi");
                }

                if (birim.aktiflik == true)
                {
                   

                    switch (personel.personelYetkiTurId)
                    {
                        case 1:
                            return RedirectToAction("Index", "Yonetici");

                        case 2:
                            return RedirectToAction("Index", "Calisan");
                       

                        default:
                            return View();
                    }
                }
                else
                {
                    ViewBag.mesaj = "Biriminiz Silindiği için Giriş yapamassınız";
                    return View();
                }

            }
            else
            {
                ViewBag.mesaj = "kullanıcı adı ya da parola yanlış";
                return View();
            }

        }
    }
}