using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Osobni_telefonski_imenik.Models;

namespace Osobni_telefonski_imenik.Controllers
{
    [Authorize]
    public class OsobaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OsobaController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            ViewBag.GradID = new SelectList(_context.Grad, "GradID", "Naziv");
            return View();
        }

        [HttpPost]
        public ActionResult Create([Bind(Include = "OsobaID,GradID,Ime,Prezime,Opis,UserID")] Osoba osoba)
        {
            if (ModelState.IsValid)
            {
                osoba.OsobaID = Guid.NewGuid();
                osoba.UserID = User.Identity.GetUserId();
                _context.Osoba.Add(osoba);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(osoba);
        }

        [HttpGet]
        public ActionResult Edit(Guid? ID)
        {
            if (ID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Osoba osoba = _context.Osoba.Find(ID);
            ViewBag.GradID = new SelectList(_context.Grad, "GradID", "Naziv", osoba.OsobaID);
            return View(osoba);
        }

        [HttpPost]
        public ActionResult Edit([Bind(Include = "OsobaID,GradID,Ime,Prezime,Opis,UserID")] Osoba osoba)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(osoba).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(osoba);
        }

        [HttpPost, ActionName("Delete")]
        public JsonResult Delete(Guid? ID)
        {
            Osoba osoba = _context.Osoba.Find(ID);

            if (ID == null)
            {
                return null;
            }
            else
            {
                _context.Osoba.Remove(osoba);
                _context.SaveChanges();
            }
            return Json(osoba);
        }

        public JsonResult GetOsobe()
        {
            var UserID = User.Identity.GetUserId();
            String nazivGrada;
            var gradovi = _context.Osoba.Where(x => x.UserID == UserID).Select(x => new { x.OsobaID, x.Ime, x.Prezime, nazivGrada = x.Grad.Naziv, x.Opis }).ToList();
            return Json(gradovi, JsonRequestBehavior.AllowGet);
        }
    }
}