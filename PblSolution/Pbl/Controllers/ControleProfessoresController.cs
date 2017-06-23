using Pbl.Models;
using Pbl.Models.DbClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pbl.Controllers
{
    public class ControleProfessoresController : Controller
    {
        // GET: ControleProfessores
        [Authorize(Roles = "Diretor")]
        public ActionResult Index()
        {
            ViewBag.Message = TempData["Message"];
            return View(new MProfessor().BringAll());
        }

        [Authorize(Roles = "Diretor")]
        public ActionResult Create()
        {
            ViewBag.Message = TempData["Message"];
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Diretor")]
        public ActionResult Create(Professor professor)
        {
            MProfessor mProfessor = new MProfessor();
            TempData["Message"] = mProfessor.Add(professor) ? "Professor cadastrado com sucesso" : "Ação não foi realizada";
            return RedirectToAction("Create");
        }

        [Authorize(Roles = "Diretor")]
        public ActionResult Update(int id)
        {
            MProfessor mProfessor = new MProfessor();
            return View(mProfessor.BringOne(c => c.idProfessor == id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Diretor")]
        public ActionResult Update(Professor professor)
        {
            TempData["Message"] = new MProfessor().Update(professor) ? "Professor atualizado com sucesso" : "Ação não foi realizada";
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Diretor")]
        public ActionResult Delete(int id)
        {
            MProfessor mProfessor = new MProfessor();
            Professor professor = mProfessor.BringOne(c => c.idProfessor == id);
            TempData["Message"] = mProfessor.Delete(professor) ? "Professor deletado com sucesso" : "Ação não foi realizada";
            return RedirectToAction("Index");
        }
    }
}