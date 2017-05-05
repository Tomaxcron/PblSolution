using Pbl.Models;
using Pbl.Models.DbClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pbl.Controllers
{
    public class ControleProblemasController : Controller
    {
        // GET: ControleProblemas
        public ActionResult Index()
        {
            ViewBag.Message = TempData["Message"];
            return View(new MProblema().BringAll());
        }

        public ActionResult Create()
        {
            ViewBag.Message = TempData["Message"];
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Problema problema)
        {
            MProblema mProblema = new MProblema();
            TempData["Message"] = mProblema.Add(problema) ? "Problema Cadastrado" : "Ação não realizada";
            return RedirectToAction("Create");
        }

        public ActionResult Update(int id)
        {
            return View(new MProblema().BringOne(c => c.idProblema == id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(Problema problema)
        {
            TempData["Message"] = new MProblema().Update(problema) ? "Problema atualizado com sucesso" : "Ação não foi realizada";
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            MProblema mProblema = new MProblema();
            Problema problema = mProblema.BringOne(c => c.idProblema == id);
            TempData["Message"] = mProblema.Delete(problema) ? "Problema deletado com sucesso" : "Ação não foi realizada";
            return RedirectToAction("Index");
        }
    }
}