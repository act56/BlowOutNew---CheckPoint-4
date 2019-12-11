using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using BlowOutNew.DAL;
using BlowOutNew.Models;

namespace BlowOutNew.Controllers
{
    public class HomeController : Controller
    {
        private BlowOutNewContext db = new BlowOutNewContext();

        public ActionResult Index()
        {
            return View();
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

        public ActionResult Rentals()
        {
            return View(db.Instruments.ToList());
        }

        //User must go through this method in order to see UpdateData
        [HttpGet]
        public ActionResult Login()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Login(FormCollection form, bool rememberMe = false)
        {
            String username = form["Username"].ToString();
            String password = form["Password"].ToString();

            if (username == "Missouri" && password == "ShowMe")
            {
                FormsAuthentication.SetAuthCookie(username, rememberMe);
                return RedirectToAction("UpdateData", "Home");
            }
            else
            {
                return View();
            }
            
        }


        //When user logs in, they are automatically directed to this method, which displays the UpdateData view
        [Authorize]
        public ActionResult UpdateData()
        {
            IEnumerable<InstrumentClient> lstInstruments = db.Database.SqlQuery<InstrumentClient>(
                "SELECT client.clientID, client.first_Name, client.last_Name, client.address, client.city, client.state, client.email, client.zip, client.phone, " +
                "instrument.instrumentID, instrument.description, instrument.type, instrument.price " +
                "FROM  instrument INNER JOIN client on instrument.clientID = client.clientID "
                );
            return View(lstInstruments);
        }


        //Edits the client record from the indicated client in the list from UpdateData
        [Authorize]
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Instrument newInstrument = db.Instruments.Find(id);
            if (newInstrument == null)
            {
                return HttpNotFound();
            }
            Client newClient = db.Clients.Find(newInstrument.clientID);
            if (newClient == null)
            {
                return HttpNotFound();
            }
            return View(newClient);
        }


        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "clientID,first_Name,last_Name,address,city,state,zip,email,phone")] Client client)
        {
            if (ModelState.IsValid)
            {
                db.Entry(client).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("UpdateData");
            }
            return View(client);
        }

        //Deletes the client record from indicated client in list from UpdateData view, also makes clientID in corresponding instrument table null
        [Authorize]
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Instrument newInstrument = db.Instruments.Find(id);
            if (newInstrument == null)
            {
                return HttpNotFound();
            }
            Client newClient = db.Clients.Find(newInstrument.clientID);
            if (newClient == null)
            {
                return HttpNotFound();
            }
            newInstrument.clientID = null;

            db.Instruments.Attach(newInstrument);
            db.Entry(newInstrument).Property(x => x.clientID).IsModified = true;
            db.SaveChanges();

            db.Clients.Remove(newClient);
            db.SaveChanges();
            return RedirectToAction("UpdateData");
        }


    }
}