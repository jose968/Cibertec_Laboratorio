using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDeveloper.Filters;
using WebDeveloper.Model;
using WebDeveloper.Repository;

namespace WebDeveloper.Areas.Personnel.Controllers
{
    [AuditControl]
    public class EmailAddressController : PersonBaseController<EmailAddress>
    {
        // GET: Personnel/EmailAddress

        public EmailAddressController(IRepository<EmailAddress> repository)
            :base(repository)
        {

        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult EmailByPerson(int id)
        {
            return PartialView("_Email", _repository.ListById(x => x.BusinessEntityID == id));
        }
        

    }
}