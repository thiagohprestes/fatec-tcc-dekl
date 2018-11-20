using AutoMapper;
using DEKL.CP.Domain.Entities;
using DEKL.CP.Infra.CrossCutting.Identity.ViewModels;
using DEKL.CP.UI.ViewModels.UsersAdmin;

namespace DEKL.CP.UI.Mappers
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            //CreateMap<ApplicationUser, LoginVM>();
            //CreateMap<ApplicationUser, TrocaSenhaVM>();
            //CreateMap<ApplicationUser, UsuarioVM>();
            CreateMap<ApplicationUser, ApplicationUsersViewModel>();
            CreateMap<ApplicationUser, RegisterViewModel>();
        }
    }
}
