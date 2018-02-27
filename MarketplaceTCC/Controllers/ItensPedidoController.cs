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
    public class ItensPedidoController : Controller
    {
        private Context db = new Context();

        // GET: ItensPedido
        public ActionResult Index()
        {
            return View(db.ItensPedidoes.ToList());
        }


        // GET: ItensPedido
        public ActionResult Carrinho()
        {
            var pedido = Int32.Parse((string)Session["Pedido"]);
            var carrinho = db.ItensPedidoes.Where(p => p.Numero == pedido);
            return View(carrinho.ToList());
        }




        // GET: ItensPedido/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItensPedido itensPedido = db.ItensPedidoes.Find(id);
            if (itensPedido == null)
            {
                return HttpNotFound();
            }
            return View(itensPedido);
        }

        // GET: ItensPedido/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ItensPedido/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ItensPedidoId,ProdutosId,Numero,Quantidade")] ItensPedido itensPedido)
        {
            if (ModelState.IsValid)
            {
                db.ItensPedidoes.Add(itensPedido);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(itensPedido);
        }



        public ActionResult AddProduto(int id)
        {
            ItensPedido itensPedido = new ItensPedido();
            Produtos produto = db.Produtos.Find(id);

            itensPedido.ProdutosId = produto.ProdutosId;
            int pedi = Int32.Parse((string)Session["Pedido"]);

            if (ModelState.IsValid)
            {
                itensPedido.Numero = pedi;
                itensPedido.Quantidade = 1;
                produto.Estoque = produto.Estoque - 1;
                db.Entry(produto).State = EntityState.Modified;
                db.ItensPedidoes.Add(itensPedido);
                db.SaveChanges();
                return RedirectToAction("Carrinho", "ItensPedido");
            }

            return View(itensPedido);
        }




        // GET: ItensPedido/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItensPedido itensPedido = db.ItensPedidoes.Find(id);
            if (itensPedido == null)
            {
                return HttpNotFound();
            }
            return View(itensPedido);
        }

        // POST: ItensPedido/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ItensPedidoId,ProdutosId,Numero,Quantidade")] ItensPedido itensPedido)
        {


            if (ModelState.IsValid)
            {
                Produtos produto = new Produtos();
                var quant = itensPedido.Quantidade;
                produto = db.Produtos.Find(itensPedido.ProdutosId);
                produto.Estoque = (produto.Estoque - quant) + 1;
                db.Entry(produto).State = EntityState.Modified;
                db.Entry(itensPedido).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Carrinho", "ItensPedido");
            }
            return View(itensPedido);
        }

        // GET: ItensPedido/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItensPedido itensPedido = db.ItensPedidoes.Find(id);
            if (itensPedido == null)
            {
                return HttpNotFound();
            }
            return View(itensPedido);
        }

        // POST: ItensPedido/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Produtos produto = new Produtos();
            ItensPedido itensPedido = db.ItensPedidoes.Find(id);

            var quant = itensPedido.Quantidade;
            var prod = itensPedido.ProdutosId;
            produto = db.Produtos.Find(prod);
            produto.Estoque = produto.Estoque + quant;
            db.Entry(produto).State = EntityState.Modified;
            db.ItensPedidoes.Remove(itensPedido);
            db.SaveChanges();
            return RedirectToAction("Carrinho", "ItensPedido");
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
