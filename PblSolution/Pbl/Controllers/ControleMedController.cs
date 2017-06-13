using Pbl.Models;
using Pbl.Models.DbClasses;
using Pbl.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.UI;

namespace Pbl.Controllers
{
    public class ControleMedController : Controller
    {
        // GET: ControleMed
        public ActionResult Index()
        {
            ViewBag.Message = TempData["Message"];
            return View(new MMed().BringAll());
        }

        public ActionResult GerenciarMed(int id)
        {
            ViewBag.Message = TempData["Message"];
            GerenciarMedViewModel dados = new GerenciarMedViewModel();
            dados.problemasCadastrados = new MProblemaXMed().RetornaProblemasCadastrados(id);
            dados.turmasCadastradas = new MTurma().Bring(c => c.idMed == id);
            dados.med = new MMed().BringOne(c => c.idMed == id);
            return View(dados);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AdicionarTurma(int id)
        {
            ViewBag.Message = TempData["Message"];
            Turma nova = new Turma();
            nova.idMed = id;
            return View(nova);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AdicionarNovaTurma(Turma nova)
        {
            TempData["Message"] = new MTurma().Add(nova) ? "Nova Turma Cadastrada" : "Algo Errado Ocorreu";
            GerenciarMedViewModel dados = new GerenciarMedViewModel();
            dados.problemasCadastrados = new MProblemaXMed().RetornaProblemasCadastrados((int)nova.idMed);
            dados.turmasCadastradas = new MTurma().Bring(c => c.idMed == nova.idMed);
            dados.med = new MMed().BringOne(c => c.idMed == nova.idMed);
            return RedirectToAction("GerenciarMed", new { id = nova.idMed });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult VincularProblemas(int idMed)
        {
            VincularProblemaViewModel dados = new VincularProblemaViewModel();
            dados.MedAtual = new MMed().BringOne(c => c.idMed == idMed);
            dados.ListProblemasCadastrados = new MProblemaXMed().Bring(c => c.idMed == idMed);
            dados.ListProblemaDisponiveis = new MProblemaXMed().RetornaProblemasDisponiveis(dados.MedAtual.idMed);
            return View(dados);
        }

        [HttpPost]
        public void DeletarVinculoProblema(int idProblema, int idMed)
        {
            MProblemaXMed mProblemaXMed = new MProblemaXMed();
            ProblemaXMed cadastroProblemaEncontrado = mProblemaXMed.BringOne(c => (c.idMed == idMed) && (c.idProblema == idProblema));
            TempData["Message"] = mProblemaXMed.Delete(cadastroProblemaEncontrado) ? "Vinculo deletado" : "Algo Errado Ocorreu";
            GerenciarMedViewModel dados = new GerenciarMedViewModel();
            dados.problemasCadastrados = new MProblemaXMed().RetornaProblemasCadastrados(idMed);
            dados.turmasCadastradas = new MTurma().Bring(c => c.idMed == idMed);
            dados.med = new MMed().BringOne(c => c.idMed == idMed);
            //Response.Redirect(Url.Action("GerenciarMed", "ControleMed", idMed));
             Response.Redirect(Request.RawUrl);
            //Page.Response.Redirect(Page.Request.Url.ToString(), true);
            //return RedirectToAction("GerenciarMed", new { id = idMed });
        }

        [HttpPost]
        public ActionResult AdicionarProblema(int idMed, int idProblema)
        {
            MProblemaXMed mProblemaXMed = new MProblemaXMed();
            ProblemaXMed novo = new ProblemaXMed();
            novo.idMed = idMed;
            novo.idProblema = idProblema;
            ViewBag.Message = mProblemaXMed.Add(novo) ? "Problema Vinculado" : "Algo errado ocorreu";
            VincularProblemaViewModel dados = new VincularProblemaViewModel();
            dados.MedAtual = new MMed().BringOne(c => c.idMed == novo.idMed);
            dados.ListProblemasCadastrados = new MProblemaXMed().Bring(c => c.idMed == novo.idMed);
            dados.ListProblemaDisponiveis = new MProblemaXMed().RetornaProblemasDisponiveis(dados.MedAtual.idMed);
            return View("VincularProblemas", dados);
        }

        public ActionResult ControleTurma()
        {
            InscricaoAlunoViewModel viewModel = new InscricaoAlunoViewModel();
            viewModel.alunosCadastrados = new MAluno().BringAll();
            viewModel.alunosDisponiveis = new List<Aluno>();
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ControleTurma(ICollection<int> alunos)
        {
            MAluno mAluno = new MAluno();
            foreach (var aluno in alunos)
            {
                //Console.WriteLine(aluno.nomeAluno);
                Console.WriteLine(mAluno.BringOne(c => c.idAluno == aluno).nomeAluno);
            }
            return View();
        }

        
        public ActionResult AdicionarAlunosTurma(int idTurma)
        {
            AlunosTurmaViewModel viewModel = new AlunosTurmaViewModel();
            MTurma mTurma = new MTurma();
            MInscricaoTurma mInscricaoTurma = new MInscricaoTurma();
            MAluno mAluno = new MAluno();
            List<Aluno> AlunosCadastrados = mInscricaoTurma.Bring(c => c.idTurma == idTurma).Select(c => c.Aluno).ToList();
            viewModel.AlunosCadastrados = AlunosCadastrados;
            List<Aluno> AlunosDisponiveis = mAluno.BringAll();
            AlunosDisponiveis.RemoveAll(c => AlunosCadastrados.Contains(c));
            viewModel.AlunosDisponiveis = AlunosDisponiveis;
            viewModel.turmaAtual = mTurma.BringOne(c => c.idTurma == idTurma);
            //Teste(viewModel);
            return //PartialView("AdicionarAlunosTurma", viewModel); 
               View("AdicionarAlunosTurma",viewModel);
        }

        private ActionResult Teste(AlunosTurmaViewModel viewModel)
        {
            return View("AdicionarAlunosTurma", viewModel);
        }

        [HttpPost]
        public ActionResult AdicionarAlunosTurmaAction(int idAluno, int idTurma)
        {
            return View();
        }

        [HttpPost]
        public ActionResult RemoverAlunosTurma(int idAluno, int idTurma)
        {
            
            return View();
        }

        

    }
}