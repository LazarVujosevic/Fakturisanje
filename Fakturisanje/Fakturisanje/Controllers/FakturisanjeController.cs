using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Fakturisanje.Models;
using System.Data.SqlClient;


namespace Fakturisanje.Controllers
{
    public class BasicAuthenticationAttribute : ActionFilterAttribute
    {
       
        public string BasicRealm { get; set; }
        protected string Username { get; }
        protected string Password { get; }

        public BasicAuthenticationAttribute(string username, string password)
        {

            this.Username = username;
            this.Password = password;
        }
        
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var req = filterContext.HttpContext.Request;
            var auth = req.Headers["Authorization"];
            if (!String.IsNullOrEmpty(auth))
            {
                var cred = System.Text.ASCIIEncoding.ASCII.GetString(Convert.FromBase64String(auth.Substring(6))).Split(':');
                var user = new { Name = cred[0], Pass = cred[1] };
                if (user.Name == Username && user.Pass == Password) return;
            }
            filterContext.HttpContext.Response.AddHeader("WWW-Authenticate", String.Format("Basic realm=\"{0}\"", BasicRealm));            
            filterContext.Result = new HttpUnauthorizedResult();
            
            
        }

    }
    public class FakturisanjeController : Controller
    {
        Model1 db = new Model1();

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
        }
        
        public ActionResult Index()
        {
            return View();
        }
        
        [BasicAuthentication("saradnik", "saradnik")]
        public ActionResult PregledFaktura()
        {
            List<Fakture> prod = db.Faktures.ToList();
            
            return View(prod);
            
        }

        public ActionResult NovaFaktura(int ukupno)
        {
            var e = db.Faktures.SingleOrDefault(p => p.Ukupno == ukupno);
            return View(e);
        }

        public ActionResult Save(Fakture fakturen)
        {

            try
            {
                if (fakturen.Id == 0)
                {
                    fakturen.Datum_Fakture = DateTime.Now;
                    db.Faktures.Add(fakturen);
                    db.SaveChanges();
                    SqlConnection conn = new SqlConnection(@"Data Source=lazar-pc\sqlexpress;Initial Catalog=Fakturisanje;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework");
                    SqlCommand cmd = new SqlCommand("TRUNCATE TABLE Stavke", conn);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
                else
                {
                    var update = db.Faktures.SingleOrDefault(p => p.Id == fakturen.Id);
                    update.Br_Dokumenta = fakturen.Br_Dokumenta;
                    update.Ukupno = fakturen.Ukupno;                    
                    db.SaveChanges();
                }
                return RedirectToAction("PregledFaktura", "Fakturisanje");
                

            }
            catch
            {
                               
                return RedirectToAction("PregledFaktura", "Fakturisanje");
                
            }
            
            
        }
        public ActionResult Edit(int Id)
        {
            var e = db.Faktures.SingleOrDefault(p => p.Id == Id);
            if (e == null)
                return HttpNotFound();
            else
                return View("NovaFaktura", e);

        }

        public ActionResult Delete(int Id)
        {
            var e = db.Faktures.SingleOrDefault(p => p.Id == Id);
            db.Faktures.Remove(e);
            db.SaveChanges();
            return RedirectToAction("PregledFaktura", "Fakturisanje");
        }


    }
}