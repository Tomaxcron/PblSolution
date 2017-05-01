using Pbl.Models;
using Pbl.Models.DbClasses;
using Pbl.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pbl.Controllers
{
    public class ControleDisciplinasController : Controller
    {
        // GET: ControleDisciplinas
        public ActionResult Index()
        { 
            return View(new MDisciplina().BringAll());
        }

        public ActionResult Create()
        {
            //MDisciplina mDisciplina = new MDisciplina();
            MTipoDisciplina mTipoDisciplina = new MTipoDisciplina();
            DisciplinaViewModel viewModel = new DisciplinaViewModel();
            viewModel.listaTipoDisciplina = mTipoDisciplina.BringAll();
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Disciplina Disciplina)
        {
            MDisciplina mDisciplina = new MDisciplina();
            ViewBag.Message = mDisciplina.Add(Disciplina) ? "Disciplina cadastrado" : "Ação não foi realizada"; MTipoDisciplina mTipoDisciplina = new MTipoDisciplina();
            DisciplinaViewModel viewModel = new DisciplinaViewModel();
            viewModel.listaTipoDisciplina = mTipoDisciplina.BringAll();
            return View(viewModel);
        }
        public ActionResult Update(int id)
        {
            MDisciplina mDisciplina = new MDisciplina();
            DisciplinaViewModel viewModel = new DisciplinaViewModel();
            MTipoDisciplina mTipoDisciplina = new MTipoDisciplina();
            viewModel.disciplina = mDisciplina.BringOne(c => c.idDisciplina == id);
            viewModel.listaTipoDisciplina = mTipoDisciplina.BringAll();
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(Disciplina Disciplina)
        {
            new MDisciplina().Update(Disciplina);
            return View("Index", new MDisciplina().BringAll());
        }

        public ActionResult Delete(int id)
        {
            MDisciplina mDisciplina = new MDisciplina();
            Disciplina Disciplina = mDisciplina.BringOne(c => c.idDisciplina == id);
            ViewBag.Message = mDisciplina.Delete(Disciplina) ? "Disciplina deletado com sucesso" : "Ação não foi realizada";
            return View("Index", mDisciplina.BringAll());
        }
    }
}