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
        public ActionResult Index()
        {
            return View(new MProfessor().BringAll());
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Professor professor)
        {
            MProfessor mProfessor = new MProfessor();
            ViewBag.Message = mProfessor.Add(professor) ? "Professor cadastrado com sucesso" : "Ação não foi realizada";
            return View();
        }
        
        public ActionResult Update(int id)
        {
            MProfessor mProfessor = new MProfessor();
            return View(mProfessor.BringOne(c => c.idProfessor == id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(Professor professor)
        {
            new MProfessor().Update(professor);
            return View("Index", new MProfessor().BringAll());
        } 

        public ActionResult Delete(int id)
        {
            MProfessor mProfessor = new MProfessor();
            Professor professor = mProfessor.BringOne(c => c.idProfessor == id);
            ViewBag.Message = mProfessor.Delete(professor) ? "Professor deletado com sucesso" : "Ação não foi realizada";
            return View("Index",mProfessor.BringAll());
        }
    }
}