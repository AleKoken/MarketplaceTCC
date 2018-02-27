using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using MarketplaceTCC.Models;
using static MarketplaceTCC.Classes.UserHelper;

namespace MarketplaceTCC.Controllers
{
    [Authorize(Roles = "Admin,Cliente")]
    public class ClientesController : Controller
    {
        private Context db = new Context();

        // GET: Clientes
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var clientes = db.Clientes.Include(c => c.Cidade).Include(c => c.Estado).Include(c => c.Status);
            return View(clientes.ToList());
        }


        // GET: Clientes Conta
        [Authorize(Roles = "Cliente")]
        public ActionResult Conta()
        {
            var user = db.Clientes.Where(v => v.UserName == User.Identity.Name).FirstOrDefault();
            var clienteid = user.ClientesId;
            var clientes = db.Clientes.Include(v => v.Cidade).Include(v => v.Estado).Include(v => v.Status);
            clientes = clientes.Where(p => p.ClientesId == clienteid);
            return View(clientes.ToList());
        }



        // GET: Clientes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clientes clientes = db.Clientes.Find(id);
            if (clientes == null)
            {
                return HttpNotFound();
            }
            return View(clientes);
        }

        // GET: Clientes/Create
        [AllowAnonymous]
        public ActionResult Create()
        {
            ViewBag.CidadesId = new SelectList(db.Cidades, "CidadesId", "Nome");
            ViewBag.EstadosId = new SelectList(db.Estados, "EstadosId", "Nome");
            ViewBag.StatusId = new SelectList(db.Status, "StatusId", "Nome");
            return View();
        }

        // POST: Clientes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Create([Bind(Include = "ClientesId,Nome,UserName,Password,CPF,Telefone,Endereco,CidadesId,EstadosId,StatusId")] Clientes clientes)
        {
            if (ModelState.IsValid)
            {
                clientes.StatusId = 1;
                db.Clientes.Add(clientes);
                try
                {
                    db.SaveChanges();
                    UsersHelper.CreateUserASP(clientes.UserName, "Cliente", clientes.Password);
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
                                                               ex.InnerException.InnerException.Message.Contains("CPF_Index"))

                    {
                        ModelState.AddModelError(string.Empty, "Este CPF já esta sendo usado.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, ex.Message);
                    }

                    ViewBag.CidadesId = new SelectList(db.Cidades, "CidadesId", "Nome", clientes.CidadesId);
                    ViewBag.EstadosId = new SelectList(db.Estados, "EstadosId", "Nome", clientes.EstadosId);
                    ViewBag.StatusId = new SelectList(db.Status, "StatusId", "Nome", clientes.StatusId);
                    return View("Index", "Home");
                }
            }

            ViewBag.CidadesId = new SelectList(db.Cidades, "CidadesId", "Nome", clientes.CidadesId);
            ViewBag.EstadosId = new SelectList(db.Estados, "EstadosId", "Nome", clientes.EstadosId);
            ViewBag.StatusId = new SelectList(db.Status, "StatusId", "Nome", clientes.StatusId);
            return View("Index", "Home");
        }

        // GET: Clientes/Edit/5
        [Authorize(Roles = "Admin,Cliente")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clientes clientes = db.Clientes.Find(id);
            if (clientes == null)
            {
                return HttpNotFound();
            }
            ViewBag.CidadesId = new SelectList(db.Cidades, "CidadesId", "Nome", clientes.CidadesId);
            ViewBag.EstadosId = new SelectList(db.Estados, "EstadosId", "Nome", clientes.EstadosId);
            ViewBag.StatusId = new SelectList(db.Status, "StatusId", "Nome", clientes.StatusId);
            return View(clientes);
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Cliente")]
        public ActionResult Edit([Bind(Include = "ClientesId,Nome,UserName,Password,CPF,Telefone,Endereco,CidadesId,EstadosId,StatusId")] Clientes clientes)
        {
            if (ModelState.IsValid)
            {

                var db2 = new Context();
                var currentUser = db2.Clientes.Find(clientes.ClientesId);
                if (currentUser.UserName != clientes.UserName)
                {
                    UsersHelper.UpdateUserName(currentUser.UserName, clientes.UserName);
                }
                db2.Dispose();
                db.Entry(clientes).State = EntityState.Modified;
                
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
                                                               ex.InnerException.InnerException.Message.Contains("CPF_Index"))

                    {
                        ModelState.AddModelError(string.Empty, "Este CPF já esta sendo usado.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, ex.Message);
                    }

                    ViewBag.CidadesId = new SelectList(db.Cidades, "CidadesId", "Nome", clientes.CidadesId);
                    ViewBag.EstadosId = new SelectList(db.Estados, "EstadosId", "Nome", clientes.EstadosId);
                    ViewBag.StatusId = new SelectList(db.Status, "StatusId", "Nome", clientes.StatusId);
                    return View(clientes);
                }
            }
            return View(clientes);
        }


        // GET: Clientes/Edit/5
        [Authorize(Roles = "Admin,Cliente")]
        public ActionResult EditCliente(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clientes clientes = db.Clientes.Find(id);
            if (clientes == null)
            {
                return HttpNotFound();
            }
            ViewBag.CidadesId = new SelectList(db.Cidades, "CidadesId", "Nome", clientes.CidadesId);
            ViewBag.EstadosId = new SelectList(db.Estados, "EstadosId", "Nome", clientes.EstadosId);
            ViewBag.StatusId = new SelectList(db.Status, "StatusId", "Nome", clientes.StatusId);
            return View(clientes);
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Cliente")]
        public ActionResult EditCliente([Bind(Include = "ClientesId,Nome,UserName,Password,CPF,Telefone,Endereco,CidadesId,EstadosId,StatusId")] Clientes clientes)
        {
            if (ModelState.IsValid)
            {

                var db2 = new Context();
                var currentUser = db2.Clientes.Find(clientes.ClientesId);
                if (currentUser.UserName != clientes.UserName)
                {
                    UsersHelper.UpdateUserName(currentUser.UserName, clientes.UserName);
                }
                db2.Dispose();
                db.Entry(clientes).State = EntityState.Modified;

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
                                                               ex.InnerException.InnerException.Message.Contains("CPF_Index"))

                    {
                        ModelState.AddModelError(string.Empty, "Este CPF já esta sendo usado.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, ex.Message);
                    }

                    ViewBag.CidadesId = new SelectList(db.Cidades, "CidadesId", "Nome", clientes.CidadesId);
                    ViewBag.EstadosId = new SelectList(db.Estados, "EstadosId", "Nome", clientes.EstadosId);
                    ViewBag.StatusId = new SelectList(db.Status, "StatusId", "Nome", clientes.StatusId);
                    return View(clientes);
                }
            }
            return View(clientes);
        }





        // GET: Clientes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clientes clientes = db.Clientes.Find(id);
            if (clientes == null)
            {
                return HttpNotFound();
            }
            return View(clientes);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Clientes clientes = db.Clientes.Find(id);
            db.Clientes.Remove(clientes);
            db.SaveChanges();
            UsersHelper.DeleteUser(clientes.UserName);
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
