using System.Web.Mvc;
using DAL.Data;
using DAL.Repository;
using Unity;
using Unity.Mvc5;

namespace UI
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            container.RegisterType<IAdminRepository,AdminRepository>();

            container.RegisterType<ICustomerRepository,CustomerRepository>();
            
            // register all your components with the container here
            // it is NOT necessary to register your controllers
            
            // e.g. container.RegisterType<ITestService, TestService>();
            
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}