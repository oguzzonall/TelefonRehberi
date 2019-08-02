using Microsoft.Practices.Unity;
using System.Web.Http;
using TelefonRehberiNuevo.DATA.UnitOfWork;
using TelefonRehberiNuevo.SERVICES.Interfaces;
using TelefonRehberiNuevo.SERVICES.Services;
using Unity.WebApi;

namespace TelefonRehberiNuevo.IoC
{
    public static class UnityConfigApi
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers
            container.BindInRequstScope<IUnitOfWork, UnitOfWork>();
            container.BindInRequstScope<IKullaniciService, KullaniciService>();
            container.BindInRequstScope<IDepartmanService, DepartmanService>();
            container.BindInRequstScope<IRolService, RolService>();
            // e.g. container.RegisterType<ITestService, TestService>();

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}