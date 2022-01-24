using System;
using System.Collections.Generic;
using System.Text;

namespace TrayCorp.Domain.Models
{
    public class Produto
    {
        public int IdProduto { get; set; }
        public string Nome { get; set;}
        public decimal Estoque { get; set; }
        public decimal Valor { get; set; }
    }
}
