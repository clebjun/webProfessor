using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebProfessor.Models
{
    public class MyContext : DbContext, IDisposable
    {
        public MyContext() 
            : base(ConfigurationManager.ConnectionStrings["conexao"].ConnectionString)
        {

        }
        public DbSet<Professor> Professor { get; set; }
        public DbSet<Aluno> Aluno { get; set; }

    }
}