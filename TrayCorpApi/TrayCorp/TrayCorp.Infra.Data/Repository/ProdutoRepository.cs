using TrayCorp.Domain.Interfaces;
using TrayCorp.Domain.Models;
using TrayCorp.Infra.Data.Context;
using System;
using System.Linq;
using Dapper;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Net.Http;
using System.Net.Http.Headers;
//using Newtonsoft.Json;
//using System.Dynamic;
using Newtonsoft.Json.Converters;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;

namespace TrayCorp.Infra.Data.Repository
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly TrayCorpContext _context;
        private readonly IUnitOfWork _UoW;
        private readonly DbSet<Produto> DbSet;
        protected readonly DbConnection _dbConnection;

        public ProdutoRepository(TrayCorpContext context, IUnitOfWork UoW)
        {
            _context = context;
            _UoW = UoW;
            DbSet = _context.Set<Produto>();
            _dbConnection = _context.Database.GetDbConnection();
        }

        public async Task<Produto> GetById(int IdProduto)
        {
            return await DbSet.AsNoTracking().FirstAsync(c => c.IdProduto == IdProduto);
        }

        public async Task<IEnumerable<Produto>> GetAll()
        {
            return await DbSet.AsNoTracking().ToListAsync();
        }

        public async Task<int> Create(Produto produto)
        {
            DbSet.Add(produto);
            await _UoW.Commit();
            return Convert.ToInt32(produto.IdProduto);
        }

        public async Task<bool> Update(Produto produto)
        {
            var xProduto = await GetById(produto.IdProduto);
            if (xProduto is null)
            {
                // adderror();
                return false;
            }
            // _context.Entry(produto).State = EntityState.Modified;

            //  DbSet.  Update(produto).State = EntityState.Modified;
            DbSet.Update(produto).State = EntityState.Modified;
          //  var resultado = await _context.SaveChangesAsync();
            return await _UoW.Commit();
        }
        public async Task<bool> Delete(int Id)
        {
            var produto = await GetById(Id);
            if (produto is null)
            {
                // AddError("Esta produto nao existe.");
                return false;
            }

            DbSet.Remove(produto).State = EntityState.Deleted;
         //   var resultado = await _context.SaveChangesAsync();
            return await _UoW.Commit();
        }
        public async Task<IEnumerable<Produto>> ObterProdutoPorNome(string nome)
        {
            var sql = $@"select prod.* 
            from TrayCorp_Produto Prod
            where 
              prod.Nome = @Nome ";

            return await _dbConnection.QueryAsync<Produto>(sql, new { @Nome = nome }, commandType: CommandType.Text);
        }

        public async Task<IEnumerable<Produto>> ProcurarProduto(string ProcurarPor, string ordenarPor)
        {
           var sql = "select * from TrayCorp_Produto";

            if (!string.IsNullOrEmpty(ProcurarPor))
                sql = string.Format(sql + " where Nome = \'{0}\'", ProcurarPor);

            if (!string.IsNullOrEmpty(ordenarPor))
                sql = string.Format(sql + " order by {0}", ordenarPor);

            //  var sql = string.Format( "select * from TrayCorp_Produto order by {0}",campo);

            return await _dbConnection.QueryAsync<Produto>(sql,null, commandType: CommandType.Text);
        }
    }
}

