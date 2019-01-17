using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public class Carro
    {
        public int Id { get; set; }

        public int IdCategoria { get; set; }
        public Categoria Categoria { get; set; }

        public string Modelo { get; set; }
        public decimal Preco { get; set; }
        public DateTime DataCompra { get; set; }
        public bool RegistroAtivo { get; set; }

    }
}