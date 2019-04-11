using DEKL.CP.Domain.Contracts.Repositories;
using DEKL.CP.Infra.CrossCutting.Identity.Configuration;
using DEKL.CP.Infra.CrossCutting.Identity.Context;
using DEKL.CP.Infra.CrossCutting.Identity.Models;
using DEKL.CP.Infra.Data.EF.Context;
using DEKL.CP.Infra.Data.EF.Repositories;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SimpleInjector;

namespace DEKL.CP.Infra.CrossCutting.IoC
{
    public class BootStrapper
    {
        public static void RegisterServices(Container container)
        {
            container.Register<DEKLCPDataContextEF>(Lifestyle.Scoped);
            container.Register<ApplicationDbContext>(Lifestyle.Scoped);
            container.Register<IUserStore<ApplicationUser, int>>(()
                => new UserStore<ApplicationUser, ApplicationRole, int, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>(
                    new ApplicationDbContext()), Lifestyle.Scoped
                );
            container.Register<IRoleStore<ApplicationRole, int>>(()
                => new RoleStore<ApplicationRole, int, ApplicationUserRole>(new ApplicationDbContext()), Lifestyle.Scoped);
            container.Register<ApplicationRoleManager>(Lifestyle.Scoped);
            container.Register<ApplicationUserManager>(Lifestyle.Scoped);
            container.Register<ApplicationSignInManager>(Lifestyle.Scoped);
            container.Register<IStateRepository, StateRepositoryEF>(Lifestyle.Scoped);
            container.Register<IBankRepository, BankRepositoryEF>(Lifestyle.Scoped);
            container.Register<IBankAgencyRepository, BankAgencyRepositoryEF>(Lifestyle.Scoped);
            container.Register<IBankTransactionRepository, BankTransactionRepositoryEF>(Lifestyle.Scoped);
            container.Register<IInternalBankAccountRepository, InternalBankAccountRepositoryEF>(Lifestyle.Scoped);
            container.Register<IProviderBankAccountRepository, ProviderBankAccountRepositoryEF>(Lifestyle.Scoped);
            container.Register<IProviderRepository, ProviderRepositoryEF>(Lifestyle.Scoped);
            container.Register<IAccountToPayRepository, AccountToPayRepositoryEF>(Lifestyle.Scoped);
            container.Register<IReportRepository, ReportRepositoryEF>(Lifestyle.Scoped);            
            container.Register<IAuditRepository, AuditRepositoryEF>(Lifestyle.Scoped);
        }
    }
}