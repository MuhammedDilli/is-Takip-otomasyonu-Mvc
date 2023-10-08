using IsTakipSistemiMVCı.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using System.Web.UI;

namespace IsTakipSistemiMVCı.Filters
{
    public class ActFilter : FilterAttribute, IActionFilter
    {
        IsTakipDBEntities0 entity = new IsTakipDBEntities0();

        protected string aciklama;
        
        public ActFilter(string actAciklama) 
        {
            this.aciklama = actAciklama;    
        }  
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            Loglar log = new Loglar();
            log.logAciklama = this.aciklama +"  ("+  filterContext.Controller.TempData["Bilgi"] + ")";
            log.actionAd = filterContext.ActionDescriptor.ActionName;
            log.controllerAd = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            log.tarih=DateTime.Now;
            log.personelId = Convert.ToInt32(filterContext.HttpContext.Session["PersonelId"]);
      
            entity.Loglar.Add(log);
            entity.SaveChanges();   
    
        }
     
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            
        }
    }
}