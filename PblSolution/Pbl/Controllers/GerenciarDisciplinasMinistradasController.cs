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
    public class GerenciarDisciplinasMinistradasController : Controller
    {
        // GET: GerenciarDisciplinasMinistradas
        public ActionResult Index()
        {
            Usuario user = (Usuario)Session["usuario"];
            if (user == null)
            {
                return RedirectToAction("Login", "Login");
            }
            Professor prof = user.Professor.First();
            List<Aula> aulasMinistradas = new MAula().Bring(c => c.idProfessor == prof.idProfessor);
            return View(aulasMinistradas);
        }

        public ActionResult SelecionarModulo(int idAula)
        {
            Med med = new MAula().BringOne(c => c.idAula == idAula).Turma.Med;
            Aula aula = new MAula().BringOne(c => c.idAula == idAula);
            ViewData["Aula"] = aula;
            List<Modulo> listModulos = med.Semestre.Modulo.ToList();
            return View(listModulos);
        }

        public ActionResult SelecionarAlunos(int idAula, int idModulo)
        {
            Med med = new MAula().BringOne(c => c.idAula == idAula).Turma.Med;
            Modulo modulo = new MModulo().BringOne(c => c.idModulo == idModulo);
            Aula aula = new MAula().BringOne(c => c.idAula == idAula);
            ViewData["Aula"] = aula;
            Turma turma = aula.Turma;
            List<InscricaoTurma> alunosInscritos = new MInscricaoTurma().Bring(c => c.idTurma == turma.idTurma);
            List<SelecionarAlunosViewModel> viewModel = new List<SelecionarAlunosViewModel>();
            MControleNotasXAula teste = new MControleNotasXAula();
            foreach (var inscrito in alunosInscritos)
            {
                SelecionarAlunosViewModel novo = new SelecionarAlunosViewModel();
                novo.inscricao = inscrito;
                novo.nota = inscrito.ControleNotas.Where(c => c.idModulo == idModulo).First().ControleNotasXAula.Where(c => c.idAula == idAula).First().nota;
                viewModel.Add(novo);
            }
            ViewData["Aula"] = aula;
            ViewData["Modulo"] = modulo;
            return View(viewModel);
        }

        public ActionResult AvaliarAluno(int idInscricaoTurma, int idModulo, int idAula)
        {
            ControleNotas controleNotas = new MControleNotas().BringOne(c => (c.idInscricaoTurma == idInscricaoTurma) && (c.idModulo == idModulo));
            ControleNotasXAula controleNotasAula = new ControleNotasXAula();
            controleNotasAula.idAula = idAula;
            //controleNotasAula.nota = nota;
            controleNotasAula.idControleNotas = controleNotas.idControleNotas;
            MControleNotasXAula mControleNotasXAula = new MControleNotasXAula();
            return View(controleNotasAula);
        }

        public ActionResult AvaliarAlunoAction()
        {
            return null;
        }
    }
}
