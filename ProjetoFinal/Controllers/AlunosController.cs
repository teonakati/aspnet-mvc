using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ProjetoFinal.DAO;
using ProjetoFinal.Models;

namespace ProjetoFinal.Controllers
{
    public class AlunosController : Controller
    {
        private Context db = new Context();

        public ActionResult Index()
        {
            var alunos = db.Alunos.Include(a => a.Curso).Include(a => a.Usuario);
            return View(alunos.ToList());
        }


        public ActionResult Details(int id)
        {
            Aluno aluno = db.Alunos.Find(id);
            return View(aluno);
        }

        public ActionResult Create()
        {
            ViewBag.CursoId = new SelectList(db.Cursos, "Id", "Nome");
            ViewBag.Cursos = db.Cursos.ToList();
            return View();
        }

        [HttpPost]
        public ActionResult Create(Aluno aluno)
        {
            var vagas = db.Cursos.Where(x => x.Id == aluno.CursoId).Select(x => x.QuantidadeVagas).First();

            if (db.Alunos.Where(x => x.CursoId == aluno.CursoId).Count() == vagas)
                TempData["limiteVagas"] = "Este curso atingiu o limite de vagas.";

            if (db.Alunos.Any(x => x.Cpf == aluno.Cpf))
                TempData["cpfCadastrado"] = "CPF já cadastrado!";

            if (string.IsNullOrEmpty(aluno.Nome))
                TempData["nome"] = "O campo nome é obrigatório!";
                
            if (string.IsNullOrEmpty(aluno.Telefone))
                TempData["telefone"] = "O campo telefone é obrigatório!";
                    
            if (string.IsNullOrEmpty(aluno.Email))
                TempData["email"] = "O campo email é obrigatório!";
            
            if (string.IsNullOrEmpty(aluno.Cpf))
                TempData["cpf"] = "O campo CPF é obrigatório!";
            
            if (TempData.Count > 0)
            {
                ViewBag.CursoId = new SelectList(db.Cursos, "Id", "Nome");
                return View(aluno);
            }

            aluno.DataInscricao = DateTime.Now;
            var cookie = Request.Cookies[".ASPXAUTH"];
            var cookieValue = FormsAuthentication.Decrypt(cookie.Value);
            aluno.UsuarioId = Convert.ToInt32(cookieValue.UserData);

            db.Alunos.Add(aluno);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            Aluno aluno = db.Alunos.Find(id);
            ViewBag.CursoId = new SelectList(db.Cursos, "Id", "Nome", aluno.CursoId);
            return View(aluno);
        }

        [HttpPost]
        public ActionResult Edit(Aluno aluno)
        {
            var vagas = db.Cursos.AsNoTracking().Where(x => x.Id == aluno.CursoId).Select(x => x.QuantidadeVagas).First();

            var entidade = db.Alunos.AsNoTracking().Where(x => x.Id == aluno.Id).First();

            if (entidade.CursoId != aluno.CursoId && db.Alunos.Where(x => x.CursoId == aluno.CursoId).Count() == vagas)
                TempData["limiteVagas"] = "Este curso atingiu o limite de vagas.";

            if (entidade.Cpf != aluno.Cpf && db.Alunos.Any(x => x.Cpf == aluno.Cpf))
                TempData["cpfCadastrado"] = "CPF já cadastrado!";

            if (string.IsNullOrEmpty(aluno.Nome))
                TempData["nome"] = "O campo nome é obrigatório!";

            if (string.IsNullOrEmpty(aluno.Telefone))
                TempData["telefone"] = "O campo telefone é obrigatório!";

            if (string.IsNullOrEmpty(aluno.Email))
                TempData["email"] = "O campo email é obrigatório!";

            if (string.IsNullOrEmpty(aluno.Cpf))
                TempData["cpf"] = "O campo CPF é obrigatório!";

            if (TempData.Count > 0)
            {
                ViewBag.CursoId = new SelectList(db.Cursos, "Id", "Nome");
                return View(aluno);
            }

            db.Entry(aluno).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            Aluno aluno = db.Alunos.Find(id);
            return View(aluno);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Aluno aluno = db.Alunos.Find(id);
            db.Alunos.Remove(aluno);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
