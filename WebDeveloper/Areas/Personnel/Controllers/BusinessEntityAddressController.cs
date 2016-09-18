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
    public class BusinessEntityAddressController : Controller
    {
        // GET: Personnel/BusinessEntityAddress
        private BusinessEntityAddressRepository _businessentityaddress = new BusinessEntityAddressRepository();

        public ActionResult Index()
        {
            return View(_businessentityaddress.GetListBySize(15));
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(BusinessEntityAddress businessentityaddress)
        {
            if (!ModelState.IsValid) return View(businessentityaddress);
            businessentityaddress.rowguid = Guid.NewGuid();
            businessentityaddress.ModifiedDate = DateTime.Now;
            businessentityaddress.BusinessEntity = new BusinessEntity
            {
                rowguid = businessentityaddress.rowguid,
                ModifiedDate = DateTime.Now
            };

            _businessentityaddress.Add(businessentityaddress);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int BusinessEntityID,int AddressID , int AddressTypeID)
        {
            var businessentityaddress = _businessentityaddress.GetById(BusinessEntityID, AddressID, AddressTypeID);
            if (businessentityaddress == null) return RedirectToAction("Index");
            return View(businessentityaddress);
        }

        [HttpPost]
        public ActionResult Edit(BusinessEntityAddress businessentityaddress)
        {
            if (!ModelState.IsValid) return View(businessentityaddress);
            _businessentityaddress.Update(businessentityaddress);
            return RedirectToAction("Index");
        }


        public ActionResult Delete(int BusinessEntityID, int AddressID, int AddressTypeID)
        {
            var businessentityaddress = _businessentityaddress.GetById(BusinessEntityID, AddressID, AddressTypeID);
            if (businessentityaddress == null) return RedirectToAction("Index");
            return View(businessentityaddress);
        }

        [HttpPost]
        public ActionResult Delete(BusinessEntityAddress businessentityaddress)
        {
            //if (!ModelState.IsValid) return View(person);
            businessentityaddress = _businessentityaddress.GetById(businessentityaddress.BusinessEntityID, businessentityaddress.AddressID, businessentityaddress.AddressTypeID);
            _businessentityaddress.Delete(businessentityaddress);
            return RedirectToAction("Index");
        }


        public ActionResult Details(int BusinessEntityID, int AddressID, int AddressTypeID)
        {
            var businessentityaddress = _businessentityaddress.GetById(BusinessEntityID, AddressID, AddressTypeID);
            if (businessentityaddress == null) return RedirectToAction("Index");
            return View(businessentityaddress);

        }
    }
}