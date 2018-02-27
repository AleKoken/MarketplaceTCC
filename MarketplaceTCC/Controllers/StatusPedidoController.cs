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
    public class StatusPedidoController : Controller
    {
        private Context db = new Context();

        // GET: StatusPedido
        public ActionResult Index()
        {
            return View(db.StatusPedidoes.ToList());
        }

        // GET: StatusPedido/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StatusPedido statusPedido = db.StatusPedidoes.Find(id);
            if (statusPedido == null)
            {
                return HttpNotFound();
            }
            return View(statusPedido);
        }

        // GET: StatusPedido/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StatusPedido/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StatusPedidoId,Nome")] StatusPedido statusPedido)
        {
            if (ModelState.IsValid)
            {
                db.StatusPedidoes.Add(statusPedido);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(statusPedido);
        }

        // GET: StatusPedido/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StatusPedido statusPedido = db.StatusPedidoes.Find(id);
            if (statusPedido == null)
            {
                return HttpNotFound();
            }
            return View(statusPedido);
        }

        // POST: StatusPedido/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StatusPedidoId,Nome")] StatusPedido statusPedido)
        {
            if (ModelState.IsValid)
            {
                db.Entry(statusPedido).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(statusPedido);
        }

        // GET: StatusPedido/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StatusPedido statusPedido = db.StatusPedidoes.Find(id);
            if (statusPedido == null)
            {
                return HttpNotFound();
            }
            return View(statusPedido);
        }

        // POST: StatusPedido/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StatusPedido statusPedido = db.StatusPedidoes.Find(id);
            db.StatusPedidoes.Remove(statusPedido);
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
