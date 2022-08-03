using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BugTrackingSystem.Models;
using System.IO;

namespace BugTrackingSystem.Controllers
{
    public class IssuesController : Controller
    {
       

        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {

            //var currentUser = db.Users.FirstOrDefault(n => n.UserName == User.Identity.Name);
            return View(db.Issues.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Issue issue = db.Issues.Find(id);
            if (issue == null)
            {
                return HttpNotFound();
            }
            return View(issue);
        }


        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ProjectName,Priority,IssueType,Summary,Description,Reporter")] Issue issue)
        {
            if (ModelState.IsValid)
            {
                var checkId = db.Issues.Select(e=>e.Id).FirstOrDefault();
                if (checkId == 0)
                {
                    issue.Id = 1;
                }
                else
                {
                    issue.Id = db.Issues.Max(c => c.Id) + 1;
                }
                db.Issues.Add(issue);
                issue.EntryDate = DateTime.Now;
                issue.ResolveDate = DateTime.Now;
                    db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(issue);
        }
        [HttpPost]
        public ActionResult UploadFile(System.Web.HttpPostedFileBase UploadFile)
        {
            if(UploadFile!= null && UploadFile.ContentLength>0)
            {
                UploadFile.SaveAs(Server.MapPath("~/Dosyalar" + UploadFile.FileName));
            }

            return View();
        }
        public ActionResult UploadFile()
        {


            return View();
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Issue issue = db.Issues.Find(id);
            if (issue == null)
            {
                return HttpNotFound();
            }
            return View(issue);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ProjectName,Priority,IssueType,Summary,Description,Reporter")] Issue issue)
        {
            if (ModelState.IsValid)
            {
                db.Entry(issue).State = EntityState.Modified;
                issue.EntryDate = DateTime.Now;
                issue.ResolveDate = DateTime.Now;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(issue);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Issue issue = db.Issues.Find(id);
            if (issue == null)
            {
                return HttpNotFound();
            }
            return View(issue);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Issue issue = db.Issues.Find(id);
            db.Issues.Remove(issue);
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
