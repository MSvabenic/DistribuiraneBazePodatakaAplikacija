using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Osobni_telefonski_imenik.Models;

namespace Osobni_telefonski_imenik.Controllers
{
    [Authorize]
    public class GradController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GradController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            ViewBag.DrzavaID = new SelectList(_context.Drzava, "DrzavaID", "Naziv");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GradID,DrzavaID,Naziv")] Grad grad)
        {
            if (ModelState.IsValid)
            {
                grad.GradID = Guid.NewGuid();
                _context.Grad.Add(grad);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(grad);
        }

        [HttpGet]
        public ActionResult Edit(Guid? ID)
        {
            if (ID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Grad grad = _context.Grad.Find(ID);
            ViewBag.DrzavaID = new SelectList(_context.Drzava, "DrzavaID", "Naziv", grad.GradID);
            return View(grad);
        }

        [HttpPost]
        public ActionResult Edit([Bind(Include = "GradID,DrzavaID,Naziv")] Grad grad)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(grad).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(grad);
        }

        [HttpPost, ActionName("Delete")]
        public JsonResult Delete(Guid? ID)
        {
            Grad grad = _context.Grad.Find(ID);

            if (ID == null)
            {
                return null;
            }
            else
            {
                _context.Grad.Remove(grad);
                _context.SaveChanges();
            }
            return Json(grad);
        }

        public JsonResult GetGradovi()
        {
            String nazivDrzave;
            var gradovi = _context.Grad.Select(x => new {x.GradID, x.Naziv, nazivDrzave = x.Drzava.Naziv}).ToList();
            return Json(gradovi, JsonRequestBehavior.AllowGet);
        }
    }
}