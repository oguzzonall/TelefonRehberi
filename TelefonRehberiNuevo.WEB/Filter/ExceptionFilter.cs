using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TelefonRehberiNuevo.WEB.Filter
{
    public class ExceptionFilter : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            filterContext.ExceptionHandled = true;
            filterContext.Result = new RedirectResult("/Kullanici/Error");
        }
    }
}