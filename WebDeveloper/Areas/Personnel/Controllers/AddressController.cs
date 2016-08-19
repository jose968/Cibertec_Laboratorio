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
    public class AddressController : Controller
    {
        // GET: AddressArea/Address
        private AddressRepository _address = new AddressRepository();
        public ActionResult Index()
        {
            return View(_address.GetListBySize(15));
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Address address)
        {
            if (!ModelState.IsValid) return View(address);
            address.rowguid = Guid.NewGuid();
            address.ModifiedDate = DateTime.Now;
            //address.BusinessEntityAddress = new BusinessEntity
            //{
            //    rowguid = address.rowguid,
            //    ModifiedDate = DateTime.Now
            //};

            _address.Add(address);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var address = _address.GetById(id);
            if (address == null) return RedirectToAction("Index");
            return View(address);
        }

        [HttpPost]
        public ActionResult Edit(Address address)
        {
            if (!ModelState.IsValid) return View(address);
            _address.Update(address);
            return RedirectToAction("Index");
        }


        public ActionResult Delete(int id)
        {
            var address = _address.GetById(id);
            if (address == null) return RedirectToAction("Index");
            return View(address);
        }

        [HttpPost]
        public ActionResult Delete(Address address)
        {
            //if (!ModelState.IsValid) return View(person);
            address = _address.GetById(address.AddressID);
            _address.Delete(address);
            return RedirectToAction("Index");
        }


        public ActionResult Details(int id)
        {
            var address = _address.GetById(id);
            if (address == null) return RedirectToAction("Index");
            return View(address);

        }
    }
}