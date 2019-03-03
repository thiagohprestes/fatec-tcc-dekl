using AutoMapper;
using DEKL.CP.Domain.Contracts.Repositories;
using DEKL.CP.Domain.Entities.Filters;
using DEKL.CP.UI.ViewModels.AccountsToPay;
using DEKL.CP.UI.ViewModels.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using DEKL.CP.UI.Extensions;
using DEKL.CP.UI.ViewModels.Report;

namespace DEKL.CP.UI.Controllers
{
    public class ReportController : Controller
    {
        private readonly IReportRepository _reportRepository;
        private readonly DateTime _5MONTHSAGO = DateTime.Now.AddMonths(-5);
        private readonly DateTime _TODAY = DateTime.Now;

        public ReportController(IReportRepository reportRepository) => _reportRepository = reportRepository;

        public ActionResult AccountToPayReport()
        {
            var accountToPayFilter = new AccountsToPayFilter
            {
                InitialDate = _5MONTHSAGO,
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

        public ActionResult BankAccountReport() => View(Mapper.Map<IEnumerable<BankAccountViewModel>>(_reportRepository.BankAccountReport()));

        #region ExportarAccountToPayReport
        public ActionResult ExportarAccountToPayReport()
        {
            try
            {
                var AccountToPayFilter = new AccountsToPayFilter
                {
                    InitialDate = _5MONTHSAGO,
                    FinalDate = _TODAY
                };

                var lista = Mapper.Map<IEnumerable<AccountToPayRelashionships>>(_reportRepository.AccountToPayReport(AccountToPayFilter));
                var listaExportar = lista.Select(item => new ExportarAccountToPayRelashionships
                {
                    DocumentNumber = item.DocumentNumber,
                    MaturityDate = item.MaturityDate,
                    Provider = item.Provider,
                    Value = item.Value
                }).ToList();

                // Retorna os resultados
                var fileName = "AccountToPayReport-" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm") + ".csv";

                Response.Clear();
                Response.ClearHeaders();
                Response.ContentType = "text/csv";

                Response.BufferOutput = true;
                Response.ContentEncoding = Encoding.GetEncoding("ISO-8859-1");

                Response.AddHeader("content-disposition", "attachment; filename=" + fileName);
                Response.AddHeader("pragma", "public");

                Response.Write(Exportar(listaExportar, ";"));
                Response.End();

            }
            catch
            {
                return Json(new { success = false });
            }
            return Json(new { success = true });
        }

        #endregion

        #region ExportarProviderReport

        public ActionResult ExportarProviderReport()
        {
            try
            {
                var lista = Mapper.Map<IEnumerable<ProviderPhysicalLegalPersonViewModel>>(_reportRepository.ProviderReport());
                var ListaExportar = lista.Select(item => new ExportarProviderPhysicalLegalPersonViewModel
                {
                    Id = item.Id,
                    TypeProvider = item.TypeProvider.GetDescription(),
                    Email = item.Email,
                    NameCorporateName = item.NameCorporateName,
                    PhoneNumber = item.PhoneNumber
                }).ToList();

                // Retorna os resultados
                var fileName = $"ExportarProviderReport-{DateTime.Now:yyyy-MM-dd-HH-mm}.csv";

                Response.Clear();
                Response.ClearHeaders();
                Response.ContentType = "text/csv";

                Response.BufferOutput = true;
                Response.ContentEncoding = Encoding.GetEncoding("ISO-8859-1");

                Response.AddHeader("content-disposition", $"attachment; filename= + {fileName}");
                Response.AddHeader("pragma", "public");

                Response.Write(Exportar(ListaExportar, ";"));
                Response.End();

            }
            catch
            {
                return Json(new { success = false });
            }

            return Json(new { success = true });
        }

        #endregion

        #region ExportarBankTransactionReport

        public ActionResult ExportarBankTransactionReport()
        {
            try
            {
                var lista = _reportRepository.BankAccountReport();
                var ListaExportar = lista.Select(item => new ExportarBankAccountViewModel
                {
                    Agency = item.Agency,
                    Balance = item.Balance,
                    Number = item.Number,
                    TypeBankAccount = item.TypeBankAccount.GetDescription()
                }).ToList();

            // Retorna os resultados
            var fileName = $"ExportarProviderReport-{DateTime.Now:yyyy-MM-dd-HH-mm}.csv";

                Response.Clear();
                Response.ClearHeaders();
                Response.ContentType = "text/csv";

                Response.BufferOutput = true;
                Response.ContentEncoding = Encoding.GetEncoding("ISO-8859-1");

                Response.AddHeader("content-disposition", $"attachment; filename={fileName}");
                Response.AddHeader("pragma", "public");

                Response.Write(Exportar(ListaExportar, ";"));

                Response.End();

            }
            catch
            {
                return Json(new { success = false });
            }
            return Json(new { success = true });
        }

        #endregion

        #region CSV

        public static string Exportar<T>(List<T> lista, string delimitador, Dictionary<string, string> replace = null)
        {
            var sb = new StringBuilder();

            var propriedades = typeof(T).GetProperties();

            // header
            foreach (var p in propriedades)
            {
                sb.Append(p.CustomAttributes.FirstOrDefault()?.ConstructorArguments[0].Value);
                sb.Append(delimitador);
            }
            sb.Remove(sb.Length - delimitador.Length, delimitador.Length);
            sb.AppendLine();

            // conteudo
            foreach (var item in lista)
            {
                foreach (var p in propriedades)
                {
                    var conteudo = Convert.ToString(p.GetValue(item, null));

                    //pesquisa em 'conteudo' em 'replace', realiza troca de valor caso seja encontrado
                    if (replace != null && !string.IsNullOrEmpty(conteudo) && replace.ContainsKey(conteudo))
                    {
                        conteudo = replace[conteudo];
                    }

                    sb.Append(conteudo.Replace(delimitador, "")
                        .Replace("\r\n", "")
                        .Replace("\n", "")
                        .Replace("\r", "")
                        .Replace(";", ""));

                    sb.Append(delimitador);

                }

                sb.Remove(sb.Length - delimitador.Length, delimitador.Length);
                sb.AppendLine();
            }

            return sb.ToString();
        }

        #endregion
    }
}