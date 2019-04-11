using AutoMapper;
using DEKL.CP.Domain.Contracts.Entities;
using DEKL.CP.Domain.Entities;
using DEKL.CP.Infra.CrossCutting.Identity.ViewModels;
using DEKL.CP.UI.ViewModels.AccountsToPay;
using DEKL.CP.UI.ViewModels.Address;
using DEKL.CP.UI.ViewModels.Audit;
using DEKL.CP.UI.ViewModels.Bank;
using DEKL.CP.UI.ViewModels.BankAgency;
using DEKL.CP.UI.ViewModels.InternalBankAccount;
using DEKL.CP.UI.ViewModels.Provider;
using DEKL.CP.UI.ViewModels.Report;
using DEKL.CP.UI.ViewModels.UsersAdmin;

namespace DEKL.CP.UI.Mappers
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<ApplicationUser, ApplicationUsersViewModel>();
            CreateMap<ApplicationUser, RegisterViewModel>();
            CreateMap<Address, AddressViewModel>();
            CreateMap<Bank, BankViewModel>();
            CreateMap<BankAgency, BankAgencyViewModel>();
            CreateMap<ProviderBankAccount, ProviderBankAccountViewModel>();
            CreateMap<IProviderBankAccountRelashionships, ProviderBankAccountRelashionshipsViewModel>();
            CreateMap<IInternalBankAccountRelashionships, InternalBankAccountRelashionshipsViewModel>();
            CreateMap<InternalBankAccount, InternalBankAccountViewModel>();
            CreateMap<Provider, ProviderViewModel>();
            CreateMap<ProviderPhysicalPerson, ProviderPhysicalPersonViewModel>();
            CreateMap<ProviderLegalPerson, ProviderLegalPersonViewModel>();
            CreateMap<IProviderPhysicalLegalPerson, ProviderPhysicalLegalPersonViewModel>();
            CreateMap<AccountToPay, AccountToPayViewModel>();
            CreateMap<IAccountToPayRelashionships, AccountToPayRelashionships>();
            CreateMap<IBankAccountReport, BankAccountViewModel>();
            CreateMap<Audit, AuditViewModel>();
        }
    }
}
