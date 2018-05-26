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
            ViewBag.OsobaID = new SelectList(_context.Osoba, "OsobaID","ImePrezime");
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

            var broj = new OsobaBroj()
            {
                ID = new Guid(),
                OsobaID = viewModel.OsobaID,
                BrojTipID = viewModel.BrojTipID,
                Broj = viewModel.Broj,
                Opis = viewModel.OpisBroja
            };

            _context.OsobaBroj.Add(broj);
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
                Broj = _context.OsobaBroj.Where(x => x.ID == ID).Select(x => x.Broj).FirstOrDefault(),
                BrojTip = _context.BrojTip.ToList(),
                OpisBroja = _context.OsobaBroj.Where(x => x.ID == ID).Select(x => x.Opis).FirstOrDefault()
            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Edit([Bind(Include = "OsobaID,BrojID,BrojTipID,Broj,OpisBroja")] BrojeviViewModel brojevi, Guid? ID)
        {
            ViewBag.BrojTipID = new SelectList(_context.BrojTip, "BrojTipID", "Naziv");
            OsobaBroj osobaBroj = _context.OsobaBroj.SingleOrDefault(x => x.ID == ID);

            if (ModelState.IsValid)
            {
                osobaBroj.BrojTipID = brojevi.BrojTipID;
                osobaBroj.Broj = brojevi.Broj;
                osobaBroj.Opis = brojevi.OpisBroja;
                _context.Entry(osobaBroj).State = EntityState.Modified;
                _context.SaveChanges();
            }
            return RedirectToAction("EditIndex", new {@ID = osobaBroj.OsobaID});
        }

        [HttpPost, ActionName("Delete")]
        public JsonResult Delete(Guid? ID)
        {
            OsobaBroj brojevi = _context.OsobaBroj.Find(ID);

            if (ID == null)
            {
                return null;
            }
            else
            {
                _context.OsobaBroj.Remove(brojevi);
                _context.SaveChanges();
            }
            return Json(brojevi);
        }

        public JsonResult GetBrojevi(Guid ID)
        {
            var brojevi = _context.OsobaBroj.Where(x => x.OsobaID == ID)
                .Select(x => new
                {
                    brojID = x.ID,
                    broj = x.Broj,
                    brojTip = x.BrojTip.Naziv,
                    opis = x.Opis
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
                                 Broj = string.Join(",", g.Select(x => x.Broj))
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