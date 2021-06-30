using Service.Impl;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebProfessor.Models;

namespace WebProfessor.Controllers
{
    public class ProfessorController : Controller
    {
        // GET: Professor
        public ActionResult Index()
        {
            return View();
        }

        // GET: Professor/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Professor/Create
        [HttpPost]
        public ActionResult Create(Professor model)
        {
            try
            {
                ModelState.Remove("Codigo");

                if (ModelState.IsValid)
                {
                    ProfessorService srv = new ProfessorService();
                    srv.Salvar(model);

                    return View("List", srv.Listar());
                }
                else
                    //return View(model);
                    return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult List()
        {
            ProfessorService srv = new ProfessorService();
            return View(srv.Listar());
        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase postedFile, int id)
        {
            List<Aluno> alunos = new List<Aluno>();
            string filePath = string.Empty;

            if (postedFile != null)
            {
                string path = Server.MapPath("~/Uploads/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                filePath = path + Path.GetFileName(postedFile.FileName);
                string extension = Path.GetExtension(postedFile.FileName);
                postedFile.SaveAs(filePath);

                string csvData = System.IO.File.ReadAllText(filePath);

                foreach (String linha in csvData.Split('\n'))
                {
                    var arrayLinha = linha.Split(new string[] { "||" }, StringSplitOptions.None);
                    
                    alunos.Add(new Aluno
                    {
                        Nome = arrayLinha[0],
                        Mensalidade = Decimal.Parse(arrayLinha[1]),
                        DataVencimento = DateTime.Parse(arrayLinha[2]),
                        CodigoProfessor = id
                    });
                }

                if (!ExisteBloqueio())
                {
                    ImportarAlunos(alunos);
                    ViewBag.Messagem = "Processo de importar arquivo realizado com sucesso!";
                }
                else 
                {
                    ViewBag.Messagem = "Processo de importar arquivo bloqueado!";
                }
                
                return View();
            }
            else
            {
                ViewBag.Messagem = "Favor escolher um arquivo para importar!";
                return View();
            }
        }

        public void ImportarAlunos(List<Aluno> alunos)
        {
                AlunoService alunoService = new AlunoService();
                alunoService.Importar(alunos);
                BloquearImportacao(DateTime.Now);
        }

        public bool ExisteBloqueio()
        {
            Configuration objConfig = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");
            AppSettingsSection objAppsettings = (AppSettingsSection)objConfig.GetSection("appSettings");

            if (objAppsettings != null)
            {
                var UltimaDataBloqueio = Convert.ToDateTime(objAppsettings.Settings["DataBloqueio"].Value);
                var DiasBloqueio = Convert.ToInt32(objAppsettings.Settings["DiasBloqueio"].Value);

                if (UltimaDataBloqueio.AddDays(DiasBloqueio) > DateTime.Now)
                    return true;
            }

            return false;
        }

        public void BloquearImportacao(DateTime dataBloqueio)
        {
            Configuration objConfig = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");
            AppSettingsSection objAppsettings = (AppSettingsSection)objConfig.GetSection("appSettings");

            if (objAppsettings != null)
            {
                objAppsettings.Settings["DataBloqueio"].Value = dataBloqueio.ToString("dd/MM/yyyy");
                objConfig.Save();
            }
        }
    }
}
