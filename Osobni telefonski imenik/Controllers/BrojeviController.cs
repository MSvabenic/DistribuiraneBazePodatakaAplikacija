using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Osobni_telefonski_imenik.Models;
using Osobni_telefonski_imenik.ViewModels;

namespace Osobni_telefonski_imenik.Controllers
{
    [Authorize]
    public class BrojeviController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BrojeviController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Create()
        {
            ViewBag.OsobaID = new SelectList(_context.Osoba, "OsobaID", "ImePrezime");
            ViewBag.BrojTipID = new SelectList(_context.BrojTip, "BrojTipID", "Naziv");

            var viewModel = new BrojeviViewModel()
            {
                Osoba = _context.Osoba.ToList(),
                BrojTip = _context.BrojTip.ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Create(BrojeviViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Osoba = _context.Osoba.ToList();
                viewModel.BrojTip = _context.BrojTip.ToList();
                return View("Create", viewModel);
            }

            var broj = new Brojevi
            {
                ID = new Guid(),
                BrojTipID = viewModel.BrojTipID,
                Broj = viewModel.Broj,
                Opis = viewModel.OpisBroja
            };

            var osobaBroj = new OsobaBroj
            {
                ID = new Guid(),
                OsobaID = viewModel.OsobaID,
                BrojID = broj.ID
            };

            _context.Broj.Add(broj);
            _context.OsobaBroj.Add(osobaBroj);
            _context.SaveChanges();

            return RedirectToAction("Index", "Brojevi");
        }

        public ActionResult EditIndex(Guid? ID)
        {
            return View();
        }

        [HttpGet]
        public ActionResult Edit(Guid? ID)
        {
            ViewBag.BrojTipID = new SelectList(_context.BrojTip, "BrojTipID", "Naziv");

            var viewModel = new BrojeviViewModel()
            {
                Broj = _context.Broj.Where(x => x.ID == ID).Select(x => x.Broj).FirstOrDefault(),
                BrojTip = _context.BrojTip.ToList(),
                OpisBroja = _context.Broj.Where(x => x.ID == ID).Select(x => x.Opis).FirstOrDefault()
            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Edit([Bind(Include = "OsobaID,BrojID,BrojTipID,Broj,OpisBroja")] BrojeviViewModel brojevi, Guid? ID)
        {
            ViewBag.BrojTipID = new SelectList(_context.BrojTip, "BrojTipID", "Naziv");
            Brojevi broj = _context.Broj.SingleOrDefault(x => x.ID == ID);
            var redirectID = _context.OsobaBroj.Where(x => x.BrojID == ID).Select(x => x.OsobaID).FirstOrDefault();

            if (ModelState.IsValid)
            {
                broj.BrojTipID = brojevi.BrojTipID;
                broj.Broj = brojevi.Broj;
                broj.Opis = brojevi.OpisBroja;
                _context.Entry(broj).State = EntityState.Modified;
                _context.SaveChanges();
            }
            return RedirectToAction("EditIndex", new { @ID = redirectID});
        }

        [HttpPost, ActionName("Delete")]
        public JsonResult Delete(Guid? ID)
        {
            Brojevi brojevi = _context.Broj.Find(ID);

            if (ID == null)
            {
                return null;
            }
            else
            {
                _context.Broj.Remove(brojevi);
                _context.SaveChanges();
            }
            return Json(brojevi);
        }

        public JsonResult GetBrojevi(Guid ID)
        {
            var brojevi = _context.OsobaBroj.Where(x => x.OsobaID == ID)
                .Select(x => new
                {
                    brojID = x.BrojID,
                    broj = x.Broj.Broj,
                    brojTip = x.Broj.BrojTip.Naziv,
                    opis = x.Broj.Broj
                });
            return Json(brojevi, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetSviBrojevi()
        {
            var UserID = User.Identity.GetUserId();
            var svibrojevi = from bro in _context.OsobaBroj.ToList()
                             group bro by bro.Osoba.OsobaID into g
                             select new
                             {
                                 OsobaId = g.Key,
                                 Broj = string.Join(",", g.Select(x => x.Broj.Broj))
                             };

            var osoba = from brojevi in svibrojevi
                        join osobe in _context.Osoba on brojevi.OsobaId equals osobe.OsobaID
                        where UserID == osobe.UserID
                        select new
                        {
                            OsobaId = osobe.OsobaID,
                            Ime = osobe.Ime,
                            Prezime = osobe.Prezime,
                            Broj = brojevi.Broj
                        };

            return Json(osoba, JsonRequestBehavior.AllowGet);
        }
    }
}