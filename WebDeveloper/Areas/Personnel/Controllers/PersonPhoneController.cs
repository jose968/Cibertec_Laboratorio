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
    public class PersonPhoneController : Controller
    {
        // GET: Personnel/PersonPhone
        private PersonPhoneRepository _personphone = new PersonPhoneRepository();
        public ActionResult Index()
        {
            return View(_personphone.GetListBySize(15));
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(PersonPhone personphone)
        {
            if (!ModelState.IsValid) return View(personphone);
            //personphone.rowguid = Guid.NewGuid();
            personphone.ModifiedDate = DateTime.Now;
            //personphone.BusinessEntity = new BusinessEntity
            //{
            //    rowguid = personphone.rowguid,
            //    ModifiedDate = DateTime.Now
            //};

            _personphone.Add(personphone);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var personphone = _personphone.GetById(id);
            if (personphone == null) return RedirectToAction("Index");
            return View(personphone);
        }

        [HttpPost]
        public ActionResult Edit(PersonPhone personphone)
        {
            if (!ModelState.IsValid) return View(personphone);
            _personphone.Update(personphone);
            return RedirectToAction("Index");
        }


        public ActionResult Delete(int id)
        {
            var personphone = _personphone.GetById(id);
            if (personphone == null) return RedirectToAction("Index");
            return View(personphone);
        }

        [HttpPost]
        public ActionResult Delete(PersonPhone personphone)
        {
            //if (!ModelState.IsValid) return View(person);
            personphone = _personphone.GetById(personphone.BusinessEntityID);
            _personphone.Delete(personphone);
            return RedirectToAction("Index");
        }


        public ActionResult Details(int id)
        {
            var personphone = _personphone.GetById(id);
            if (personphone == null) return RedirectToAction("Index");
            return View(personphone);

        }
    }
}