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
            return View(listControleNotas);
        }
    }
}
