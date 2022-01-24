
using TrayCorp.Domain.Models;
using TrayCorp.Infra.Data.Mappings;
using System.Data.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TrayCorp.Infra.Data.Context
{
    public class TrayCorpContext : DbContext
    {
        public TrayCorpContext(DbContextOptions<TrayCorpContext> options)
            : base(options)
        {
        }
        public DbSet<Produto> TrayCorp_Produto { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfiguration(new ProdutoMap());
            base.OnModelCreating(modelBuilder);
        }
    }

}
