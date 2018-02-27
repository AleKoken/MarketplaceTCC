using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using MarketplaceTCC.Models;
using static MarketplaceTCC.Classes.UserHelper;

namespace MarketplaceTCC.Controllers
{
    [Authorize(Roles = "Admin,Vendedor")]
    public class VendedoresController : Controller
    {
        private Context db = new Context();

        // GET: Vendedores
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var vendedores = db.Vendedores.Include(v => v.Cidade).Include(v => v.Estado).Include(v => v.Status);
            return View(vendedores.ToList());
        }

        // GET: Vendedores
        [Authorize(Roles = "Vendedor")]
        public ActionResult Conta()
        {
            var user = db.Vendedores.Where(v => v.UserName == User.Identity.Name).FirstOrDefault();
            var vendedorid = user.VendedoresId;
            var vendedores = db.Vendedores.Include(v => v.Cidade).Include(v => v.Estado).Include(v => v.Status);
            vendedores = vendedores.Where(p => p.VendedoresId == vendedorid);
            return View(vendedores.ToList());
        }


        // GET: Vendedores/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vendedores vendedores = db.Vendedores.Find(id);
            if (vendedores == null)
            {
                return HttpNotFound();
            }
            return View(vendedores);
        }

        // GET: Vendedores/Create
        [AllowAnonymous]
        public ActionResult Create()
        {
            ViewBag.CidadesId = new SelectList(db.Cidades, "CidadesId", "Nome");
            ViewBag.EstadosId = new SelectList(db.Estados, "EstadosId", "Nome");
            ViewBag.StatusId = new SelectList(db.Status, "StatusId", "Nome");
            return View();
        }

        // POST: Vendedores/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "VendedoresId,Nome,Nome_Fantasia,UserName,Password,CNPJ,Telefone,Endereco,CidadesId,EstadosId,Comissao,StatusId")] Vendedores vendedores)
        {
            if (ModelState.IsValid)
            {
                vendedores.StatusId = 1;
                db.Vendedores.Add(vendedores);
                try
                {
                    db.SaveChanges();
                    UsersHelper.CreateUserASP(vendedores.UserName, "Vendedor", vendedores.Password);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    if (ex.InnerException != null &&
                                                               ex.InnerException.InnerException != null &&
                                                               ex.InnerException.InnerException.Message.Contains("Email_Index"))
                    {
                        ModelState.AddModelError(string.Empty, "E-mail já cadastrado.");
                    }
                    else if (ex.InnerException != null &&
                                                               ex.InnerException.InnerException != null &&
                                                               ex.InnerException.InnerException.Message.Contains("CNPJ_Index"))

                    {
                        ModelState.AddModelError(string.Empty, "Este CNPJ já esta sendo usado.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, ex.Message);
                    }

                    ViewBag.CidadesId = new SelectList(db.Cidades, "CidadesId", "Nome", vendedores.CidadesId);
                    ViewBag.EstadosId = new SelectList(db.Estados, "EstadosId", "Nome", vendedores.EstadosId);
                    ViewBag.StatusId = new SelectList(db.Status, "StatusId", "Nome", vendedores.StatusId);
                    return View("Index", "Home");
                }
            }
            return View("Index", "Home");
        }

        // GET: Vendedores/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vendedores vendedores = db.Vendedores.Find(id);
            if (vendedores == null)
            {
                return HttpNotFound();
            }
            ViewBag.CidadesId = new SelectList(db.Cidades, "CidadesId", "Nome", vendedores.CidadesId);
            ViewBag.EstadosId = new SelectList(db.Estados, "EstadosId", "Nome", vendedores.EstadosId);
            ViewBag.StatusId = new SelectList(db.Status, "StatusId", "Nome", vendedores.StatusId);
            return View(vendedores);
        }

        // POST: Vendedores/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "VendedoresId,Nome,Nome_Fantasia,UserName,Password,CNPJ,Telefone,Endereco,CidadesId,EstadosId,Comissao,StatusId")] Vendedores vendedores)
        {
            if (ModelState.IsValid)
            {

                var db2 = new Context();
                var currentUser = db2.Vendedores.Find(vendedores.VendedoresId);
                if (currentUser.UserName != vendedores.UserName)
                {
                    UsersHelper.UpdateUserName(currentUser.UserName, vendedores.UserName);
                }
                db2.Dispose();
                db.Entry(vendedores).State = EntityState.Modified;

                try
                {
                    db.SaveChanges();
                    return RedirectToAction("Edit");
                }
                catch (Exception ex)
                {
                    if (ex.InnerException != null &&
                                                               ex.InnerException.InnerException != null &&
                                                               ex.InnerException.InnerException.Message.Contains("Email_Index"))
                    {
                        ModelState.AddModelError(string.Empty, "E-mail já cadastrado.");
                    }
                    else if (ex.InnerException != null &&
                                                               ex.InnerException.InnerException != null &&
                                                               ex.InnerException.InnerException.Message.Contains("CNPJ_Index"))

                    {
                        ModelState.AddModelError(string.Empty, "Este CNPJ já esta sendo usado.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, ex.Message);
                    }

                    ViewBag.CidadesId = new SelectList(db.Cidades, "CidadesId", "Nome", vendedores.CidadesId);
                    ViewBag.EstadosId = new SelectList(db.Estados, "EstadosId", "Nome", vendedores.EstadosId);
                    ViewBag.StatusId = new SelectList(db.Status, "StatusId", "Nome", vendedores.StatusId);
                    return View(vendedores);
                }
            }
            return View(vendedores);
        }



        // GET: Vendedores/Edit/5
        [Authorize(Roles = "Vendedor")]
        public ActionResult EditVendedor(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vendedores vendedores = db.Vendedores.Find(id);
            if (vendedores == null)
            {
                return HttpNotFound();
            }
            ViewBag.CidadesId = new SelectList(db.Cidades, "CidadesId", "Nome", vendedores.CidadesId);
            ViewBag.EstadosId = new SelectList(db.Estados, "EstadosId", "Nome", vendedores.EstadosId);
            ViewBag.StatusId = new SelectList(db.Status, "StatusId", "Nome", vendedores.StatusId);
            return View(vendedores);
        }

        // POST: Vendedores/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Vendedor")]
        public ActionResult EditVendedor([Bind(Include = "VendedoresId,Nome,Nome_Fantasia,UserName,Password,CNPJ,Telefone,Endereco,CidadesId,EstadosId,Comissao,StatusId")] Vendedores vendedores)
        {
            if (ModelState.IsValid)
            {

                var db2 = new Context();
                var currentUser = db2.Vendedores.Find(vendedores.VendedoresId);
                if (currentUser.UserName != vendedores.UserName)
                {
                    UsersHelper.UpdateUserName(currentUser.UserName, vendedores.UserName);
                }
                db2.Dispose();
                db.Entry(vendedores).State = EntityState.Modified;

                try
                {
                    db.SaveChanges();
                    return RedirectToAction("Conta");
                }
                catch (Exception ex)
                {
                    if (ex.InnerException != null &&
                                                               ex.InnerException.InnerException != null &&
                                                               ex.InnerException.InnerException.Message.Contains("Email_Index"))
                    {
                        ModelState.AddModelError(string.Empty, "E-mail já cadastrado.");
                    }
                    else if (ex.InnerException != null &&
                                                               ex.InnerException.InnerException != null &&
                                                               ex.InnerException.InnerException.Message.Contains("CNPJ_Index"))

                    {
                        ModelState.AddModelError(string.Empty, "Este CNPJ já esta sendo usado.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, ex.Message);
                    }

                    ViewBag.CidadesId = new SelectList(db.Cidades, "CidadesId", "Nome", vendedores.CidadesId);
                    ViewBag.EstadosId = new SelectList(db.Estados, "EstadosId", "Nome", vendedores.EstadosId);
                    ViewBag.StatusId = new SelectList(db.Status, "StatusId", "Nome", vendedores.StatusId);
                    return View(vendedores);
                }
            }
            return View(vendedores);
        }





        // GET: Vendedores/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vendedores vendedores = db.Vendedores.Find(id);
            if (vendedores == null)
            {
                return HttpNotFound();
            }
            return View(vendedores);
        }

        // POST: Vendedores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Vendedores vendedores = db.Vendedores.Find(id);
            db.Vendedores.Remove(vendedores);
            db.SaveChanges();
            UsersHelper.DeleteUser(vendedores.UserName);
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
