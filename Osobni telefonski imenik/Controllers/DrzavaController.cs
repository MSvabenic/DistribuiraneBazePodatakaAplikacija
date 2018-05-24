using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Osobni_telefonski_imenik.Models;
using Osobni_telefonski_imenik.ViewModels;

namespace Osobni_telefonski_imenik.Controllers
{
    [Authorize]
    public class DrzavaController : Controller
    {
        private ApplicationDbContext _context;

        public DrzavaController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult Index()
        {
           return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create([Bind(Include = "DrzavaID,Naziv")] Drzava drzava)
        {
            if (ModelState.IsValid)
            {
                drzava.DrzavaID = Guid.NewGuid();
                _context.Drzava.Add(drzava);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(drzava);
        }

        [HttpGet]
        public ActionResult Edit(Guid? ID)
        {
            if (ID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Drzava drzava = _context.Drzava.Find(ID);
            return View(drzava);
        }

        [HttpPost]
        public ActionResult Edit([Bind(Include = "DrzavaID,Naziv")] Drzava drzava)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(drzava).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(drzava);
        }

        [HttpPost,ActionName("Delete")]
        public JsonResult Delete(Guid? ID)
        {
            Drzava drzava = _context.Drzava.Find(ID);

            if (ID == null)
            {
                return null;
            }
            else
            {
                _context.Drzava.Remove(drzava);
                _context.SaveChanges();
            }
            return Json(drzava);
        }

        public JsonResult GetDrzave()
        {
            var drzave = _context.Drzava.ToList();
            return Json(drzave, JsonRequestBehavior.AllowGet);
        }
    }
}