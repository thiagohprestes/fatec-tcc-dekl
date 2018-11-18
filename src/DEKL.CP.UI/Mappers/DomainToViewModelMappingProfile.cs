using AutoMapper;
using DEKL.CP.Domain.Entities;
using DEKL.CP.UI.ViewModels;

namespace DEKL.CP.UI.Mappers
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<ApplicationUser, LoginVM>();
            CreateMap<ApplicationUser, TrocaSenhaVM>();
            CreateMap<ApplicationUser, UsuarioVM>();
        }
    }
}
