using System;
using DEKL.CP.Domain.Entities;

namespace DEKL.CP.Domain.Contracts.Repositories
{
    public interface IApplicationUserRepository : IRepository<ApplicationUser>
    {
        ApplicationUser Get(string email);
    }
}
