using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MarketplaceTCC.Models;

namespace MarketplaceTCC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class MarcasController : Controller
    {
        private Context db = new Context();

        // GET: Marcas
        public ActionResult Index()
        {
            return View(db.Marcas.ToList());
        }

        // GET: Marcas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Marcas marcas = db.Marcas.Find(id);
            if (marcas == null)
            {
                return HttpNotFound();
            }
            return View(marcas);
        }

        // GET: Marcas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Marcas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MarcasId,Nome")] Marcas marcas)
        {
            if (ModelState.IsValid)
            {
                db.Marcas.Add(marcas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(marcas);
        }

        // GET: Marcas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Marcas marcas = db.Marcas.Find(id);
            if (marcas == null)
            {
                return HttpNotFound();
            }
            return View(marcas);
        }

        // POST: Marcas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MarcasId,Nome")] Marcas marcas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(marcas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(marcas);
        }

        // GET: Marcas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Marcas marcas = db.Marcas.Find(id);
            if (marcas == null)
            {
                return HttpNotFound();
            }
            return View(marcas);
        }

        // POST: Marcas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Marcas marcas = db.Marcas.Find(id);
            db.Marcas.Remove(marcas);
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
