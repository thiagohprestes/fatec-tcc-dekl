using System;
using DEKL.CP.Infra.CrossCutting.Identity.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DEKL.CP.Infra.CrossCutting.Identity.Context
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, CustomRole, int, CustomUserLogin, CustomUserRole, 
        CustomUserClaim>, IDisposable
    {
        public ApplicationDbContext() : base("DEKLCPConnIdentity")
        { }
    }
}