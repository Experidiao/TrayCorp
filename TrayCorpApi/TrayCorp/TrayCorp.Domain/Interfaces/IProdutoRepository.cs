using TrayCorp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TrayCorp.Domain.Interfaces
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        Task<IEnumerable<Produto>> ObterProdutoPorNome(string nome);
        Task<IEnumerable<Produto>> ProcurarProduto(string ProcurarPor, string ordenarPor);
    }
}
