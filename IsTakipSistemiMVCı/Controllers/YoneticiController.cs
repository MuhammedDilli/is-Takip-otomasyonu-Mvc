using IsTakipSistemiMVCı.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace IsTakipSistemiMVCı.Controllers
{
    public class YoneticiController : Controller
    {
        IsTakipDBEntities0 entity = new IsTakipDBEntities0();
        // GET: Yonetici
        public ActionResult Index()
        {
            int yetkiTurId = Convert.ToInt32(Session["PersonelYetkiTurId"]);
            if (yetkiTurId == 1)
            {
                int birimId = Convert.ToInt32(Session["PersonelBirimId"]);
                var birim=(from b in  entity.Birimler where b.birimId== birimId select b ).FirstOrDefault();

                ViewBag.birimAd = birim.birimAd;

                var personeller = from p in entity.Personeller
                                  join i in entity.Isler on p.personelId equals i.isPersonelId into isler
                                  where p.personelBirimId == birimId && p.personelYetkiTurId != 1
                                  select new
                                  {
                                      personelAd = p.personelAdSoyad,
                                      isler = isler
                                  };
                 List <ToplamIs> list = new List<ToplamIs>();   


                foreach(var personel in personeller)
                {
                    ToplamIs toplamIs = new ToplamIs();
                    toplamIs.personelAdSoyad = personel.personelAd;



                    if (personel.isler.Count() == 0)
                    {
                        toplamIs.toplamIs = 0;
                    }
                    else
                    {
                        int toplam =0;
                         foreach(var item in personel.isler)
                        {
                            if(item.yapılanTarih != null)
                            {
                                toplam++;
                            }
                        }
                         toplamIs.toplamIs=toplam;
                    }

                    list.Add(toplamIs); 
                }
                IEnumerable<ToplamIs> siraliListe = new List<ToplamIs>();
                siraliListe = list.OrderByDescending(i => i.toplamIs);


                return View(siraliListe);
            }
            else
            {
                return RedirectToAction("Index","Login");
            }
            
        }
        public ActionResult Ata()
        {
            int yetkiTurId = Convert.ToInt32(Session["PersonelYetkiTurId"]);
            if (yetkiTurId == 1)
            {
                int birimId = Convert.ToInt32(Session["PersonelBirimId"]);
                var calisanlar=(from p in entity.Personeller where p.personelBirimId== birimId   && p.personelYetkiTurId==2 select p).ToList();

                ViewBag.personeller= calisanlar;

                
                var birim = (from b in entity.Birimler where b.birimId == birimId select b).FirstOrDefault();

                ViewBag.birimAd = birim.birimAd;
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }
        [HttpPost]
        public ActionResult Ata(FormCollection formCollection)
        {
            string isBaslik = formCollection["isBaslik"];
            string isAciklama = formCollection["isAciklama"];
            int secilenPersonelId = Convert.ToInt32 (formCollection["selectPer"]);

            Isler yeniIs = new Isler();

             yeniIs.isBaslik =   isBaslik;
            yeniIs.isAciklama=isAciklama;   
            yeniIs.isPersonelId = secilenPersonelId;
            yeniIs.iletilenTarih=DateTime.Now;
            yeniIs.isDurumId = 1;
            yeniIs.isOkunma=false;

            entity.Isler.Add(yeniIs);   
            entity.SaveChanges();



            return RedirectToAction("Takip", "Yonetici");
        }
        public ActionResult Takip()
        {
            int yetkiTurId = Convert.ToInt32(Session["PersonelYetkiTurId"]);
            if (yetkiTurId == 1)
            {
                int birimId = Convert.ToInt32(Session["PersonelBirimId"]);
                var calisanlar = (from p in entity.Personeller where p.personelBirimId == birimId && p.personelYetkiTurId == 2 select p).ToList();

                ViewBag.personeller = calisanlar;


                var birim = (from b in entity.Birimler where b.birimId == birimId select b).FirstOrDefault();

                ViewBag.birimAd = birim.birimAd;
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }
        [HttpPost]
        public ActionResult Takip(int selectPer)
        {
           var  secilenPersonel=(from p in entity.Personeller where p.personelId==selectPer select p).FirstOrDefault();
            TempData["secilen"] = secilenPersonel;

            return RedirectToAction("Listele", "Yonetici");
            }
        [HttpGet]
        public  ActionResult Listele()
        {
            int yetkiTurId = Convert.ToInt32(Session["PersonelYetkiTurId"]);
            if (yetkiTurId == 1)
            {
                Personeller secilenPersonel = (Personeller)TempData["secilen"];
                try
                {
                    var isler = (from i in entity.Isler where i.isPersonelId == secilenPersonel.personelId select i).ToList().OrderByDescending(i => i.iletilenTarih);
                     
                    ViewBag.isler = isler;  
                    ViewBag.personel = secilenPersonel;
                    ViewBag.isSayisi = isler.Count();

                    return View();

                }
                catch (Exception )
                 {
                    return RedirectToAction("Takip","Yonetici");
                        
                }

                
               
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        public ActionResult AyinElemani()
        {
            int yetkiTurId = Convert.ToInt32(Session["PersonelYetkiTurId"]);
             if(yetkiTurId == 1)
            {
                 int simdikiYil=DateTime.Now.Year;  
                 List<int> yillar=new List<int>();  
                for( int i = simdikiYil ; i>=2023 ; i--)
                {
                    yillar.Add(i);
                }

                ViewBag.yillar = yillar;
                 
                return View();

            }
            else
            {
                return RedirectToAction("Index", "Login");
            }

            
        
        } 

    }
}