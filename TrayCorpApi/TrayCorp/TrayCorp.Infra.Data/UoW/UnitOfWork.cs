using TrayCorp.Domain.Interfaces;
using TrayCorp.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TrayCorp.Infra.Data.UoW
{
    public class UnitOfWork :IUnitOfWork
    {
        private readonly TrayCorpContext _context;

        public UnitOfWork(TrayCorpContext context) => _context = context;
        
        public async Task<bool> Commit()
        {
            var success = (await _context.SaveChangesAsync()) > 0;
            return success;
        }
    }
}
