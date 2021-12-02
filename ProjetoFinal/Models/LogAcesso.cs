using System;

namespace ProjetoFinal.Models
{
    public class LogAcesso
    {
        public LogAcesso(int usuarioId)
        {
            DataAcesso = DateTime.Now;
            UsuarioId = usuarioId;
        }

        public int Id { get; set; }
        public DateTime DataAcesso { get; set; }
        public int UsuarioId { get; set; }
        
        public Usuario Usuario { get; set; }
    }
}