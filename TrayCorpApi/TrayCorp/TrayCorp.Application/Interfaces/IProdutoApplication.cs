using TrayCorp.Application.DTO;
using TrayCorp.Domain.Interfaces;
using TrayCorp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TrayCorp.Application.Interfaces
{
    public interface IProdutoApplication : IRepository<Produto>
    {
        Task<List<Produto>> ObterProdutoPorNome(string nome);
        Task<List<Produto>> ProcurarProduto(string ProcurarPor, string ordenarPor);
    }
}
