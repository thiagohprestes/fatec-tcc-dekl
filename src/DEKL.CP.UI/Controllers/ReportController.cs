using AutoMapper;
using DEKL.CP.Domain.Contracts.Repositories;
using DEKL.CP.Domain.Entities.Filters;
using DEKL.CP.UI.ViewModels;
using DEKL.CP.UI.ViewModels.AccountsToPay;
using DEKL.CP.UI.ViewModels.Provider;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace DEKL.CP.UI.Controllers
{
    public class ReportController : Controller
    {
        private readonly IReportRepository _reportRepository;
        private readonly DateTime _YESTERDAY = DateTime.Now.AddDays(-1);
        private readonly DateTime _TODAY = DateTime.Now;

        public ReportController(IReportRepository reportRepository) => _reportRepository = reportRepository;

        public ActionResult AccountToPayReport()
        {
            var accountToPayFilter = new AccountsToPayFilter
            {
                InitialDate = _YESTERDAY,
                FinalDate = _TODAY
            };

           return View(Mapper.Map<IEnumerable<AccountToPayRelashionships>>
                (_reportRepository.AccountToPayReport(accountToPayFilter)));
        }   
        
        [HttpPost]
        public JsonResult SearchAccountToPayReport(AccountsToPayFilter accountToPayFilter)
        {
            var accountToPay = Mapper.Map<IEnumerable<AccountToPayRelashionships>>
                (_reportRepository.AccountToPayReport(accountToPayFilter));

            return Json(new { AccountToPay = accountToPay }, JsonRequestBehavior.AllowGet);
        }
                 
        public ActionResult ProviderReport() => View(Mapper.Map<IEnumerable<ProviderPhysicalLegalPersonViewModel>>
            (_reportRepository.ProviderReport()));

        public ActionResult BankTransactionReport() => View(Mapper.Map<IEnumerable<BankTransactionViewModel>>
            (_reportRepository.BankTransactionReport(_YESTERDAY, _TODAY)));
    }
}