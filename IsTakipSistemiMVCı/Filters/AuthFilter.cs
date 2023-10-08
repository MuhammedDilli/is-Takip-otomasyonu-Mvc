using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IsTakipSistemiMVCı.Filters
{
    public class AuthFilter : FilterAttribute, IAuthorizationFilter
    {
        protected int yetkiTur;  

        public AuthFilter(int yetkiTur)
        {
            this.yetkiTur = yetkiTur;
        } 

        public void OnAuthorization(AuthorizationContext filterContext)
        {

            int yetkiTurId = Convert.ToInt32(filterContext.HttpContext.Session["PersonelYetkiTurId"]);

            if(this.yetkiTur != yetkiTurId)
            {
                filterContext.Result = new RedirectResult("/Login/Index");
            }
        }
    }
}