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
    public class EmailAddressController : Controller
    {
        // GET: Personnel/EmailAddress
        private EmailAddressRepository _emailaddress = new EmailAddressRepository();
        public ActionResult Index()
        {
            return View(_emailaddress.GetListBySize(15));
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(EmailAddress emailaddress)
        {
            if (!ModelState.IsValid) return View(emailaddress);
            emailaddress.rowguid = Guid.NewGuid();
            emailaddress.ModifiedDate = DateTime.Now;
            //emailaddress.BusinessEntity = new BusinessEntity
            //{
            //    rowguid = person.rowguid,
            //    ModifiedDate = DateTime.Now
            //};

            _emailaddress.Add(emailaddress);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var emailaddress = _emailaddress.GetById(id);
            if (emailaddress == null) return RedirectToAction("Index");
            return View(emailaddress);
        }

        [HttpPost]
        public ActionResult Edit(EmailAddress emailaddress)
        {
            if (!ModelState.IsValid) return View(emailaddress);
            _emailaddress.Update(emailaddress);
            return RedirectToAction("Index");
        }


        public ActionResult Delete(int id)
        {
            var emailaddress = _emailaddress.GetById(id);
            if (emailaddress == null) return RedirectToAction("Index");
            return View(emailaddress);
        }

        [HttpPost]
        public ActionResult Delete(EmailAddress emailaddress)
        {
            //if (!ModelState.IsValid) return View(person);
            emailaddress = _emailaddress.GetById(emailaddress.BusinessEntityID);
            _emailaddress.Delete(emailaddress);
            return RedirectToAction("Index");
        }


        public ActionResult Details(int id)
        {
            var emailaddress = _emailaddress.GetById(id);
            if (emailaddress == null) return RedirectToAction("Index");
            return View(emailaddress);

        }
    }
}