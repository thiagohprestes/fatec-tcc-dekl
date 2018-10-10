using AutoMapper;
using DEKL.CP.Domain.Entities;
using DEKL.CP.Domain.Helpers;
using DEKL.CP.UI.ViewModels;
using System;

namespace DEKL.CP.UI.Mappers
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<LoginVM, Usuario>();
            CreateMap<TrocaSenhaVM, Usuario>();
            CreateMap<UsuarioVM, Usuario>()
                .ForMember(source => source.Senha, dest => dest.MapFrom(u => u.Senha.Encrypt()));
        }
    }
}
