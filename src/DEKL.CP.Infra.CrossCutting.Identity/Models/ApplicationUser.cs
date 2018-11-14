using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using System.Threading.Tasks;
using DEKL.CP.Infra.CrossCutting.Identity.Configuration;

namespace DEKL.CP.Infra.CrossCutting.Identity.Models
{
    public class ApplicationUser : IdentityUser<int, CustomUserLogin, CustomUserRole, CustomUserClaim>
    {
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public DateTime DataCadastro { get; set; } = DateTime.Now;
        public DateTime? DataAlteracao { get; set; }
        public virtual ICollection<Client> Clients { get; set; } = new Collection<Client>();

        [NotMapped]
        public string CurrentClientId { get; set; }


        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(ApplicationUserManager manager, bool isPersistent)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            userIdentity.SetIsPersistent(isPersistent);
            return userIdentity;
        }

        //public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser, int> manager)
        //{
        //    // Observe que o authenticationType precisa ser o mesmo que foi definido em CookieAuthenticationOptions.AuthenticationType
        //    var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);

        //    var claims = new List<System.Security.Claims.Claim>();

        //    if (!string.IsNullOrEmpty(CurrentClientId))
        //    {
        //        claims.Add(new System.Security.Claims.Claim("AspNet.Identity.ClientId", CurrentClientId));
        //    }

        //    //  Adicione novos Claims aqui //

        //    // Gerenciamento de Claims para informaçoes do usuario
        //    //claims.Add(new Claim("AdmRoles", "True"));

        //    userIdentity.AddClaims(claims);

        //    return userIdentity;
        //}
    }
}