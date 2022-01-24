using FluentValidation;
using TrayCorp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrayCorp.Services.Api.Validation
{
    public class ProdutoValidation : AbstractValidator<Produto>
    {
        public ProdutoValidation()
        {
            RuleFor(x => x.Nome).NotNull().WithMessage("Descriçao do produto é obrigatório").MaximumLength(60).WithMessage("O Descrição tamanho máximo de 60 letras."); 
            RuleFor(x => x.Estoque).NotNull().NotEmpty().WithMessage("Qtde Estoque é obrigatório");
            RuleFor(x => x.Valor).NotNull().NotEmpty().WithMessage("Valor é obrigatório").GreaterThan(0).WithMessage("Valor do Produto deve ser maior que zero.");
        }
    }
}
