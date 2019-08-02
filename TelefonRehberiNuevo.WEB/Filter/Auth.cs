using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TelefonRehberiNuevo.WEB.Models;

namespace TelefonRehberiNuevo.WEB.Filter
{
    public class Auth : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (CurrentSession.User == null || CurrentSession.User.Rol != "Admin")
            {
                filterContext.Result = new RedirectResult("/Kullanici/Kisiler");
            }
        }
    }
}