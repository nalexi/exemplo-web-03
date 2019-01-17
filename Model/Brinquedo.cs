using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public class Brinquedo
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Marca { get; set; }
        public decimal Preco { get; set; }
        public short Estoque { get; set; }
        public bool RegistroAtivo { get; set; }
    }
}