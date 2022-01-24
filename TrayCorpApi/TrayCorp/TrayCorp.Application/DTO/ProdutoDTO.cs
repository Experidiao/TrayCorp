using System;
using System.Collections.Generic;
using System.Text;

namespace TrayCorp.Application.DTO
{
    public class ProdutoDTO
    {
       public int IdProduto { get; set; }
        public string Nome { get; set; }
        public decimal Estoque { get; set; }
        public decimal Valor { get; set; }
    }

}

