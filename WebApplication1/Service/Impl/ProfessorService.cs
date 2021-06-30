using System.Collections.Generic;
using System.Linq;
using WebProfessor.Models;

namespace Service.Impl
{
    public class ProfessorService
    {
        public void Salvar(Professor professor)
        {
            using(var db = new MyContext())
            {
                if(professor.Codigo > 0)
                {
                    db.Professor.Attach(professor);
                    db.Entry(professor).State = System.Data.Entity.EntityState.Modified;
                }
                else
                    db.Professor.Add(professor);

                db.SaveChanges();
            }
        }

        public List<Professor> Listar()
        {
            using(var db = new MyContext())
            {
                return db.Professor.ToList();
            }
        }
    }
}
