using BugTrackingSystem.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BugTrackingSystem.Controllers
{
    public class AssignManageController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Admin()
        {

            var model = db.Issues.ToList();
            return View(model);

        }
        public ActionResult AssignEdit(Issue issue)
        {
                     
            var model = new IssueUserViewModel()
            {
                User = db.Users.ToList(),
                IssueId = issue.Id
            };
            return View("AssignEdit", model);
        }

        public ActionResult AssignSonucu(IssueUserViewModel model)
        {
           
            var issue = db.Issues.FirstOrDefault(n => n.Id == model.IssueId);
            issue.Asignee = model.AssigneeId;
            if (issue.Asignee == null)
            {
                issue.Status = "";
            }
            else
            {
                issue.Status = "Assigned";
            }
            db.Entry(issue).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();



              return View(issue);

    
        }

        public FileResult Download(string file)
        {

            byte[] fileBytes=System.IO.File.ReadAllBytes(Server.MapPath("~/Dosyalar/") + file + " ");

            return File(fileBytes,System.Net.Mime.MediaTypeNames.Application.Octet,file);



        }
        public ActionResult MyTasks()
        {
          
  
            var currentUser = db.Users.FirstOrDefault(n => n.UserName == User.Identity.Name);
            var model = db.Issues.Where(n => n.Asignee == currentUser.Name).ToList();
          
            return View(model);
            
        }

        public ActionResult GeriDonut(Issue issue)
        {
            var model = new IssueUserViewModel()
            {
                User = db.Users.ToList(),
                IssueId = issue.Id
            };


            return View("GeriDonut", model);

        }
        public ActionResult Kaydet(IssueUserViewModel model)
        {
            var issue = db.Issues.FirstOrDefault(n => n.Id == model.IssueId);
            issue.Status = model.Statuss;
            issue.AsigneeDescription = model.tamamlanmaOrani;
            issue.ResolveDate = DateTime.Now;
            db.Entry(issue).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            var currentUser = db.Users.FirstOrDefault(n => n.UserName == User.Identity.Name);
            return View("MyTasks", db.Issues.Where(n=>n.Asignee == currentUser.Id).ToList());
        }

        public ActionResult UploadFile(System.Web.HttpPostedFileBase UploadFile)
        {
            if (UploadFile != null)
            {
                string dosyaYolu = Path.GetFileName(UploadFile.FileName);
                var yuklemeYeri = Path.Combine(Server.MapPath("~/Dosyalar"), dosyaYolu);
                UploadFile.SaveAs(yuklemeYeri);
            }

            return View("GeriDonut");
        }
     
        public ActionResult UploadFile()
        {


            return View("GeriDonut");
        }
    }
}