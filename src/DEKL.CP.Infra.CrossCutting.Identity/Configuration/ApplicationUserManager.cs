using DEKL.CP.Infra.CrossCutting.Identity.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.DataProtection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DEKL.CP.Infra.CrossCutting.Identity.Configuration
{
    public sealed class ApplicationUserManager : UserManager<ApplicationUser, int>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser, int> store)
        : base(store)
        {
            // Configurando validator para nome de usuario
            UserValidator = new UserValidator<ApplicationUser, int>(this)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Logica de validação e complexidade de senha
            PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false,
            };

            // Configuração de Lockout
            UserLockoutEnabledByDefault = true;
            DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            MaxFailedAccessAttemptsBeforeLockout = 5;

            // Providers de Two Factor Autentication
            RegisterTwoFactorProvider("Código via SMS", new PhoneNumberTokenProvider<ApplicationUser, int>
            {
                MessageFormat = "Seu código de segurança é: {0}"
            });

            RegisterTwoFactorProvider("Código via E-mail", new EmailTokenProvider<ApplicationUser, int>
            {
                Subject = "Código de Segurança",
                BodyFormat = "Seu código de segurança é: {0}"
            });

            // Definindo a classe de serviço de e-mail
            EmailService = new EmailService();

            // Definindo a classe de serviço de SMS
            SmsService = new SmsService();

            var provider = new DpapiDataProtectionProvider("Thiago");
            var dataProtector = provider.Create("ASP.NET Identity");

            UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser, int>(dataProtector);
        }

        // Metodo para login async que guarda os dados Client conectado
        public async Task<IdentityResult> SignInClientAsync(ApplicationUser user, string clientKey)
        {
            if (string.IsNullOrEmpty(clientKey))
            {
                throw new ArgumentNullException(nameof(clientKey));
            }

            var client = user.Clients.SingleOrDefault(c => c.ClientKey == clientKey);
            if (client == null)
            {
                client = new Client() { ClientKey = clientKey };
                user.Clients.Add(client);
            }

            var result = await UpdateAsync(user);
            user.CurrentClientId = client.Id.ToString();
            return result;
        }

        // Metodo para login async que remove os dados Client conectado
        public async Task<IdentityResult> SignOutClientAsync(ApplicationUser user, string clientKey)
        {
            if (string.IsNullOrEmpty(clientKey))
            {
                throw new ArgumentNullException(nameof(clientKey));
            }

            var client = user.Clients.SingleOrDefault(c => c.ClientKey == clientKey);
            if (client != null)
            {
                user.Clients.Remove(client);
            }

            user.CurrentClientId = null;
            return await UpdateAsync(user);
        }
    }
}