using Pbl.Models;
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
            if ("admin".Equals(login) && "123".Equals(senha))
            {
                Session["user"] = new User() { login = login, name = "Lucas" };
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
    }
}