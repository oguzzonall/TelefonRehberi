using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc5;
using TelefonRehberiNuevo.DATA.UnitOfWork;
using TelefonRehberiNuevo.SERVICES.Interfaces;
using TelefonRehberiNuevo.SERVICES.Services;

namespace TelefonRehberiNuevo.IoC
{
    public static class UnityConfigMvc
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
        public static void RegisterTypes(IUnityContainer container)
        {
            container.BindInRequstScope<IUnitOfWork, UnitOfWork>();
            container.BindInRequstScope<IKullaniciService, KullaniciService>();
            container.BindInRequstScope<IDepartmanService, DepartmanService>();
            container.BindInRequstScope<IRolService, RolService>();
            //container.BindInRequstScope<IResourceService,ResourceService>();
        }

        public static void BindInRequstScope<T1, T2>(this IUnityContainer container) where T2 : T1
        {
            container.RegisterType<T1, T2>(new HierarchicalLifetimeManager());
        }
    }
}