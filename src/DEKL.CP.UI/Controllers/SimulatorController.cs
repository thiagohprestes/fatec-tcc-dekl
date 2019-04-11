
using AutoMapper;
using DEKL.CP.Domain.Contracts.Repositories;
using DEKL.CP.UI.ViewModels.AccountsToPay;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;
using System;

namespace DEKL.CP.UI.Controllers
{
    [Authorize]
    public class SimulatorController : BaseController
    {
        private readonly IAccountToPayRepository _accountToPayRepository;

        public SimulatorController(IAccountToPayRepository accountToPayRepository) => _accountToPayRepository = accountToPayRepository;

        public ActionResult Index()
            => View(Mapper.Map<IEnumerable<AccountToPayRelashionships>>(_accountToPayRepository.AccountToPayOpenedRelashionships));

        public JsonResult Simular(int id)
        {
            //todo Alterar para exibir o valor total a pagar das contas selecionadas e o total de juros
            var amountDue = 0M;
            var accountToPay = _accountToPayRepository.Find(id);
            var penaltySum = 0M;
            var dailyInterestSum = 0M;

            //não possuí parcelas
            if (!accountToPay.Installments.Any())
            {
                //conta em atraso
                if (accountToPay.MaturityDate < DateTime.Now && !accountToPay.PaymentDate.HasValue)
                {
                    var daysPastDue = (int)DateTime.Now.Subtract(accountToPay.MaturityDate).TotalDays;

                    penaltySum = accountToPay.Value * accountToPay.Penalty / 100;
                    dailyInterestSum = (accountToPay.Value + penaltySum) * daysPastDue * accountToPay.DailyInterest / 100;
                    amountDue = accountToPay.Value + penaltySum + dailyInterestSum;
                }
            }
            //possuí parcelas
            else
            {
                //parcelas vencidas
                var overdueInstallments = accountToPay.Installments
                                                      .Where(i => i.MaturityDate < DateTime.Now && !i.PaymentDate.HasValue)
                                                      .ToList();

                //parcelas em dia e ainda não pagas
                var installmentsOk = accountToPay.Installments
                                                 .Except(overdueInstallments)
                                                 .Where(i => !i.PaymentDate.HasValue)
                                                 .ToList();

                overdueInstallments.ForEach(o =>
                {
                    decimal penalty;
                    var daysPastDue = (int)DateTime.Now.Subtract(o.MaturityDate).TotalDays;

                    penaltySum += penalty = o.Value * accountToPay.Penalty / 100;
                    dailyInterestSum += (o.Value + penalty) * daysPastDue * accountToPay.DailyInterest / 100;
                });         

                amountDue = penaltySum + dailyInterestSum + installmentsOk.Sum(i => i.Value) + overdueInstallments.Sum(i => i.Value);
            }

            return Json(new { penaltySum, dailyInterestSum, amountDue }, JsonRequestBehavior.AllowGet);
        }
    }
}
