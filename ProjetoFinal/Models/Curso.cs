using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ProjetoFinal.Models
{
    public class Curso
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal Valor { get; set; }
        public int QuantidadeVagas { get; set; }
    }
}