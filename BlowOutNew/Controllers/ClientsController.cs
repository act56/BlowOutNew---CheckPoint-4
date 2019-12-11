using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BlowOutNew.DAL;
using BlowOutNew.Models;

namespace BlowOutNew.Controllers
{
    public class ClientsController : Controller
    {
        private BlowOutNewContext db = new BlowOutNewContext();

        // GET: Clients
        public ActionResult Index()
        {
            return View(db.Clients.ToList());
        }

        // GET: Clients/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }
        // GET: Clients/Create
        [HttpGet]
        public ActionResult Create(int ID)
        {
            return View();
        }

        // POST: Clients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "clientID,first_Name,last_Name,address,city,state,zip,email,phone")] Client client, int ID)
        {
            if (ModelState.IsValid)
            {
                db.Clients.Add(client);
                db.SaveChanges();

                //lookup instrument
                Instrument instrument = db.Instruments.Find(ID);
                //update instrument
                instrument.clientID = client.clientID;
                using (db)
                {
                    db.Instruments.Attach(instrument);
                    db.Entry(instrument).Property(x => x.clientID).IsModified = true;
                    db.SaveChanges();
                }
                    //db.Entry(client).State = EntityState.Modified;
                    //save changes

                    return RedirectToAction("Summary", new { ClientID = client.clientID, InstrumentID = instrument.instrumentID });
            }

            return View(client);
        }

        public ActionResult Summary(int ClientID, int InstrumentID)
        {
            Client client = db.Clients.Find(ClientID);
            Instrument instrument = db.Instruments.Find(InstrumentID);

            ViewBag.Client = client;
            ViewBag.Instrument = instrument.description;
            ViewBag.Price = instrument.price;
            ViewBag.TotalPrice = int.Parse(instrument.price);
            ViewBag.Image = instrument.image;
            ViewBag.Type = instrument.type;

            return View();
        }

        // GET: Clients/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // POST: Clients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "clientID,first_Name,last_Name,address,city,state,zip,email,phone")] Client client)
        {
            if (ModelState.IsValid)
            {
                db.Entry(client).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(client);
        }

        // GET: Clients/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Client client = db.Clients.Find(id);
            db.Clients.Remove(client);
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
