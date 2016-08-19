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
    public class BusinessEntityController : Controller
    {
        // GET: Personnel/BusinessEntity
        private BusinessEntityRepository _businessentity = new BusinessEntityRepository();
        public ActionResult Index()
        {
            return View(_businessentity.GetListBySize(15));
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(BusinessEntity businessentity)
        {
            if (!ModelState.IsValid) return View(businessentity);
            businessentity.rowguid = Guid.NewGuid();
            businessentity.ModifiedDate = DateTime.Now;
            //address.BusinessEntityAddress = new BusinessEntity
            //{
            //    rowguid = address.rowguid,
            //    ModifiedDate = DateTime.Now
            //};

            _businessentity.Add(businessentity);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var businessentity = _businessentity.GetById(id);
            if (businessentity == null) return RedirectToAction("Index");
            return View(businessentity);
        }

        [HttpPost]
        public ActionResult Edit(BusinessEntity businessentity)
        {
            if (!ModelState.IsValid) return View(businessentity);
            _businessentity.Update(businessentity);
            return RedirectToAction("Index");
        }


        public ActionResult Delete(int id)
        {
            var businessentity = _businessentity.GetById(id);
            if (businessentity == null) return RedirectToAction("Index");
            return View(businessentity);
        }

        [HttpPost]
        public ActionResult Delete(BusinessEntity businessentity)
        {
            //if (!ModelState.IsValid) return View(person);
            businessentity = _businessentity.GetById(businessentity.BusinessEntityID);
            _businessentity.Delete(businessentity);
            return RedirectToAction("Index");
        }


        public ActionResult Details(int id)
        {
            var businessentity = _businessentity.GetById(id);
            if (businessentity == null) return RedirectToAction("Index");
            return View(businessentity);

        }
    }
}