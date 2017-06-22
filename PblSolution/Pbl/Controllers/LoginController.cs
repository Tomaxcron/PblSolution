using Pbl.Models;
using Pbl.Models.DbClasses;
using System.Web.Mvc;

namespace Pbl.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string login, string senha)
        {
            MUsuario mUser = new MUsuario();
            Usuario user = mUser.BringOne(c => (c.login == login) && (c.senha == senha));
            if (user == null)
            {
                return View();
            }
            Session["Usuario"] = user;
            return RedirectToAction("Index","Home");
        }
    }
}