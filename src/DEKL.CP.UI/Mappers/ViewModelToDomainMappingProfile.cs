using AutoMapper;
using DEKL.CP.Domain.Contracts.Entities;
using DEKL.CP.Domain.Entities;
using DEKL.CP.Infra.CrossCutting.Identity.ViewModels;
using DEKL.CP.UI.ViewModels.AccountsToPay;
using DEKL.CP.UI.ViewModels.Address;
using DEKL.CP.UI.ViewModels.Bank;
using DEKL.CP.UI.ViewModels.BankAgency;
using DEKL.CP.UI.ViewModels.InternalBankAccount;
using DEKL.CP.UI.ViewModels.Provider;
using DEKL.CP.UI.ViewModels.UsersAdmin;

namespace DEKL.CP.UI.Mappers
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<ApplicationUsersViewModel, ApplicationUser>();
            CreateMap<RegisterViewModel, ApplicationUser>();
            CreateMap<AddressViewModel, Address>();
            CreateMap<BankViewModel, Bank>();
            CreateMap<BankAgencyViewModel, BankAgency>();
            CreateMap<ProviderBankAccountViewModel, ProviderBankAccount>();
            CreateMap<ProviderBankAccountRelashionshipsViewModel, IProviderBankAccountRelashionships>();
            CreateMap<InternalBankAccountRelashionshipsViewModel, IInternalBankAccountRelashionships>();
            CreateMap<InternalBankAccountViewModel, InternalBankAccount>();
            CreateMap<ProviderViewModel, Provider>();
            CreateMap<ProviderPhysicalPersonViewModel, ProviderPhysicalPerson>();
            CreateMap<ProviderLegalPersonViewModel, ProviderLegalPerson>();
            CreateMap<ProviderPhysicalLegalPersonViewModel, IProviderPhysicalLegalPerson>();
            CreateMap<AccountToPayViewModel, AccountToPay>();

        }
    }
}