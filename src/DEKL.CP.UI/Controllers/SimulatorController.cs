
using AutoMapper;
using DEKL.CP.Domain.Contracts.Repositories;
using DEKL.CP.UI.ViewModels.AccountsToPay;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;

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

            var objSimulador = Mapper.Map<AccountToPayViewModel>(_accountToPayRepository.Find(id));

            // soma os valores a pagar das parcelas
            decimal totalParcelas = 0;
            var objParcelas = objSimulador.Installments.ToList().FindAll(obj => !obj.PaymentDate.HasValue);
            if (objParcelas.Count > 0) totalParcelas = objParcelas.Sum(objx => objx.Value);

            var resultado = new List<object>
            {
                new
                {
                    Parcelas = objParcelas.Count + 1, // quantidade das parcelas mais 1 da parcela atual
                    Valor = objParcelas.Count > 0 ? totalParcelas : objSimulador.Value
                }
            };

            return Json(resultado, JsonRequestBehavior.AllowGet);
        }
    }
}
