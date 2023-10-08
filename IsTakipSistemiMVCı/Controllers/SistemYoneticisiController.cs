using IsTakipSistemiMVCı.Filters;
using IsTakipSistemiMVCı.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IsTakipSistemiMVCı.Controllers
{
    public class SistemYoneticisiController : Controller
    {
        IsTakipDBEntities0 entity = new IsTakipDBEntities0();
        // GET: SistemYoneticisi
        [AuthFilter(3)]
        public ActionResult Index()
        {
            return View();
           
        }
        public ActionResult Birim() {
            int yetkiTurId = Convert.ToInt32(Session["PersonelYetkiTurId"]);
            if (yetkiTurId == 3)
            {
                var birimler = (from b in entity.Birimler where b.aktiflik==true  select b).ToList();
                return View(birimler);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }
        
        public ActionResult Olustur () {

            int yetkiTurId = Convert.ToInt32(Session["PersonelYetkiTurId"]);
            if (yetkiTurId==3)
            {
                
                return View();
            }
            else
            {
                return RedirectToAction("Index","Login");
            }

        }
        [HttpPost, ActFilter("Yeni Birim Eklendi")]
        public ActionResult Olustur(string birimAd)
        {
            Birimler yeniBirim=new Birimler();
            string yeniAd=CultureInfo.CurrentCulture.TextInfo.ToTitleCase(birimAd);

            yeniBirim.birimAd = yeniAd ;     
            yeniBirim.aktiflik=true ;
            entity.Birimler.Add(yeniBirim) ;     
            entity.SaveChanges() ;

            TempData["Bilgi"]=yeniBirim.birimAd;  
                
            return RedirectToAction("Birim");    
        }

       
        public ActionResult Güncelle(int id)
        {
            int yetkiTurId = Convert.ToInt32(Session["PersonelYetkiTurId"]);
            if (yetkiTurId == 3)
            {
                var birim=(from b in  entity.Birimler where b.birimId==id select b).FirstOrDefault(); 

                return View(birim);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }

        }   
        [HttpPost, ActFilter("Yeni Birim Eklendi")]
        public ActionResult Güncelle(FormCollection fc)
        {
            int birimId = Convert.ToInt32(fc["birimId"]);
            string yeniAd = fc["birimAd"];

            var birim = (from b in entity.Birimler where b.birimId == birimId select b).FirstOrDefault();
            birim.birimAd = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(yeniAd);
            entity.SaveChanges();
            TempData["Bilgi"]=birim.birimAd;    

            return RedirectToAction("Birim");
                   
                   
        }
        [ActFilter("Yeni Birim Eklendi")]
        public ActionResult Sil(int id)
        {                     
            int yetkiTurId = Convert.ToInt32(Session["PersonelYetkiTurId"]);
            if (yetkiTurId == 3)
            {
                var birim = (from b in entity.Birimler where b.birimId == id select b).FirstOrDefault();
                birim.aktiflik = false;
                entity.SaveChanges();
                TempData["Bilgi"] = birim.birimAd;


                return RedirectToAction("Birim");
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }

        }
        [AuthFilter(3)]
        public ActionResult Loglar()
        {

            var loglar = (from l in entity.Loglar
                          orderby l.tarih
                        descending
                          select l).ToList();
            return View(loglar);
        }
    }

}