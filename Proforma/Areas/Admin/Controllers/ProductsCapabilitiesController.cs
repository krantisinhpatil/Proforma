using Proforma.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Proforma.Areas.Admin.Controllers
{
    public class ProductsCapabilitiesController : Controller
    {
        private SourcingGuideDevEntities db = new SourcingGuideDevEntities();

        // GET: Admin/ProductsCapabilities
        public ActionResult Index(int companyId, int? id = 0)
        {
            ProductsCapability productsCapability = db.ProductsCapabilities.Find(id);
            ViewBag.productsCapabilities = db.ProductsCapabilities.Where(a => a.CompanyId == companyId).ToList();
            ViewBag.CompanyId = companyId;
            return View(productsCapability);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "ProductsCapabilityId,CompanyId,Name,Description,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate")] ProductsCapability productsCapability)
        {

            ViewBag.productsCapabilities = db.ProductsCapabilities.Where(a => a.CompanyId == productsCapability.CompanyId).ToList();
            ViewBag.CompanyId = productsCapability.CompanyId;
            if (ModelState.IsValid)
            {
                if (productsCapability.ProductsCapabilityId == 0)
                {
                    productsCapability.CreatedDate = DateTime.Now;
                    productsCapability.ModifiedDate = DateTime.Now;
                    db.ProductsCapabilities.Add(productsCapability);
                }
                else
                {
                    var record = db.ProductsCapabilities.FirstOrDefault(a => a.ProductsCapabilityId == productsCapability.ProductsCapabilityId);
                    record.Name = productsCapability.Name;
                    record.CompanyId = productsCapability.CompanyId;
                    record.ModifiedDate = DateTime.Now;
                    db.Entry(record).State = EntityState.Modified;
                }

                db.SaveChanges();
                return RedirectToAction("Index", new { companyId = productsCapability.CompanyId });
            }

            return View(productsCapability);
        }

        // GET: Admin/ProductsCapabilities/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductsCapability productsCapability = db.ProductsCapabilities.Find(id);
            if (productsCapability == null)
            {
                return HttpNotFound();
            }
            return View(productsCapability);
        }

        // GET: Admin/ProductsCapabilities/Create
        public ActionResult Create()
        {
            ViewBag.CompanyId = new SelectList(db.Companies, "CompanyId", "PartnerType");
            return View();
        }

        // POST: Admin/ProductsCapabilities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductsCapabilityId,CompanyId,Name,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate")] ProductsCapability productsCapability)
        {
            if (ModelState.IsValid)
            {
                db.ProductsCapabilities.Add(productsCapability);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CompanyId = new SelectList(db.Companies, "CompanyId", "PartnerType", productsCapability.CompanyId);
            return View(productsCapability);
        }

        // GET: Admin/ProductsCapabilities/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductsCapability productsCapability = db.ProductsCapabilities.Find(id);
            if (productsCapability == null)
            {
                return HttpNotFound();
            }
            ViewBag.CompanyId = new SelectList(db.Companies, "CompanyId", "PartnerType", productsCapability.CompanyId);
            return View(productsCapability);
        }

        // POST: Admin/ProductsCapabilities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductsCapabilityId,CompanyId,Name,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate")] ProductsCapability productsCapability)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productsCapability).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CompanyId = new SelectList(db.Companies, "CompanyId", "PartnerType", productsCapability.CompanyId);
            return View(productsCapability);
        }

        // GET: Admin/ProductsCapabilities/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductsCapability productsCapability = db.ProductsCapabilities.Find(id);
            if (productsCapability == null)
            {
                return HttpNotFound();
            }
            return View(productsCapability);
        }

        // POST: Admin/ProductsCapabilities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            ProductsCapability productsCapability = db.ProductsCapabilities.Find(id);
            int companyid = productsCapability.CompanyId;
            db.ProductsCapabilities.Remove(productsCapability);
            db.SaveChanges();
            return RedirectToAction("Index", new { companyId = companyid });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
