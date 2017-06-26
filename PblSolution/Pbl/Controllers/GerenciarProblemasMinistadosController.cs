using Pbl.Models;
using Pbl.Models.DbClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pbl.Controllers
{
    public class GerenciarProblemasMinistadosController : Controller
    {
        // GET: GerenciarProblemasMinistados
        public ActionResult Index()
        {
            Usuario user = (Usuario)Session["usuario"];
            if (user == null)
            {
                return RedirectToAction("Login", "Login");
            }
            Professor prof = user.Professor.First();
            List<Grupo> problemasMinistrados = new MGrupo().Bring(c => c.idProfessor == prof.idProfessor);
            return View(problemasMinistrados);
        }

       public ActionResult SelecionarProblema()
        {

            return null;
        }
    }
}
