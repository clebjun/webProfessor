using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using WebProfessor.Models;

namespace Service.Impl
{
    public class AlunoService
    {
        public void Salvar(Aluno aluno)
        {
            using(var db = new MyContext())
            {
                if(aluno.Codigo > 0)
                {
                    db.Aluno.Attach(aluno);
                    db.Entry(aluno).State = System.Data.Entity.EntityState.Modified;
                }
                else
                    db.Aluno.Add(aluno);

                db.SaveChanges();
            }
        }

        public Aluno Obter(int codigo)
        {
            using(var db = new MyContext())
            {
                return db.Aluno.Find(codigo);
            }
        }

        public List<AlunoDTO> ListarAlunosProfessor(int codigoProfessor)
        {
            using (var db = new MyContext())
            {
                var alunosList = (from al in db.Aluno
                                  join pr in db.Professor on al.CodigoProfessor equals pr.Codigo
                                  where al.CodigoProfessor == codigoProfessor
                                  select new AlunoDTO
                                  {
                                      Codigo = al.Codigo,
                                      Nome = al.Nome,
                                      Mensalidade = al.Mensalidade,
                                      DataVencimento = al.DataVencimento,
                                      CodigoProfessor = al.CodigoProfessor,
                                      NomeProfessor = pr.Nome
                                  }
                                 ).ToList();

                return alunosList;
            }
        }

        public List<AlunoDTO> Listar()
        {
            using (var db = new MyContext())
            {
                var alunosList = (from al in db.Aluno
                                  join pr in db.Professor on al.CodigoProfessor equals pr.Codigo
                                  select new AlunoDTO
                                     {
                                         Codigo = al.Codigo,
                                         Nome = al.Nome,
                                         Mensalidade = al.Mensalidade,
                                         DataVencimento = al.DataVencimento,
                                         CodigoProfessor = al.CodigoProfessor,
                                         NomeProfessor = pr.Nome
                                     }
                                 ).ToList();

                return alunosList;
            }
        }

        public void Deletar(int codigo)
        {
            try
            {
                using (var db = new MyContext())
                {
                    var p = new Aluno() { Codigo = codigo };
                    db.Aluno.Attach(p);
                    db.Aluno.Remove(p);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
                { 
                   throw ex; 
                }
        }

        public void Importar(List<Aluno> aluno)
        {
            
            using (var db = new MyContext())
            {
                if (aluno.Count > 0)
                    db.Aluno.AddRange(aluno);

                db.SaveChanges();
            }
        }
    }

    public class AlunoDTO
    {
        public int Codigo { get; set; }
        public string Nome { get; set; }

        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:c}")]
        public decimal Mensalidade { get; set; }
        
        [Display(Name = "Data Vencimento")]
        public DateTime DataVencimento { get; set; }
        public int CodigoProfessor { get; set; }

        [Display(Name = "Nome Professor")]
        public string NomeProfessor { get; set; }
    }
}
