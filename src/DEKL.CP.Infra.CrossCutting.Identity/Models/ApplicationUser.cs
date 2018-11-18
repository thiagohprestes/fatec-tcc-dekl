using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Security.Claims;
using System.Threading.Tasks;
using DEKL.CP.Domain.Contracts.Entities;

namespace DEKL.CP.Infra.CrossCutting.Identity.Models
{
    public class ApplicationUser : IdentityUser<int, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>, IEntityBase
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime AddedDate { get; set; } = DateTime.Now;
        public DateTime? ModifiedDate { get; set; }
        public bool Active { get; set; } = true;
        public virtual ICollection<Client> Clients { get; set; } = new Collection<Client>();

        public string CurrentClientId { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser, int> manager, ClaimsIdentity ext = null)
        {
            // Observe que o authenticationType precisa ser o mesmo que foi definido em CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);

            var claims = new List<System.Security.Claims.Claim>();

            if (!string.IsNullOrEmpty(CurrentClientId))
            {
                claims.Add(new System.Security.Claims.Claim("AspNet.Identity.ClientId", CurrentClientId));
            }

            //  Adicione novos Claims aqui //

            // Adicionando Claims externos capturados no login
            if (ext != null)
            {
                SetExternalProperties(userIdentity, ext);
            }

            // Gerenciamento de Claims para informaçoes do usuario
            //claims.Add(new Claim("AdmRoles", "True"));

            userIdentity.AddClaims(claims);

            return userIdentity;
        }

        private static void SetExternalProperties(ClaimsIdentity identity, ClaimsIdentity ext)
        {
            if (ext == null)
            {
                return;
            }

            const string ignoreClaim = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims";
            // Adicionando Claims Externos no Identity
            foreach (var c in ext.Claims)
            {
                if (c.Type.StartsWith(ignoreClaim))
                {
                    continue;
                }

                if (!identity.HasClaim(c.Type, c.Value))
                {
                    identity.AddClaim(c);
                }
            }
        }
    }
}