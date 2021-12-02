using ProjetoFinal.DAO;
using ProjetoFinal.Models;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ProjetoFinal.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Login(string username, string senha)
        {
            var db = new Context();

            var usuario = db.Usuarios.Where(x => x.Username == username && x.Senha == senha).FirstOrDefault();

            if (usuario != null)
            {
                var ticket = new FormsAuthenticationTicket(1, usuario.Username, DateTime.Now, DateTime.Now.AddMinutes(10), false, usuario.Id.ToString());
                string encTicket = FormsAuthentication.Encrypt(ticket);
                HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
                cookie.Expires = ticket.Expiration;
                Response.Cookies.Add(cookie);

                var log = new LogAcesso(usuario.Id);
                db.LogAcessos.Add(log);
                db.SaveChanges();

                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Index");
        }

        public ActionResult Logout()
        {
            Response.Cookies.Clear();
            return RedirectToAction("Index");
        }
    }
}
