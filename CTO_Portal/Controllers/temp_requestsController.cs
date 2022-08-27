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
    public class temp_requestsController : Controller
    {
        private CTOEntities db = new CTOEntities();

        // GET: temp_requests
        public ActionResult Index()
        {
            var temp_requests = db.temp_requests.Include(t => t.cours).Include(t => t.student).Include(t => t.student1).Include(t => t.student2).Include(t => t.student3).Include(t => t.student4).Include(t => t.student5);
            return View(temp_requests.ToList());
        }

        // GET: temp_requests/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            temp_requests temp_requests = db.temp_requests.Find(id);
            if (temp_requests == null)
            {
                return HttpNotFound();
            }
            return View(temp_requests);
        }

        // GET: temp_requests/Create
        public ActionResult Create()
        {
            ViewBag.courseId = new SelectList(db.courses, "Id", "name");
            ViewBag.stidentIdOne = new SelectList(db.students, "Id", "name");
            ViewBag.studentIdTwo = new SelectList(db.students, "Id", "name");
            ViewBag.studentIdThree = new SelectList(db.students, "Id", "name");
            ViewBag.studentIdFour = new SelectList(db.students, "Id", "name");
            ViewBag.studendIdFive = new SelectList(db.students, "Id", "name");
            ViewBag.studentIdSix = new SelectList(db.students, "Id", "name");
            return View();
        }

        // POST: temp_requests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,stidentIdOne,studentIdTwo,studentIdThree,studentIdFour,studendIdFive,studentIdSix,courseId,note")] temp_requests temp_requests)
        {
            if (ModelState.IsValid)
            {
                db.temp_requests.Add(temp_requests);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.courseId = new SelectList(db.courses, "Id", "name", temp_requests.courseId);
            ViewBag.stidentIdOne = new SelectList(db.students, "Id", "name", temp_requests.stidentIdOne);
            ViewBag.studentIdTwo = new SelectList(db.students, "Id", "name", temp_requests.studentIdTwo);
            ViewBag.studentIdThree = new SelectList(db.students, "Id", "name", temp_requests.studentIdThree);
            ViewBag.studentIdFour = new SelectList(db.students, "Id", "name", temp_requests.studentIdFour);
            ViewBag.studendIdFive = new SelectList(db.students, "Id", "name", temp_requests.studendIdFive);
            ViewBag.studentIdSix = new SelectList(db.students, "Id", "name", temp_requests.studentIdSix);
            return View(temp_requests);
        }

        // GET: temp_requests/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            temp_requests temp_requests = db.temp_requests.Find(id);
            if (temp_requests == null)
            {
                return HttpNotFound();
            }
            ViewBag.courseId = new SelectList(db.courses, "Id", "name", temp_requests.courseId);
            ViewBag.stidentIdOne = new SelectList(db.students, "Id", "name", temp_requests.stidentIdOne);
            ViewBag.studentIdTwo = new SelectList(db.students, "Id", "name", temp_requests.studentIdTwo);
            ViewBag.studentIdThree = new SelectList(db.students, "Id", "name", temp_requests.studentIdThree);
            ViewBag.studentIdFour = new SelectList(db.students, "Id", "name", temp_requests.studentIdFour);
            ViewBag.studendIdFive = new SelectList(db.students, "Id", "name", temp_requests.studendIdFive);
            ViewBag.studentIdSix = new SelectList(db.students, "Id", "name", temp_requests.studentIdSix);
            return View(temp_requests);
        }

        // POST: temp_requests/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,stidentIdOne,studentIdTwo,studentIdThree,studentIdFour,studendIdFive,studentIdSix,courseId,note")] temp_requests temp_requests)
        {
            if (ModelState.IsValid)
            {
                db.Entry(temp_requests).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.courseId = new SelectList(db.courses, "Id", "name", temp_requests.courseId);
            ViewBag.stidentIdOne = new SelectList(db.students, "Id", "name", temp_requests.stidentIdOne);
            ViewBag.studentIdTwo = new SelectList(db.students, "Id", "name", temp_requests.studentIdTwo);
            ViewBag.studentIdThree = new SelectList(db.students, "Id", "name", temp_requests.studentIdThree);
            ViewBag.studentIdFour = new SelectList(db.students, "Id", "name", temp_requests.studentIdFour);
            ViewBag.studendIdFive = new SelectList(db.students, "Id", "name", temp_requests.studendIdFive);
            ViewBag.studentIdSix = new SelectList(db.students, "Id", "name", temp_requests.studentIdSix);
            return View(temp_requests);
        }

        // GET: temp_requests/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            temp_requests temp_requests = db.temp_requests.Find(id);
            if (temp_requests == null)
            {
                return HttpNotFound();
            }
            return View(temp_requests);
        }

        // POST: temp_requests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            temp_requests temp_requests = db.temp_requests.Find(id);
            db.temp_requests.Remove(temp_requests);
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
