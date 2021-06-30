using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebProfessor.Models
{
    public class Aluno
    {
        [Key]
        public int Codigo { get; set; }

        [Required]
        [MaxLength(50)]
        public string Nome { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:c}")]
        public decimal Mensalidade{ get; set; }

        [Display(Name = "Data Vencimento")]
        [Required]
        [DataType(DataType.Date)]
        public DateTime DataVencimento { get; set; }

        public int CodigoProfessor { get; set; }

        [ForeignKey("CodigoProfessor")]
        public Professor Professor { get; set;  }

    }
}