using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using MarketplaceTCC.Models;

namespace MarketplaceTCC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CidadesController : Controller
    {
        private Context db = new Context();

        // GET: Cidades
        public ActionResult Index()
        {
            var cidades = db.Cidades.Include(c => c.Estado);
            return View(cidades.ToList());
        }

        // GET: Cidades/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cidades cidades = db.Cidades.Find(id);
            if (cidades == null)
            {
                return HttpNotFound();
            }
            return View(cidades);
        }

        // GET: Cidades/Create
        public ActionResult Create()
        {
            ViewBag.EstadosId = new SelectList(db.Estados, "EstadosId", "Nome");
            return View();
        }

        // POST: Cidades/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CidadesId,Nome,EstadosId")] Cidades cidades)
        {
            if (ModelState.IsValid)
            {
                db.Cidades.Add(cidades);
                try
                {
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (System.Exception ex)
                {
                    if (ex.InnerException != null &&
                        ex.InnerException.InnerException != null &&
                        ex.InnerException.InnerException.Message.Contains("_Index"))
                    {
                        ModelState.AddModelError(string.Empty, "Não é possível inserir duas cidades com o mesmo nome!");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, ex.Message);
                    }

                    ViewBag.EstadosId = new SelectList(db.Estados, "EstadosId", "Nome", cidades.EstadosId);
                    return View(cidades);
                }
            }

            return View(cidades);
        }

        // GET: Cidades/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cidades cidades = db.Cidades.Find(id);
            if (cidades == null)
            {
                return HttpNotFound();
            }
            ViewBag.EstadosId = new SelectList(db.Estados, "EstadosId", "Nome", cidades.EstadosId);
            return View(cidades);
        }

        // POST: Cidades/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CidadesId,Nome,EstadosId")] Cidades cidades)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cidades).State = EntityState.Modified;
                try
                {
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (System.Exception ex)
                {
                    if (ex.InnerException != null &&
                        ex.InnerException.InnerException != null &&
                        ex.InnerException.InnerException.Message.Contains("_Index"))
                    {
                        ModelState.AddModelError(string.Empty, "Não é possível inserir duas cidades com o mesmo nome!");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, ex.Message);
                    }

                    ViewBag.EstadosId = new SelectList(db.Estados, "EstadosId", "Nome", cidades.EstadosId);
                    return View(cidades);
                }
            }
            return View(cidades);
        }

        // GET: Cidades/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cidades cidades = db.Cidades.Find(id);
            if (cidades == null)
            {
                return HttpNotFound();
            }
            return View(cidades);
        }

        // POST: Cidades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cidades cidades = db.Cidades.Find(id);
            db.Cidades.Remove(cidades);
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
