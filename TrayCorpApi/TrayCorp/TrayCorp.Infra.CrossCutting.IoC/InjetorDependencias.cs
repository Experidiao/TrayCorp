using Microsoft.Extensions.DependencyInjection;
using TrayCorp.Application.Services;
using TrayCorp.Application.Interfaces;
using TrayCorp.Domain.Interfaces;
using TrayCorp.Infra.Data.Context;
using TrayCorp.Infra.Data.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using TrayCorp.Infra.Data.UoW;

namespace TrayCorp.Infra.CrossCutting.IoC
{
    public static class InjetorDependencias
    {
        public static void RegistrarDependencia(IServiceCollection services)
        {
            // application 
            services.AddScoped<IProdutoApplication, ProdutoApplication>();

            // infra
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            // contexto
            services.AddScoped<TrayCorpContext>();
            services.AddScoped<IUnitOfWork,UnitOfWork>();
        }
    }
}
