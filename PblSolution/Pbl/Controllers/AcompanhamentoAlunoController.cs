using Pbl.Models;
using Pbl.Models.DbClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pbl.Controllers
{
    public class AcompanhamentoAlunoController : Controller
    {

        public ActionResult Index()
        {
            Usuario user = (Usuario)Session["usuario"];
            if (user == null)
            {
                return RedirectToAction("Login", "Login");
            }
            Aluno aluno = user.Aluno.First();
            List<InscricaoTurma> inscricoesEmTurmas = aluno.InscricaoTurma.ToList();
            return View(inscricoesEmTurmas);
        }

        public ActionResult AcompanharMed(int idInscricaoTurma, int idMed)
        {
            List<ControleNotas> listControleNotas = new MControleNotas().Bring(c => (c.idInscricaoTurma == idInscricaoTurma));
            int numDisciplinasPraticas = new MMed().BringOne(c => c.idMed == idMed).Disciplina.Where(c => c.idTipoDisciplina == 2).Count();
            int numDisciplinasMorfofuncionais = new MMed().BringOne(c => c.idMed == idMed).Disciplina.Where(c => c.idTipoDisciplina == 1).Count();
            
            foreach (ControleNotas modulo in listControleNotas)
            {
                var disciplinasPraticas = modulo.ControleNotasXAula.Where(c => c.Aula.Disciplina.idTipoDisciplina == 2).Sum(c => c.nota);
                var disciplinasMorfofuncionais = modulo.ControleNotasXAula.Where(c => c.Aula.Disciplina.idTipoDisciplina == 1).Sum(c => c.nota);
                var ProvaoTutoria = modulo.ControleNotasXProva.Where(c => c.Prova.idTipoProva == 1).Select(c => (c.Prova.valorQuestao * c.numAcertos));
                var ProvaoPratica = modulo.ControleNotasXProva.Where(c => c.Prova.idTipoProva == 2).Select(c => (c.Prova.valorQuestao * c.numAcertos));
                
            }
            return View(listControleNotas);
        }
    }
}
