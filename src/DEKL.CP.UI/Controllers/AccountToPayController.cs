using System.Collections.Generic;
using System.Web.Mvc;
using DEKL.CP.UI.ViewModels.AccountsToPay;

namespace DEKL.CP.UI.Controllers
{
    public class AccountToPayController : Controller
    {
        public ActionResult Index()
        {
            var users = new List<AccountsToPayViewModel>
            {
                new AccountsToPayViewModel { Id = 1, Documento = "255987236991", Fornecedor = "C&C Casa e Contrução Ltda.", Tipo = "à vista",
                    Valor = "R$ 1.537,87", Juros = "0.25 ad", Multa = "R$ 8,27", Vencimento = "05/10/2017"},
                new AccountsToPayViewModel { Id = 2, Documento = "632455-A", Fornecedor = "Russo Funilária e Pintura Ltda.", Tipo = "à prazo",
                    Valor = "R$ 787,00", Juros = "0.25 ad", Multa = "R$ 10,97", Vencimento = "05/11/2017"},
                new AccountsToPayViewModel { Id = 3, Documento = "632455-B", Fornecedor = "Russo Funilária e Pintura Ltda.", Tipo = "à prazo",
                    Valor = "R$ 787,00", Juros = "0.25 ad", Multa = "R$ 10,97", Vencimento = "05/11/2017"},
                new AccountsToPayViewModel { Id = 4, Documento = "632455-C", Fornecedor = "Auto Posto Rachid", Tipo = "à vista",
                    Valor = "R$ 135,00", Juros = "N/A", Multa = "N/A", Vencimento = "05/10/2017"},
            };

            return View(users);
        }

        public ActionResult Create()
        {
            return View();
        }
    }
}
