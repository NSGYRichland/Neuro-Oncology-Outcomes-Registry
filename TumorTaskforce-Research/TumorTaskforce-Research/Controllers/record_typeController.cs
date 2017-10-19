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
    public class record_typeController : Controller
    {
        private healthEntities db = new healthEntities();

        // GET: record_type
        public ActionResult Index()
        {
            return View(db.record_type.ToList());
        }

        // GET: record_type/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            record_type record_type = db.record_type.Find(id);
            if (record_type == null)
            {
                return HttpNotFound();
            }
            return View(record_type);
        }

        // GET: record_type/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: record_type/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RecordType_ID,RecordType_Name")] record_type record_type)
        {
            if (ModelState.IsValid)
            {
                db.record_type.Add(record_type);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(record_type);
        }

        // GET: record_type/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            record_type record_type = db.record_type.Find(id);
            if (record_type == null)
            {
                return HttpNotFound();
            }
            return View(record_type);
        }

        // POST: record_type/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RecordType_ID,RecordType_Name")] record_type record_type)
        {
            if (ModelState.IsValid)
            {
                db.Entry(record_type).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(record_type);
        }

        // GET: record_type/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            record_type record_type = db.record_type.Find(id);
            if (record_type == null)
            {
                return HttpNotFound();
            }
            return View(record_type);
        }

        // POST: record_type/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            record_type record_type = db.record_type.Find(id);
            db.record_type.Remove(record_type);
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
