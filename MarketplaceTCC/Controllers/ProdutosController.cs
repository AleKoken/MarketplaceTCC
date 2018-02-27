using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MarketplaceTCC.Models;
using MarketplaceTCC.Classes;

namespace MarketplaceTCC.Controllers
{
    [Authorize(Roles = "Admin,Vendedor")]
    public class ProdutosController : Controller
    {
        private Context db = new Context();

        // GET: Produtos
        [AllowAnonymous]
        public ActionResult Index()
        {
            var produtos = db.Produtos.Include(p => p.Categoria).Include(p => p.Marca).Include(p => p.Status).Include(p => p.Vendedor);
            produtos = produtos.Where(p => p.StatusId == 1);
            return View(produtos.ToList());
        }

        // GET: Produtos
        [Authorize(Roles = "Admin")]
        public ActionResult Ativos()
        {
            var produtos = db.Produtos.Include(p => p.Categoria).Include(p => p.Marca).Include(p => p.Status).Include(p => p.Vendedor);
            produtos = produtos.Where(p => p.StatusId == 1);
            return View(produtos.ToList());
        }



        // GET: Produtos
        [Authorize(Roles = "Admin")]
        public ActionResult Todos()
        {
            var produtos = db.Produtos.Include(p => p.Categoria).Include(p => p.Marca).Include(p => p.Status).Include(p => p.Vendedor);
            return View(produtos.ToList());
        }

        // GET: Produtos
        [Authorize(Roles = "Admin")]
        public ActionResult Bloqueados()
        {
            var produtos = db.Produtos.Include(p => p.Categoria).Include(p => p.Marca).Include(p => p.Status).Include(p => p.Vendedor);
            produtos = produtos.Where(p => p.StatusId == 2);
            return View(produtos.ToList());
        }



        // GET: Meus Produtos
        [Authorize(Roles = "Vendedor")]
        public ActionResult MeusProdutos()
        {
            var user = db.Vendedores.Where(v => v.UserName == User.Identity.Name).FirstOrDefault();
            var vendedorid = user.VendedoresId;
            var produtos = db.Produtos.Include(p => p.Categoria).Include(p => p.Marca).Include(p => p.Status).Include(p => p.Vendedor);
            produtos = produtos.Where(p => p.VendedoresId == vendedorid);
            return View(produtos.ToList());
        }

        // GET: Produtos/Details
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produtos produtos = db.Produtos.Find(id);
            if (produtos == null)
            {
                return HttpNotFound();
            }
            return View(produtos);
        }

        // GET: Produtos/Create
        public ActionResult Create()
        {
            ViewBag.CategoriasId = new SelectList(db.Categorias, "CategoriasId", "Nome");
            ViewBag.MarcasId = new SelectList(db.Marcas, "MarcasId", "Nome");
            ViewBag.StatusId = new SelectList(db.Status, "StatusId", "Nome");
            ViewBag.VendedoresId = new SelectList(db.Vendedores, "VendedoresId", "Nome");
            return View();
        }

        // POST: Produtos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Produtos produtos)
        {
            if (ModelState.IsValid)
            {
                var user = db.Vendedores.Where(v => v.UserName == User.Identity.Name).FirstOrDefault();
                var pic = string.Empty;
                var folder = "~/Content/img";

                if (produtos.ImagemFile == null)
                {
                    produtos.Imagem = produtos.Imagem;
                }
                else
                {
                    pic = FilesHelper.UploadPhoto(produtos.ImagemFile, folder);
                    pic = string.Format("{0}/{1}", folder, pic);
                    produtos.Imagem = pic;
                }
                produtos.VendedoresId = user.VendedoresId;
                produtos.StatusId = 2;
                db.Produtos.Add(produtos);
                db.SaveChanges();
                return RedirectToAction("MeusProdutos");
            }

            ViewBag.CategoriasId = new SelectList(db.Categorias, "CategoriasId", "Nome", produtos.CategoriasId);
            ViewBag.MarcasId = new SelectList(db.Marcas, "MarcasId", "Nome", produtos.MarcasId);
            ViewBag.StatusId = new SelectList(db.Status, "StatusId", "Nome", produtos.StatusId);
            ViewBag.VendedoresId = new SelectList(db.Vendedores, "VendedoresId", "Nome", produtos.VendedoresId);
            return View(produtos);
        }

