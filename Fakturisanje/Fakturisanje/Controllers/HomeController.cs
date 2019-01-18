using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Fakturisanje.Models;
using Fakturisanje.viewModel;

namespace Fakturisanje.Controllers
{
    public class HomeController : Controller
    {
        Model1 db = new Model1();

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
        }
        public ActionResult Index()
        {
            var prod = db.Stavkes.ToList();
            var prod1 = db.Stavkes.ToList();            
            var viewModel1 = new Stavke1
            {

                Stavken = prod,
                Stavken1 = prod1,
                
                
            };           
            return View(viewModel1);
        }        
        public ActionResult Save(Stavke stavke)
        {
            if (stavke.Redni_broj == 0)
            {
                db.Stavkes.Add(stavke);
                
            }
            else
            {
                var update = db.Stavkes.SingleOrDefault(p => p.Redni_broj == stavke.Redni_broj);
                update.Cena = stavke.Cena;
                update.Kolicina = stavke.Kolicina;
                update.Ukupno = stavke.Ukupno;
                
            }
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Edit(int Id)
        {
            var e = db.Stavkes.SingleOrDefault(p => p.Redni_broj == Id);

            if (e == null)
                return HttpNotFound();

            var viewModel1 = new Stavke1
            {
                Stavke = e
            };
                return View("Index", viewModel1);       

        }
        public ActionResult Delete(int Id)
        {
            Stavke s = db.Stavkes.SingleOrDefault(g => g.Redni_broj == Id);
            db.Stavkes.Remove(s);
            db.SaveChanges();
            return RedirectToAction("PregledStavki", "Home");

        }
        public ActionResult PregledStavki()
        {
            List<Stavke> prod = db.Stavkes.ToList();
            return View(prod);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}