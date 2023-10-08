using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IsTakipSistemiMVCı.Filters
{
    public class ExcFilter : FilterAttribute, IExceptionFilter
    {
         public ExcFilter() 
        {        
        }
        public void OnException(ExceptionContext filterContext)
        {            
            filterContext.ExceptionHandled = true;
            filterContext.Controller.TempData["error"] = filterContext.Exception;
            filterContext.Result = new RedirectResult("/error/Index");

        }
    }
}