        // GET: Produtos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produtos produtos = db.Produtos.Find(id);
            if (produtos == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoriasId = new SelectList(db.Categorias, "CategoriasId", "Nome", produtos.CategoriasId);
            ViewBag.MarcasId = new SelectList(db.Marcas, "MarcasId", "Nome", produtos.MarcasId);
            ViewBag.StatusId = new SelectList(db.Status, "StatusId", "Nome", produtos.StatusId);
            ViewBag.VendedoresId = new SelectList(db.Vendedores, "VendedoresId", "Nome", produtos.VendedoresId);
            return View(produtos);
        }

        // POST: Produtos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Produtos produtos)
        {
            if (ModelState.IsValid)
            {
              //var user = db.Vendedores.Where(v => v.UserName == User.Identity.Name).FirstOrDefault();
                var pic = string.Empty;
                var folder = "~/Content/img";

                if (produtos.ImagemFile != null)
                {
                    pic = FilesHelper.UploadPhoto(produtos.ImagemFile, folder);
                    pic = string.Format("{0}/{1}", folder, pic);
                    produtos.Imagem = pic;
                }
             // produtos.VendedoresId = user.VendedoresId;
                db.Entry(produtos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Edit");
            }
            ViewBag.CategoriasId = new SelectList(db.Categorias, "CategoriasId", "Nome", produtos.CategoriasId);
            ViewBag.MarcasId = new SelectList(db.Marcas, "MarcasId", "Nome", produtos.MarcasId);
            ViewBag.StatusId = new SelectList(db.Status, "StatusId", "Nome", produtos.StatusId);
            ViewBag.VendedoresId = new SelectList(db.Vendedores, "VendedoresId", "Nome", produtos.VendedoresId);
            return View(produtos);
        }






        // GET: Produtos/EditVendedor/5
        [Authorize(Roles = "Vendedor")]
        public ActionResult EditVendedor(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produtos produtos = db.Produtos.Find(id);
            if (produtos == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoriasId = new SelectList(db.Categorias, "CategoriasId", "Nome", produtos.CategoriasId);
            ViewBag.MarcasId = new SelectList(db.Marcas, "MarcasId", "Nome", produtos.MarcasId);
        //    ViewBag.StatusId = new SelectList(db.Status, "StatusId", "Nome", produtos.StatusId);
         //   ViewBag.VendedoresId = new SelectList(db.Vendedores, "VendedoresId", "Nome", produtos.VendedoresId);
            return View(produtos);
        }

        // POST: Produtos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Vendedor")]
        public ActionResult EditVendedor(Produtos produtos)
        {
            if (ModelState.IsValid)
            {
                //var user = db.Vendedores.Where(v => v.UserName == User.Identity.Name).FirstOrDefault();
                var pic = string.Empty;
                var folder = "~/Content/img";

                if (produtos.ImagemFile != null)
                {
                    pic = FilesHelper.UploadPhoto(produtos.ImagemFile, folder);
                    pic = string.Format("{0}/{1}", folder, pic);
                    produtos.Imagem = pic;
                }
                // produtos.VendedoresId = user.VendedoresId;
                db.Entry(produtos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Edit");
            }
            ViewBag.CategoriasId = new SelectList(db.Categorias, "CategoriasId", "Nome", produtos.CategoriasId);
            ViewBag.MarcasId = new SelectList(db.Marcas, "MarcasId", "Nome", produtos.MarcasId);
         //   ViewBag.StatusId = new SelectList(db.Status, "StatusId", "Nome", produtos.StatusId);
          //  ViewBag.VendedoresId = new SelectList(db.Vendedores, "VendedoresId", "Nome", produtos.VendedoresId);
            return View(produtos);
        }







        // GET: Produtos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produtos produtos = db.Produtos.Find(id);
            if (produtos == null)
            {
                return HttpNotFound();
            }
            return View(produtos);
        }

        // POST: Produtos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Produtos produtos = db.Produtos.Find(id);
            db.Produtos.Remove(produtos);
            db.SaveChanges();
            return RedirectToAction("MeusProdutos");
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
