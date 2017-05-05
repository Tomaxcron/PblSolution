using Pbl.Models;
using Pbl.Models.DbClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pbl.Controllers
{
    public class ControleAlunosController : Controller
    {
        // GET: ControleAlunos
        public ActionResult Index()
        {
            ViewBag.Message = TempData["Message"];
            return View(new MAluno().BringAll());
        }

        public ActionResult Create()
        {
            ViewBag.Message = TempData["Message"];
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string nomeAluno, string cpfAluno, string matriculaAluno)
        {
            Aluno aluno = new Aluno();
            aluno.nomeAluno = nomeAluno;
            aluno.cpfAluno = cpfAluno;
            aluno.matriculaAluno = matriculaAluno;
            MAluno mAluno = new MAluno();
            TempData["Message"] = mAluno.Add(aluno) ? "Aluno cadastrado" : "Ação não foi realizada";
            return RedirectToAction("Create");
        }
        public ActionResult Update(int id)
        {
            MAluno mAluno = new MAluno();
            return View(mAluno.BringOne(c => c.idAluno == id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(int idAluno, string nomeAluno, string cpfAluno, string matriculaAluno)
        {
            Aluno aluno = new Aluno();
            aluno.idAluno = idAluno;
            aluno.nomeAluno = nomeAluno;
            aluno.cpfAluno = cpfAluno;
            aluno.matriculaAluno = matriculaAluno;
            if (new MAluno().Update(aluno))
            {
                TempData["Message"] = "Aluno atualizado com sucesso";
                return RedirectToAction("Index");
            }
            TempData["Message"] = "Ação não foi realizada";
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            MAluno mAluno = new MAluno();
            Aluno aluno = mAluno.BringOne(c => c.idAluno == id);
            TempData["Message"] = mAluno.Delete(aluno) ? "Aluno deletado com sucesso" : "Ação não foi realizada";
            return RedirectToAction("Index", "ControleAlunos");
        }
    }
}