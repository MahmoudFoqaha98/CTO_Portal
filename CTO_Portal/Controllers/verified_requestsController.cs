using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CTO_Portal.Models;

namespace CTO_Portal.Controllers
{
    public class verified_requestsController : Controller
    {
        private CTOEntities db = new CTOEntities();

        // GET: verified_requests
        public ActionResult Index()
        {
            var verified_requests = db.verified_requests.Include(v => v.cours).Include(v => v.student).Include(v => v.student1).Include(v => v.student2).Include(v => v.student3).Include(v => v.student4).Include(v => v.student5).Include(v => v.temp_requests);
            return View(verified_requests.ToList());
        }

        // GET: verified_requests/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            verified_requests verified_requests = db.verified_requests.Find(id);
            if (verified_requests == null)
            {
                return HttpNotFound();
            }
            return View(verified_requests);
        }

        // GET: verified_requests/Create
        public ActionResult Create()
        {
            ViewBag.courseId = new SelectList(db.courses, "Id", "name");
            ViewBag.studentIdOne = new SelectList(db.students, "Id", "name");
            ViewBag.studentIdTwo = new SelectList(db.students, "Id", "name");
            ViewBag.studentIdThree = new SelectList(db.students, "Id", "name");
            ViewBag.studentIdFour = new SelectList(db.students, "Id", "name");
            ViewBag.studentIdFive = new SelectList(db.students, "Id", "name");
            ViewBag.studentIdSix = new SelectList(db.students, "Id", "name");
            ViewBag.id = new SelectList(db.temp_requests, "id", "note");
            return View();
        }

        // POST: verified_requests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,studentIdOne,studentIdTwo,studentIdThree,studentIdFour,studentIdFive,studentIdSix,courseId,note,approved")] verified_requests verified_requests)
        {
            if (ModelState.IsValid)
            {
                db.verified_requests.Add(verified_requests);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.courseId = new SelectList(db.courses, "Id", "name", verified_requests.courseId);
            ViewBag.studentIdOne = new SelectList(db.students, "Id", "name", verified_requests.studentIdOne);
            ViewBag.studentIdTwo = new SelectList(db.students, "Id", "name", verified_requests.studentIdTwo);
            ViewBag.studentIdThree = new SelectList(db.students, "Id", "name", verified_requests.studentIdThree);
            ViewBag.studentIdFour = new SelectList(db.students, "Id", "name", verified_requests.studentIdFour);
            ViewBag.studentIdFive = new SelectList(db.students, "Id", "name", verified_requests.studentIdFive);
            ViewBag.studentIdSix = new SelectList(db.students, "Id", "name", verified_requests.studentIdSix);
            ViewBag.id = new SelectList(db.temp_requests, "id", "note", verified_requests.id);
            return View(verified_requests);
        }

        // GET: verified_requests/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            verified_requests verified_requests = db.verified_requests.Find(id);
            if (verified_requests == null)
            {
                return HttpNotFound();
            }
            ViewBag.courseId = new SelectList(db.courses, "Id", "name", verified_requests.courseId);
            ViewBag.studentIdOne = new SelectList(db.students, "Id", "name", verified_requests.studentIdOne);
            ViewBag.studentIdTwo = new SelectList(db.students, "Id", "name", verified_requests.studentIdTwo);
            ViewBag.studentIdThree = new SelectList(db.students, "Id", "name", verified_requests.studentIdThree);
            ViewBag.studentIdFour = new SelectList(db.students, "Id", "name", verified_requests.studentIdFour);
            ViewBag.studentIdFive = new SelectList(db.students, "Id", "name", verified_requests.studentIdFive);
            ViewBag.studentIdSix = new SelectList(db.students, "Id", "name", verified_requests.studentIdSix);
            ViewBag.id = new SelectList(db.temp_requests, "id", "note", verified_requests.id);
            return View(verified_requests);
        }

        // POST: verified_requests/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,studentIdOne,studentIdTwo,studentIdThree,studentIdFour,studentIdFive,studentIdSix,courseId,note,approved")] verified_requests verified_requests)
        {
            if (ModelState.IsValid)
            {
                db.Entry(verified_requests).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.courseId = new SelectList(db.courses, "Id", "name", verified_requests.courseId);
            ViewBag.studentIdOne = new SelectList(db.students, "Id", "name", verified_requests.studentIdOne);
            ViewBag.studentIdTwo = new SelectList(db.students, "Id", "name", verified_requests.studentIdTwo);
            ViewBag.studentIdThree = new SelectList(db.students, "Id", "name", verified_requests.studentIdThree);
            ViewBag.studentIdFour = new SelectList(db.students, "Id", "name", verified_requests.studentIdFour);
            ViewBag.studentIdFive = new SelectList(db.students, "Id", "name", verified_requests.studentIdFive);
            ViewBag.studentIdSix = new SelectList(db.students, "Id", "name", verified_requests.studentIdSix);
            ViewBag.id = new SelectList(db.temp_requests, "id", "note", verified_requests.id);
            return View(verified_requests);
        }

        // GET: verified_requests/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            verified_requests verified_requests = db.verified_requests.Find(id);
            if (verified_requests == null)
            {
                return HttpNotFound();
            }
            return View(verified_requests);
        }

        // POST: verified_requests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            verified_requests verified_requests = db.verified_requests.Find(id);
            db.verified_requests.Remove(verified_requests);
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
