using System;

namespace ProjetoFinal.Models
{
    public class Aluno
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public DateTime DataInscricao { get; set; }
        public int CursoId { get; set; }
        public int UsuarioId { get; set; }

        public Curso Curso { get; set; }
        public Usuario Usuario { get; set; }
    }
}