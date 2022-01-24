using TrayCorp.Application.Interfaces;
using TrayCorp.Application.DTO;
using TrayCorp.Domain.Interfaces;
using TrayCorp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Net;
using System.Threading;

namespace TrayCorp.Application.Services
{
    public class ProdutoApplication : IProdutoApplication
    {
        private readonly IProdutoRepository _produtoRepository;
        public ProdutoApplication(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        public async Task<IEnumerable<Produto>> GetAll()
        {
            return await _produtoRepository.GetAll();
        }

        public async Task<Produto> GetById(int id)
        {
            return await _produtoRepository.GetById(id);
        }

        public async Task<int> Create(Produto produto)
        {
            return await _produtoRepository.Create(produto);
        }

        public async Task<bool> Update(Produto produto)
        {
            return await _produtoRepository.Update(produto);
        }

        public async Task<bool> Delete(int Id)
        {
            return await _produtoRepository.Delete(Id);
        }

        public async Task<List<Produto>> ObterProdutoPorNome(string nome)
        {
          var listaProduto = await _produtoRepository.ObterProdutoPorNome(nome);
            // o objeto de saida para a camada de apresentaçao, nem sempre sõa todos que estao vindo do banco
            // aqui o DTO, pode ser mapeado de maneira diferente.
       //   List<ProdutoDTO> listaProdutoViewModel = (List<ProdutoDTO>)listaProduto;
            return (List<Produto>)listaProduto;
        }
        public async Task<List<Produto>> ProcurarProduto(string ProcurarPor, string ordenarPor)
        {
            var listaProduto = await _produtoRepository.ProcurarProduto(ProcurarPor, ordenarPor);
            // List<ProdutoDTO> listaProdutoViewModel = (List<ProdutoDTO>)listaProduto;
            return (List<Produto>)listaProduto;
        }

    }

}

