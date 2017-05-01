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
            return View(new MProblema().BringAll());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Problema problema)
        {
            MProblema mProblema = new MProblema();
            ViewBag.Message = mProblema.Add(problema) ? "Problema Inserido" : "Algo errado ocorreu";
            return View();
        }

        public ActionResult Update(int id)
        {
            return View(new MProblema().BringOne(c => c.idProblema == id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(Problema problema)
        {
            new MProblema().Update(problema);
            return View("Index", new MProblema().BringAll());
        }

        public ActionResult Delete(int id)
        {
            MProblema mProblema = new MProblema();
            Problema problema = mProblema.BringOne(c => c.idProblema == id);
            ViewBag.Message = mProblema.Delete(problema) ? "Problema deletado com sucesso" : "Ação não foi realizada";
            return View("Index", mProblema.BringAll());
        }
    }
}