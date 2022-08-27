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
    public class groupsController : Controller
    {
        private CTOEntities db = new CTOEntities();

        // GET: groups
        public ActionResult Index()
        {
            var groups = db.groups.Include(g => g.cours).Include(g => g.day).Include(g => g.department).Include(g => g.hospital).Include(g => g.shift).Include(g => g.student).Include(g => g.student1).Include(g => g.student2).Include(g => g.student3).Include(g => g.student4).Include(g => g.student5).Include(g => g.trainer);
            return View(groups.ToList());
        }

        // GET: groups/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            group group = db.groups.Find(id);
            if (group == null)
            {
                return HttpNotFound();
            }
            return View(group);
        }

        // GET: groups/Create
        public ActionResult Create()
        {
            ViewBag.courseId = new SelectList(db.courses, "Id", "name");
            ViewBag.dayId = new SelectList(db.days, "Id", "name");
            ViewBag.departmentId = new SelectList(db.departments, "Id", "name");
            ViewBag.hospitalId = new SelectList(db.hospitals, "Id", "name");
            ViewBag.shiftId = new SelectList(db.shifts, "Id", "name");
            ViewBag.studentIdOne = new SelectList(db.students, "Id", "name");
            ViewBag.studentIdTwo = new SelectList(db.students, "Id", "name");
            ViewBag.studentIdThree = new SelectList(db.students, "Id", "name");
            ViewBag.studentIdFour = new SelectList(db.students, "Id", "name");
            ViewBag.studentIdFive = new SelectList(db.students, "Id", "name");
            ViewBag.studentIdSix = new SelectList(db.students, "Id", "name");
            ViewBag.trainerId = new SelectList(db.trainers, "Id", "name");
            return View();
        }

        // POST: groups/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "courseId,hospitalId,departmentId,dayId,shiftId,trainerId,studentIdOne,studentIdTwo,studentIdThree,studentIdFour,studentIdFive,studentIdSix")] group group)
        {
            if (ModelState.IsValid)
            {
                db.groups.Add(group);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.courseId = new SelectList(db.courses, "Id", "name", group.courseId);
            ViewBag.dayId = new SelectList(db.days, "Id", "name", group.dayId);
            ViewBag.departmentId = new SelectList(db.departments, "Id", "name", group.departmentId);
            ViewBag.hospitalId = new SelectList(db.hospitals, "Id", "name", group.hospitalId);
            ViewBag.shiftId = new SelectList(db.shifts, "Id", "name", group.shiftId);
            ViewBag.studentIdOne = new SelectList(db.students, "Id", "name", group.studentIdOne);
            ViewBag.studentIdTwo = new SelectList(db.students, "Id", "name", group.studentIdTwo);
            ViewBag.studentIdThree = new SelectList(db.students, "Id", "name", group.studentIdThree);
            ViewBag.studentIdFour = new SelectList(db.students, "Id", "name", group.studentIdFour);
            ViewBag.studentIdFive = new SelectList(db.students, "Id", "name", group.studentIdFive);
            ViewBag.studentIdSix = new SelectList(db.students, "Id", "name", group.studentIdSix);
            ViewBag.trainerId = new SelectList(db.trainers, "Id", "name", group.trainerId);
            return View(group);
        }

        // GET: groups/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            group group = db.groups.Find(id);
            if (group == null)
            {
                return HttpNotFound();
            }
            ViewBag.courseId = new SelectList(db.courses, "Id", "name", group.courseId);
            ViewBag.dayId = new SelectList(db.days, "Id", "name", group.dayId);
            ViewBag.departmentId = new SelectList(db.departments, "Id", "name", group.departmentId);
            ViewBag.hospitalId = new SelectList(db.hospitals, "Id", "name", group.hospitalId);
            ViewBag.shiftId = new SelectList(db.shifts, "Id", "name", group.shiftId);
            ViewBag.studentIdOne = new SelectList(db.students, "Id", "name", group.studentIdOne);
            ViewBag.studentIdTwo = new SelectList(db.students, "Id", "name", group.studentIdTwo);
            ViewBag.studentIdThree = new SelectList(db.students, "Id", "name", group.studentIdThree);
            ViewBag.studentIdFour = new SelectList(db.students, "Id", "name", group.studentIdFour);
            ViewBag.studentIdFive = new SelectList(db.students, "Id", "name", group.studentIdFive);
            ViewBag.studentIdSix = new SelectList(db.students, "Id", "name", group.studentIdSix);
            ViewBag.trainerId = new SelectList(db.trainers, "Id", "name", group.trainerId);
            return View(group);
        }

        // POST: groups/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "courseId,hospitalId,departmentId,dayId,shiftId,trainerId,studentIdOne,studentIdTwo,studentIdThree,studentIdFour,studentIdFive,studentIdSix")] group group)
        {
            if (ModelState.IsValid)
            {
                db.Entry(group).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.courseId = new SelectList(db.courses, "Id", "name", group.courseId);
            ViewBag.dayId = new SelectList(db.days, "Id", "name", group.dayId);
            ViewBag.departmentId = new SelectList(db.departments, "Id", "name", group.departmentId);
            ViewBag.hospitalId = new SelectList(db.hospitals, "Id", "name", group.hospitalId);
            ViewBag.shiftId = new SelectList(db.shifts, "Id", "name", group.shiftId);
            ViewBag.studentIdOne = new SelectList(db.students, "Id", "name", group.studentIdOne);
            ViewBag.studentIdTwo = new SelectList(db.students, "Id", "name", group.studentIdTwo);
            ViewBag.studentIdThree = new SelectList(db.students, "Id", "name", group.studentIdThree);
            ViewBag.studentIdFour = new SelectList(db.students, "Id", "name", group.studentIdFour);
            ViewBag.studentIdFive = new SelectList(db.students, "Id", "name", group.studentIdFive);
            ViewBag.studentIdSix = new SelectList(db.students, "Id", "name", group.studentIdSix);
            ViewBag.trainerId = new SelectList(db.trainers, "Id", "name", group.trainerId);
            return View(group);
        }

        // GET: groups/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            group group = db.groups.Find(id);
            if (group == null)
            {
                return HttpNotFound();
            }
            return View(group);
        }

        // POST: groups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            group group = db.groups.Find(id);
            db.groups.Remove(group);
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
