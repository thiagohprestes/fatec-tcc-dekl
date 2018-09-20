using DEKL.CP.Data.EF.Repositories;
using DEKL.CP.Domain.Contracts.Repositories;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;

namespace DEKL.CP.UI
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            container.RegisterType<IUsuarioRepository, UsuarioRepositoryEF>();
            
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}