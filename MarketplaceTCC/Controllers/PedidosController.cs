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
    public class PedidosController : Controller
    {
        private Context db = new Context();

        // GET: Pedidos
        public ActionResult Index()
        {
            var pedidos = db.Pedidos.Include(p => p.Cliente).Include(p => p.StatusPedido);
            return View(pedidos.ToList());
        }


        // GET: Meus Pedidos Clientes
        public ActionResult MeusPedidos()
        {
            var user = db.Clientes.Where(v => v.UserName == User.Identity.Name).FirstOrDefault();
            var clienteid = user.ClientesId;
            var pedidos = db.Pedidos.Include(p => p.Cliente).Include(p => p.StatusPedido);
            pedidos = pedidos.Where(p => p.ClientesId == clienteid);
            return View(pedidos.ToList());
        }



        // GET: Pedidos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pedidos pedidos = db.Pedidos.Find(id);
            if (pedidos == null)
            {
                return HttpNotFound();
            }
            return View(pedidos);
        }

        // GET: Pedidos/Details/5
        public ActionResult Detalhes(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pedidos pedidos = db.Pedidos.Find(id);
            if (pedidos == null)
            {
                return HttpNotFound();
            }
            var numero = Int32.Parse((string)pedidos.Numero);
            var detalhes = db.ItensPedidoes.Where(p => p.Numero == numero);
            return View(detalhes);
        }

        // GET: Pedidos/Create
        public ActionResult Create()
        {
            ViewBag.ClientesId = new SelectList(db.Clientes, "ClientesId", "Nome");
            ViewBag.StatusPedidoId = new SelectList(db.StatusPedidoes, "StatusPedidoId", "Nome");
            return View();
        }

        // POST: Pedidos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PedidosId,Numero,DataPedido,ValorTotal,ClientesId,StatusPedidoId")] Pedidos pedidos)
        {
            if (ModelState.IsValid)
            {
                var user = db.Clientes.Where(v => v.UserName == User.Identity.Name).FirstOrDefault();
                var date = DateTime.Now;
                var cli = user.ClientesId;
                int n;
                n = new Random().Next(1000, 3000);
                pedidos.ClientesId = cli;
                pedidos.StatusPedidoId = 1;
                pedidos.DataPedido = date;
                pedidos.PedidosId = n;
                pedidos.Numero = n.ToString();
                Session["Pedido"] = n.ToString();
                db.Pedidos.Add(pedidos);
                db.SaveChanges();
                return RedirectToAction("Index", "Produtos");
            }

            ViewBag.ClientesId = new SelectList(db.Clientes, "ClientesId", "Nome", pedidos.ClientesId);
            ViewBag.StatusPedidoId = new SelectList(db.StatusPedidoes, "StatusPedidoId", "Nome", pedidos.StatusPedidoId);
            return View(pedidos);
        }


        public ActionResult CriarCarrinho()
        {
            Pedidos pedidos = new Pedidos();
            var user = db.Clientes.Where(v => v.UserName == User.Identity.Name).FirstOrDefault();
            var date = DateTime.Now;
            var cli = user.ClientesId;
            int n;
            n = new Random().Next(1000, 3000);
            pedidos.ClientesId = cli;
            pedidos.StatusPedidoId = 1;
            pedidos.DataPedido = date;
            pedidos.PedidosId = n;
            pedidos.Numero = n.ToString();
            Session["Pedido"] = n.ToString();
            db.Pedidos.Add(pedidos);
            db.SaveChanges();
            return RedirectToAction("Index", "Produtos");
        }

        public ActionResult FinalizarPedido()
        {
            return RedirectToAction("MeusPedidos", "Pedidos");
        }



        // GET: Pedidos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pedidos pedidos = db.Pedidos.Find(id);
            if (pedidos == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClientesId = new SelectList(db.Clientes, "ClientesId", "Nome", pedidos.ClientesId);
            ViewBag.StatusPedidoId = new SelectList(db.StatusPedidoes, "StatusPedidoId", "Nome", pedidos.StatusPedidoId);
            return View(pedidos);
        }

        // POST: Pedidos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PedidosId,Numero,DataPedido,ValorTotal,ClientesId,StatusPedidoId")] Pedidos pedidos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pedidos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClientesId = new SelectList(db.Clientes, "ClientesId", "Nome", pedidos.ClientesId);
            ViewBag.StatusPedidoId = new SelectList(db.StatusPedidoes, "StatusPedidoId", "Nome", pedidos.StatusPedidoId);
            return View(pedidos);
        }

        // GET: Pedidos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pedidos pedidos = db.Pedidos.Find(id);
            if (pedidos == null)
            {
                return HttpNotFound();
            }
            return View(pedidos);
        }

        // POST: Pedidos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pedidos pedidos = db.Pedidos.Find(id);
            db.Pedidos.Remove(pedidos);
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
