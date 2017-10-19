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
    public class recordsController : Controller
    {
        private healthEntities db = new healthEntities();

        // GET: records
        public ActionResult Index()
        {
            var records = db.records.Include(r => r.record_type1);
            return View(records.ToList());
        }

        // GET: records/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            record record = db.records.Find(id);
            if (record == null)
            {
                return HttpNotFound();
            }
            return View(record);
        }

        // GET: records/Create
        public ActionResult Create()
        {
            ViewBag.Record_Type = new SelectList(db.record_type, "RecordType_ID", "RecordType_Name");
            return View();
        }

        // POST: records/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Record_ID,Record_Type")] record record)
        {
            if (ModelState.IsValid)
            {
                db.records.Add(record);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Record_Type = new SelectList(db.record_type, "RecordType_ID", "RecordType_Name", record.Record_Type);
            return View(record);
        }

        // GET: records/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            record record = db.records.Find(id);
            if (record == null)
            {
                return HttpNotFound();
            }
            ViewBag.Record_Type = new SelectList(db.record_type, "RecordType_ID", "RecordType_Name", record.Record_Type);
            return View(record);
        }

        // POST: records/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Record_ID,Record_Type")] record record)
        {
            if (ModelState.IsValid)
            {
                db.Entry(record).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Record_Type = new SelectList(db.record_type, "RecordType_ID", "RecordType_Name", record.Record_Type);
            return View(record);
        }

        // GET: records/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            record record = db.records.Find(id);
            if (record == null)
            {
                return HttpNotFound();
            }
            return View(record);
        }

        // POST: records/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            record record = db.records.Find(id);
            db.records.Remove(record);
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
