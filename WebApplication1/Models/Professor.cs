using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebProfessor.Models
{
    public class Professor
    {
        [Key]
        public int Codigo { get; set; }

        [Display(Name = "Nome Professor")]
        [Required]
        [MaxLength(50)]
        public string Nome { get; set; }

        public List<Aluno> Alunos { get; set;  }

    }
}