using AutoMapper;
using DEKL.CP.Domain.Contracts.Repositories;
using DEKL.CP.Domain.Entities.Filters;
using DEKL.CP.UI.ViewModels;
using DEKL.CP.UI.ViewModels.AccountsToPay;
using DEKL.CP.UI.ViewModels.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        #region ExportarAccountToPayReport
        public ActionResult ExportarAccountToPayReport()
        {
            try
            {
                AccountsToPayFilter AccountToPayFilter = new AccountsToPayFilter()
                {
                    InitialDate = _YESTERDAY,
                    FinalDate = _TODAY
                };

                var lista = Mapper.Map<IEnumerable<AccountToPayRelashionships>>(_reportRepository.AccountToPayReport(AccountToPayFilter));
                var ListaExportar = new List<ExportarAccountToPayRelashionships>();

                foreach (var item in lista)
                {
                    var obj = new ExportarAccountToPayRelashionships();
                    obj.Id = item.Id;
                    obj.DocumentNumber = item.DocumentNumber;
                    obj.MaturityDate = item.MaturityDate;
                    obj.Provider = item.Provider;
                    obj.Value = item.Value;
                    ListaExportar.Add(obj);
                }

                // Retorna os resultados
                string fileName = "AccountToPayReport-" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm") + ".csv";

                Response.Clear();
                Response.ClearHeaders();
                Response.ContentType = "text/csv";

                Response.BufferOutput = true;
                Response.ContentEncoding = System.Text.ASCIIEncoding.GetEncoding("ISO-8859-1");

                Response.AddHeader("content-disposition", "attachment; filename=" + fileName);
                Response.AddHeader("pragma", "public");

                Response.Write(exportar<ExportarAccountToPayRelashionships>(ListaExportar, ";"));
                Response.End();

            }
            catch (Exception)
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
                var ListaExportar = new List<ExportarProviderPhysicalLegalPersonViewModel>();

                foreach (var item in lista)
                {
                    var obj = new ExportarProviderPhysicalLegalPersonViewModel();
                    obj.Id = item.Id;
                    obj.TypeProvider = item.TypeProvider.ToString().Equals("PhysicalPerson") ? "Pessoa Física" : "Pessoa Jurídica";
                    obj.Email = item.Email;
                    obj.NameCorporateName = item.NameCorporateName;
                    obj.PhoneNumber = item.PhoneNumber;
                    ListaExportar.Add(obj);
                }

                // Retorna os resultados
                string fileName = "ExportarProviderReport-" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm") + ".csv";

                Response.Clear();
                Response.ClearHeaders();
                Response.ContentType = "text/csv";

                Response.BufferOutput = true;
                Response.ContentEncoding = System.Text.ASCIIEncoding.GetEncoding("ISO-8859-1");

                Response.AddHeader("content-disposition", "attachment; filename=" + fileName);
                Response.AddHeader("pragma", "public");

                Response.Write(exportar<ExportarProviderPhysicalLegalPersonViewModel>(ListaExportar, ";"));
                Response.End();

            }
            catch (Exception)
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
                var lista = Mapper.Map<IEnumerable<BankTransactionViewModel>>(_reportRepository.BankTransactionReport(_YESTERDAY, _TODAY));
                var ListaExportar = new List<ExportBankTransactionViewModel>();

                foreach (var item in lista)
                {
                    var obj = new ExportBankTransactionViewModel();
                    //obj.Id = item.Id;
                    ////obj.CPFCNPJ = item.CPFCNPJ;
                    //obj.Email = item.Email;
                    //obj.NameCorporateName = item.NameCorporateName;
                    //obj.PhoneNumber = item.PhoneNumber;
                    //ListaExportar.Add(obj);
                }

                // Retorna os resultados
                string fileName = "ExportarProviderReport-" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm") + ".csv";

                Response.Clear();
                Response.ClearHeaders();
                Response.ContentType = "text/csv";

                Response.BufferOutput = true;
                Response.ContentEncoding = System.Text.ASCIIEncoding.GetEncoding("ISO-8859-1");

                Response.AddHeader("content-disposition", "attachment; filename=" + fileName);
                Response.AddHeader("pragma", "public");

                Response.Write(exportar<ExportBankTransactionViewModel>(ListaExportar, ";"));

                Response.End();

            }
            catch (Exception)
            {
                return Json(new { success = false });
            }
            return Json(new { success = true });
        }

        #endregion

        #region CSV

        public static string exportar<TipoModel>(List<TipoModel> lista, string delimitador, Dictionary<string, string> replace = null)
        {
            var sb = new StringBuilder();

            try
            {
                var propriedades = typeof(TipoModel).GetProperties();

                // header
                foreach (var p in propriedades)
                {
                    sb.Append(p.CustomAttributes.FirstOrDefault().ConstructorArguments[0].Value);
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

                        sb.Append(conteudo.Replace(delimitador, "").Replace("\r\n", "").Replace("\n", "").Replace("\r", "").Replace(";", ""));
                        sb.Append(delimitador);

                    }

                    sb.Remove(sb.Length - delimitador.Length, delimitador.Length);
                    sb.AppendLine();

                }

            }
            catch (Exception)
            {
                throw;
            }

            return sb.ToString();
        }

        #endregion
    }
}