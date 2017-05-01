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
    public class ControleSemestresController : Controller
    {
        // GET: ControleSemestres
        public ActionResult Index()
        {
            return View(new MSemestre().BringAll());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SemestreViewModel semestre)
        {
            MSemestre mSemestre = new MSemestre();
            ViewBag.Message = mSemestre.Add(semestre) ? "Dados inseridos" : "Dados não inseridos";
            return View();
        }

        public ActionResult Update(int id)
        {
            SemestreViewModel novo = new SemestreViewModel();
            novo.idSemestre = id;
            return View(novo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(SemestreViewModel semestre)
        {
            MSemestre mSemestre = new MSemestre();
            ViewBag.Message = mSemestre.Update(semestre) ? "Dados inseridos" : "Dados não inseridos";
            return View("Index",mSemestre.BringAll());
        }

        public ActionResult Delete(int idSemestre)
        {
            MSemestre mSemestre = new MSemestre();
            ViewBag.Message = mSemestre.Delete(mSemestre.BringOne(c => c.idSemestre == idSemestre)) ? "Dados inseridos" : "Dados não inseridos";
            return View("Index", mSemestre.BringAll());


        }
    }
}