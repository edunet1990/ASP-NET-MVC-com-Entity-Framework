using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Mvc_Contatos.Models;
using PagedList;
using PagedList.Mvc;

namespace Mvc_Contatos.Controllers
{
    public class ContatoController : Controller
    {
        private CadastroEntities db = new CadastroEntities();

        // GET: Contato
        public ActionResult Index(string procurarPor, string criterio, int? pagina, string ordenarPor)
        {
            ViewBag.OrdenarPorNome = string.IsNullOrEmpty(ordenarPor) ? "Nome desc" : "";
            ViewBag.OrdenarPorSexo = ordenarPor == "Sexo" ? "Sexo desc" : "Sexo";

            var contatos = db.Contatos.AsQueryable();

            if(procurarPor == "Sexo")
            {
                contatos = contatos.Where(x => x.Sexo == criterio || criterio == null);
            }
            else
            {
                contatos = contatos.Where(x => x.Nome.StartsWith(criterio) || criterio == null); 
            }

            switch (ordenarPor)
            {
                case "Nome desc":
                    contatos = contatos.OrderByDescending(x => x.Nome);
                    break;

                case "Sexo desc":
                    contatos = contatos.OrderByDescending(x => x.Sexo);
                    break;

                case "Sexo":
                    contatos = contatos.OrderByDescending(x => x.Sexo);
                    break;

                default:
                    contatos = contatos.OrderBy(x => x.Nome);
                    break;  
            }
            return View(contatos.ToPagedList ( pagina ?? 1,5));
        }

        // GET: Contato/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contato contato = db.Contatos.Find(id);
            if (contato == null)
            {
                return HttpNotFound();
            }
            return View(contato);
        }

        // GET: Contato/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Contato/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nome,Email,Sexo")] Contato contato)
        {
            if (ModelState.IsValid)
            {
                db.Contatos.Add(contato);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(contato);
        }

        // GET: Contato/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contato contato = db.Contatos.Find(id);
            if (contato == null)
            {
                return HttpNotFound();
            }
            return View(contato);
        }

        // POST: Contato/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nome,Email,Sexo")] Contato contato)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contato).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(contato);
        }

        // GET: Contato/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contato contato = db.Contatos.Find(id);
            if (contato == null)
            {
                return HttpNotFound();
            }
            return View(contato);
        }

        // POST: Contato/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Contato contato = db.Contatos.Find(id);
            db.Contatos.Remove(contato);
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
