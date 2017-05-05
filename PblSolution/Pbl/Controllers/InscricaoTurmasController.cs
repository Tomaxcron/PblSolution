using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Pbl.Models;

namespace Pbl.Controllers
{
    public class InscricaoTurmasController : Controller
    {
        private FamervEntities db = new FamervEntities();

        // GET: InscricaoTurmas
        public ActionResult Index()
        {
            var inscricaoTurma = db.InscricaoTurma.Include(i => i.Aluno).Include(i => i.Turma);
            return View(inscricaoTurma.ToList());
        }

        // GET: InscricaoTurmas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InscricaoTurma inscricaoTurma = db.InscricaoTurma.Find(id);
            if (inscricaoTurma == null)
            {
                return HttpNotFound();
            }
            return View(inscricaoTurma);
        }

        // GET: InscricaoTurmas/Create
        public ActionResult Create()
        {
            ViewBag.idAluno = new SelectList(db.Aluno, "idAluno", "nomeAluno");
            ViewBag.idTurma = new SelectList(db.Turma, "idTurma", "descTurma");
            return View();
        }

        // POST: InscricaoTurmas/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idInscricaoTurma,idTurma,idAluno,ativo")] InscricaoTurma inscricaoTurma)
        {
            if (ModelState.IsValid)
            {
                db.InscricaoTurma.Add(inscricaoTurma);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idAluno = new SelectList(db.Aluno, "idAluno", "nomeAluno", inscricaoTurma.idAluno);
            ViewBag.idTurma = new SelectList(db.Turma, "idTurma", "descTurma", inscricaoTurma.idTurma);
            return View(inscricaoTurma);
        }

        // GET: InscricaoTurmas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InscricaoTurma inscricaoTurma = db.InscricaoTurma.Find(id);
            if (inscricaoTurma == null)
            {
                return HttpNotFound();
            }
            ViewBag.idAluno = new SelectList(db.Aluno, "idAluno", "nomeAluno", inscricaoTurma.idAluno);
            ViewBag.idTurma = new SelectList(db.Turma, "idTurma", "descTurma", inscricaoTurma.idTurma);
            return View(inscricaoTurma);
        }

        // POST: InscricaoTurmas/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idInscricaoTurma,idTurma,idAluno,ativo")] InscricaoTurma inscricaoTurma)
        {
            if (ModelState.IsValid)
            {
                db.Entry(inscricaoTurma).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idAluno = new SelectList(db.Aluno, "idAluno", "nomeAluno", inscricaoTurma.idAluno);
            ViewBag.idTurma = new SelectList(db.Turma, "idTurma", "descTurma", inscricaoTurma.idTurma);
            return View(inscricaoTurma);
        }

        // GET: InscricaoTurmas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InscricaoTurma inscricaoTurma = db.InscricaoTurma.Find(id);
            if (inscricaoTurma == null)
            {
                return HttpNotFound();
            }
            return View(inscricaoTurma);
        }

        // POST: InscricaoTurmas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            InscricaoTurma inscricaoTurma = db.InscricaoTurma.Find(id);
            db.InscricaoTurma.Remove(inscricaoTurma);
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
