using System;
using System.ComponentModel.DataAnnotations;

namespace TrayCorpWeb.Models
{
    public class Produto
    { 
        public int IdProduto { get; set; }

        [Required(ErrorMessage = "Nome é obrigatório")]
        [StringLength(60, ErrorMessage = "Nome, maxímo 60 letras")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Estoque é obrigatório")]
        public decimal Estoque { get; set; }
        [Required(ErrorMessage = "Valor é obrigatório")]
        [Range(0.0, Double.PositiveInfinity, ErrorMessage = "Valor somente maior que zero")]
        public decimal Valor { get; set; }
    }
}
