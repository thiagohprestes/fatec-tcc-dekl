
using AutoMapper;
using DEKL.CP.Domain.Contracts.Repositories;
using DEKL.CP.Domain.Entities;
using DEKL.CP.UI.ViewModels.AccountsToPay;
using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using System.Linq;
using DEKL.CP.Domain.Enums;
using Microsoft.AspNet.Identity;

namespace DEKL.CP.UI.Controllers
{
    [Authorize]
    public class SimulatorController : BaseController
    {
        private readonly IProviderRepository _providerRepository;
        private readonly IAccountToPayRepository _accountToPayRepository;
        private readonly IAccountToPayRepository _accountToPayRepositoryAdd;
        private readonly IInternalBankAccountRepository _internalBankAccountRepository;
        private readonly IProviderBankAccountRepository _providerBankAccountRepository;

        public SimulatorController(IAccountToPayRepository accountToPayRepository,
                                        IAccountToPayRepository accountToPayRepositoryAdd,
                                      IProviderRepository providerRepository,
                                      IInternalBankAccountRepository internalBankAccountRepository,
                                      IProviderBankAccountRepository providerBankAccountRepository)
        {
            _providerRepository = providerRepository;
            _internalBankAccountRepository = internalBankAccountRepository;
            _providerBankAccountRepository = providerBankAccountRepository;
            _accountToPayRepository = accountToPayRepository;
            _accountToPayRepositoryAdd = accountToPayRepositoryAdd;

        }

        public ActionResult Index()
            => View(Mapper.Map<IEnumerable<AccountToPayRelashionships>>(_accountToPayRepository.AccountToPayActivesRelashionships));

        public JsonResult Simular(int id)
        {
            var objSimulador = Mapper.Map<AccountToPayViewModel>(_accountToPayRepository.Find(id));

            // soma os valores a pagar das parcelas
            decimal totalParcelas = 0;
            var objParcelas = objSimulador.Installments.ToList().FindAll(obj => !obj.PaymentDate.HasValue);
            if (objParcelas.Count > 0) totalParcelas = objParcelas.Sum(objx => objx.Value);

            List<Object> resultado = new List<object>();
            resultado.Add(new
            {
                Parcelas = objParcelas.Count + 1, // quantidade das parcelas mais 1 da parcela atual
                Valor = (objSimulador.Value + totalParcelas)
            });
            
            return Json(resultado, JsonRequestBehavior.AllowGet);
        }
    }
}
