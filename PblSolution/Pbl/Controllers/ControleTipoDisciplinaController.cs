using Pbl.Models;
using Pbl.Models.DbClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pbl.Controllers
{
    public class ControleTipoDisciplinaController : Controller
    {
        // GET: ControleTipoDisciplina
        public ActionResult Index()
        {
            return View(new MTipoDisciplina().BringAll());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TipoDisciplina TipoDisciplina)
        {
            MTipoDisciplina mTipoDisciplina = new MTipoDisciplina();
            ViewBag.Message = mTipoDisciplina.Add(TipoDisciplina) ? "TipoDisciplina cadastrado" : "Ação não foi realizada";
            return View();
        }
        public ActionResult Update(int id)
        {
            MTipoDisciplina mTipoDisciplina = new MTipoDisciplina();
            return View(mTipoDisciplina.BringOne(c => c.idTipoDisciplina == id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(TipoDisciplina TipoDisciplina)
        {
            new MTipoDisciplina().Update(TipoDisciplina);
            return View("Index", new MTipoDisciplina().BringAll());
        }

        public ActionResult Delete(int id)
        {
            MTipoDisciplina mTipoDisciplina = new MTipoDisciplina();
            TipoDisciplina TipoDisciplina = mTipoDisciplina.BringOne(c => c.idTipoDisciplina == id);
            ViewBag.Message = mTipoDisciplina.Delete(TipoDisciplina) ? "TipoDisciplina deletado com sucesso" : "Ação não foi realizada";
            return View("Index", mTipoDisciplina.BringAll());
        }
    }
}