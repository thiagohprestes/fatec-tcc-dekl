using AutoMapper;
using DEKL.CP.Domain.Entities;
using DEKL.CP.UI.ViewModels;

namespace DEKL.CP.UI.Mappers
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<LoginVM, Usuario>();
            CreateMap<TrocaSenhaVM, Usuario>();
            CreateMap<UsuarioVM, Usuario>();
        }
    }
}
