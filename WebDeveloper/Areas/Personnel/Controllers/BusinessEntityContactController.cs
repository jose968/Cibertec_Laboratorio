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
    public class BusinessEntityContactController : Controller
    {
        // GET: Personnel/BusinessEntityContact
        private BusinessEntityContactRepository _businessentitycontact = new BusinessEntityContactRepository();
        public ActionResult Index()
        {
            return View(_businessentitycontact.GetListBySize(15));
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(BusinessEntityContact businessentitycontact)
        {
            if (!ModelState.IsValid) return View(businessentitycontact);
            businessentitycontact.rowguid = Guid.NewGuid();
            businessentitycontact.ModifiedDate = DateTime.Now;
            businessentitycontact.BusinessEntity = new BusinessEntity
            {
                rowguid = businessentitycontact.rowguid,
                ModifiedDate = DateTime.Now
            };

            _businessentitycontact.Add(businessentitycontact);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var businessentitycontact = _businessentitycontact.GetById(id);
            if (businessentitycontact == null) return RedirectToAction("Index");
            return View(businessentitycontact);
        }

        [HttpPost]
        public ActionResult Edit(BusinessEntityContact businessentitycontact)
        {
            if (!ModelState.IsValid) return View(businessentitycontact);
            _businessentitycontact.Update(businessentitycontact);
            return RedirectToAction("Index");
        }


        public ActionResult Delete(int id)
        {
            var businessentitycontact = _businessentitycontact.GetById(id);
            if (businessentitycontact == null) return RedirectToAction("Index");
            return View(businessentitycontact);
        }

        [HttpPost]
        public ActionResult Delete(BusinessEntityContact businessentitycontact)
        {
            //if (!ModelState.IsValid) return View(person);
            businessentitycontact = _businessentitycontact.GetById(businessentitycontact.BusinessEntityID);
            _businessentitycontact.Delete(businessentitycontact);
            return RedirectToAction("Index");
        }


        public ActionResult Details(int id)
        {
            var businessentitycontact = _businessentitycontact.GetById(id);
            if (businessentitycontact == null) return RedirectToAction("Index");
            return View(businessentitycontact);

        }
    }
}