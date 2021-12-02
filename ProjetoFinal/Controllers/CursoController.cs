using ProjetoFinal.DAO;
using ProjetoFinal.Models;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace ProjetoFinal.Controllers
{
    public class CursoController : Controller
    {
        private Context db = new Context();

        public ActionResult Index()
        {
            return View(db.Cursos.ToList());
        }

        public ActionResult Details(int id)
        {
            Curso curso = db.Cursos.Find(id);
            var alunos = db.Alunos
                .AsNoTracking()
                .Where(x => x.CursoId == id)
                .ToList();

            ViewBag.Inscritos = alunos.Count;
            ViewBag.Alunos = alunos;
            ViewBag.Saldo = curso.Valor * alunos.Count;

            return View(curso);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Curso curso)
        {
            if (string.IsNullOrEmpty(curso.Nome))
            {
                TempData["mensagem"] = "O campo nome é obrigatório.";
                return View(curso);
            }

            db.Cursos.Add(curso);
            db.SaveChanges();
            return RedirectToAction("Index");

        }

        public ActionResult Edit(int id)
        {
            Curso curso = db.Cursos.Find(id);
            return View(curso);
        }

        [HttpPost]
        public ActionResult Edit(Curso curso)
        {
            if (string.IsNullOrEmpty(curso.Nome))
            {
                TempData["mensagem"] = "O campo nome é obrigatório.";
                return View(curso);
            }

            db.Entry(curso).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            Curso curso = db.Cursos.Find(id);
            return View(curso);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Curso curso = db.Cursos.Find(id);
            db.Cursos.Remove(curso);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Informacoes(int cursoId)
        {
            var curso = db.Cursos.Find(cursoId);
            var alunos = db.Alunos.Where(x => x.CursoId == cursoId).Count();

            return Json(new { Result = new { curso.Valor, QuantidadeVagas = (curso.QuantidadeVagas - alunos) } }, JsonRequestBehavior.AllowGet);
        }
    }
}
