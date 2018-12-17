using System.Text.RegularExpressions;
using AutoMapper;
using DEKL.CP.Domain.Entities;
using DEKL.CP.Infra.CrossCutting.Identity.ViewModels;
using DEKL.CP.UI.ViewModels.Provider;
using DEKL.CP.UI.ViewModels.UsersAdmin;

namespace DEKL.CP.UI.Mappers
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<ApplicationUsersViewModel, ApplicationUser>();
            CreateMap<RegisterViewModel, ApplicationUser>()
                .ForSourceMember(src => src.ConfirmPasswordHash, opt => opt.Ignore())
                .ForMember(dest => dest.PhoneNumber,
                    opt => opt.MapFrom(src => Regex.Replace(src.PhoneNumber, @"[^\d]", "")));
            CreateMap<ProviderViewModel, Provider>();
        }
    }
}