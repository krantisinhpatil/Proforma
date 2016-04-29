using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Proforma.DAL;

namespace Proforma.Areas.Admin.Controllers
{
    public class ProformaProgramsController : Controller
    {
        private SourcingGuideDevEntities db = new SourcingGuideDevEntities();

        // GET: Admin/ProformaPrograms
        public ActionResult Index(int companyId, int? id = 0)
        {
            ProformaProgram proformaProgram = db.ProformaPrograms.Find(id);
            ViewBag.CompanyId = companyId;
            ViewBag.proformaPrograms = db.ProformaPrograms.Where(a => a.CompanyId == companyId).ToList();
            return View(proformaProgram);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "ProformaProgramId,CompanyId,Name,Description,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate")] ProformaProgram proformaProgram)
        {
            ViewBag.CompanyId = proformaProgram.CompanyId;
            ViewBag.proformaPrograms = db.ProformaPrograms.Where(a => a.CompanyId == proformaProgram.CompanyId).ToList();

            if (ModelState.IsValid)
            {
                if (proformaProgram.ProformaProgramId == 0)
                {
                    proformaProgram.CreatedDate = DateTime.Now;
                    proformaProgram.ModifiedDate = DateTime.Now;
                    db.ProformaPrograms.Add(proformaProgram);
                }
                else
                {
                    var record = db.ProformaPrograms.FirstOrDefault(a => a.ProformaProgramId == proformaProgram.ProformaProgramId);
                    record.Name = proformaProgram.Name;
                    record.CompanyId = proformaProgram.CompanyId;
                    record.ModifiedDate = DateTime.Now;
                    db.Entry(record).State = EntityState.Modified;
                }
                db.SaveChanges();
                return RedirectToAction("Index", new { companyId = proformaProgram.CompanyId });
            }

            return View(proformaProgram);
        }

        // GET: Admin/ProformaPrograms/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProformaProgram proformaProgram = db.ProformaPrograms.Find(id);
            if (proformaProgram == null)
            {
                return HttpNotFound();
            }
            return View(proformaProgram);
        }

        // GET: Admin/ProformaPrograms/Create
        public ActionResult Create()
        {
            ViewBag.CompanyId = new SelectList(db.Companies, "CompanyId", "PartnerType");
            return View();
        }

        // POST: Admin/ProformaPrograms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProformaProgramId,CompanyId,Name,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate")] ProformaProgram proformaProgram)
        {
            if (ModelState.IsValid)
            {
                db.ProformaPrograms.Add(proformaProgram);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CompanyId = new SelectList(db.Companies, "CompanyId", "PartnerType", proformaProgram.CompanyId);
            return View(proformaProgram);
        }

        // GET: Admin/ProformaPrograms/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProformaProgram proformaProgram = db.ProformaPrograms.Find(id);
            if (proformaProgram == null)
            {
                return HttpNotFound();
            }
            ViewBag.CompanyId = new SelectList(db.Companies, "CompanyId", "PartnerType", proformaProgram.CompanyId);
            return View(proformaProgram);
        }

        // POST: Admin/ProformaPrograms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProformaProgramId,CompanyId,Name,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate")] ProformaProgram proformaProgram)
        {
            if (ModelState.IsValid)
            {
                db.Entry(proformaProgram).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CompanyId = new SelectList(db.Companies, "CompanyId", "PartnerType", proformaProgram.CompanyId);
            return View(proformaProgram);
        }

        // GET: Admin/ProformaPrograms/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProformaProgram proformaProgram = db.ProformaPrograms.Find(id);
            if (proformaProgram == null)
            {
                return HttpNotFound();
            }
            return View(proformaProgram);
        }

        // POST: Admin/ProformaPrograms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProformaProgram proformaProgram = db.ProformaPrograms.Find(id);
            db.ProformaPrograms.Remove(proformaProgram);
            db.SaveChanges();
            return RedirectToAction("Index");
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
