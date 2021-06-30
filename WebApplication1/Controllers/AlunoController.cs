using Service.Impl;
using System.Collections.Generic;
using System.Web.Mvc;
using WebProfessor.Models;
using System.Linq;

namespace WebApplication1.Controllers
{
    public class AlunoController : Controller
    {
        // GET: Aluno
        public ActionResult Index()
        {
            return View();
        }

        // GET: Aluno/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Aluno/Create
        public ActionResult Create() 
        {
            ViewBag.ProfessorList = GetProfessorsForDropDown();
            return View();
        }

        public List<SelectListItem> GetProfessorsForDropDown()
        {
            ProfessorService professorService = new ProfessorService();
            var professorList = professorService.Listar();

            var listaResult = from item in professorList
                              select new SelectListItem() { Text = item.Nome, Value = item.Codigo.ToString() };

            return listaResult.ToList();
        }

        // POST: Aluno/Create
        [HttpPost]
        public ActionResult Create(Aluno model)
        {
            try
            {
                ModelState.Remove("Codigo");

                if (ModelState.IsValid)
                {
                    AlunoService srv = new AlunoService();
                    srv.Salvar(model);

                    return View("List", srv.Listar());
                }
                else
                    return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Aluno/Edit/5
        public ActionResult Edit(int id)
        {
            var srv = new AlunoService();
            return View("Create", srv.Obter(id));
        }

        // POST: Aluno/Edit/5
        [HttpPost]
        public ActionResult Edit(Aluno model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    AlunoService srv = new AlunoService();
                    srv.Salvar(model);

                    return View("List", srv.Listar());
                }
                else
                    return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Aluno/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                AlunoService srv = new AlunoService();
                srv.Deletar(id);
                return View("List", srv.Listar());
            }
            catch
            {
                return View();
            }
        }

        public ActionResult List()
        {
            AlunoService srv = new AlunoService();
            return View(srv.Listar());
        }

        public ActionResult ListAlunosProfessor(int id)
        {
            AlunoService srv = new AlunoService();
            return View(srv.ListarAlunosProfessor(id));
        }
    }
}
