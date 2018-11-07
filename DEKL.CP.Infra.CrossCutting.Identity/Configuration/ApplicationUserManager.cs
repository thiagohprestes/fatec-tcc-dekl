using DEKL.CP.Infra.CrossCutting.Identity.Context;
using DEKL.CP.Infra.CrossCutting.Identity.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.DataProtection;
using System;

namespace DEKL.CP.Infra.CrossCutting.Identity.Configuration
{
    public class ApplicationUserManager : UserManager<ApplicationUser, int>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser, int> store)
        : base(store)
        { }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            var manager = new ApplicationUserManager(new CustomUserStore(context.Get<ApplicationDbContext>()));

            // Configurando validator para nome de usuario
            manager.UserValidator = new UserValidator<ApplicationUser, int>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Logica de validação e complexidade de senha
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false,
            };

            // Configuração de Lockout
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            // Providers de Two Factor Autentication
            manager.RegisterTwoFactorProvider("Código via SMS", new PhoneNumberTokenProvider<ApplicationUser, int>
            {
                MessageFormat = "Seu código de segurança é: {0}"
            });

            manager.RegisterTwoFactorProvider("Código via E-mail", new EmailTokenProvider<ApplicationUser, int>
            {
                Subject = "Código de Segurança",
                BodyFormat = "Seu código de segurança é: {0}"
            });

            // Definindo a classe de serviço de e-mail
            manager.EmailService = new EmailService();

            // Definindo a classe de serviço de SMS
            manager.SmsService = new SmsService();

            var provider = new DpapiDataProtectionProvider("Thiago");
            var dataProtector = provider.Create("ASP.NET Identity");

            manager.UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser, int>(dataProtector);

            return manager;
        }
    }
}