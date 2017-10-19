using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TumorTaskforce_Research;

namespace TumorTaskforce_Research.Controllers
{
    public class record_entryController : Controller
    {
        private healthEntities db = new healthEntities();

        // GET: record_entry
        public ActionResult Index()
        {
            var record_entry = db.record_entry.Include(r => r.record);
            return View(record_entry.ToList());
        }

        // GET: record_entry/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            record_entry record_entry = db.record_entry.Find(id);
            if (record_entry == null)
            {
                return HttpNotFound();
            }
            return View(record_entry);
        }

        // GET: record_entry/Create
        public ActionResult Create()
        {
            ViewBag.Record_ID = new SelectList(db.records, "Record_ID", "Record_ID");
            return View();
        }

        // POST: record_entry/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RecordEntry_ID,Record_ID,Entry,Date,Notes")] record_entry record_entry)
        {
            if (ModelState.IsValid)
            {
                db.record_entry.Add(record_entry);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Record_ID = new SelectList(db.records, "Record_ID", "Record_ID", record_entry.Record_ID);
            return View(record_entry);
        }

        // GET: record_entry/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            record_entry record_entry = db.record_entry.Find(id);
            if (record_entry == null)
            {
                return HttpNotFound();
            }
            ViewBag.Record_ID = new SelectList(db.records, "Record_ID", "Record_ID", record_entry.Record_ID);
            return View(record_entry);
        }

        // POST: record_entry/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RecordEntry_ID,Record_ID,Entry,Date,Notes")] record_entry record_entry)
        {
            if (ModelState.IsValid)
            {
                db.Entry(record_entry).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Record_ID = new SelectList(db.records, "Record_ID", "Record_ID", record_entry.Record_ID);
            return View(record_entry);
        }

        // GET: record_entry/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            record_entry record_entry = db.record_entry.Find(id);
            if (record_entry == null)
            {
                return HttpNotFound();
            }
            return View(record_entry);
        }

        // POST: record_entry/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            record_entry record_entry = db.record_entry.Find(id);
            db.record_entry.Remove(record_entry);
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
