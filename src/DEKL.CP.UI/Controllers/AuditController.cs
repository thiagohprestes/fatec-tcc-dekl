using AutoMapper;
using DEKL.CP.Domain.Contracts.Repositories;
using DEKL.CP.UI.ViewModels.Audit;
using System.Collections.Generic;
using System.Web.Mvc;

namespace DEKL.CP.UI.Controllers
{
    public class AuditController : Controller
    {
        private readonly IAuditRepository _auditRepository;

        public AuditController(IAuditRepository auditRepository) => _auditRepository = auditRepository;

        public ActionResult Index()
          => View(Mapper.Map<IEnumerable<AuditViewModel>>(_auditRepository.Actives));
    }
}