using DEKL.CP.Domain.Entities;

namespace DEKL.CP.Domain.Contracts.Repositories
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        Usuario Get(string email);
    }
}
